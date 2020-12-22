using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    [ValueConversion(typeof(int), typeof(Visibility))]
    public class IntegerToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueToConvert = (int)value;

            int trueValue;
            if (int.TryParse(parameter.ToString(), NumberStyles.Integer, culture, out trueValue))
            {
                if (valueToConvert == trueValue)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
