using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.Views
{
    public class CloseMethodToVisbilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CloseMethod && parameter is CloseMethod)
            {
                return (CloseMethod)value == (CloseMethod)parameter ? Visibility.Visible : Visibility.Collapsed;
            }
            if (value is CloseMethod && parameter is string)
            {
                return value.ToString().Equals((string)parameter, StringComparison.InvariantCultureIgnoreCase) ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
