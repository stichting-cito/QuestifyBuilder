using System;
using System.Globalization;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class IntegerToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueToConvert = (int)value;

            int trueValue;
            if (int.TryParse(parameter.ToString(), NumberStyles.Integer, culture, out trueValue))
            {
                if (valueToConvert == trueValue)
                {
                    return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
}
