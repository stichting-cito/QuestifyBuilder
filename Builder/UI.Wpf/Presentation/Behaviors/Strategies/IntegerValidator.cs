using System.Text;
using System.Text.RegularExpressions;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies
{
    internal class IntegerValidator : IInputValidationStrategy
    {
        public bool IsInputAllowed(string input)
        {
            string pattern = GetRegExPatternForInteger();
            Regex regularExpression = new Regex(pattern);
            return regularExpression.IsMatch(input);
        }

        public bool IsInputValid(string input)
        {
            string pattern = GetRegExPatternForInteger();
            Regex regularExpression = new Regex(pattern);
            return regularExpression.IsMatch(input);
        }

        private string GetRegExPatternForInteger()
        {
            if (MaxLength < 1) MaxLength = 1;
            var sb = new StringBuilder();
            if (!PositiveValueOnly)
            {
                sb.Append(@"^(0|-?(?!0)(\d{0,");
            }
            else
            {
                sb.Append(@"^((\d{0,");
            }
            sb.Append(MaxLength.ToString());
            sb.Append(@"}))$");

            return sb.ToString();
        }

        public int MaxLength { get; set; }

        public bool PositiveValueOnly { get; set; }
    }
}
