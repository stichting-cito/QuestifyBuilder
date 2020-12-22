using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class ListHasItemsToVisibilityConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var col = value as IEnumerable;
            bool b = GotAny(col);
            return (b) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        private bool GotAny(IEnumerable source)
        {
            if (source == null)
            {
                return false;
            }
            IEnumerator enumerator = source.GetEnumerator();
            {
                if (enumerator.MoveNext())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
