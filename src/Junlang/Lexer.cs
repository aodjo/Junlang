using System.Numerics;

namespace Junlang;

internal sealed class Lexer
{
    private static readonly (string Text, TokenType Type)[] Keywords =
    [
        ("오ㅋ준ㅋ서ㅋ", TokenType.Input),
        ("오준서", TokenType.Print),
        ("~준서", TokenType.Assign),
        ("맞냐?", TokenType.IfThen),
        ("또처먹냐?", TokenType.WhileThen),
        ("준서야", TokenType.IfStart),
        ("아니냐?", TokenType.Else),
        ("아니면", TokenType.Elif),
        ("이건?", TokenType.ElifThen),
        ("그만처먹어", TokenType.Break),
        ("더처먹어", TokenType.Continue),
        ("ㅁ@", TokenType.Lte),
        ("ㅊ@", TokenType.Gte),
        ("..", TokenType.Pow),
    ];

    private static readonly Dictionary<char, TokenType> SingleCharTokens = new()
    {
        ['~'] = TokenType.Plus,
        ['.'] = TokenType.Mul,
        ['#'] = TokenType.Div,
        ['@'] = TokenType.Eq,
        ['ㅁ'] = TokenType.Lt,
        ['ㅊ'] = TokenType.Gt,
        ['준'] = TokenType.LParen,
        ['서'] = TokenType.RParen,
        ['ㅋ'] = TokenType.End,
    };

    private readonly string source;
    private readonly List<Token> tokens = [];
    private int pos;
    private int line = 1;
    private int column = 1;

    public Lexer(string source)
    {
        this.source = source;
    }

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            if (SkipWhitespaceOrComment())
            {
                continue;
            }

            var keyword = KeywordToken();
            if (keyword is not null)
            {
                tokens.Add(keyword);
                continue;
            }

            if (CanStartNumber(pos))
            {
                tokens.Add(NumberToken());
                continue;
            }

            var ch = Peek();
            var startLine = line;
            var startColumn = column;

            if (ch == '?')
            {
                Advance();
                tokens.Add(new Token(TokenType.Neg, "?", startLine, startColumn));
                continue;
            }

            if (ch == '!')
            {
                tokens.Add(BangsToken());
                continue;
            }

            if (SingleCharTokens.TryGetValue(ch, out var type))
            {
                Advance();
                tokens.Add(new Token(type, ch.ToString(), startLine, startColumn));
                continue;
            }

