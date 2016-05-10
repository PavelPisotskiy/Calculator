using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.Model
{
    public class Calculator : ICalculator
    {
        string numbersPattern = @"[-]?((∞)|(((\d+,\d+)|(\d+))(E[+-]\d+)?))";

        private readonly string patternMultiplyDivide = @"(?<firstNumber>[-]?((∞)|(((\d+,\d+)|(\d+))(E[+-]\d+)?)))(?<symbol>[*/])(?<secondNumber>[-]?((∞)|(((\d+,\d+)|(\d+))(E[+-]\d+)?)))";
        private readonly string patternPlusMinus = @"(?<firstNumber>(?<![*/\d-])([-]?((∞)|(((\d+,\d+)|(\d+))(E[+-]\d+)?))))(?<symbol>[-+])(?<secondNumber>[-]?((∞)|(((\d+,\d+)|(\d+))(E[+-]\d+)?))(?![/*\d]))";

        private readonly string parenthesesPattern = @"(?<numberInParentheses>\((?<number>[-]?((∞)|(((\d+,\d+)|(\d+))(E[+-]\d+)?)))\))";

        private readonly string rulesOfOperationsPattern = @"(?<operations>[+-]{2})";

        public string Calculate(string expression)
        {
            expression = expression.Replace('.', ',');
            expression = expression.Replace(" ", "");

            Regex parenthesesRegex = new Regex(parenthesesPattern);

            double result;
            while (!double.TryParse(expression, out result))
            {
                string start = expression;
                //Debug.WriteLine(expression);

                while (Regex.IsMatch(expression, rulesOfOperationsPattern))
                {
                    expression = Regex.Replace(expression, rulesOfOperationsPattern, (m) =>
                    {
                        string operationResult = string.Empty;
                        switch (m.Groups["operations"].Value)
                        {
                            case "--": operationResult = "+"; break;
                            case "-+": operationResult = "-"; break;
                            case "+-": operationResult = "-"; break;
                            case "++": operationResult = "+"; break;
                        }

                        return operationResult;
                    });
                }


                expression = expression.Replace("/+", "/");
                expression = expression.Replace("*+", "*");

                // Debug.WriteLine(expression);

                expression = CalculateBaseMathExpression(expression, patternMultiplyDivide);
                // Debug.WriteLine(expression);
                expression = CalculateBaseMathExpression(expression, patternPlusMinus);
                // Debug.WriteLine(expression);
                expression = parenthesesRegex.Replace(expression, m => m.Groups["number"].Value);
                // Debug.WriteLine(expression);
                if (start.Equals(expression))
                    throw new FormatException();//"При вводе была допущена ошибка"//(1,8446744073709552E+19)*(1,8446744073709552E+19)
            }
            return result.ToString("R");
        }

        public async Task<string> CalculateAsync(string expression)
        {
            return await Task<string>.Factory.StartNew(() => Calculate(expression));
        }

        private string CalculateBaseMathExpression(string expression, string pattern)
        {
            Regex regex = new Regex(pattern);

            while (regex.IsMatch(expression))
            {
                Match match = regex.Match(expression);
                double firstNumber = double.Parse(match.Groups["firstNumber"].Value);
                double secondNumber = double.Parse(match.Groups["secondNumber"].Value);

                switch (match.Groups["symbol"].Value)
                {
                    case "-":
                        {
                            expression = expression.Remove(match.Index, match.Length);
                            expression = expression.Insert(match.Index, (firstNumber - secondNumber).ToString("R"));
                        }
                        break;
                    case "+":
                        {
                            expression = expression.Remove(match.Index, match.Length);
                            expression = expression.Insert(match.Index, (firstNumber + secondNumber).ToString("R"));
                        }
                        break;
                    case "*":
                        {
                            expression = expression.Remove(match.Index, match.Length);
                            if (firstNumber < 0 && secondNumber < 0) // -x * -y = +xy;
                                expression = expression.Insert(match.Index, "+" + (firstNumber * secondNumber).ToString("R"));
                            else
                                expression = expression.Insert(match.Index, (firstNumber * secondNumber).ToString("R"));
                        }
                        break;
                    case "/":
                        {
                            if (secondNumber == 0d)
                                throw new DivideByZeroException();

                            expression = expression.Remove(match.Index, match.Length);
                            if (firstNumber < 0 && secondNumber < 0)// -x / -y = +x/y;
                                expression = expression.Insert(match.Index, "+" + (firstNumber / secondNumber).ToString("R"));
                            else
                                expression = expression.Insert(match.Index, (firstNumber / secondNumber).ToString("R"));
                        }
                        break;
                }
            }

            return expression;
        }
    }
}
