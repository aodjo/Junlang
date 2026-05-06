namespace Junlang;

internal static class Errors
{
    public const string BadInput = "어! 이건 오~준서! 도 안 할 실수인데?";
    public const string FileNotReadable = "아 먹을거 어디있어";
    public const string InputEmpty = "준서가 먹을거 없어서 슬프대";
    public const string DivisionByZero = "이건 기하반도 안 하는 실수인데?";
    public const string NonIntegerExponent = "준서가 어렵대";
    public const string DigitOverflow = "오 10개? 좀 진정해";
    public const string MissingEnd = "준서야 그만 처 먹어";
    public const string UndefinedVariable = "그 준서는 누구야?";
    public const string LoopControlOutside = "먹지도 않았는데 뭘 그만 처 먹어?";
    public const string UnopenedBlockEnd = "넌 이 상황이 처 웃겨?";
}

internal sealed class JunlangSyntaxException : Exception
{
    public JunlangSyntaxException(string message, int line, int column)
        : base(message)
    {
        Line = line;
        Column = column;
    }

    public int Line { get; }
    public int Column { get; }
}

internal sealed class JunlangRuntimeException : Exception
{
    public int? Line { get; }
    public int? Column { get; }

    public JunlangRuntimeException(string message)
        : base(message)
    {
    }

    public JunlangRuntimeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public JunlangRuntimeException(string message, SourceLocation location)
        : base(message)
    {
        Line = location.Line;
        Column = location.Column;
    }

    public JunlangRuntimeException(string message, SourceLocation location, Exception innerException)
        : base(message, innerException)
    {
        Line = location.Line;
        Column = location.Column;
    }
}
