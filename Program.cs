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
        var valuation = new Dictionary<string, double> { ["x"] = x, ["pi"] = Math.PI };

        Console.WriteLine($"{x}° = {evaluator.Evaluate(valuation)} radians");
    }
}

DemoFarenheitToCelsius();
DemoDegreesToRadians();