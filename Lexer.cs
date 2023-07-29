using System.Text;

namespace Lexer;

public class Lexer
{
    private readonly string expression;
    private readonly TextReader reader;
    private readonly Queue<Token> lookahead = new Queue<Token>();

    public Lexer(string expression)
    {
        this.expression = expression;
        this.reader = new StringReader(expression);
    }

    public Token? Peek()
    {
        if (lookahead.Count == 0)
        {
            if (ReadToken() is Token token)
            {
                lookahead.Enqueue(token);
            }
            else 
            {
                return null;
            }
        }

        return lookahead.Peek();
    }

    public Token? Read()
    {
        if (lookahead.Count > 0)
        {
            return lookahead.Dequeue();
        }
        return ReadToken();
    }

    private Token? ReadToken()
    {
        ConsumeWhitespace();

        var d = reader.Peek();

        if (d == -1)
        {
            return null;
        }

        char c = (char)d;
        
        if (char.IsDigit(c))
        {
            return ReadNumber();
        }
        
        if (char.IsLetter(c))
        {
            return ReadVariable();
        }
        
        return ReadOperator();
   }

   private void ConsumeWhitespace()
    {
         while (true)
         {
              var c = reader.Peek();
              if (c == -1)
              {
                break;
              }
              else if (char.IsWhiteSpace((char)c))
              {
                reader.Read();
              }
              else
              {
                break;
              }
         }
    }

    private Token ReadNumber()
    {
        var builder = new StringBuilder();

        while (true)
        {
            var c = reader.Peek();
            if (c == -1)
            {
                break;
            }
            else if (char.IsDigit((char)c) || (char)c == '.')
            {
                builder.Append((char)reader.Read());
            }
            else
            {
                break;
            }
        }

        return new Constant(double.Parse(builder.ToString()));
    }

    private Token ReadVariable()
    {
        var builder = new StringBuilder();

        while (true)
        {
            var c = reader.Peek();
            if (c == -1)
            {
                break;
            }
            else if (char.IsLetter((char)c) || char.IsDigit((char)c))
            {
                builder.Append((char)reader.Read());
            }
            else
            {
                break;
            }
        }

        return new Variable(builder.ToString());
    }

    private Token ReadOperator()
    {
        var c = (char)reader.Read();
        switch (c)
        {
            case '+':
                return new Plus();
            case '-':
                return new Minus();
            case '*':
                return new Multiply();
            case '/':
                return new Divide();
            case '(':
                return new LParen();
            case ')':
                return new RParen();
            default:
                throw new Exception($"Unexpected character: {c}");
        }
    }
}

public interface Token
{
    string Text { get; }
}

public struct Constant: Token
{
    public readonly double Value;

    public Constant(double value)
    {
        Value = value;
    }

    public string Text => Value.ToString();
}

public struct Variable: Token
{
    public readonly string Name;

    public Variable(string name)
    {
        Name = name;
    }

    public string Text => Name;
}

public struct Plus: Token 
{
    public string Text => "+";
}

public struct Minus: Token
{
    public string Text => "-";
}

public struct Multiply: Token
{
    public string Text => "*";
}

public struct Divide: Token 
{
    public string Text => "/";
}

public struct LParen: Token 
{
    public string Text => "(";
}

public struct RParen: Token 
{
    public string Text => ")";
}