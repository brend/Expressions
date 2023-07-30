void DemoFarenheitToCelsius()
{
    var evaluator = new Evaluator("(x - 32) * 5/9");

    foreach (double x in new[] { 0, 32, 72, 100, 212 })
    {
        Console.WriteLine($"{x}°F = {evaluator.Evaluate(x)}°C");
    }
}

void DemoDegreesToRadians()
{
    var evaluator = new Evaluator("x * pi / 180");

    foreach (double x in new[] { 0, 30, 45, 60, 90, 180, 270, 360 })
    {
        var valuation = Valuation.Math().Extend("x", x);

        Console.WriteLine($"{x}° = {evaluator.Evaluate(valuation)} radians");
    }
}

void DemoRectangleArea()
{
    var evaluator = new Evaluator("width * height");

    foreach (var (width, height) in new[] { (1, 1), (2, 3), (5, 5), (12, 10) })
    {
        var valuation = Valuation.Create("width", width, "height", height);

        Console.WriteLine($"Rectangle with width {width} and height {height} has area {evaluator.Evaluate(valuation)}");
    }
}

if (args.Length == 0)
{
    DemoFarenheitToCelsius();
    DemoDegreesToRadians();
    DemoRectangleArea();
}
else
{
    var expression = args[0];

    System.Console.WriteLine("Attempting to parse expression: \"" + expression + "\"");

    var evaluator = new Evaluator(expression);

    System.Console.WriteLine("Parsed expression: " + evaluator.Expression);

    var valuation = Valuation.Math();

    for (int i = 1; i < args.Length; ++i)
    {
        var assignment = args[i].Split('=');
        
        if (assignment.Length != 2)
        {
            Console.Error.WriteLine("Invalid assignment: " + args[i]);
            continue;
        }

        valuation[assignment[0]] = double.Parse(assignment[1]);
    }

    System.Console.WriteLine("Evaluation result: " + evaluator.Evaluate(valuation));
}