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

DemoFarenheitToCelsius();
DemoDegreesToRadians();
DemoRectangleArea();