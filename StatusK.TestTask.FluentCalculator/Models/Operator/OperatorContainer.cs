namespace StatusK.TestTask.FluentCalculator.Models.Operator;

public static class OperatorContainer
{
    private static readonly List<Operator?> Operators = new();

    private const int HighPriority = 2;

    private const int LowPriority = 1;

    static OperatorContainer()
    {
        Operators.Add(new Operator(HighPriority, OperatorConstants.Times));
        Operators.Add(new Operator(HighPriority, OperatorConstants.DividedBy));
        Operators.Add(new Operator(LowPriority, OperatorConstants.Plus));
        Operators.Add(new Operator(LowPriority, OperatorConstants.Minus));
    }

    public static Operator? FindOperator(string s)
    {
        return Operators.FirstOrDefault(op => op != null && op.Symbol == s);
    }
}