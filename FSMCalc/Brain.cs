using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMCalc
{
    public enum CalcStates
    {
        Zero,
        AccumulateDigits,
        AccumulateDigitsWithDecimal,
        ComputePending,
        Compute
    }

    public delegate void PrintDelegate(string message);

    public class Brain
    {
        public PrintDelegate invoker;
        public CalcStates currentState = CalcStates.Zero;

        public string numbers = "";
        public char op = '.';
        public string result = "";

        public char[] zero = { '0'};
        public char[] equals = { '='};
        public char[] operations = { '+','-' };
        public char[] separators = { ','};
        public char[] digits = {'0','1','2','3','4','5','6','7','8','9' };
        public char[] nonzerodigits = {'1','2','3','4','5','6','7','8','9' };

        public void Processor(char item)
        {
            switch (currentState)
            {
                case CalcStates.Zero:
                    Zero(item, false);
                    break;
                case CalcStates.AccumulateDigits:
                    AccumulateDigits(item, false);
                    break;
                case CalcStates.AccumulateDigitsWithDecimal:
                    AccumulateDigitsWithDecimal(item, false);
                    break;
                case CalcStates.ComputePending:
                    ComputePending(item, false);
                    break;
                case CalcStates.Compute:
                    Compute(item, false);
                    break;
                default:
                    break;
            }
        }

        public void Zero(char item, bool isInput)
        {
            if (isInput)
            {
                numbers = "0";
                result = "0";
                op = '.';
                invoker.Invoke(numbers);
                currentState = CalcStates.Zero;
            }
            else
            {
                if (nonzerodigits.Contains(item))
                {
                    AccumulateDigits(item,true);
                }else if (separators.Contains(item))
                {
                    AccumulateDigitsWithDecimal(item, true);
                }
                else if (zero.Contains(item))
                {
                    Zero(item, true);
                }
            }
        }

        public void AccumulateDigits(char item, bool isInput)
        {
            if (isInput)
            {
                result = result + item;
                invoker.Invoke(result);
                currentState = CalcStates.AccumulateDigits;
            }
            else
            {
                if (digits.Contains(item))
                {
                    AccumulateDigits(item, true);
                }else if (operations.Contains(item))
                {
                    ComputePending(item, true);
                }else if (separators.Contains(item))
                {
                    AccumulateDigitsWithDecimal(item, true);
                }else if (equals.Contains(item))
                {
                    Compute(item, true);
                }
                else if (operations.Contains(item))
                {
                    ComputePending(item, true);
                }
            }
        }

        public void AccumulateDigitsWithDecimal(char item, bool isInput)
        {

            if (isInput)
            {
                currentState = CalcStates.AccumulateDigitsWithDecimal;
                if (!result.Contains(item))
                {
                    result = result + item;
                }
                invoker.Invoke(result);
            }
            else
            {
                if (digits.Contains(item))
                {
                    AccumulateDigitsWithDecimal(item, true);
                }else if (operations.Contains(item))
                {
                    ComputePending(item, true);
                }
                else if (equals.Contains(item))
                {
                    Compute(item, true);
                }
            }
        }

        public void ComputePending(char item, bool isInput)
        {
            if (isInput)
            {
                currentState = CalcStates.ComputePending;
                op = item;
                if(numbers != "")
                {
                    double a1 = double.Parse(numbers);
                    double a2 = double.Parse(result);
                    double a3 = 0;
                    if (op == '+')
                    {
                        a3 = a1 + a2;
                    }
                    else if (op == '-')
                    {
                        a3 = a1 - a2;
                    }
                    result = a3.ToString();
                }
                numbers = result;
                invoker.Invoke(result);
                result = "";
            }
            else
            {
                if (digits.Contains(item))
                {
                    AccumulateDigits(item, true);
                }
            }
        }

        public void Compute(char item, bool isInput)
        {
            if (isInput)
            {
                currentState = CalcStates.Compute;
                double a1 = double.Parse(numbers);
                double a2 = double.Parse(result);
                double a3 = 0;
                if(op == '+')
                {
                    a3 = a1 + a2;
                }else if(op == '-')
                {
                    a3 = a1 - a2;
                }
                result = a3.ToString();
                invoker.Invoke(result);
            }
            else
            {
                if (nonzerodigits.Contains(item))
                {
                    AccumulateDigits(item, true);
                }else if (zero.Contains(item))
                {
                    Zero(item, true);
                }
                else if (operations.Contains(item))
                {
                    ComputePending(item, true);
                }
            }
        }


    }
}
