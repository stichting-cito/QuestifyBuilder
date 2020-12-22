using System;
using System.Windows.Data;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class StateEntityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((StateEntity)value).StateId == 0 ? null : value;
        }
    }
}
