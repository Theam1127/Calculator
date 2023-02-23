// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

while (true)
{
    try
    {
        Console.Write("Enter Calculation: ");
        string calculation = Console.ReadLine()!;
        Console.WriteLine(string.Format("{0:0.##}", Calculate(calculation!)));
    }
    catch
    {
        Console.WriteLine("Invalid input!");
    }
}

static double Calculate(string calculation)
{
    while (calculation.Contains('('))
    {
        Match extractMatch = Regex.Match(calculation, @"\(\s\-?[0-9](\.[0-9])?\s[\+\-\*\/]\s\-?[0-9](\.[0-9])?\s\)");
        string extractCalculation = extractMatch.Value;
        double matchCalculatedResult = Calculate(extractCalculation[(extractCalculation.IndexOf('(') + 2)..(extractCalculation.IndexOf(')') - 1)]);
        calculation = calculation.Replace(extractCalculation, matchCalculatedResult.ToString());
    }

    List<string> separatedCalculation = new(calculation.Split(' '));
    string[] mathSequence = new[] { "*", "/", "-", "+" };
    foreach (string symbol in mathSequence)
    {
        while (separatedCalculation.Any(x => x.Equals(symbol)))
        {
            int index = separatedCalculation.IndexOf(symbol);
            string firstValue = separatedCalculation[index - 1];
            string secondValue = separatedCalculation[index + 1];
            double result = Compute(Convert.ToDouble(firstValue), Convert.ToDouble(secondValue), symbol);
            separatedCalculation.RemoveAt(index + 1);
            separatedCalculation.RemoveAt(index);
            separatedCalculation[index - 1] = result.ToString();
        }
    }
    return Convert.ToDouble(separatedCalculation[0]);
}


static double Compute(double firstValue, double secondValue, string symbol)
{
    return symbol switch
    {
        "+" => firstValue + secondValue,
        "-" => firstValue - secondValue,
        "*" => firstValue * secondValue,
        "/" => firstValue / secondValue,
        _ => default
    };
}