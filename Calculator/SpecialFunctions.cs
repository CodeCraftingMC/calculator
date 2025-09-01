namespace Calculator
{
    class SpecialFunctions
    {

        public static decimal Sin(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Sin((double)x);
        }
        public static decimal Cos(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Cos((double)x);
        }
        public static decimal Tan(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Tan((double)x);
        }
        public static decimal Cot(decimal[] args)
        {
            decimal x = args[0];
            return 1 / (decimal)Math.Tan((double)x);
        }
        public static decimal Sec(decimal[] args)
        {
            decimal x = args[0];
            return 1 / (decimal)Math.Cos((double)x);
        }
        public static decimal Csc(decimal[] args)
        {
            decimal x = args[0];
            return 1 / (decimal)Math.Sin((double)x);
        }



        public static decimal Arcsin(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Asin((double)x);
        }
        public static decimal Arccos(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Acos((double)x);
        }
        public static decimal Arctan(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Atan((double)x);
        }
        public static decimal Arccot(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Atan((double)(1 / x));
        }
        public static decimal Arcsec(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Acos((double)(1 / x));
        }
        public static decimal Arccsc(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Asin((double)(1 / x));
        }



        public static decimal Sinh(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Sinh((double)x);
        }
        public static decimal Cosh(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Cosh((double)x);
        }
        public static decimal Tanh(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Tanh((double)x);
        }
        public static decimal Csch(decimal[] args)
        {
            decimal x = args[0];
            return 1 / (decimal)Math.Sinh((double)x);
        }
        public static decimal Sech(decimal[] args)
        {
            decimal x = args[0];
            return 1 / (decimal)Math.Cosh((double)x);
        }
        public static decimal Coth(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Cosh((double)x) / (decimal)Math.Sinh((double)x);
        }


        public static decimal Arcsinh(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Asinh((double)x);
        }
        public static decimal Arccosh(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Acosh((double)x);
        }
        public static decimal Arctanh(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Atanh((double)x);
        }
        public static decimal Arcsech(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Acosh((double)(1 / x));
        }
        public static decimal Arccoth(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Atanh((double)(1 / x));
        }
        public static decimal Arccsch(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Asinh((double)(1 / x));
        }

        public static decimal Factorial(decimal[] args)
        {
            decimal x = args[0];
            decimal result = 1;
            for (decimal i = 1; i < x + 1; i++)
            {
                result *= i;
            }

            return result;
        }

        public static decimal Abs(decimal[] args)
        {
            decimal x = args[0];
            return Math.Abs(x);
        }

        public static decimal Sqrt(decimal[] args)
        {
            decimal x = args[0];
            return (decimal)Math.Sqrt((double)x);
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
