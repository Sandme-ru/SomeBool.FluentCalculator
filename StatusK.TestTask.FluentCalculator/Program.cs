namespace StatusK.TestTask.FluentCalculator;

public class FluentCalculator
{
    private static readonly Dictionary<string, int> NumberMap = new Dictionary<string, int>
    {
        {"zero", 0}, {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4},
        {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}, {"ten", 10}
    };

    private readonly Queue<int> _operands = new Queue<int>();
    private readonly Queue<string> _operators = new Queue<string>();

    public FluentCalculator Zero => Value("zero");
    public FluentCalculator One => Value("one");
    public FluentCalculator Two => Value("two");
    public FluentCalculator Three => Value("three");
    public FluentCalculator Four => Value("four");
    public FluentCalculator Five => Value("five");
    public FluentCalculator Six => Value("six");
    public FluentCalculator Seven => Value("seven");
    public FluentCalculator Eight => Value("eight");
    public FluentCalculator Nine => Value("nine");
    public FluentCalculator Ten => Value("ten");

    public FluentCalculator Plus => Operator("+");
    public FluentCalculator Minus => Operator("-");
    public FluentCalculator Times => Operator("*");
    public FluentCalculator DividedBy => Operator("/");

    private FluentCalculator Value(string value)
    {
        _operands.Enqueue(NumberMap[value]);
        return this;
    }

    private FluentCalculator Operator(string op)
    {
        _operators.Enqueue(op);
        return this;
    }

    public int Result()
    {
        if (_operands.Count != _operators.Count + 1)
        {
            throw new InvalidOperationException("Invalid expression.");
        }

        var result = _operands.Dequeue();
        while (_operators.Count > 0)
        {
            var op = _operators.Dequeue();
            var operand = _operands.Dequeue();
            if (op == "+")
                result += operand;
            else if (op == "-")
                result -= operand;
            else if (op == "*")
                result *= operand;
            else if (op == "/")
                result /= operand;
        }
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        FluentCalculator calc = new FluentCalculator();

        int result1 = calc.One.Plus.Two.Plus.Three.Minus.One.Minus.Two.Minus.Four.Result(); // Should be -1
        Console.WriteLine(result1);

        int result2 = calc.One.Plus.Ten.Result(); // Should be 1
        Console.WriteLine(result2);

        int result3 = calc.One.Plus.Ten.Result(); // Should be 1
        Console.WriteLine(result3);
    }
}