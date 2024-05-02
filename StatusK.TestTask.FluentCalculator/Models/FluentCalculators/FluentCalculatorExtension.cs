using StatusK.TestTask.FluentCalculator.Models.Operands;
using StatusK.TestTask.FluentCalculator.Models.Operator;

namespace StatusK.TestTask.FluentCalculator.Models.FluentCalculators;

public partial class FluentCalculator
{
    public FluentCalculator Zero => Value(OperandConstants.Zero);

    public FluentCalculator One => Value(OperandConstants.One);

    public FluentCalculator Two => Value(OperandConstants.Two);

    public FluentCalculator Three => Value(OperandConstants.Three);

    public FluentCalculator Four => Value(OperandConstants.Four);

    public FluentCalculator Five => Value(OperandConstants.Five);

    public FluentCalculator Six => Value(OperandConstants.Six);

    public FluentCalculator Seven => Value(OperandConstants.Seven);

    public FluentCalculator Eight => Value(OperandConstants.Eight);

    public FluentCalculator Nine => Value(OperandConstants.Nine);

    public FluentCalculator Ten => Value(OperandConstants.Ten);

    public FluentCalculator Plus => Operator(OperatorConstants.Plus);

    public FluentCalculator Minus => Operator(OperatorConstants.Minus);

    public FluentCalculator Times => Operator(OperatorConstants.Times);

    public FluentCalculator DividedBy => Operator(OperatorConstants.DividedBy);
}