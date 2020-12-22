namespace Questify.Builder.Logic.Service.HelperFunctions.Symbols
{
    public class SpecialSymbol
    {
        public SpecialSymbol(string htmlEntity, int unicodeValue, string symbolName)
        {
            HtmlEntity = htmlEntity;
            UnicodeValue = unicodeValue;
            SymbolName = symbolName;
        }

        public string SymbolName { get; set; }

        public string HtmlEntity { get; set; }

        public int UnicodeValue { get; set; }

        public string UnicodeString
        {
            get { return System.Text.Encoding.Unicode.GetString(System.BitConverter.GetBytes(UnicodeValue)); }
        }

        public string HtmlUnicodeDecimalCode
        {
            get { return $"&#{UnicodeValue:####};"; }
        }
    }
}
