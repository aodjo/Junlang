namespace Junlang;

internal sealed class Parser
{
    private static readonly Dictionary<TokenType, string> BinaryOperators = new()
    {
        [TokenType.Eq] = "==",
        [TokenType.Lt] = "<",
        [TokenType.Gt] = ">",
        [TokenType.Lte] = "<=",
        [TokenType.Gte] = ">=",
        [TokenType.Neq] = "!=",
        [TokenType.Plus] = "+",
        [TokenType.Mul] = "*",
        [TokenType.Div] = "/",
        [TokenType.Pow] = "**",
    };

    private readonly IReadOnlyList<Token> tokens;
    private int current;
    private int loopDepth;

    public Parser(IReadOnlyList<Token> tokens)
    {
        this.tokens = tokens;
    }

    public SourceProgram Parse()
    {
        var statements = new List<Stmt>();
        while (!Check(TokenType.Eof))
        {
            if (Check(TokenType.End))
            {
                throw ErrorCurrent(Errors.UnopenedBlockEnd);
            }

            statements.Add(Statement());
        }

        return new SourceProgram(statements);
    }

    public static SourceProgram ParseSource(string source)
    {
        return new Parser(new Lexer(source).ScanTokens()).Parse();
    }

    private Stmt Statement()
    {
        if (Match(TokenType.IfStart))
        {
            return IfOrWhileStatement();
        }

        if (Match(TokenType.Print))
        {
            return PrintStatement();
        }

        if (Match(TokenType.Break))
        {
            return LoopControlStatement(new BreakStmt());
        }

        if (Match(TokenType.Continue))
        {
            return LoopControlStatement(new ContinueStmt());
        }

        var expression = Expression();
        if (Match(TokenType.Assign))
        {
            var variable = Consume(TokenType.Bangs, Errors.BadInput);
            ConsumeEnd();
            return new AssignStmt(variable.Lexeme.Length, expression);
        }

        ConsumeEnd();
        return new ExprStmt(expression);
    }

    private Stmt IfOrWhileStatement()
    {
        var condition = Expression();

        if (Match(TokenType.IfThen))
        {
            var thenBody = Block();
            var elifs = new List<ElifBranch>();
            while (Match(TokenType.Elif))
            {
                var elifCondition = Expression();
                Consume(TokenType.ElifThen, Errors.BadInput);
                elifs.Add(new ElifBranch(elifCondition, Block()));
            }

            var elseBody = Match(TokenType.Else) ? Block() : null;
            return new IfStmt(condition, thenBody, elifs, elseBody);
        }

        if (Match(TokenType.WhileThen))
        {
            loopDepth++;
            try
            {
                return new WhileStmt(condition, Block());
            }
            finally
            {
                loopDepth--;
            }
        }

        throw ErrorCurrent(Errors.BadInput);
    }

    private IReadOnlyList<Stmt> Block()
    {
        var statements = new List<Stmt>();
        while (!Check(TokenType.End))
        {
            if (Check(TokenType.Eof))
            {
                throw ErrorCurrent(Errors.MissingEnd);
            }

            statements.Add(Statement());
        }

        Advance();
        return statements;
    }

    private PrintStmt PrintStatement()
    {
        var value = Expression();
        ConsumeEnd();
        return new PrintStmt(value);
    }

    private Stmt LoopControlStatement(Stmt statement)
    {
        if (loopDepth == 0)
        {
            throw ErrorPrevious(Errors.LoopControlOutside);
        }

        ConsumeEnd();
        return statement;
    }

    private Expr Expression()
    {
        return Comparison();
    }

    private Expr Comparison()
    {
        var expression = Addition();
        while (true)
        {
            string op;
            if (Match(TokenType.Eq, TokenType.Lt, TokenType.Gt, TokenType.Lte, TokenType.Gte, TokenType.Neq))
            {
                var operatorToken = Previous();
                op = BinaryOperators[operatorToken.Type];
                var right = Addition();
                expression = new BinaryExpr(expression, op, right, LocationOf(operatorToken));
                continue;
            }
            else if (MatchNotEqualOperator(out var notEqualToken))
            {
                op = "!=";
                var right = Addition();
                expression = new BinaryExpr(expression, op, right, LocationOf(notEqualToken));
                continue;
            }
            else
            {
                break;
            }
        }

        return expression;
    }

    private Expr Addition()
    {
        var expression = Multiplication();
        while (Match(TokenType.Plus))
        {
            var operatorToken = Previous();
            var op = BinaryOperators[operatorToken.Type];
            var right = Multiplication();
            expression = new BinaryExpr(expression, op, right, LocationOf(operatorToken));
        }

        return expression;
    }

