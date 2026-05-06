using System.Numerics;

namespace Junlang;

internal sealed class VirtualMachine
{
    private readonly Stack<FractionValue> stack = [];
    private readonly Dictionary<int, FractionValue> variables = [];
    private readonly Func<string?> inputFunc;
    private readonly Action<string> outputFunc;

    public VirtualMachine(Func<string?>? inputFunc = null, Action<string>? outputFunc = null)
    {
        this.inputFunc = inputFunc ?? Console.ReadLine;
        this.outputFunc = outputFunc ?? Console.WriteLine;
    }

    public void Run(BytecodeProgram program)
    {
        var ip = 0;
        while (ip < program.Instructions.Count)
        {
            var instruction = program.Instructions[ip];
            switch (instruction.OpCode)
            {
                case OpCode.PushNumber:
                    stack.Push(ReadOperand<FractionValue>(instruction));
                    ip++;
                    break;
                case OpCode.LoadVariable:
                    LoadVariable(instruction);
                    ip++;
                    break;
                case OpCode.StoreVariable:
                    variables[ReadOperand<int>(instruction)] = Pop(instruction.Location);
                    ip++;
                    break;
                case OpCode.ReadInput:
                    stack.Push(ReadInput(instruction.Location));
                    ip++;
                    break;
                case OpCode.Print:
                    outputFunc(RuntimeSupport.FormatNumber(Pop(instruction.Location)));
                    ip++;
                    break;
                case OpCode.Pop:
                    Pop(instruction.Location);
                    ip++;
                    break;
                case OpCode.Negate:
                    stack.Push(-Pop(instruction.Location));
                    ip++;
                    break;
                case OpCode.Not:
                    stack.Push(IsTruthy(Pop(instruction.Location)) ? FractionValue.Zero : FractionValue.One);
                    ip++;
                    break;
                case OpCode.Add:
                case OpCode.Multiply:
                case OpCode.Divide:
                case OpCode.Power:
                case OpCode.Equal:
                case OpCode.Less:
                case OpCode.Greater:
                case OpCode.LessEqual:
                case OpCode.GreaterEqual:
                case OpCode.NotEqual:
                    ExecuteBinary(instruction);
                    ip++;
                    break;
                case OpCode.Jump:
                    ip = ReadOperand<int>(instruction);
                    break;
                case OpCode.JumpIfFalse:
                    ip = IsTruthy(Pop(instruction.Location)) ? ip + 1 : ReadOperand<int>(instruction);
                    break;
                case OpCode.Halt:
                    return;
                default:
                    throw new JunlangRuntimeException(Errors.BadInput, instruction.Location);
            }
        }
    }

    private void LoadVariable(Instruction instruction)
    {
        var variable = ReadOperand<int>(instruction);
        if (!variables.TryGetValue(variable, out var value))
        {
            throw new JunlangRuntimeException(Errors.UndefinedVariable, instruction.Location);
        }

        stack.Push(value);
    }

    private FractionValue ReadInput(SourceLocation location)
    {
        var source = inputFunc() ?? throw new JunlangRuntimeException(Errors.InputEmpty, location);
        try
        {
            return RuntimeSupport.ParseInputNumber(source);
        }
        catch (JunlangRuntimeException ex) when (ex.Line is null || ex.Column is null)
        {
            throw new JunlangRuntimeException(ex.Message, location, ex);
        }
    }

    private void ExecuteBinary(Instruction instruction)
    {
        var right = Pop(instruction.Location);
        var left = Pop(instruction.Location);

        try
        {
            var result = instruction.OpCode switch
            {
                OpCode.Add => left + right,
                OpCode.Multiply => left * right,
                OpCode.Divide => left / right,
                OpCode.Power => EvaluatePower(left, right, instruction.Location),
                OpCode.Equal => BoolValue(left == right),
                OpCode.Less => BoolValue(left < right),
                OpCode.Greater => BoolValue(left > right),
                OpCode.LessEqual => BoolValue(left <= right),
                OpCode.GreaterEqual => BoolValue(left >= right),
                OpCode.NotEqual => BoolValue(left != right),
                _ => throw new JunlangRuntimeException(Errors.BadInput, instruction.Location),
            };
            stack.Push(result);
        }
        catch (DivideByZeroException ex)
        {
            throw new JunlangRuntimeException(Errors.DivisionByZero, instruction.Location, ex);
        }
        catch (OverflowException ex)
        {
            throw new JunlangRuntimeException(Errors.NonIntegerExponent, instruction.Location, ex);
        }
    }

    private static FractionValue EvaluatePower(FractionValue left, FractionValue right, SourceLocation location)
    {
        if (right.Denominator != BigInteger.One
            || right.Numerator > int.MaxValue
            || right.Numerator < int.MinValue)
        {
            throw new JunlangRuntimeException(Errors.NonIntegerExponent, location);
        }

        return left.Pow((int)right.Numerator);
    }

    private FractionValue Pop(SourceLocation location)
    {
        if (stack.Count == 0)
        {
            throw new JunlangRuntimeException(Errors.BadInput, location);
        }

        return stack.Pop();
    }

    private static bool IsTruthy(FractionValue value)
    {
        return value != FractionValue.Zero;
    }

    private static FractionValue BoolValue(bool value)
    {
        return value ? FractionValue.One : FractionValue.Zero;
    }

    private static T ReadOperand<T>(Instruction instruction)
    {
        if (instruction.Operand is T value)
        {
            return value;
        }

        throw new JunlangRuntimeException(Errors.BadInput, instruction.Location);
    }
}
