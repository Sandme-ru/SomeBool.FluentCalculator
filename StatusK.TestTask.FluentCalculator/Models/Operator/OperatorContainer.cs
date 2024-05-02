namespace StatusK.TestTask.FluentCalculator.Models.Operator;

public class OperatorContainer
{
    public static List<Operator?> Operators = new List<Operator?>();

    public static void AddOperator(Operator? @operator)
    {
        Operators.Add(@operator);
    }

    public static Operator? FindOperator(string s)
    {
        foreach (var @operator in Operators)
        {
            if(@operator != null && @operator.Symbol == s)
                return @operator;
        }

        return null;
    }
}