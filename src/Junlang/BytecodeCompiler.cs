namespace Junlang;

internal sealed class BytecodeCompiler
{
    private readonly List<Instruction> instructions = [];
    private readonly Stack<LoopContext> loops = [];

    public static BytecodeProgram Compile(SourceProgram program)
    {
        var compiler = new BytecodeCompiler();
        compiler.CompileProgram(program);
        return new BytecodeProgram(compiler.instructions);
    }

    private void CompileProgram(SourceProgram program)
    {
        foreach (var statement in program.Statements)
        {
            CompileStatement(statement);
        }

        Emit(OpCode.Halt, default);
    }

    private void CompileBlock(IReadOnlyList<Stmt> statements)
    {
        foreach (var statement in statements)
        {
            CompileStatement(statement);
        }
    }

    private void CompileStatement(Stmt statement)
    {
        switch (statement)
        {
            case AssignStmt assign:
                CompileExpression(assign.Value);
                Emit(OpCode.StoreVariable, assign.Value.Location, assign.Target);
                return;
            case PrintStmt print:
                CompileExpression(print.Value);
                Emit(OpCode.Print, print.Value.Location);
                return;
            case ExprStmt expr:
                CompileExpression(expr.Expression);
                Emit(OpCode.Pop, expr.Expression.Location);
                return;
            case IfStmt ifStmt:
                CompileIf(ifStmt);
                return;
            case WhileStmt whileStmt:
                CompileWhile(whileStmt);
                return;
            case BreakStmt:
                CompileBreak();
                return;
            case ContinueStmt:
                CompileContinue();
                return;
            default:
                throw new JunlangRuntimeException(Errors.BadInput);
        }
    }

    private void CompileIf(IfStmt statement)
    {
        var endJumps = new List<int>();

        CompileExpression(statement.Condition);
        var nextBranchJump = EmitJump(OpCode.JumpIfFalse, statement.Condition.Location);

        CompileBlock(statement.ThenBody);
        endJumps.Add(EmitJump(OpCode.Jump, statement.Condition.Location));
        PatchJump(nextBranchJump, CurrentOffset);

        foreach (var branch in statement.Elifs)
        {
            CompileExpression(branch.Condition);
            nextBranchJump = EmitJump(OpCode.JumpIfFalse, branch.Condition.Location);

            CompileBlock(branch.Body);
            endJumps.Add(EmitJump(OpCode.Jump, branch.Condition.Location));
            PatchJump(nextBranchJump, CurrentOffset);
        }

        if (statement.ElseBody is not null)
        {
            CompileBlock(statement.ElseBody);
        }

        foreach (var jump in endJumps)
        {
            PatchJump(jump, CurrentOffset);
        }
    }

    private void CompileWhile(WhileStmt statement)
    {
        var loopStart = CurrentOffset;
        CompileExpression(statement.Condition);
        var exitJump = EmitJump(OpCode.JumpIfFalse, statement.Condition.Location);

        var loop = new LoopContext();
        loops.Push(loop);
        try
        {
            CompileBlock(statement.Body);
        }
        finally
        {
            loops.Pop();
        }

        foreach (var continueJump in loop.Continues)
        {
            PatchJump(continueJump, loopStart);
        }

        Emit(OpCode.Jump, statement.Condition.Location, loopStart);
        PatchJump(exitJump, CurrentOffset);

        foreach (var breakJump in loop.Breaks)
        {
            PatchJump(breakJump, CurrentOffset);
        }
    }

    private void CompileBreak()
    {
        if (loops.Count == 0)
        {
            throw new JunlangRuntimeException(Errors.LoopControlOutside);
        }

        loops.Peek().Breaks.Add(EmitJump(OpCode.Jump, default));
    }

    private void CompileContinue()
    {
        if (loops.Count == 0)
        {
            throw new JunlangRuntimeException(Errors.LoopControlOutside);
        }

        loops.Peek().Continues.Add(EmitJump(OpCode.Jump, default));
    }

    private void CompileExpression(Expr expression)
    {
        switch (expression)
        {
            case NumberExpr number:
                Emit(OpCode.PushNumber, number.Location, number.Value);
                return;
            case VariableExpr variable:
                Emit(OpCode.LoadVariable, variable.Location, variable.Index);
                return;
            case InputExpr input:
                Emit(OpCode.ReadInput, input.Location);
                return;
            case GroupExpr group:
                CompileExpression(group.Expression);
                return;
            case UnaryExpr unary:
                CompileExpression(unary.Operand);
                Emit(UnaryOpCode(unary.Operator), unary.Location);
                return;
            case BinaryExpr binary:
                CompileExpression(binary.Left);
                CompileExpression(binary.Right);
                Emit(BinaryOpCode(binary.Operator), binary.Location);
                return;
            default:
                throw new JunlangRuntimeException(Errors.BadInput, expression.Location);
        }
    }

    private static OpCode UnaryOpCode(string op)
    {
        return op switch
        {
            "-" => OpCode.Negate,
            "!" => OpCode.Not,
            _ => throw new JunlangRuntimeException(Errors.BadInput),
        };
    }

    private static OpCode BinaryOpCode(string op)
    {
        return op switch
        {
            "+" => OpCode.Add,
            "*" => OpCode.Multiply,
            "/" => OpCode.Divide,
            "**" => OpCode.Power,
            "==" => OpCode.Equal,
            "<" => OpCode.Less,
            ">" => OpCode.Greater,
            "<=" => OpCode.LessEqual,
            ">=" => OpCode.GreaterEqual,
            "!=" => OpCode.NotEqual,
            _ => throw new JunlangRuntimeException(Errors.BadInput),
        };
    }

    private int Emit(OpCode opCode, SourceLocation location, object? operand = null)
    {
        instructions.Add(new Instruction(opCode, operand, location));
        return instructions.Count - 1;
    }

    private int EmitJump(OpCode opCode, SourceLocation location)
    {
        return Emit(opCode, location, -1);
    }

    private void PatchJump(int instructionIndex, int target)
    {
        var instruction = instructions[instructionIndex];
        instructions[instructionIndex] = instruction with { Operand = target };
    }

    private int CurrentOffset => instructions.Count;

    private sealed class LoopContext
    {
        public List<int> Breaks { get; } = [];
        public List<int> Continues { get; } = [];
    }
}
