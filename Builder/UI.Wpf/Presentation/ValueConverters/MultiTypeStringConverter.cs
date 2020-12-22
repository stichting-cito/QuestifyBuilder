using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;
using Questify.Builder.Logic;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class MultiTypeStringConverter : IValueConverter
    {
        private enum ConvertToType
        {
            ConvertToMultiTypeInteger,
            ConvertToMultiTypeDecimal,
            ConvertToMultiTypeString
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                throw new ArgumentException(nameof(parameter));
            }

            MultiType multiValue = (MultiType)value;
            ConvertToType type = (ConvertToType)Enum.Parse(typeof(ConvertToType), parameter.ToString());

            switch (type)
            {
                case ConvertToType.ConvertToMultiTypeInteger:
                    if (multiValue.IntegerValue.HasValue)
                    {
                        return multiValue.ToString();
                    }
                    break;
                case ConvertToType.ConvertToMultiTypeDecimal:
                    if (multiValue.DecimalValue.HasValue)
                    {
                        return multiValue.ToString();
                    }
                    break;
                case ConvertToType.ConvertToMultiTypeString:
                    return multiValue.StringValue;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                throw new ArgumentException(nameof(parameter));
            }

            MultiType multiValue = new MultiType();
            ConvertToType type = (ConvertToType)Enum.Parse(typeof(ConvertToType), parameter.ToString());

            switch (type)
            {
                case ConvertToType.ConvertToMultiTypeInteger:
                    multiValue.IntegerValue = ConvertToInteger(value);
                    break;
                case ConvertToType.ConvertToMultiTypeDecimal:
                    multiValue.DecimalValue = ConvertToDecimal(value);
                    break;
                case ConvertToType.ConvertToMultiTypeString:
                    multiValue.StringValue = value.ToString();
                    break;
            }
            return multiValue;
        }

        private int? ConvertToInteger(object value)
        {
            int intValue;
            if (int.TryParse(value.ToString(), out intValue))
            {
                return intValue;
            }
            if (value.ToString().Equals("-", StringComparison.InvariantCulture))
            {
                return int.MinValue;
            }
            return null;
        }

        private decimal? ConvertToDecimal(object value)
        {
            decimal mtDecValue;
            if (decimal.TryParse(value.ToString(), NumberStyles.Number, Thread.CurrentThread.CurrentCulture, out mtDecValue))
            {
                return mtDecValue;
            }
            if (value.ToString().Equals("-", StringComparison.InvariantCulture))
            {
                return decimal.MinValue;
            }
            return null;
        }
    }
}
