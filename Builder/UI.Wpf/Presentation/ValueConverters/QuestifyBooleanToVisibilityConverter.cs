using System;
using System.Linq;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class QuestifyBooleanToVisibilityConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {

        private enum QuestifyBooleanToVisibilityConverterInstructions
        {
            DefaultBehavior,
            InvertBooleanValue,
            ConvertFalseToHidden
        };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            QuestifyBooleanToVisibilityConverterInstructions ci = GetPipeLineSeparatedInstructions((string)parameter);

            Boolean b = (Boolean)value;

            if ((ci & QuestifyBooleanToVisibilityConverterInstructions.InvertBooleanValue) == QuestifyBooleanToVisibilityConverterInstructions.InvertBooleanValue)
            {
                b = !b;
            }

            if (!b)
            {
                if ((ci & QuestifyBooleanToVisibilityConverterInstructions.ConvertFalseToHidden) == QuestifyBooleanToVisibilityConverterInstructions.ConvertFalseToHidden)
                {
                    return System.Windows.Visibility.Hidden;
                }
                else
                {
                    return System.Windows.Visibility.Collapsed;
                }
            }

            return System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        private QuestifyBooleanToVisibilityConverterInstructions GetPipeLineSeparatedInstructions(string instructions)
        {
            QuestifyBooleanToVisibilityConverterInstructions ci = QuestifyBooleanToVisibilityConverterInstructions.DefaultBehavior;

            if (instructions != null)
            {
                instructions.Split('|').ToList<string>().ForEach(x =>
                                                                        {
                                                                            QuestifyBooleanToVisibilityConverterInstructions cix;

                                                                            if (Enum.TryParse<QuestifyBooleanToVisibilityConverterInstructions>(x, true, out cix))
                                                                            {
                                                                                ci |= cix;
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Diagnostics.Debug.Assert(false, string.Format("Enum QuestifyBooleanToVisibilityConverterInstructions does not define value named: {0}", x));
                                                                            }
                                                                        }
                                                                );

            }

            return ci;
        }
    }
}
