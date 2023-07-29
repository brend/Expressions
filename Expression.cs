public interface Expression
{

}

public struct Constant: Expression
{
    public readonly double Value;

    public Constant(double value)
    {
        Value = value;
    }
}

public struct Variable: Expression
{
    public readonly string Name;

    public Variable(string name)
    {
        Name = name;
    }
}

public enum BinaryOperationType
{
    Add,
    Subtract,
    Multiply,
    Divide
}

public enum UnaryOperationType
{
    Negate
}

public struct BinaryOperation: Expression
{
    public readonly Expression Left;
    public readonly Expression Right;
    public readonly BinaryOperationType Type;

    public BinaryOperation(Expression left, Expression right, BinaryOperationType type)
    {
        Left = left;
        Right = right;
        Type = type;
    }
}

public struct UnaryOperation: Expression
{
    public readonly Expression Expression;
    public readonly UnaryOperationType Type;

    public UnaryOperation(Expression expression, UnaryOperationType type)
    {
        Expression = expression;
        Type = type;
    }
}