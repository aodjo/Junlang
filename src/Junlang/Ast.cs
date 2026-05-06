namespace Junlang;

internal sealed record SourceProgram(IReadOnlyList<Stmt> Statements);

internal readonly record struct SourceLocation(int Line, int Column);

internal abstract record Stmt;

internal sealed record AssignStmt(int Target, Expr Value) : Stmt;

internal sealed record PrintStmt(Expr Value) : Stmt;

internal sealed record ExprStmt(Expr Expression) : Stmt;

internal sealed record ElifBranch(Expr Condition, IReadOnlyList<Stmt> Body);

internal sealed record IfStmt(
    Expr Condition,
    IReadOnlyList<Stmt> ThenBody,
    IReadOnlyList<ElifBranch> Elifs,
    IReadOnlyList<Stmt>? ElseBody) : Stmt;

internal sealed record WhileStmt(Expr Condition, IReadOnlyList<Stmt> Body) : Stmt;

internal sealed record BreakStmt : Stmt;

internal sealed record ContinueStmt : Stmt;

internal abstract record Expr(SourceLocation Location);

internal sealed record NumberExpr(FractionValue Value, string Raw, SourceLocation Location) : Expr(Location);

internal sealed record VariableExpr(int Index, SourceLocation Location) : Expr(Location);

internal sealed record InputExpr(SourceLocation Location) : Expr(Location);

internal sealed record GroupExpr(Expr Expression, SourceLocation Location) : Expr(Location);

internal sealed record UnaryExpr(string Operator, Expr Operand, SourceLocation Location) : Expr(Location);

internal sealed record BinaryExpr(Expr Left, string Operator, Expr Right, SourceLocation Location) : Expr(Location);
