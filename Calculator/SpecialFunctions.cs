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
            return 1 / (decimal)Math.Asin((double)x);
        }
        public static decimal Arccos(decimal x)
        {
            return 1 / (decimal)Math.Acos((double)x);
        }
        public static decimal Arctan(decimal x)
        {
            return 1 / (decimal)Math.Atan((double)x);
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

        public static decimal Ans(decimal index, List<decimal> answers)
        {
            if (index < 1 || index > answers.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range of answers list");
            return answers[^((int)index)];
        }
    }
}
