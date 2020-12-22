using System.Text.RegularExpressions;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies
{
    internal class TimeValidator : IInputValidationStrategy
    {
        private string _timeFormat;

        public bool IsInputAllowed(string input)
        {
            string pattern = GetRegExPatternForTimeWithOptionalParts();
            Regex regularExpression = new Regex(pattern);
            return regularExpression.IsMatch(input);
        }

        public bool IsInputValid(string input)
        {
            string pattern = GetRegExPatternForTimeWithMandatoryParts();
            Regex regularExpression = new Regex(pattern);
            return regularExpression.IsMatch(input);
        }


        private string GetRegExPatternForTimeWithMandatoryParts()
        {

            if (string.IsNullOrEmpty(_timeFormat) || _timeFormat == "hh:mm")
            {
                return @"^(?:([01]\d|2[0-3]):)([0-5]\d)$";
            }
            else if (_timeFormat == "hh:mm:ss")
            {
                return @"^(?:(?:([01]\d|2[0-3]):)([0-5]\d):)([0-5]\d)$";
            }
            else if (_timeFormat == "mm:ss")
            {
                return @"^(?:([0-5]\d):)([0-5]\d)$";
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetRegExPatternForTimeWithOptionalParts()
        {
            if (_timeFormat == "hh:mm:ss")
            {
                return @"^(\d{0,2})(:{0,1})(\d{0,2})(:{0,1})(\d{0,2})$";
            }
            else
            {
                return @"^(\d{0,2})(:{0,1})(\d{0,2})$";
            }
        }


        public string TimeFormat
        {
            get { return _timeFormat; }
            set { _timeFormat = value; }
        }
    }
}