            throw Error(Errors.BadInput);
        }

        tokens.Add(new Token(TokenType.Eof, string.Empty, line, column));
        return tokens;
    }

    private bool SkipWhitespaceOrComment()
    {
        var ch = Peek();
        if (ch is ' ' or '\t' or '\r' or '\n')
        {
            Advance();
            return true;
        }

        if (StartsWithAt("걍", pos))
        {
            while (!IsAtEnd() && Peek() != '\n')
            {
                Advance();
            }

            return true;
        }

        if (StartsWithAt("참고로", pos))
        {
            var end = source.IndexOf("알았냐?", pos + "참고로".Length, StringComparison.Ordinal);
            if (end < 0)
            {
                throw Error(Errors.BadInput);
            }

            AdvanceTo(end + "알았냐?".Length);
            return true;
        }

        return false;
    }

    private Token? KeywordToken()
    {
        foreach (var (text, type) in Keywords)
        {
            if (!StartsWithAt(text, pos))
            {
                continue;
            }

            var startLine = line;
            var startColumn = column;
            AdvanceTo(pos + text.Length);
            return new Token(type, text, startLine, startColumn);
        }

        return null;
    }

    private Token BangsToken()
    {
        var startLine = line;
        var startColumn = column;
        var start = pos;
        var count = 0;

        while (pos + count < source.Length && source[pos + count] == '!')
        {
            count++;
        }

        var tokenLength = count;
        if (count >= 2 && pos + count < source.Length && source[pos + count] == '@')
        {
            tokenLength = count - 1;
        }

        for (var i = 0; i < tokenLength; i++)
        {
            Advance();
        }

        return new Token(TokenType.Bangs, source[start..pos], startLine, startColumn);
    }

    private Token NumberToken()
    {
        var start = pos;
        var startLine = line;
        var startColumn = column;
        var (value, end) = ReadNumberValue(pos);
        var lexeme = source[start..end];
        AdvanceTo(end);
        return new Token(TokenType.Number, lexeme, startLine, startColumn, value);
    }

    private (FractionValue Value, int End) ReadNumberValue(int start)
    {
        var sign = 1;
        var current = start;
        if (StartsWithAt("?", current))
        {
            sign = -1;
            current++;
        }

        var (integerDigits, integerEnd) = ReadDigitSequence(current);
        current = integerEnd;
        var numerator = BigInteger.Zero;
        foreach (var digit in integerDigits)
        {
            numerator = numerator * 10 + digit;
        }

        var value = new FractionValue(numerator, 1);

        if (StartsWithAt("ㅋ", current) && CanStartDigitChunk(current + 1))
        {
            current++;
            var (fractionDigits, fractionEnd) = ReadDigitSequence(current);
            current = fractionEnd;

            var denominator = BigInteger.Pow(10, fractionDigits.Count);
            var fractionNumerator = BigInteger.Zero;
            foreach (var digit in fractionDigits)
            {
                fractionNumerator = fractionNumerator * 10 + digit;
            }

            value += new FractionValue(fractionNumerator, denominator);
        }

        return (sign < 0 ? -value : value, current);
    }

    private (List<int> Digits, int End) ReadDigitSequence(int start)
    {
        var (firstDigit, current) = ReadDigitChunk(start);
        var digits = new List<int> { firstDigit };

        while (true)
        {
            var gapEnd = ConsumeInlineSpaces(current);
            if (gapEnd == current || !CanStartDigitChunk(gapEnd))
            {
                return (digits, current);
            }

            var (digit, next) = ReadDigitChunk(gapEnd);
            digits.Add(digit);
            current = next;
        }
    }

    private (int Digit, int End) ReadDigitChunk(int start)
    {
        if (StartsWithAt("오?", start))
        {
            return (0, start + 2);
        }

        var count = 0;
        while (start + count < source.Length && source[start + count] == '오')
        {
            count++;
        }

        if (count >= 10)
        {
            throw ErrorAt(Errors.DigitOverflow, start);
        }

        if (count == 0)
        {
            throw ErrorAt(Errors.BadInput, start);
        }

        return (count, start + count);
    }

    private bool CanStartNumber(int index)
    {
        return StartsWithAt("?", index)
            ? CanStartDigitChunk(index + 1)
            : CanStartDigitChunk(index);
    }

    private bool CanStartDigitChunk(int index)
    {
        return index < source.Length && source[index] == '오';
    }

    private int ConsumeInlineSpaces(int index)
    {
        while (index < source.Length && source[index] is ' ' or '\t' or '\r')
        {
            index++;
        }

        return index;
    }

    private char Peek()
    {
        return IsAtEnd() ? '\0' : source[pos];
    }

    private char Advance()
    {
        var ch = source[pos];
        pos++;
        if (ch == '\n')
        {
            line++;
            column = 1;
        }
        else
        {
            column++;
        }

        return ch;
    }

    private void AdvanceTo(int target)
    {
        while (pos < target)
        {
            Advance();
        }
    }

    private bool IsAtEnd()
    {
        return pos >= source.Length;
    }

    private bool StartsWithAt(string text, int index)
    {
        return index >= 0
            && index + text.Length <= source.Length
            && string.CompareOrdinal(source, index, text, 0, text.Length) == 0;
    }

    private JunlangSyntaxException Error(string message)
    {
        return new JunlangSyntaxException(message, line, column);
    }

    private JunlangSyntaxException ErrorAt(string message, int index)
    {
        var (errorLine, errorColumn) = LineColumnAt(index);
        return new JunlangSyntaxException(message, errorLine, errorColumn);
    }

    private (int Line, int Column) LineColumnAt(int index)
    {
        var currentLine = 1;
        var currentColumn = 1;
        for (var i = 0; i < index; i++)
        {
            if (source[i] == '\n')
            {
                currentLine++;
                currentColumn = 1;
            }
            else
            {
                currentColumn++;
            }
        }

        return (currentLine, currentColumn);
    }
}
