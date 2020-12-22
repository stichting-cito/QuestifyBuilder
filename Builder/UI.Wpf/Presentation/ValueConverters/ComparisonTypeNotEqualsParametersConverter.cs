using System;
using System.Linq;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    class ComparisonTypeNotEqualsParametersConverter : ObjectEqualsParameterConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter is string)
            {
                string parameters = (string)parameter;
                foreach (string prm in parameters.Split('|').ToList<string>())
                {
                    GapComparisonType comp;
                    if (Enum.TryParse<GapComparisonType>(prm, true, out comp))
                    {
                        if ((bool)base.Convert(value, targetType, comp, culture))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false, string.Format("Parameter should contain GapComparisonTypes: {0}", prm));
                    }
                }
                return true;
            }
            return (bool)base.Convert(value, targetType, parameter, culture);
        }
    }
}
