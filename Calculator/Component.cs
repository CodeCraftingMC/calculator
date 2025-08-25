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
        public decimal n;
        public DecimalComponent(decimal n)
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


        public SpecialFunctionComponent(string specialFunction) { 
            this.specialFunction = specialFunction;
        }
    }
}
