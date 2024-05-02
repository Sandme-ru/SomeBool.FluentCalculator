using System.Text;
using StatusK.TestTask.FluentCalculator.Models.ExceptionMessages;
using StatusK.TestTask.FluentCalculator.Models.Operands;
using StatusK.TestTask.FluentCalculator.Models.Operator;

namespace StatusK.TestTask.FluentCalculator.Models.FluentCalculators;

public partial class FluentCalculator
{
    private StringBuilder _inputString = new();
    
    private StringBuilder _tempString = new();

    private Stack<string> _operatorStack = new();

    private int _leftOperand;

    private int _rightOperand;
    
    public FluentCalculator Value(string value)
    {
        IsLastCharacterOperand();
            
        _inputString.Append(value);
        return this;
    }

    public FluentCalculator Operator(char op)
    {
        var operationSymbol = op.ToString();

        IsLastCharacterOperator();

        if (!_operatorStack.Any())
        {
            _operatorStack.Push(operationSymbol);
            return this;
        }
        else if (_operatorStack.Any())
        {
            if (OperatorContainer.FindOperator(_operatorStack.Peek())?.Priority < OperatorContainer.FindOperator(operationSymbol)?.Priority)
            {
                _operatorStack.Push(operationSymbol);
                return this;
            }

            while (OperatorContainer.FindOperator(_operatorStack.Peek())?.Priority >= OperatorContainer.FindOperator(operationSymbol)?.Priority)
            {
                _inputString.Append(_operatorStack.Pop());

                if (!_operatorStack.Any())
                {
                    _operatorStack.Push(operationSymbol);
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

        foreach (var c in _inputString.ToString())
        {
            if (char.IsDigit(c) || c.ToString() == OperandConstants.Ten)
            {
                _operatorStack.Push(Convert.ToString(c));
                continue;
            }
            else if (!char.IsDigit(c))
            {
                CheckLastCharacter();
                
                FillExpression();

                switch (c)
                {
                    case OperatorConstants.Plus:
                        _tempString.Append(_leftOperand + _rightOperand);
                        break;
                    case OperatorConstants.Minus:
                        _tempString.Append(_leftOperand - _rightOperand);
                        break;
                    case OperatorConstants.Times:
                        _tempString.Append(_leftOperand * _rightOperand);
                        break;
                    case OperatorConstants.DividedBy:
                        if (_rightOperand == 0)
                            throw new DivideByZeroException(ExceptionMessageConstants.DivideByZeroExceptionMessage);

                        _tempString.Append(_leftOperand / _rightOperand);
                        break;
                }

                _operatorStack.Push(_tempString.ToString());
            }
        }

        if (_operatorStack.Count == 1 && _tempString.Length == 0)
            _tempString.Append(_operatorStack.Pop());

        var result = Convert.ToInt32(_tempString.ToString());

        ClearStructures();

        return result;
    }

    private void ClearStructures()
    {
        _operatorStack.Clear();
        _inputString.Clear();

        _leftOperand = 0; 
        _rightOperand = 0;

        _tempString.Clear();
    }

    private void FillExpression()
    {
        if (_operatorStack.Peek() == OperandConstants.Ten)
        {
            _rightOperand = 10;
            _operatorStack.Pop();
        }
        else
            _rightOperand = Convert.ToInt32(_operatorStack.Pop());

        if (_operatorStack.Peek() == OperandConstants.Ten)
        {
            _leftOperand = 10;
            _operatorStack.Pop();
        }
        else
            _leftOperand = _operatorStack.Count > 0 ? Convert.ToInt32(_operatorStack.Pop()) : 0;

        _tempString.Clear();
    }

    private void CheckLastCharacter()
    {
        if (_operatorStack.Count is 0 or 1)
            throw new InvalidOperationException(ExceptionMessageConstants.OperatorLastExceptionMessage);
    }

    public static implicit operator int(FluentCalculator calc)
    {
        return calc.Result();
    }

    private void IsLastCharacterOperand()
    {
        var inputStringLength = _inputString.Length;
        var operatorStackCount = _operatorStack.Count;

        if (inputStringLength > 0)
        {
            if (operatorStackCount <= inputStringLength)
                return;

            throw new InvalidOperationException(ExceptionMessageConstants.OperandFollowExceptionMessage);
        }
    }

    private void IsLastCharacterOperator()
    {
        if (_inputString.Length > 0)
        {
            var lastChar = _inputString[^1];

            if(lastChar is OperatorConstants.Plus or OperatorConstants.Minus or OperatorConstants.Times or OperatorConstants.DividedBy)
                throw new InvalidOperationException(ExceptionMessageConstants.OperatorFollowExceptionMessage);
        }
    }
}