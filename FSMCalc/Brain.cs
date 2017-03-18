using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMCalc
{

    class Brain
    {
        public string currentState = "";

        public string numbers = "";
        public char op = '';
        public string result = "";

        public char[] zero = { '0'};
        public char[] equals = { '='};
        public char[] operations = { '+','-' };
        public char[] separators = { ','};
        public char[] digits = {'0','1','2','3','4','5','6','7','8','9' };
        public char[] nonzerodigits = {'1','2','3','4','5','6','7','8','9' };



        public void Processor(char item)
        {
            if(currentState == "Zero")
            {
                Zero(item, false);
            }
            else if(currentState == "AccumulateDigits")
            {
                AccumulateDigits(item, false);
            }
        }

        public void Zero(char item, bool isInput)
        {
            numbers = "";
            result = "";
            op = '.';

            if (isInput)
            {
                currentState = "Zero";
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
            }
        }

        public void AccumulateDigits(char item, bool isInput)
        {
            if (isInput)
            {
                numbers = numbers + item;
                currentState = "AccumulateDigits";
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
            }
        }

        public void AccumulateDigitsWithDecimal(char item, bool isInput)
        {

            if (isInput)
            {
                currentState = "AccumulateDigitsWithDecimal";
                if (!numbers.Contains(item))
                {
                    numbers = numbers + item;
                }
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
            }
        }

        public void ComputePending(char item, bool isInput)
        {
            if (isInput)
            {
                currentState = "ComputePending";
                op = item;
                result = numbers;
                numbers = "";
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
                currentState = "Compute";
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
            }
        }


    }
}
