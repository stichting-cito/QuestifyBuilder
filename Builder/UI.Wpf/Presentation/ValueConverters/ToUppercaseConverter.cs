using System;
using System.Globalization;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class ToUppercaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToUpper(CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
