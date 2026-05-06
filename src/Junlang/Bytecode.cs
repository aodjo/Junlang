using System.Text;

namespace Junlang;

internal enum OpCode
{
    PushNumber,
    LoadVariable,
    StoreVariable,
    ReadInput,
    Print,
    Pop,
    Negate,
    Not,
    Add,
    Multiply,
    Divide,
    Power,
    Equal,
    Less,
    Greater,
    LessEqual,
    GreaterEqual,
    NotEqual,
    Jump,
    JumpIfFalse,
    Halt,
}

internal sealed record Instruction(OpCode OpCode, object? Operand, SourceLocation Location);

internal sealed record BytecodeProgram(IReadOnlyList<Instruction> Instructions)
{
    public string Disassemble()
    {
        var builder = new StringBuilder();
        for (var index = 0; index < Instructions.Count; index++)
        {
            var instruction = Instructions[index];
            builder.Append(index.ToString("D4"));
            builder.Append(' ');
            builder.Append(instruction.OpCode);
            if (instruction.Operand is not null)
            {
                builder.Append(' ');
                builder.Append(FormatOperand(instruction.Operand));
            }

            if (instruction.Location.Line > 0)
            {
                builder.Append(" ; ");
                builder.Append(instruction.Location.Line);
                builder.Append(':');
                builder.Append(instruction.Location.Column);
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static string FormatOperand(object operand)
    {
        return operand switch
        {
            FractionValue value => value.ToString(),
            _ => operand.ToString() ?? string.Empty,
        };
    }
}
