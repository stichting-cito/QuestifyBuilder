using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Data;
using Questify.Builder.Logic;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class RangeConverter : IMultiValueConverter
    {
        private enum ConvertToType
        {
            ConvertToDecimal,
            ConvertToMultiTypeInteger,
            ConvertToMultiTypeDecimal
        };

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
            {
                return string.Empty;
            }
            return string.Format("[{0};{1}]", values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            ConvertToType type = (ConvertToType)Enum.Parse(typeof(ConvertToType), parameter.ToString());
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            string val = (string)value;
            val = val.Replace("[", "");
            val = val.Replace("]", "");

            string[] splitValues = val.Split(';');
            List<object> lst = new List<object>();
            foreach (var str in splitValues)
            {
                switch (type)
                {
                    case ConvertToType.ConvertToDecimal:
                        decimal decimalValue;
                        if (decimal.TryParse(str, NumberStyles.Number, currentCulture, out decimalValue))
                        {
                            lst.Add(decimalValue);
                        }
                        break;

                    case ConvertToType.ConvertToMultiTypeInteger:
                        lst.Add(ConvertBackMultiInteger(str));
                        break;
                    case ConvertToType.ConvertToMultiTypeDecimal:
                        lst.Add(ConvertBackMultiDecimal(str, currentCulture));
                        break;
                }
            }
            return lst.ToArray();
        }

        private MultiType ConvertBackMultiInteger(string str)
        {
            MultiType result = new MultiType();
            int intValue;
            if (int.TryParse(str, out intValue))
            {
                result.IntegerValue = intValue;
            }
            else
            {
                result.IntegerValue = null;
            }
            return result;
        }

        private MultiType ConvertBackMultiDecimal(string str, CultureInfo currentCulture)
        {
            MultiType result = new MultiType();
            decimal mtDecValue;
            if (decimal.TryParse(str, NumberStyles.Number, currentCulture, out mtDecValue))
            {
                result.DecimalValue = mtDecValue;
            }
            else
            {
                result.DecimalValue = null;
            }
            return result;
        }
    }
}

