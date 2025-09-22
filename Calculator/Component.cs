namespace Calculator
{
    public enum BracketType
    {
        OPENING,
        CLOSING
    }
    public class Component
    {

    }

    public class DecimalComponent : Component
    {
        public double N { get; set; }
        public DecimalComponent(double n)
        {
            N = n;
        }
    }

    public class OperatorComponent : Component
    {
        public string OperatorString { get; set; }

        public OperatorComponent(string operatorChar)
        {
            OperatorString = operatorChar;
        }

        public void RemoveLast()
        {
            OperatorString = OperatorString.Substring(0, OperatorString.Length - 1);
        }
    }

    public class BracketComponent : Component
    {
        public BracketType Type { get; set; }
        public BracketComponent(BracketType type)
        {
            Type = type;
        }

        public BracketComponent(string bracket)
        {
            if (bracket == "(")
            {
                Type = BracketType.OPENING;
            }
            else
            {
                Type = BracketType.CLOSING;
            }
        }

        public override string ToString()
        {
            if (Type == BracketType.OPENING)
            {
                return "(";
            }
            else
            {
                return ")";
            }
        }
    }

    public class SpecialFunctionComponent : Component
    {
        public string SpecialFunction { get; set; }
        public List<double> Args { get; set; }


        public SpecialFunctionComponent(string specialFunction, List<double> args)
        {
            SpecialFunction = specialFunction;
            Args = args;
        }
    }
}
