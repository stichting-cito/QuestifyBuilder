using System;
using System.Globalization;
using System.Windows.Data;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    [ValueConversion(typeof(string), typeof(CasVariableViewModel))]
    internal class CasVariableValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string)value;
            return CasVariableViewModel.CreateFor(stringValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vm = (CasVariableViewModel)value;
            return vm.GetStringValue();
        }
    }
}
