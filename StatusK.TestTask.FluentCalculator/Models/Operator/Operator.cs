namespace StatusK.TestTask.FluentCalculator.Models.Operator;

public class Operator(int priority, char symbol)
{
    public int Priority = priority;

    public string Symbol = symbol.ToString();
}