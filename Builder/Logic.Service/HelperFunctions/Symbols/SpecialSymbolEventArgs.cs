namespace Questify.Builder.Logic.Service.HelperFunctions.Symbols
{
    public class SpecialSymbolEventArgs : System.EventArgs
    {
        public SpecialSymbolEventArgs(string hmtlUnicodeDecimalCode, string unicodeValue)
        {
            HtmlUnicodeDecimalCode = hmtlUnicodeDecimalCode;
            UnicodeValue = unicodeValue;
        }

        public string HtmlUnicodeDecimalCode { get; }

        public string UnicodeValue { get; }
    }

}