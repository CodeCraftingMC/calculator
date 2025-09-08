using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public double n;
        public DecimalComponent(double n)
        {
            this.n = n;
        }
    }

    public class OperatorComponent : Component
    {
        public string operatorString;

        public OperatorComponent(string operatorChar) { 
            this.operatorString = operatorChar;
        }

        public void RemoveLast()
        {
            operatorString = operatorString.Substring(0, operatorString.Length - 1);
        }
    }

    public class BracketComponent : Component
    {
        public BracketType type; // true = (, false = )
        public BracketComponent(BracketType type)
        {
            this.type = type;
        }

        public BracketComponent(string bracket)
        {
            if (bracket == "(")
            {
                type = BracketType.OPENING;
            }
            else
            {
                type = BracketType.CLOSING;
            }
        }

        public string toString()
        {
            if (type == BracketType.OPENING)
            {
                return "(";
            }
            else
            {
                return ")";
            }
        }
    }

    public class SpecialFunctionComponent : Component {
        public string specialFunction;
        public List<double> args;


        public SpecialFunctionComponent(string specialFunction, List<double> args) { 
            this.specialFunction = specialFunction;
            this.args = args;
        }
    }
}
