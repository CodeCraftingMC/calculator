using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Parser
    {
        public static List<string> ParseNumbers(string expression)
        {
            List<string> parsedExpression = new List<string>();

            HashSet<char> numberCharSet = new HashSet<char>();

            string numbers = "1234567890.";

            foreach(char c in numbers)
            {
                numberCharSet.Add(c);
            }

            string current = "";
            char previousChar = 'g';

            for(int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (numberCharSet.Contains(c) != numberCharSet.Contains(previousChar))
                {
                    parsedExpression.Add(current);
                    current = "";
                }
                current += c;
                previousChar = c;
            }
            parsedExpression.Add(current);

            return parsedExpression;
        }
        public static decimal Parse(string expression)
        {
            
            expression = expression.Replace(" ", "");

            Console.WriteLine(expression);

            List<char> operatorList = new List<char>(1);
            char[] operators = { '+' };

            List<string> parts= new List<string>(expression.Length);

            parts = ParseNumbers(expression);

            foreach (string part in parts)
            {
                Console.WriteLine(part);
            }


            return 0;
        }
    }
}
