using System.Globalization;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.Helpers
{
    public class CultureAwareBinding : Binding
    {
        public CultureAwareBinding() : base()
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }

        public CultureAwareBinding(string path)
            : base(path)
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }
    }
}
