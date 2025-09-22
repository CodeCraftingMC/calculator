namespace Calculator
{
    public class Parser
    {

        private readonly HashSet<char> _numberCharSet = [];
        private readonly HashSet<char> _operatorCharSet = [];
        private readonly HashSet<char> _bracketCharSet = [];

        private readonly string _numbers = "1234567890."; // number charset
        private readonly string _operators = "+-/*^"; // operator charset
        private readonly string _brackets = "()"; // bracket charset

        private readonly string _operators0 = "^"; // E
        private readonly string _operators1 = "/*"; // MD
        private readonly string _operators2 = "+-"; // AS

        private string[] _operatorHierarchy = []; // operator hierarchy

        private readonly List<double> _history = [];

        public Dictionary<string, Func<double[], double>> SpecialFunctionMap { get; }
        public Dictionary<string, double> ConstantsMap { get; }
        public Dictionary<string, int> ArgumentCountMap { get; }

        public Dictionary<string, List<string>> SpecialFuncGroups { get; } = [];

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
            SpecialFunctionMap = new()
            {
                {"fact", SpecialFunctions.Factorial},
                {"abs", SpecialFunctions.Abs},
                {"sqrt", SpecialFunctions.Sqrt},
                {"root", SpecialFunctions.Root},
                { "ans", (dec) => SpecialFunctions.Ans(dec[0], _history) },

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

            ConstantsMap = new()
            {
                {"pi", MathConstants.PI},
                {"e", MathConstants.E},
                {"tau", MathConstants.PI * 2},
                {"gr", MathConstants.GoldenRatio},
                {"inf", double.PositiveInfinity},
            };

            ArgumentCountMap = new()
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

            SpecialFuncGroups = new()
            {
                {
                    "Exp",
                    new()
                    {
                        "sqrt",
                        "root",
                        "log2",
                        "log10",
                        "ln",
                        "log",
                        "exp",
                    }
                },

                {
                    "Trig",
                    new()
                    {
                        "sin", "cos", "tan", "cot", "sec", "csc",
                        "arcsin", "arccos", "arctan", "arccot", "arcsec", "arccsc",
                        "sinh", "cosh", "tanh", "coth", "sech", "csch",
                        "arcsinh", "arccosh", "arctanh", "arccoth", "arcsech", "arccsch"
                    }
                },

                {
                    "Other",
                    new()
                    {
                        "abs",
                        "mod",
                        "fact",
                    }
                },


            };
        }


        private ComponentType GetCharType(char c)
        {
            /*returns which charset contains the char c*/
            if (_numberCharSet.Contains(c))
            {
                return ComponentType.NUMBER;
            }
            else if (_operatorCharSet.Contains(c))
            {
                return ComponentType.OPERATOR;
            }
            else if (_bracketCharSet.Contains(c))
            {
                return ComponentType.BRACKET;
            }
            return ComponentType.SPECIALFUNC;
        }


        public static void ParseNegatives(ref List<Component> components, bool verbose)
        {

            bool ppcIsInt = false;
            for (int i = 1; i < components.Count; i++)
            {
                Component c = components[i];
                Component pc = components[i - 1];

                if (pc.GetType() == typeof(OperatorComponent))
                {
                    OperatorComponent? op = pc as OperatorComponent ?? throw new NotImplementedException();
                    if (op.OperatorString.EndsWith('-') && (op.OperatorString.Length > 1 || !ppcIsInt))
                    {
                        if (c.GetType() == typeof(DecimalComponent))
                        {
                            if (c is not DecimalComponent dec)
                            {
                                throw new NotImplementedException();
                            }
                            dec.N = -dec.N;
                            components[i] = dec;

                            if (op.OperatorString.Length > 1)
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

                            if (op.OperatorString.Length > 1)
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


                            PrintComponents(components, verbose);
                        }
                    }
                }
                ppcIsInt = pc.GetType() == typeof(DecimalComponent);
            }
        }
        public (Component, int, ComponentType) ParseNumber(int start, string expression)
        {
            int end = start;
            while (end < expression.Length && _numberCharSet.Contains(expression[end]))
            {
                end++;
            }
            double n = double.Parse(expression[start..end]);

            if (end == expression.Length)
            {
                return (new DecimalComponent(n), end, ComponentType.NONE);
            }

            ComponentType nextType = ComponentType.OPERATOR;

            if (_bracketCharSet.Contains(expression[end]))
            {
                nextType = ComponentType.BRACKET;
            }

            return (new DecimalComponent(n), end, nextType);
        }

        public (Component, int, ComponentType) ParseOperator(int start, string expression)
        {
            int end = start;
            while (_operatorCharSet.Contains(expression[end]) && end < expression.Length)
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
            if (_numberCharSet.Contains(nextChar))
            {
                nextType = ComponentType.NUMBER;
            }
            else if (_bracketCharSet.Contains(nextChar))
            {
                nextType = ComponentType.BRACKET;
            }
            else
            {
                nextType = ComponentType.SPECIALFUNC;
            }

            return (new OperatorComponent(op), end, nextType);
        }
        public (Component, int, ComponentType) ParseBracket(int start, string expression)
        {
            int end = start + 1;

            string bracket = expression[start..end];

            if (end == expression.Length)
            {
                return (new BracketComponent(bracket), end, ComponentType.NONE);
            }

            char nextChar = expression[end];

            ComponentType nextType = ComponentType.SPECIALFUNC;
            if (_numberCharSet.Contains(nextChar))
            {
                nextType = ComponentType.NUMBER;
            }
            else if (_bracketCharSet.Contains(nextChar))
            {
                nextType = ComponentType.BRACKET;
            }
            else if (_operatorCharSet.Contains(nextChar))
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

            List<string> stringArgs = [];

            for (int i = argstart; i < expression.Length; i++)
            {
                char c = expression[i];

                if (_bracketCharSet.Contains(c))
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

            List<double> args = [];

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

            DecimalComponent dc = new(ConstantsMap[constant]);

            if (end == expression.Length)
            {
                return (dc, end, ComponentType.NONE);
            }

            ComponentType nextType = ComponentType.OPERATOR;
            if (_bracketCharSet.Contains(expression[end]))
            {
                nextType = ComponentType.BRACKET;
            }

            return (dc, end, nextType);
        }


        public (Component, int, ComponentType) ParseSpecialFunc(int start, string expression)
        {
            int end = start;
            while (true)
            {
                if (end == expression.Length || _operatorCharSet.Contains(expression[end]) || expression[end] == ')')
                {
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

            SpecialFunctionComponent sfc = new(sf, args);

            if (end == expression.Length)
            {
                return (sfc, end, ComponentType.NONE);
            }

            ComponentType nextType = ComponentType.OPERATOR;
            if (_bracketCharSet.Contains(expression[end]))
            {
                nextType = ComponentType.BRACKET;
            }
            return (sfc, end, nextType);
        }

        public (Component component, int end, ComponentType nextType) ParseComponent(int i, string expression, ComponentType type)
        {
            Component component;
            int end;
            ComponentType nextType;
            if (type == ComponentType.NUMBER)
            {
                (component, end, nextType) = ParseNumber(i, expression);
            }
            else if (type == ComponentType.OPERATOR)
            {
                (component, end, nextType) = ParseOperator(i, expression);
            }
            else if (type == ComponentType.BRACKET)
            {
                (component, end, nextType) = ParseBracket(i, expression);
            }
            else if (type == ComponentType.SPECIALFUNC)
            {
                (component, end, nextType) = ParseSpecialFunc(i, expression);
            }
            else
            {
                throw new NotImplementedException();
            }

            return (component, end, nextType);

        }

        public List<Component> ParseComponentTypes(string expression, bool verbose)
        {
            List<Component> components = [];

            // initialize charsets and hierarchy

            foreach (char c in _numbers)
            {
                _numberCharSet.Add(c);
            }
            foreach (char c in _operators)
            {
                _operatorCharSet.Add(c);
            }
            foreach (char c in _brackets)
            {
                _bracketCharSet.Add(c);
            }

            _operatorHierarchy = [_operators0, _operators1, _operators2];



            // parse
            ComponentType type = GetCharType(expression[0]);

            int i = 0;
            while (i < expression.Length)
            {
                (Component component, int end, ComponentType nextType) = ParseComponent(i, expression, type);

                components.Add(component);

                if (nextType == ComponentType.NONE)
                {
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

        private static int FindOperator(string ops, List<Component> components, bool rev = false)
        {
            for (int i = 0; i < components.Count; i++)
            {
                int j = rev ? components.Count - i - 1 : i;
                Component c = components[j];

                if (c.GetType() == typeof(OperatorComponent))
                {
                    if (c != null && c is OperatorComponent operatorComponent)
                    {
                        if (ops.Contains(operatorComponent.OperatorString))
                        {
                            return j;
                        }
                    }
                }
            }
            return -1;
        }

        private static DecimalComponent EvaluateOperator(DecimalComponent left, DecimalComponent right, OperatorComponent op)
        {
            double result;
            double leftDecimal = left.N;
            double rightDecimal = right.N;

            if (op.OperatorString == "+")
            {
                result = leftDecimal + rightDecimal;
            }
            else if (op.OperatorString == "-")
            {
                result = leftDecimal - rightDecimal;
            }
            else if (op.OperatorString == "*")
            {
                result = leftDecimal * rightDecimal;
            }
            else if (op.OperatorString == "/")
            {
                result = leftDecimal / rightDecimal;
            }
            else if (op.OperatorString == "^")
            {
                result = Math.Pow((double)leftDecimal, (double)rightDecimal);
            }
            else
            {
                throw new NotImplementedException();
            }

            return new DecimalComponent(result);
        }

        private static void EvaluateOperatorAt(int i, ref List<Component> components)
        {
            Component c = components[i];

            Component left = components[i - 1];
            Component right = components[i + 1];

            if (left.GetType() != typeof(DecimalComponent) || right.GetType() != typeof(DecimalComponent))
            {
                throw new NotImplementedException();
            }

            if (left is not DecimalComponent decimalLeft
                || right is not DecimalComponent decimalRight
                || c is not OperatorComponent op)
            {
                throw new NotImplementedException();
            }


            DecimalComponent result = EvaluateOperator(decimalLeft, decimalRight, op);

            components.RemoveRange(i - 1, 2);
            components[i - 1] = result;
        }

        public static DecimalComponent GetResult(List<Component> components)
        {
            if (components.Count != 1)
            {
                throw new NotImplementedException();
            }

            if (components[0].GetType() != typeof(DecimalComponent))
            {
                throw new NotImplementedException();
            }


            if (components[0] is not DecimalComponent evaluated)
            {
                throw new NotImplementedException();
            }


            return evaluated;
        }

        public DecimalComponent EvaluateSpecialFunc(SpecialFunctionComponent spc)
        {
            Func<double[], double> spf = SpecialFunctionMap[spc.SpecialFunction];
            return spc.Args.Count != ArgumentCountMap[spc.SpecialFunction]
                ? throw new ArgumentException("invalid arguments", nameof(spc))
                : new DecimalComponent(spf([.. spc.Args]));
        }

        public void EvaluateSpecialFuncs(ref List<Component> components)
        {
            for (int i = 0; i < components.Count; i++)
            {

                Component c = components[i];
                if (c.GetType() == typeof(SpecialFunctionComponent))
                {
                    if (c is SpecialFunctionComponent spc)
                    {
                        DecimalComponent result = EvaluateSpecialFunc(spc);
                        components[i] = result;
                    }

                }
            }

            PrintComponents(components, true);
        }

        public DecimalComponent EvaluateSimpleExpression(List<Component> components, bool verbose)
        {
            if (verbose)
            {
                Console.Write("simpleexpr ");
                PrintComponents(components, verbose);
                EvaluateSpecialFuncs(ref components);
                PrintComponents(components, verbose);
            }

            while (true)
            {
                int j = FindOperator("^", components, true);

                if (j != -1)
                {
                    EvaluateOperatorAt(j, ref components);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < _operatorHierarchy.Length; i++)
            {
                string set = _operatorHierarchy[i];

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
                    if (c is BracketComponent bracketComponent)
                    {
                        if (bracketComponent.Type == BracketType.OPENING)
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
                    PrintComponents(subExpression, verbose);

                    DecimalComponent result = EvaluateParsed(subExpression, verbose);
                    components.RemoveRange(start, i - start);
                    components[start] = result;

                    i -= i - start;
                    if (verbose)
                    {
                        Console.Write("res ");
                        PrintComponents(components, verbose);
                        Console.WriteLine();
                    }
                    nOpeningBrackets = 0;
                    nClosingBrackets = 0;
                }
            }

            return EvaluateSimpleExpression(components, verbose);
        }

        public static void PrintComponents(List<Component> components, bool verbose)
        {
            if (!verbose) { return; }

            foreach (Component c in components)
            {
                if (c.GetType() == typeof(DecimalComponent))
                {
                    if (c is DecimalComponent decimalComponent)
                    {
                        Console.Write(decimalComponent.N);
                    }
                }
                else if (c.GetType() == typeof(OperatorComponent))
                {
                    if (c is OperatorComponent operatorComponent)
                    {
                        Console.Write(operatorComponent.OperatorString);

                        if (operatorComponent.OperatorString == "")
                        {
                            Console.Write("....");
                        }
                    }
                }
                else if (c.GetType() == typeof(BracketComponent))
                {
                    if (c is BracketComponent operatorComponent)
                    {
                        Console.Write(operatorComponent.ToString());
                    }
                }
                else if (c.GetType() == typeof(SpecialFunctionComponent))
                {
                    if (c is SpecialFunctionComponent spc)
                    {
                        Console.Write(spc.SpecialFunction);
                        Console.Write("[");
                        foreach (double arg in spc.Args)
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

            List<char> operatorList = new(1);
            char[] operators = ['+'];

            List<Component> components = new(expression.Length);

            components = ParseComponentTypes(expression, verbose);

            PrintComponents(components, verbose);

            if (verbose) { Console.WriteLine(); }

            DecimalComponent result = EvaluateParsed(components, verbose);

            if (verbose)
            {
                Console.WriteLine("Result: ");
                Console.WriteLine(result.N);
            }


            return result.N;
        }

        public void AppendHistory(double dec)
        {
            _history.Add(dec);
        }

        public void ClearHistory()
        {
            _history.Clear();
        }
    }
}
