using System.Text.RegularExpressions;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies
{
    internal class IntegerIntervalValidator : IInputValidationStrategy
    {
        public bool IsInputAllowed(string input)
        {
            Regex regularExpression = new Regex(@"^\[?-?\d*[;]?-?\d*\]?$");
            return regularExpression.IsMatch(input);
        }

        public bool IsInputValid(string input)
        {
            var reInterval = new Regex(@"^\[{1}(.+)[;]{1}(.+)\]{1}$");

            if (reInterval.IsMatch(input))
            {
                var parts = reInterval.Split(input);
                var left = parts[1];
                var right = parts[2];
                var integerValidator = new IntegerValidator() { MaxLength = MaxLength };
                return integerValidator.IsInputValid(left) && integerValidator.IsInputValid(right);
            }
            return false;
        }

        public int MaxLength { get; set; }
    }
}
