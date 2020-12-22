using System;
using System.Globalization;
using System.Windows.Data;
using Questify.Builder.Logic;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class NoValueStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (NoValueType<string>)value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NoValueType<string>(value.ToString());
        }
    }
}
