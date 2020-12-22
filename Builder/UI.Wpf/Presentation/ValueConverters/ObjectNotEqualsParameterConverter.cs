using System;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class ObjectNotEqualsParameterConverter : ObjectEqualsParameterConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)base.Convert(value, targetType, parameter, culture);
        }
    }
}
