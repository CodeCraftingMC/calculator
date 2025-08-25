using Microsoft.VisualBasic.FileIO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Parser{

        HashSet<char> numberCharSet = new HashSet<char>();
        HashSet<char> operatorCharSet = new HashSet<char>();
        HashSet<char> parenthesesCharSet = new HashSet<char>();

        string numbers = "1234567890."; // number charset
        string operators = "+-/*^"; // operator charset
        string brackets = "()"; // bracket charset

        string operators0 = "^"; // E
        string operators1 = "/*"; // MD
        string operators2 = "+-"; // AS

        string[] operatorHierarchy = {}; // operator hierarchy

        List<decimal> history = new();
        Dictionary<string, Func<decimal, decimal>> specialFunctionMap;

        public enum ComponentType // component types
        {
            NUMBER,
            OPERATOR,
            BRACKET,
            SPECIALFUNC
        }

        public Parser()
        {
            specialFunctionMap = new()
            {
                { "sin", SpecialFunctions.Sin },
                { "cos", SpecialFunctions.Cos },
                { "tan", SpecialFunctions.Tan },
                { "cot", SpecialFunctions.Cot },
                { "sec", SpecialFunctions.Sec },
                { "csc", SpecialFunctions.Csc },

                { "arcsin", SpecialFunctions.Arcsin },
                { "arccos", SpecialFunctions.Arccos },
                { "arctan", SpecialFunctions.Arctan },
                { "arccot", SpecialFunctions.Arccot },
                { "arcsec", SpecialFunctions.Arcsec },
                { "arccsc", SpecialFunctions.Arccsc },

                { "sinh", SpecialFunctions.Sinh },
                { "Cosh", SpecialFunctions.Cosh },
                { "Tanh", SpecialFunctions.Tanh },
                { "Coth", SpecialFunctions.Coth },
                { "Sech", SpecialFunctions.Sech },
                { "Csch", SpecialFunctions.Csch },
                { "ans", (dec) => SpecialFunctions.Ans(dec, history) },
            };
        }

        private ComponentType GetCharType(char c)
        {
            /*returns which charset contains the char c*/
            if (numberCharSet.Contains(c))
            {
                return ComponentType.NUMBER;
            }
            else if (operatorCharSet.Contains(c)) {
                return ComponentType.OPERATOR;
            }
            else if (parenthesesCharSet.Contains(c))
            {
                return ComponentType.BRACKET;
            }
            return ComponentType.SPECIALFUNC;
        }

        private static void AddCurrentSelection(string selection, ComponentType type, ref List<Component> components)
        {
            /* adds a selection to a reference to the components. It takes the type into account*/
            if (type == ComponentType.NUMBER)
            {
                components.Add(new DecimalComponent(Decimal.Parse(selection)));
            }
            else if (type == ComponentType.OPERATOR)
            {
                components.Add(new OperatorComponent(selection));
            }
            else if (type == ComponentType.BRACKET)
            {
                components.Add(new BracketComponent(selection));
            }
            else if (type == ComponentType.SPECIALFUNC) { 
                components.Add(new SpecialFunctionComponent(selection));
            }
        }

        public void ParseNegatives(ref List<Component> components)
        {

            bool ppcIsInt = false;
            for (int i = 1; i < components.Count; i++)
            {
                Component c = components[i];
                Component pc = components[i - 1];

                if (pc.GetType() == typeof(OperatorComponent))
                {
                    OperatorComponent? op = pc as OperatorComponent;
                    if (op == null)
                    {
                        throw new NotImplementedException();
                    }

                    if (op.operatorString.EndsWith("-") && (op.operatorString.Length > 1 || !ppcIsInt))
                    {
                        if (c.GetType() == typeof(DecimalComponent)) {
                            DecimalComponent? dec = c as DecimalComponent;
                            if (dec == null)
                            {
                                throw new NotImplementedException();
                            }
                            dec.n = -dec.n;
                            components[i] = dec;

                            if (op.operatorString.Length > 1)
                            {
                                op.RemoveLast();
                            }
                            else
                            {
                                components.RemoveAt(i - 1);
                            }
                        }
                        else if (c.GetType() == typeof(BracketComponent) || c.GetType() == typeof(SpecialFunctionComponent))
                        {
                            //1.2 +- (1.2)
                            // ->
                            //1.2 + -1 * (1.2)

                            //-(1.2)
                            //->
                            //-1 * (1.2)

                            if (op.operatorString.Length > 1)
                            {
                                op.RemoveLast();
                            }
                            else
                            {
                                components.RemoveAt(i - 1);
                            }
                            components.Insert(i, new OperatorComponent("*"));
                            components.Insert(i, new DecimalComponent(Decimal.Parse("-1")));
               

                            printComponents(components);
                        }
                    }
                }
                ppcIsInt = pc.GetType() == typeof(DecimalComponent);
            }
        }

        public List<Component> ParseComponentTypes(string expression)
        {
            List<Component> components = new List<Component>();

            // initialize charsets and hierarchy

            foreach(char c in numbers){
                numberCharSet.Add(c);
            }
            foreach(char c in operators){
                operatorCharSet.Add(c);
            }
            foreach(char c in brackets){
                parenthesesCharSet.Add(c);
            }

            operatorHierarchy = new string[] { operators0, operators1, operators2 };


            string selection = "";
            ComponentType previousCharType = GetCharType(expression[0]);

            for(int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                ComponentType cCharType = GetCharType(c);
            
                if (cCharType != previousCharType || (cCharType == ComponentType.BRACKET && i != 0))
                {

                    AddCurrentSelection(selection, previousCharType, ref components);
                    
                    selection = "";
                }

                selection += c;

                previousCharType = cCharType;
            }
            AddCurrentSelection(selection, previousCharType, ref components);

            ParseNegatives(ref components);
            return components;
        }

        private int FindOperator(string ops, List<Component> components)
        {
            for (int i = 0; i < components.Count; i++)
            {
                Component c = components[i];

                if (c.GetType() == typeof(OperatorComponent))
                {
                    OperatorComponent? operatorComponent = c as OperatorComponent;
                    if (c != null && operatorComponent != null)
                    {
                        if (ops.Contains(operatorComponent.operatorString)){
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        private DecimalComponent EvaluateOperator(DecimalComponent left, DecimalComponent right, OperatorComponent op)
        {
            decimal result;
            decimal leftDecimal = left.n;
            decimal rightDecimal = right.n;

            if (op.operatorString == "+")
            {
                result = leftDecimal + rightDecimal;
            }
            else if (op.operatorString == "-")
            {
                result = leftDecimal - rightDecimal;
            }
            else if (op.operatorString == "*") { 
                result = leftDecimal * rightDecimal;
            }
            else if (op.operatorString == "/")
            {
                result = leftDecimal / rightDecimal;
            }
            else if (op.operatorString == "^")
            {
                result = (decimal) Math.Pow((double)leftDecimal, (double)rightDecimal);
            }
            else { 
                throw new NotImplementedException();
            }

            return new DecimalComponent(result);
        }

        private void EvaluateOperatorAt(int i, ref List<Component> components)
        {
            Component c = components[i];

            printComponents(components);
            Component left = components[i - 1];
            Component right = components[i + 1];
            OperatorComponent? op = c as OperatorComponent;

            if (left.GetType() != typeof(DecimalComponent) || right.GetType() != typeof(DecimalComponent))
            {
                throw new NotImplementedException();
            }

            DecimalComponent? decimalLeft = left as DecimalComponent;
            DecimalComponent? decimalRight = right as DecimalComponent;
            if (decimalLeft == null || decimalRight == null || op == null)
            {
                throw new NotImplementedException();
            }


            DecimalComponent result = EvaluateOperator(decimalLeft, decimalRight, op);

            components.RemoveRange(i - 1, 2);
            components[i - 1] = result;

            printComponents(components);
        }

        public DecimalComponent GetResult(List<Component> components)
        {
            if (components.Count != 1)
            {
                throw new NotImplementedException();
            }

            if (components[0].GetType() != typeof(DecimalComponent))
            {
                throw new NotImplementedException();
            }

            DecimalComponent? evaluated = components[0] as DecimalComponent;

            if (evaluated == null)
            {
                throw new NotImplementedException();
            }


            return evaluated;
        }

        public DecimalComponent EvaluateSpecialFunc(SpecialFunctionComponent spc, DecimalComponent n)
        {

            return new DecimalComponent(specialFunctionMap[spc.specialFunction](n.n));
        }

        public void EvaluateSpecialFuncs(ref List<Component> components)
        {
            for (int i = 0; i < components.Count - 1; i++) { 
                
                Component c = components[i];
                Component nc = components[i + 1];
                if (c.GetType() == typeof(SpecialFunctionComponent) && nc.GetType() == typeof(DecimalComponent))
                {
                    SpecialFunctionComponent? spc = c as SpecialFunctionComponent;
                    DecimalComponent? n = nc as DecimalComponent;
                    if (spc != null && n != null) {
                        Console.WriteLine(spc.specialFunction);
                        DecimalComponent result = EvaluateSpecialFunc(spc, n);
                        components.RemoveAt(i);
                        components[i] = result;
                        i -= 1;
                    }

                }
            }
        }

        public DecimalComponent EvaluateSimpleExpression(List<Component> components)
        {
            EvaluateSpecialFuncs(ref components);


            for (int i = 0; i < operatorHierarchy.Count(); i++)
            {
                string set = operatorHierarchy[i];

                while (true)
                {
                    int j = FindOperator(set, components);

                    if (j != -1)
                    {
                        EvaluateOperatorAt(j, ref components);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return GetResult(components);
        }

        public DecimalComponent EvaluateParsed(List<Component> components)
        {

            int nOpeningBrackets = 0;
            int nClosingBrackets = 0;

            int start = 0;

            for (int i = 0; i < components.Count; i++)
            {
                Component c = components[i];

                if (c.GetType() == typeof(BracketComponent))
                {
                    BracketComponent? bracketComponent = c as BracketComponent;
                    if (bracketComponent != null)
                    {
                        if (bracketComponent.type == BracketType.OPENING)
                        {
                            if (nOpeningBrackets == 0)
                            {
                                start = i;
                            }
                            nOpeningBrackets++;
                        }
                        else
                        {
                            nClosingBrackets++;
                        }
                    }
                }

                if (nOpeningBrackets != 0 && nOpeningBrackets == nClosingBrackets)
                {
                    List<Component> subExpression = components.GetRange(start + 1, i - start - 1);
                    printComponents(subExpression);

                    DecimalComponent result = EvaluateParsed(subExpression);
                    components.RemoveRange(start, i - start);
                    components[start] = result;

                    i -= i - start;
                    printComponents(components);
                    Console.WriteLine();
                    nOpeningBrackets = 0;
                    nClosingBrackets = 0;
                }
            }

            return EvaluateSimpleExpression(components);
        }

        public void printComponents(List<Component> components)
        {
            foreach (Component c in components)
            {
                if (c.GetType() == typeof(DecimalComponent))
                {
                    DecimalComponent? decimalComponent = c as DecimalComponent;
                    if (decimalComponent != null)
                    {
                        Console.Write(decimalComponent.n);
                    }
                }
                else if (c.GetType() == typeof(OperatorComponent))
                {
                    OperatorComponent? operatorComponent = c as OperatorComponent;
                    if (operatorComponent != null)
                    {
                        Console.Write(operatorComponent.operatorString);

                        if (operatorComponent.operatorString == "")
                        {
                            Console.Write("....");
                        }
                    }
                }

                else if (c.GetType() == typeof(BracketComponent))
                {
                    BracketComponent? operatorComponent = c as BracketComponent;
                    if (operatorComponent != null)
                    {
                        Console.Write(operatorComponent.toString());
                    }
                }

                else if (c.GetType() == typeof(SpecialFunctionComponent))
                {
                    SpecialFunctionComponent? spc = c as SpecialFunctionComponent;
                    if (spc != null)
                    {
                        Console.Write(spc.specialFunction);
                    }
                }

                Console.Write(", ");
            }
            Console.WriteLine();
        }

        public decimal Evaluate(string expression)
        {
            
            expression = expression.Replace(" ", "");

            Console.WriteLine(expression);

            List<char> operatorList = new List<char>(1);
            char[] operators = { '+' };

            List<Component> components = new List<Component>(expression.Length);

            components = ParseComponentTypes(expression);

            printComponents(components);

            Console.WriteLine();

            DecimalComponent result = EvaluateParsed(components);

            Console.WriteLine("Result: ");
            Console.WriteLine(result.n);


            return result.n;
        }

        public void AppendHistory(decimal dec)
        {
            history.Add(dec);
        }
    }
}
