using System.ComponentModel;
using System.Text;
using StatusK.TestTask.FluentCalculator.Models.Operator;

namespace StatusK.TestTask.FluentCalculator;

public class FluentCalculator
{

    private StringBuilder _inputString = new();

    private Stack<string> _operatorStack = new();

    private StringBuilder _tempString = new();

    private int _leftOperand;

    private int _rightOperand;

    //private static readonly Dictionary<string, int> NumberMap = new Dictionary<string, int>
    //{
    //    {"zero", 0}, {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4},
    //    {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}, {"ten", 10}
    //};

    private readonly Queue<int> _operands = new Queue<int>();
    private readonly Queue<string> _operators = new Queue<string>();

    public FluentCalculator Zero => Value("0");
    public FluentCalculator One => Value("1");
    public FluentCalculator Two => Value("2");
    public FluentCalculator Three => Value("3");
    public FluentCalculator Four => Value("4");
    public FluentCalculator Five => Value("5");
    public FluentCalculator Six => Value("6");
    public FluentCalculator Seven => Value("7");
    public FluentCalculator Eight => Value("8");
    public FluentCalculator Nine => Value("9");
    public FluentCalculator Ten => Value("10");

    public FluentCalculator Plus => Operator("+");
    public FluentCalculator Minus => Operator("-");
    public FluentCalculator Times => Operator("*");
    public FluentCalculator DividedBy => Operator("/");

    private FluentCalculator Value(string value)
    {
        _inputString.Append(value);
        return this;
    }

    private FluentCalculator Operator(string op)
    {
        if (_operatorStack.Count == 0)
        {
            _operatorStack.Push(op);
        }
        else if(_operatorStack.Count != 0)
        {
            if (OperatorContainer.FindOperator(_operatorStack.Peek())!.Priority <
                OperatorContainer.FindOperator(op)!.Priority)
            {
                _operatorStack.Push(op);
            }
        }
        if (_operatorStack.Count != 0)
        {
            if (OperatorContainer.FindOperator(_operatorStack.Peek())!.Priority >=
                OperatorContainer.FindOperator(op)!.Priority)
            {
                _inputString.Append(_operatorStack.Pop());
            }

            if (_operatorStack.Count == 0)
            {
                _operatorStack.Push(op);
            }
            else if (_operatorStack.Count != 0)
            {
                if (OperatorContainer.FindOperator(_operatorStack.Peek())!.Priority <
                    OperatorContainer.FindOperator(op)!.Priority)
                {
                    _operatorStack.Push(op);
                }
            }
        }

        return this;
    }

    public int Result()
    {

        while (_operatorStack.Count != 0)
        {
            _inputString.Append(_operatorStack.Pop());
        }

        foreach (char c in _inputString.ToString())
        {
            if (c == '+')
            {
                _tempString.Clear();
                _tempString.Append(Convert.ToString(Convert.ToInt32(_operatorStack.Pop()) +
                                   Convert.ToInt32(_operatorStack.Pop())));
            }
            else if (c == '-')
            {
                _rightOperand = Convert.ToInt32(_operatorStack.Pop());
                _leftOperand = Convert.ToInt32(_operatorStack.Pop());

                _tempString.Clear();
                _tempString.Append(Convert.ToString(_leftOperand - _rightOperand));
            }
            else if (c == '*')
            {
                _tempString.Clear();
                _tempString.Append(Convert.ToString(Convert.ToInt32(_operatorStack.Pop()) *
                                   Convert.ToInt32(_operatorStack.Pop())));
            }
            else if (c == '/')
            {
                _rightOperand = Convert.ToInt32(_operatorStack.Pop());
                _leftOperand = Convert.ToInt32(_operatorStack.Pop());

                _tempString.Clear();
                _tempString.Append(Convert.ToString(_leftOperand / _rightOperand));
            }
            _operatorStack.Push(_tempString.ToString());
        }

        return Convert.ToInt32(_tempString);
    }
}

class Program
{
    public Program()
    {
       
    }

    static void Main(string[] args)
    {
        OperatorContainer.AddOperator(new Operator(2, "*"));
        OperatorContainer.AddOperator(new Operator(2, "/"));
        OperatorContainer.AddOperator(new Operator(1, "+"));
        OperatorContainer.AddOperator(new Operator(1, "-"));

        FluentCalculator calc = new FluentCalculator();

        int result1 = calc.One.Plus.Two.Plus.Three.Minus.One.Minus.Two.Times.Four.Result(); // Should be -1
        Console.WriteLine(result1);

        int result2 = calc.One.Plus.Ten.Result(); // Should be 1
        Console.WriteLine(result2);

        int result3 = calc.One.Plus.Ten.Result(); // Should be 1
        Console.WriteLine(result3);
    }
}