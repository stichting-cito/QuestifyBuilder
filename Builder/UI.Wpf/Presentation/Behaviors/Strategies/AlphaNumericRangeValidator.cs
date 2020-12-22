using System.Globalization;
using System.Text.RegularExpressions;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies
{
    public class AlphaNumericRangeValidator : IInputValidationStrategy
    {
        public bool IsInputAllowed(string input)
        {
            if (!NumericIdentifiers)
            {
                var regex = GetAlphaRegEx();
                return regex.IsMatch(input);
            }
            else
            {
                var regex = new Regex("^[0-9]+$");
                return regex.IsMatch(input);
            }
        }

        private Regex GetAlphaRegEx()
        {
            string letter = "Z";
            if (AlternativesCount > 0 && AlternativesCount <= 26)
            {
                letter = ((char)(64 + AlternativesCount)).ToString();
            }
            return new Regex($"^[A-{letter}a-{letter.ToLower(CultureInfo.InvariantCulture)}]$");
        }

        public bool IsInputValid(string input)
        {
            if (!NumericIdentifiers)
            {
                var regex = GetAlphaRegEx();
                return regex.IsMatch(input);
            }

            int value;
            var valid = int.TryParse(input, out value);
            if (valid)
            {
                valid = value > 0 && value <= AlternativesCount;
            }
            return valid;
        }

        public int AlternativesCount { get; set; }

        public bool NumericIdentifiers { get; set; }
    }
}
