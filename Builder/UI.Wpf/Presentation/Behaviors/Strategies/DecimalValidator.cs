using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies
{
    internal class DecimalValidator : IInputValidationStrategy
    {
        private string _decimalSeparator;
        public DecimalValidator()
        {
            _decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        public bool IsInputAllowed(string input)
        {
            string pattern = GetRegexPattern(false);
            Regex regularExpression = new Regex(pattern);
            return regularExpression.IsMatch(input);
        }

        public bool IsInputValid(string input)
        {

            string pattern = GetRegexPattern(true);
            Regex regularExpression = new Regex(pattern);
            return regularExpression.IsMatch(input);
        }

        private string GetRegexPattern(bool strictValidation)
        {
            int minDigits = strictValidation ? 1 : 0;
            var sb = new StringBuilder();
            sb.Append(@"^-?(\d{");
            sb.Append(minDigits);
            sb.Append(",");
            sb.Append(Math.Max(1, IntegerPartMaxLength));
            sb.Append("})");
            sb.Append("(\\");
            sb.Append(_decimalSeparator);
            sb.Append(@"\d{");
            sb.Append(minDigits);
            sb.Append(",");
            sb.Append(Math.Max(1, FractionPartMaxLength));
            sb.Append("})?$");
            return sb.ToString();
        }
        public int IntegerPartMaxLength { get; set; }

        public int FractionPartMaxLength { get; set; }
    }
}
