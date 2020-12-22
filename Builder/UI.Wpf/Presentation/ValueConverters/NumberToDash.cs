using System;
using System.Globalization;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    [ValueConversion(typeof(int), typeof(string))]
    class NumberToDash : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int depth = (int)value;
            return new string('-', depth);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            return strValue.Length;
        }
    }
}
