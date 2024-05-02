using System.Text;
using StatusK.TestTask.FluentCalculator.Models.Operator;

namespace StatusK.TestTask.FluentCalculator
{
    public class FluentCalculator
    {
        private StringBuilder _inputString = new();
        private Stack<string> _operatorStack = new();
        private StringBuilder _tempString = new();
        private int _leftOperand;
        private int _rightOperand;

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

        public FluentCalculator Value(string value)
        {
            if (IsLastCharacterOperand())
            {
                throw new InvalidOperationException("Operand cannot follow another operand");
            }

            _inputString.Append(value);
            return this;
        }

        public FluentCalculator Operator(string op)
        {
            if (IsLastCharacterOperator())
            {
                throw new InvalidOperationException("Operator cannot follow another operator");
            }

            if (_operatorStack.Count == 0)
            {
                _operatorStack.Push(op);
                return this;
            }
            else if (_operatorStack.Count != 0)
            {
                if (OperatorContainer.FindOperator(_operatorStack.Peek())!.Priority < OperatorContainer.FindOperator(op)!.Priority)
                {
                    _operatorStack.Push(op);
                    return this;
                }

                while (OperatorContainer.FindOperator(_operatorStack.Peek())?.Priority >= OperatorContainer.FindOperator(op)?.Priority)
                {
                    _inputString.Append(_operatorStack.Pop());

                    if (!_operatorStack.Any())
                    {
                        _operatorStack.Push(op);
                        break;
                    }
                }
            }

            return this;
        }

        public int Result()
        {
            while (_operatorStack.Count != 0)
                _inputString.Append(_operatorStack.Pop());

            foreach (char c in _inputString.ToString())
            {
                if (Char.IsDigit(c))
                {
                    _operatorStack.Push(Convert.ToString(c));
                    continue;
                }
                else if (!Char.IsDigit(c))
                {

                    if (c == '+')
                    {
                        _rightOperand = Convert.ToInt32(_operatorStack.Pop());
                        _leftOperand = _operatorStack.Count > 0 ? Convert.ToInt32(_operatorStack.Pop()) : 0;

                        _tempString.Clear();
                        _tempString.Append(Convert.ToString(_leftOperand + _rightOperand));
                    }
                    else if (c == '-')
                    {
                        _rightOperand = Convert.ToInt32(_operatorStack.Pop());
                        _leftOperand = _operatorStack.Count > 0 ? Convert.ToInt32(_operatorStack.Pop()) : 0;

                        _tempString.Clear();
                        _tempString.Append(Convert.ToString(_leftOperand - _rightOperand));
                    }
                    else if (c == '*')
                    {
                        _rightOperand = Convert.ToInt32(_operatorStack.Pop());
                        _leftOperand = _operatorStack.Count > 0 ? Convert.ToInt32(_operatorStack.Pop()) : 0;

                        _tempString.Clear();
                        _tempString.Append(Convert.ToString(_leftOperand * _rightOperand));
                    }
                    else if (c == '/')
                    {
                        _rightOperand = Convert.ToInt32(_operatorStack.Pop());
                        _leftOperand = _operatorStack.Count > 0 ? Convert.ToInt32(_operatorStack.Pop()) : 0;

                        _tempString.Clear();
                        _tempString.Append(Convert.ToString(_leftOperand / _rightOperand));
                    }

                    _operatorStack.Push(_tempString.ToString());
                }
            }
            return Convert.ToInt32(_tempString.ToString());
        }

        public static implicit operator int(FluentCalculator calc)
        {
            return calc.Result();
        }

        private bool IsLastCharacterOperand()
        {
            if (_inputString.Length > 0)
            {
                if (_operatorStack.Count == _inputString.Length)
                {
                    return false;
                }
                else if (_operatorStack.Count + 1 == _inputString.Length)
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        private bool IsLastCharacterOperator()
        {
            if (_inputString.Length > 0)
            {
                char lastChar = _inputString[^1];
                return lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/';
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OperatorContainer.AddOperator(new Operator(2, "*"));
            OperatorContainer.AddOperator(new Operator(2, "/"));
            OperatorContainer.AddOperator(new Operator(1, "+"));
            OperatorContainer.AddOperator(new Operator(1, "-"));

            FluentCalculator calc = new FluentCalculator();

            int result1 = calc.Times.One.Plus.Two * 10;
            Console.WriteLine(result1);

            //int result2 = calc.One.Plus.Ten;
            //Console.WriteLine(result2);

            //int result3 = calc.One.Plus.Ten;
            //Console.WriteLine(result3);
        }
    }
}
