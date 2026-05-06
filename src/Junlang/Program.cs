namespace Junlang;

internal static class Program
{
    private static int Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var showAst = false;
        var showBytecode = false;
        string? file = null;

        foreach (var arg in args)
        {
            if (arg == "--ast")
            {
                showAst = true;
                continue;
            }

            if (arg == "--bytecode")
            {
                showBytecode = true;
                continue;
            }

            if (file is not null)
            {
                Console.Error.WriteLine(Errors.FileNotReadable);
                return 1;
            }

            file = arg;
        }

        string source;
        if (file is not null)
        {
            try
            {
                source = File.ReadAllText(file);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException)
            {
                Console.Error.WriteLine(Errors.FileNotReadable);
                return 1;
            }
        }
        else
        {
            source = ReadStandardInput();
        }

        try
        {
            var program = Parser.ParseSource(source);
            if (showAst)
            {
                Console.WriteLine(AstDumper.ToJson(program));
            }
            else
            {
                var bytecode = BytecodeCompiler.Compile(program);
                if (showBytecode)
                {
                    Console.Write(bytecode.Disassemble());
                }
                else
                {
                    new VirtualMachine().Run(bytecode);
                }
            }
        }
        catch (JunlangSyntaxException ex)
        {
            Console.Error.WriteLine($"{ex.Line}:{ex.Column}: {ex.Message}");
            return 1;
        }
        catch (JunlangRuntimeException ex)
        {
            if (ex.Line is not null && ex.Column is not null)
            {
                Console.Error.WriteLine($"{ex.Line}:{ex.Column}: {ex.Message}");
            }
            else
            {
                Console.Error.WriteLine(ex.Message);
            }

            return 1;
        }

        return 0;
    }

    private static string ReadStandardInput()
    {
        using var stdin = Console.OpenStandardInput();
        using var memory = new MemoryStream();
        stdin.CopyTo(memory);
        return DecodeSource(memory.ToArray());
    }

    private static string DecodeSource(byte[] bytes)
    {
        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
        {
            return System.Text.Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
        }

        if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xFE)
        {
            return System.Text.Encoding.Unicode.GetString(bytes, 2, bytes.Length - 2);
        }

        if (bytes.Length >= 2 && bytes[0] == 0xFE && bytes[1] == 0xFF)
        {
            return System.Text.Encoding.BigEndianUnicode.GetString(bytes, 2, bytes.Length - 2);
        }

        var utf8 = DecodeUtf8(bytes);
        if (bytes.Length % 2 != 0)
        {
            return utf8;
        }

        var utf16 = System.Text.Encoding.Unicode.GetString(bytes);
        return SourceScore(utf16) > SourceScore(utf8) ? utf16 : utf8;
    }

    private static string DecodeUtf8(byte[] bytes)
    {
        try
        {
            return new System.Text.UTF8Encoding(false, true).GetString(bytes);
        }
        catch (System.Text.DecoderFallbackException)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }

    private static int SourceScore(string source)
    {
        var score = 0;
        foreach (var ch in source)
        {
            score += ch switch
            {
                '오' or 'ㅋ' or '준' or '서' or 'ㅁ' or 'ㅊ' => 4,
                '~' or '.' or '#' or '@' or '!' or '?' => 1,
                '\0' or '\uFFFD' => -8,
                _ => 0,
            };
        }

        return score;
    }
}
