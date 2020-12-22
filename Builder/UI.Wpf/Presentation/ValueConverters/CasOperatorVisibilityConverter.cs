using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class CasOperatorVisibilityConverter : System.Windows.Markup.MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length == 2)
            {
                var scoringParameter = values[0] as ScoringParameter;
                var comboboxItem = values[1] as ComboBoxItem;
                if (scoringParameter != null && comboboxItem != null)
                {
                    var value = comboboxItem.Content.ToString();
                    var operatorValue = GapComparisonTypeConverter.ConvertOperatorValue(value);
                    var supportCasValue = EnumAttributeHelper.GetAttributeValue<CasScoringOperatorAttribute, bool>(operatorValue);
                    if (supportCasValue && !scoringParameter.SupportCasScoring)
                    {
                        return Visibility.Collapsed;
                    }
                }
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
