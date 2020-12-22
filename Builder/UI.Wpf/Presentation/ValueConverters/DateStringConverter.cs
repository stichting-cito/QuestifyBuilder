using System;
using System.Globalization;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class DateStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            DateTime dtm;
            if (DateTime.TryParseExact(value.ToString(), "d", CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out dtm))
            {
                return dtm;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return ((DateTime)value).ToString("d");
            }
            return null;
        }
    }
}
