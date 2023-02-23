// See https://aka.ms/new-console-template for more information

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
    string processedCalculation = calculation;
    if (calculation.Contains('('))
    {
        string bracketCalculation = calculation.Substring(calculation.IndexOf('(') + 2, calculation.LastIndexOf(')') - 1 - (calculation.IndexOf('(') + 2));
        processedCalculation = calculation.Replace($"( {bracketCalculation} )", Calculate(bracketCalculation).ToString());
    }

    List<string> separatedCalculation = new(processedCalculation.Split(' '));
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