namespace Junlang;

internal enum TokenType
{
    Eof,
    Number,
    Bangs,
    Input,
    Print,
    Assign,
    Plus,
    Mul,
    Pow,
    Div,
    Eq,
    Lt,
    Gt,
    Lte,
    Gte,
    Neq,
    Neg,
    LParen,
    RParen,
    End,
    IfStart,
    IfThen,
    WhileThen,
    Elif,
    ElifThen,
    Else,
    Break,
    Continue,
}

internal sealed record Token(
    TokenType Type,
    string Lexeme,
    int Line,
    int Column,
    FractionValue? Literal = null);
