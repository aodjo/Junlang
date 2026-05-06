using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Junlang;

internal static class AstDumper
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    public static string ToJson(SourceProgram program)
    {
        return JsonSerializer.Serialize(ToObject(program), JsonOptions);
    }

    private static object ToObject(SourceProgram program)
    {
        return new Dictionary<string, object?>
        {
            ["type"] = "Program",
            ["statements"] = program.Statements.Select(ToObject).ToList(),
        };
    }

    private static object ToObject(Stmt statement)
    {
        return statement switch
        {
            AssignStmt assign => new Dictionary<string, object?>
            {
                ["type"] = "AssignStmt",
                ["target"] = assign.Target,
                ["value"] = ToObject(assign.Value),
            },
            PrintStmt print => new Dictionary<string, object?>
            {
                ["type"] = "PrintStmt",
                ["value"] = ToObject(print.Value),
            },
            ExprStmt expr => new Dictionary<string, object?>
            {
                ["type"] = "ExprStmt",
                ["expression"] = ToObject(expr.Expression),
            },
            IfStmt ifStmt => new Dictionary<string, object?>
            {
                ["type"] = "IfStmt",
                ["condition"] = ToObject(ifStmt.Condition),
                ["then_body"] = ifStmt.ThenBody.Select(ToObject).ToList(),
                ["elifs"] = ifStmt.Elifs.Select(ToObject).ToList(),
                ["else_body"] = ifStmt.ElseBody?.Select(ToObject).ToList(),
            },
            WhileStmt whileStmt => new Dictionary<string, object?>
            {
                ["type"] = "WhileStmt",
                ["condition"] = ToObject(whileStmt.Condition),
                ["body"] = whileStmt.Body.Select(ToObject).ToList(),
            },
            BreakStmt _ => new Dictionary<string, object?> { ["type"] = "BreakStmt" },
            ContinueStmt _ => new Dictionary<string, object?> { ["type"] = "ContinueStmt" },
            _ => new Dictionary<string, object?> { ["type"] = statement.GetType().Name },
        };
    }

    private static object ToObject(ElifBranch branch)
    {
        return new Dictionary<string, object?>
        {
            ["condition"] = ToObject(branch.Condition),
            ["body"] = branch.Body.Select(ToObject).ToList(),
        };
    }

    private static object ToObject(Expr expression)
    {
        return expression switch
        {
            NumberExpr number => new Dictionary<string, object?>
            {
                ["type"] = "NumberExpr",
                ["value"] = ToObject(number.Value),
                ["raw"] = number.Raw,
                ["line"] = number.Location.Line,
                ["column"] = number.Location.Column,
            },
            VariableExpr variable => new Dictionary<string, object?>
            {
                ["type"] = "VariableExpr",
                ["index"] = variable.Index,
                ["line"] = variable.Location.Line,
                ["column"] = variable.Location.Column,
            },
            InputExpr input => new Dictionary<string, object?>
            {
                ["type"] = "InputExpr",
                ["line"] = input.Location.Line,
                ["column"] = input.Location.Column,
            },
            GroupExpr group => new Dictionary<string, object?>
            {
                ["type"] = "GroupExpr",
                ["expression"] = ToObject(group.Expression),
                ["line"] = group.Location.Line,
                ["column"] = group.Location.Column,
            },
            UnaryExpr unary => new Dictionary<string, object?>
            {
                ["type"] = "UnaryExpr",
                ["operator"] = unary.Operator,
                ["operand"] = ToObject(unary.Operand),
                ["line"] = unary.Location.Line,
                ["column"] = unary.Location.Column,
            },
            BinaryExpr binary => new Dictionary<string, object?>
            {
                ["type"] = "BinaryExpr",
                ["left"] = ToObject(binary.Left),
                ["operator"] = binary.Operator,
                ["right"] = ToObject(binary.Right),
                ["line"] = binary.Location.Line,
                ["column"] = binary.Location.Column,
            },
            _ => new Dictionary<string, object?> { ["type"] = expression.GetType().Name },
        };
    }

    private static object ToObject(FractionValue value)
    {
        return value.Denominator == BigInteger.One
            ? value.Numerator.ToString()
            : $"{value.Numerator}/{value.Denominator}";
    }
}
