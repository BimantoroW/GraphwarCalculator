using MathNet.Symbolics;
using System.Drawing;
using Expr = MathNet.Symbolics.SymbolicExpression;

Main();

void Main()
{
    while (true) {
        List<Point> points = new List<Point>();
        string input = String.Empty;

        Console.Write("Soldier coordinates: ");
        input = Console.ReadLine();
        if (input == "done") break;
        points.Add(ConvertInputToPoint(input));

        Console.Write("Enemy coordinates: ");
        input = Console.ReadLine();
        if (input == "done") break;
        points.Add(ConvertInputToPoint(input));

        Console.WriteLine("Points to pass through: ");
        while (true) {
            input = Console.ReadLine();
            if (input == "done")
                break;
            points.Add(ConvertInputToPoint(input));
        }

        Expr lagrange = LagrangeInterpolation(points);
        string lagrangeString = lagrange.ToString().Replace(" ", "");
        Console.WriteLine(lagrangeString);
    }
}

Expr LagrangeInterpolation(IList<Point> points)
{
    Expr function = 0;
    for (int i = 0; i < points.Count; i++) {
        function += points[i].Y * LagrangePolynomial(points, i);
    }
    function = function.Expand();
    return function;
}

Expr LagrangePolynomial(IList<Point> points, int i)
{
    Expr polynomial = 1;
    var x = Expr.Variable("x");

    for (int j = 0; j < points.Count; j++) {
        if (j == i)
            continue;
        polynomial *= (x - points[j].X) / (points[i].X - points[j].X);
    }

    return polynomial;
}

Point ConvertInputToPoint(string input)
{
    input = input.Replace(" ", "");
    string[] arr = input.Split(',');
    return new Point(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
}