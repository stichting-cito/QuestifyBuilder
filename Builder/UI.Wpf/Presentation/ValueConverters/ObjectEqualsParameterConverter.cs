using System;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{

    [ValueConversion(typeof(object), typeof(bool))]
    public class ObjectEqualsParameterConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }

}
