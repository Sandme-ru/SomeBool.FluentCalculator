namespace StatusK.TestTask.FluentCalculator.Models.ExceptionMessages;

public static class ExceptionMessageConstants
{
    public const string OperandFollowExceptionMessage = "Operand cannot follow another operand";

    public const string OperatorFollowExceptionMessage = "Operator cannot follow another operator";

    public const string OperatorLastExceptionMessage = "The first or last character in the input string cannot be an operator";

    public const string DivideByZeroExceptionMessage = "You can't divide by zero";
}