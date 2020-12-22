using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{


    [ValueConversion(typeof(GapComparisonType), typeof(string))]
    public class GapComparisonTypeConverter : IValueConverter
    {

        private static readonly Dictionary<string, GapComparisonType> ConversionList = new Dictionary<string, GapComparisonType>
        {
            { "=", GapComparisonType.Equals},
            {">", GapComparisonType.GreaterThan},
            { ">=", GapComparisonType.GreaterThanEquals},
            { "[]",  GapComparisonType.Range},
            { "<", GapComparisonType.SmallerThan},
            { "<=",  GapComparisonType.SmallerThanEquals},
            {"equivalent", GapComparisonType.Equivalent},
            { "<>",  GapComparisonType.NotEquals},
            { "evaluate", GapComparisonType.Evaluate},
            { "dependency", GapComparisonType.Dependency},
            { "Ø", GapComparisonType.NoValue},
            { "equal soft",GapComparisonType.EqualsSoft},
            { "equal equation", GapComparisonType.EqualEquation},
            { "equal strict", GapComparisonType.EqualsStrict}
        };



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueToConvert = (GapComparisonType)value;
            var result = ConversionList.FirstOrDefault(k => k.Value.Equals(valueToConvert)).Key;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueToConvert = value.ToString();
            return ConvertOperatorValue(valueToConvert);
        }

        public static GapComparisonType ConvertOperatorValue(string valueToConvert)
        {
            var result = ConversionList.FirstOrDefault(k => k.Key.Equals(valueToConvert)).Value;
            return result;
        }
    }
}