    private Expr Multiplication()
    {
        var expression = Power();
        while (Match(TokenType.Mul, TokenType.Div))
        {
            var operatorToken = Previous();
            var op = BinaryOperators[operatorToken.Type];
            var right = Power();
            expression = new BinaryExpr(expression, op, right, LocationOf(operatorToken));
        }

        return expression;
    }

    private Expr Power()
    {
        var expression = Unary();
        while (Match(TokenType.Pow))
        {
            var operatorToken = Previous();
            var op = BinaryOperators[operatorToken.Type];
            var right = Unary();
            expression = new BinaryExpr(expression, op, right, LocationOf(operatorToken));
        }

        return expression;
    }

    private Expr Unary()
    {
        if (Match(TokenType.Neg))
        {
            var operatorToken = Previous();
            return new UnaryExpr("-", Unary(), LocationOf(operatorToken));
        }

        if (Check(TokenType.Bangs)
            && Peek().Lexeme == "!"
            && !StartsAttachedNotEqual()
            && StartsExpression(PeekNext()))
        {
            var operatorToken = Advance();
            return new UnaryExpr("!", Unary(), LocationOf(operatorToken));
        }

        return Primary();
    }

    private Expr Primary()
    {
        if (Match(TokenType.Number))
        {
            var token = Previous();
            if (token.Literal is null)
            {
                throw ErrorPrevious(Errors.BadInput);
            }

            return new NumberExpr(token.Literal.Value, token.Lexeme, LocationOf(token));
        }

        if (Match(TokenType.Input))
        {
            return new InputExpr(LocationOf(Previous()));
        }

        if (Match(TokenType.Bangs))
        {
            var token = Previous();
            return new VariableExpr(token.Lexeme.Length, LocationOf(token));
        }

        if (Match(TokenType.LParen))
        {
            var leftParen = Previous();
            var expression = Expression();
            Consume(TokenType.RParen, Errors.BadInput);
            return new GroupExpr(expression, LocationOf(leftParen));
        }

        throw ErrorCurrent(Errors.BadInput);
    }

    private bool StartsExpression(Token token)
    {
        return token.Type
            is TokenType.Number
            or TokenType.Bangs
            or TokenType.Input
            or TokenType.LParen
            or TokenType.Neg;
    }

    private bool MatchNotEqualOperator(out Token operatorToken)
    {
        if (Check(TokenType.Bangs) && Peek().Lexeme == "!" && PeekNext().Type == TokenType.Eq)
        {
            operatorToken = Peek();
            Advance();
            Advance();
            return true;
        }

        operatorToken = tokens[^1];
        return false;
    }

    private bool StartsAttachedNotEqual()
    {
        return Check(TokenType.Bangs)
            && Peek().Lexeme == "!"
            && PeekNext().Type == TokenType.Bangs
            && PeekNext().Lexeme == "!"
            && PeekAt(2).Type == TokenType.Eq
            && AreAdjacent(Peek(), PeekNext())
            && AreAdjacent(PeekNext(), PeekAt(2));
    }

    private Token PeekAt(int offset)
    {
        var index = current + offset;
        return index >= tokens.Count ? tokens[^1] : tokens[index];
    }

    private bool AreAdjacent(Token left, Token right)
    {
        return left.Line == right.Line && left.Column + left.Lexeme.Length == right.Column;
    }

    private void ConsumeEnd()
    {
        Consume(TokenType.End, Errors.MissingEnd);
    }

    private bool Match(params TokenType[] types)
    {
        if (types.Any(Check))
        {
            Advance();
            return true;
        }

        return false;
    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type))
        {
            return Advance();
        }

        throw ErrorCurrent(message);
    }

    private bool Check(TokenType type)
    {
        if (IsAtEnd() && type != TokenType.Eof)
        {
            return false;
        }

        return Peek().Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd())
        {
            current++;
        }

        return Previous();
    }

    private bool IsAtEnd()
    {
        return Peek().Type == TokenType.Eof;
    }

    private Token Peek()
    {
        return tokens[current];
    }

    private Token PeekNext()
    {
        return current + 1 >= tokens.Count ? tokens[^1] : tokens[current + 1];
    }

    private Token Previous()
    {
        return tokens[current - 1];
    }

    private JunlangSyntaxException ErrorCurrent(string message)
    {
        var token = Peek();
        return new JunlangSyntaxException(message, token.Line, token.Column);
    }

    private JunlangSyntaxException ErrorPrevious(string message)
    {
        var token = Previous();
        return new JunlangSyntaxException(message, token.Line, token.Column);
    }

    private static SourceLocation LocationOf(Token token)
    {
        return new SourceLocation(token.Line, token.Column);
    }
}
