
public class Parser
{
    private readonly string expression;
    private readonly Lexer.Lexer lexer;   

    public Parser(string expression)
    {
        this.expression = expression;
        this.lexer = new Lexer.Lexer(expression);
    }

    public Expression Parse() => ParseExpression();

    private Expression ParseExpression()
    {
        var left = ParseTerm();

        while (true)
        {
            var token = lexer.Peek();

            if (token is Lexer.Plus || token is Lexer.Minus)
            {
                lexer.Read();
                var right = ParseTerm();
                left = new BinaryOperation(left, right, token is Lexer.Plus ? BinaryOperationType.Add : BinaryOperationType.Subtract);
            }
            else
            {
                return left;
            }
        }
    }

    private Expression ParseTerm()
    {
        var left = ParseFactor();

        while (true)
        {
            var token = lexer.Peek();
            if (token is Lexer.Multiply || token is Lexer.Divide)
            {
                lexer.Read();
                var right = ParseFactor();
                left = new BinaryOperation(left, right, token is Lexer.Multiply ? BinaryOperationType.Multiply : BinaryOperationType.Divide);
            }
            else
            {
                return left;
            }
        }
    }

    private Expression ParseFactor()
    {
        var token = lexer.Peek();
        
        if (token is Lexer.Constant constant)
        {
            lexer.Read();
            return new Constant(constant.Value);
        }

        else if (token is Lexer.Variable variable)
        {
            lexer.Read();
            return new Variable(variable.Name);
        }
        else if (token is Lexer.Minus)
        {
            lexer.Read();
            var expression = ParseFactor();
            return new UnaryOperation(expression, UnaryOperationType.Negate);
        }
        else if (token is Lexer.LParen)
        {
            lexer.Read();

            var expression = ParseExpression();

            if (lexer.Peek() is not Lexer.RParen)
            {
                throw new Exception("Expected )");
            }

            lexer.Read();

            return expression;
        }
        else
        {
            throw new Exception($"Unexpected token {token?.GetType().Name} {token?.Text}");
        }
    }
}