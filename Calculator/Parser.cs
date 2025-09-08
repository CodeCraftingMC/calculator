using Microsoft.VisualBasic.FileIO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Parser{

        HashSet<char> numberCharSet = new HashSet<char>();
        HashSet<char> operatorCharSet = new HashSet<char>();
        HashSet<char> bracketCharSet = new HashSet<char>();

        string numbers = "1234567890."; // number charset
        string operators = "+-/*^"; // operator charset
        string brackets = "()"; // bracket charset

        string operators0 = "^"; // E
        string operators1 = "/*"; // MD
        string operators2 = "+-"; // AS

        string[] operatorHierarchy = {}; // operator hierarchy

        List<double> history = new();
        public Dictionary<string, Func<double[], double>> specialFunctionMap;
        public Dictionary<string, double> constantsMap;
        public Dictionary<string, int> argumentCountMap;

        public enum ComponentType // component types
        {
            NONE,
            NUMBER,
            OPERATOR,
            BRACKET,
            SPECIALFUNC
        }

        public Parser()
        {
            specialFunctionMap = new()
            {
                {"fact", SpecialFunctions.Factorial},
                {"abs", SpecialFunctions.Abs},
                {"sqrt", SpecialFunctions.Sqrt},
                {"root", SpecialFunctions.Root},
                { "ans", (dec) => SpecialFunctions.Ans(dec[0], history) },

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
                { "cosh", SpecialFunctions.Cosh },
                { "tanh", SpecialFunctions.Tanh },
                { "coth", SpecialFunctions.Coth },
                { "sech", SpecialFunctions.Sech },
                { "csch", SpecialFunctions.Csch },

                { "arcsinh", SpecialFunctions.Arcsinh },
                { "arccosh", SpecialFunctions.Arccosh },
                { "arctanh", SpecialFunctions.Arctanh },
                { "arccoth", SpecialFunctions.Arccoth },
                { "arcsech", SpecialFunctions.Arcsech },
                { "arccsch", SpecialFunctions.Arccsch },


                { "log2", SpecialFunctions.Log2},
                { "log10", SpecialFunctions.Log10},
                { "log", SpecialFunctions.Log},
                { "ln", SpecialFunctions.Ln},
                { "exp", SpecialFunctions.Exp},

                { "mod", SpecialFunctions.Mod},

            };

            constantsMap = new()
            {
                {"pi", Math.PI},
                {"e", Math.E},
                {"tau", Math.PI * 2},
                {"inf", double.PositiveInfinity},
            };

            argumentCountMap = new()
            {
                {"fact", 1},
                {"abs", 1},
                {"sqrt", 1},
                {"root", 2},
                { "ans", 1},

                { "sin", 1},
                { "cos", 1},
                { "tan", 1},
                { "cot", 1},
                { "sec", 1},
                { "csc", 1},

                { "arcsin", 1},
                { "arccos", 1},
                { "arctan", 1},
                { "arccot", 1},
                { "arcsec", 1},
                { "arccsc", 1},

                { "sinh", 1},
                { "cosh", 1},
                { "tanh", 1},
                { "coth", 1},
                { "sech", 1},
                { "csch", 1},

                { "arcsinh", 1},
                { "arccosh", 1},
                { "arctanh", 1},
                { "arccoth", 1},
                { "arcsech", 1},
                { "arccsch", 1},


                { "log2", 1},
                { "log10", 1},
                { "log", 2},
                { "ln", 1},
                { "exp", 1},

                { "mod", 2}
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
            else if (bracketCharSet.Contains(c))
            {
                return ComponentType.BRACKET;
            }
            return ComponentType.SPECIALFUNC;
        }


        public void ParseNegatives(ref List<Component> components, bool verbose)
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
                                i -= 1;
                            }
                            components.Insert(i, new OperatorComponent("*"));
                            components.Insert(i, new DecimalComponent(Double.Parse("-1")));
               

                            printComponents(components, verbose);
                        }
                    }
                }
                ppcIsInt = pc.GetType() == typeof(DecimalComponent);
            }
        }
        public (Component, int, ComponentType) parseNumber(int start, string expression)
        {
            int end = start;
            while (end < expression.Length && numberCharSet.Contains(expression[end]))
            {
                end++;
            }
            double n = Double.Parse(expression[start..end]);

            if (end == expression.Length)
            {
                return (new DecimalComponent(n), end, ComponentType.NONE);
            }

            ComponentType nextType = ComponentType.OPERATOR;

            if (bracketCharSet.Contains(expression[end])){
                nextType = ComponentType.BRACKET;
            }

            return (new DecimalComponent(n), end, nextType);
        }

        public (Component, int, ComponentType) parseOperator(int start, string expression)
        {
            int end = start;
            while (operatorCharSet.Contains(expression[end]) && end < expression.Length)
            {
                end++;
            }
            string op = expression[start..end];

            if (end == expression.Length)
            {
                return (new OperatorComponent(op), end, ComponentType.NONE);
            }

            char nextChar = expression[end];

            ComponentType nextType;
            if (numberCharSet.Contains(nextChar)) { 
                nextType = ComponentType.NUMBER;
            }
            else if (bracketCharSet.Contains(nextChar))
            {
                nextType = ComponentType.BRACKET;
            }
            else
            {
                nextType = ComponentType.SPECIALFUNC;
            }

            return (new OperatorComponent(op), end, nextType);
        }
        public (Component, int, ComponentType) parseBracket(int start, string expression)
        {
            int end = start + 1;

            string bracket = expression[start..end];

            if (end == expression.Length)
            {
                return (new BracketComponent(bracket), end, ComponentType.NONE);
            }

            char nextChar = expression[end];

            ComponentType nextType = ComponentType.SPECIALFUNC;
            if (numberCharSet.Contains(nextChar))
            {
                nextType = ComponentType.NUMBER;
            }
            else if (bracketCharSet.Contains(nextChar))
            {
                nextType = ComponentType.BRACKET;
            }
            else if (operatorCharSet.Contains(nextChar))
            {
                nextType = ComponentType.OPERATOR;
            }

            return (new BracketComponent(bracket), end, nextType);
        }

        public (List<double>, int end) GetArgs(string expression, int start)
        {
            int nOpeningBrackets = 0;
            int nClosingBrackets = 0;

            int argstart = start + 1;
            int argend = 0;

            List<string> stringArgs = new List<string>();

            for (int i = argstart; i < expression.Length; i++)
            {
                char c = expression[i];

                if (bracketCharSet.Contains(c))
                {
                    if (c == '(')
                    {
                        nOpeningBrackets++;
                    }
                    else if (c == ')')
                    {
                        nClosingBrackets++;
                    }
                    if (nClosingBrackets > nOpeningBrackets)
                    {
                        break;
                    }
                }

                if (nOpeningBrackets == nClosingBrackets)
                {
                    argend = i;
                    if (c == ',')
                    {
                        stringArgs.Add(expression[argstart..argend]);
                        argstart = argend + 1;
                    }
                }
            }
            int end = argend + 1;

            stringArgs.Add(expression[argstart..end]);

            List<double> args = new List<double>();

            for (int i = 0; i < stringArgs.Count; i++)
            {
                string stringArg = stringArgs[i];
                double arg = Evaluate(stringArg);
                args.Add(arg);
            }

            return (args, end + 1);
        }

        public (Component, int, ComponentType) ParseConstant(int start, int end, string expression)
        {
            Console.WriteLine(expression[start..end]);

            string constant = expression[start..end];

            DecimalComponent dc = new DecimalComponent(constantsMap[constant]);

            if (end == expression.Length)
            {
                return (dc, end, ComponentType.NONE);
            }

            ComponentType nextType = ComponentType.OPERATOR;
            if (bracketCharSet.Contains(expression[end]))
            {
                nextType = ComponentType.BRACKET;
            }

            return (dc, end, nextType);
        }


        public (Component, int, ComponentType) parseSpecialFunc(int start, string expression)
        {
            int end = start;
            Console.WriteLine(start);
            while (true)
            {
                if (end == expression.Length || operatorCharSet.Contains(expression[end]) || expression[end] == ')') { 
                    return ParseConstant(start, end, expression);
                }
                else if (expression[end] == '(')
                {
                    break;
                }
                end++;

            }
            string sf = expression[start..end];

            (List<double> args, end) = GetArgs(expression, end);

            SpecialFunctionComponent sfc = new SpecialFunctionComponent(sf, args);

            if (end == expression.Length)
            {
                return (sfc, end, ComponentType.NONE);
            }
            char nextChar = expression[end];

            ComponentType nextType = ComponentType.OPERATOR;
            if (bracketCharSet.Contains(expression[end]))
            {
                nextType = ComponentType.BRACKET;
            }
            Console.Write(expression[end]);
            return (sfc, end, nextType);
        }

        public (Component component, int end, ComponentType nextType) parseComponent(int i, string expression, ComponentType type)
        {
            Component component;
            int end;
            ComponentType nextType;
            if (type == ComponentType.NUMBER)
            {
                (component, end, nextType) = parseNumber(i, expression);
            }
            else if (type == ComponentType.OPERATOR)
            {
                (component, end, nextType) = parseOperator(i, expression);
            }
            else if (type == ComponentType.BRACKET)
            {
                (component, end, nextType) = parseBracket(i, expression);
            }
            else if (type == ComponentType.SPECIALFUNC)
            {
                (component, end, nextType) = parseSpecialFunc(i, expression);
            }
            else
            {
                throw new NotImplementedException();
            }

            return (component, end, nextType);

        }

        public List<Component> ParseComponentTypes(string expression, bool verbose)
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
                bracketCharSet.Add(c);
            }

            operatorHierarchy = new string[] { operators0, operators1, operators2 };



            // parse
            ComponentType type = GetCharType(expression[0]);

            int i = 0;
            ComponentType nextType = ComponentType.NUMBER;
            while (i < expression.Length) {
                (Component component, int end, nextType) = parseComponent(i, expression, type);

                components.Add(component);

                if (nextType == ComponentType.NONE) {
                    break;
                }

                i = end;
                type = nextType;

      
            }
            /*                  
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
            */

            ParseNegatives(ref components, verbose);
            return components;
        }

        private int FindOperator(string ops, List<Component> components, bool rev = false)
        {


            for (int i = 0; i < components.Count; i++)
            {
                int j = rev ? components.Count - i - 1 : i;
                Component c = components[j];

                if (c.GetType() == typeof(OperatorComponent))
                {
                    OperatorComponent? operatorComponent = c as OperatorComponent;
                    if (c != null && operatorComponent != null)
                    {
                        if (ops.Contains(operatorComponent.operatorString)){
                            return j;
                        }
                    }
                }
            }
            return -1;
        }

        private DecimalComponent EvaluateOperator(DecimalComponent left, DecimalComponent right, OperatorComponent op)
        {
            double result;
            double leftDecimal = left.n;
            double rightDecimal = right.n;

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
                result = Math.Pow((double)leftDecimal, (double)rightDecimal);
            }
            else { 
                throw new NotImplementedException();
            }

            return new DecimalComponent(result);
        }

        private void EvaluateOperatorAt(int i, ref List<Component> components, bool verbose)
        {
            Component c = components[i];

            printComponents(components, verbose);
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

            printComponents(components, verbose);
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

        public DecimalComponent EvaluateSpecialFunc(SpecialFunctionComponent spc)
        {
            Func<double[], double> spf = specialFunctionMap[spc.specialFunction];
            if (spc.args.Count != argumentCountMap[spc.specialFunction]) throw new ArgumentException("invalid arguments", nameof(spc.args));
            return new DecimalComponent(spf(spc.args.ToArray()));
        }

        public void EvaluateSpecialFuncs(ref List<Component> components)
        {
            for (int i = 0; i < components.Count ; i++) { 
                
                Component c = components[i];
                if (c.GetType() == typeof(SpecialFunctionComponent))
                {
                    SpecialFunctionComponent? spc = c as SpecialFunctionComponent;
                    if (spc != null) {
                        DecimalComponent result = EvaluateSpecialFunc(spc);
                        components[i] = result;
                    }

                }
            }

            printComponents(components, true);
        }

        public DecimalComponent EvaluateSimpleExpression(List<Component> components, bool verbose)
        {
            if (verbose)
            {
                Console.Write("simpleexpr ");
                printComponents(components, verbose);
                EvaluateSpecialFuncs(ref components);
                printComponents(components, verbose);
            }

            while (true)
            {
                int j = FindOperator("^", components, true);

                if (j != -1)
                {
                    EvaluateOperatorAt(j, ref components, verbose);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < operatorHierarchy.Count(); i++)
            {
                string set = operatorHierarchy[i];

                while (true)
                {
                    int j = FindOperator(set, components);

                    if (j != -1)
                    {
                        EvaluateOperatorAt(j, ref components, verbose);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return GetResult(components);
        }

        public DecimalComponent EvaluateParsed(List<Component> components, bool verbose)
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
                    if (verbose) { Console.Write("eval "); }
                    printComponents(subExpression, verbose);

                    DecimalComponent result = EvaluateParsed(subExpression, verbose);
                    components.RemoveRange(start, i - start);
                    components[start] = result;

                    i -= i - start;
                    if (verbose) { 
                        Console.Write("res ");
                        printComponents(components, verbose);
                        Console.WriteLine();
                    }
                    nOpeningBrackets = 0;
                    nClosingBrackets = 0;
                }
            }

            return EvaluateSimpleExpression(components, verbose);
        }

        public void printComponents(List<Component> components, bool verbose)
        {
            if (!verbose) { return; }

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
                        Console.Write("[");
                        foreach (double arg in spc.args)
                        {
                            Console.Write(arg.ToString());
                            Console.Write(";");
                        }
                        Console.Write("]");
                    }
                }

                Console.Write(", ");
            }
            Console.WriteLine();
        }

        public double Evaluate(string expression, bool verbose = true)
        {
            
            expression = expression.Replace(" ", "");

            if (verbose) { Console.WriteLine("EVAL: " + expression); }

            List<char> operatorList = new List<char>(1);
            char[] operators = { '+' };

            List<Component> components = new List<Component>(expression.Length);

            components = ParseComponentTypes(expression, verbose);

            printComponents(components, verbose);

            if (verbose) { Console.WriteLine(); }

            DecimalComponent result = EvaluateParsed(components, verbose);

            if (verbose) { 
                Console.WriteLine("Result: ");
                Console.WriteLine(result.n);
            }


            return result.n;
        }

        public void AppendHistory(double dec)
        {
            history.Add(dec);
        }
    }
}
