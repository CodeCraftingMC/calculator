using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class SpecialFunctions
    {

        public static decimal Sin(decimal x)
        {
            return (decimal)Math.Sin((double)x);
        }
        public static decimal Cos(decimal x)
        {
            return (decimal)Math.Cos((double)x);
        }
        public static decimal Tan(decimal x)
        {
            return (decimal)Math.Tan((double)x);
        }
        public static decimal Cot(decimal x)
        {
            return 1 / (decimal)Math.Tan((double)x);
        }
        public static decimal Sec(decimal x)
        {
            return 1 / (decimal)Math.Cos((double)x);
        }
        public static decimal Csc(decimal x)
        {
            return 1 / (decimal)Math.Sin((double)x);
        }



        public static decimal Arcsin(decimal x)
        {
            return (decimal)Math.Asin((double)x);
        }
        public static decimal Arccos(decimal x)
        {
            return (decimal)Math.Acos((double)x);
        }
        public static decimal Arctan(decimal x)
        {
            return (decimal)Math.Atan((double)x);
        }
        public static decimal Arccot(decimal x)
        {
            return (decimal)Math.Atan((double) (1 / x));
        }
        public static decimal Arcsec(decimal x)
        {
            return (decimal)Math.Acos((double)(1 / x));
        }
        public static decimal Arccsc(decimal x)
        {
            return (decimal)Math.Asin((double)(1 / x));
        }



        public static decimal Sinh(decimal x)
        {
            return (decimal)Math.Sinh((double)x);
        }
        public static decimal Cosh(decimal x)
        {
            return (decimal)Math.Cosh((double)x);
        }
        public static decimal Tanh(decimal x)
        {
            return (decimal)Math.Tanh((double)x);
        }
        public static decimal Csch(decimal x)
        {
            return 1 / (decimal)Math.Sinh((double)x);
        }
        public static decimal Sech(decimal x)
        {
            return 1 / (decimal)Math.Cosh((double)x);
        }
        public static decimal Coth(decimal x)
        {
            return (decimal)Math.Cosh((double)x) / (decimal)Math.Sinh((double)x);
        }


        public static decimal Arcsinh(decimal x)
        {
            return (decimal)Math.Asinh((double)x);
        }
        public static decimal Arccosh(decimal x)
        {
            return (decimal)Math.Acosh((double)x);
        }
        public static decimal Arctanh(decimal x)
        {
            return (decimal)Math.Atanh((double)x);
        }
        public static decimal Arcsech(decimal x)
        {
            return (decimal)Math.Acosh((double)(1 / x));
        }
        public static decimal Arccoth(decimal x)
        {
            return (decimal)Math.Atanh((double)(1 / x));
        }
        public static decimal Arccsch(decimal x)
        {
            return (decimal)Math.Asinh((double)(1 / x));
        }

        public static decimal Factorial(decimal x)
        {
            decimal result = 1;
            for (decimal i = 1; i < x + 1; i++)
            {
                result *= i;
            }

            return result;
        }

        public static decimal Abs(decimal x)
        {
            return Math.Abs(x);
        }

        public static decimal Sqrt(decimal x)
        {
            return (decimal) Math.Sqrt((double)x);
        }

        //public static decimal Log2()


        public static decimal Ans(decimal index, List<decimal> answers)
        {
            if (index < 1 || index > answers.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range of answers list");
            return answers[^((int)index)];
        }
    }
}
