namespace Calculator
{
    class SpecialFunctions
    {

        public static double Sin(double[] args)
        {
            double x = args[0];
            return (double)Math.Sin((double)x);
        }
        public static double Cos(double[] args)
        {
            double x = args[0];
            return (double)Math.Cos((double)x);
        }
        public static double Tan(double[] args)
        {
            double x = args[0];
            return (double)Math.Tan((double)x);
        }
        public static double Cot(double[] args)
        {
            double x = args[0];
            return 1 / (double)Math.Tan((double)x);
        }
        public static double Sec(double[] args)
        {
            double x = args[0];
            return 1 / (double)Math.Cos((double)x);
        }
        public static double Csc(double[] args)
        {
            double x = args[0];
            return 1 / (double)Math.Sin((double)x);
        }



        public static double Arcsin(double[] args)
        {
            double x = args[0];
            return (double)Math.Asin((double)x);
        }
        public static double Arccos(double[] args)
        {
            double x = args[0];
            return (double)Math.Acos((double)x);
        }
        public static double Arctan(double[] args)
        {
            double x = args[0];
            return (double)Math.Atan((double)x);
        }
        public static double Arccot(double[] args)
        {
            double x = args[0];
            return (double)Math.Atan((double)(1 / x));
        }
        public static double Arcsec(double[] args)
        {
            double x = args[0];
            return (double)Math.Acos((double)(1 / x));
        }
        public static double Arccsc(double[] args)
        {
            double x = args[0];
            return (double)Math.Asin((double)(1 / x));
        }



        public static double Sinh(double[] args)
        {
            double x = args[0];
            return (double)Math.Sinh((double)x);
        }
        public static double Cosh(double[] args)
        {
            double x = args[0];
            return (double)Math.Cosh((double)x);
        }
        public static double Tanh(double[] args)
        {
            double x = args[0];
            return (double)Math.Tanh((double)x);
        }
        public static double Csch(double[] args)
        {
            double x = args[0];
            return 1 / (double)Math.Sinh((double)x);
        }
        public static double Sech(double[] args)
        {
            double x = args[0];
            return 1 / (double)Math.Cosh((double)x);
        }
        public static double Coth(double[] args)
        {
            double x = args[0];
            return (double)Math.Cosh((double)x) / (double)Math.Sinh((double)x);
        }


        public static double Arcsinh(double[] args)
        {
            double x = args[0];
            return (double)Math.Asinh((double)x);
        }
        public static double Arccosh(double[] args)
        {
            double x = args[0];
            return (double)Math.Acosh((double)x);
        }
        public static double Arctanh(double[] args)
        {
            double x = args[0];
            return (double)Math.Atanh((double)x);
        }
        public static double Arcsech(double[] args)
        {
            double x = args[0];
            return (double)Math.Acosh((double)(1 / x));
        }
        public static double Arccoth(double[] args)
        {
            double x = args[0];
            return (double)Math.Atanh((double)(1 / x));
        }
        public static double Arccsch(double[] args)
        {
            double x = args[0];
            return (double)Math.Asinh((double)(1 / x));
        }

        public static double Factorial(double[] args)
        {
            double x = args[0];
            double result = 1;
            for (double i = 1; i < x + 1; i++)
            {
                result *= i;
            }

            return result;
        }

        public static double Abs(double[] args)
        {
            double x = args[0];
            return Math.Abs(x);
        }

        public static double Sqrt(double[] args)
        {
            double x = args[0];
            return (double)Math.Sqrt((double)x);
        }

        public static double Root(double[] args)
        {
            double x = args[0];
            double _base = args[1];
            return (double)Math.Pow((double) x, 1.0 / (double) _base);
        }

        public static double Log2(double[] args)
        {
            double x = args[0];
            return (double)Math.Log((double)x, 2.0);
        }

        public static double Log10(double[] args)
        {
            double x = args[0];
            return (double)Math.Log((double)x, 10.0);
        }

        public static double Log(double[] args)
        {
            double x = args[0];
            double y = args[1];
            return (double)Math.Log((double)x, (double)y);
        }

        public static double Ln(double[] args)
        {
            double x = args[0];
            return (double)Math.Log((double)x, MathConstants.E);
        }
        public static double Exp(double[] args)
        {
            double x = args[0];
            return (double)Math.Exp((double)x);
        }

        public static double Mod(double[] args)
        {
            double x = args[0];
            double y = args[1];
            return x % y;
        }


        public static double Ans(double index, List<double> answers)
        {
            if (index < 1 || index > answers.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range of answers list");
            return answers[^((int)index)];
        }
    }
}
