using System;
using System.Windows;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class ListHasNoItemsToVisibilityConverter : ListHasItemsToVisibilityConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var b = (Visibility)base.Convert(value, targetType, parameter, culture);
            return (b == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;

        }
    }
}
