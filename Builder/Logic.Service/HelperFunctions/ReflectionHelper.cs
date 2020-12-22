using System;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace Questify.Builder.Logic.Service.HelperFunctions
{
    public static class ReflectionHelper
    {
        public static string GetPropertyValueString(object value, string propName)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var objType = value.GetType();
            var pInfo = objType.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (pInfo == null)
            {
                return string.Empty;
            }

            var propValue = pInfo.GetValue(value, BindingFlags.GetProperty, null, null, null);
            if (propValue != null && propValue.GetType().ToString() == typeof(decimal).ToString())
            {
                return null;
            }

            if (propValue != null && propValue.GetType().ToString() == typeof(DateTime).ToString())
            {
                DateTime dateValue;
                if (DateTime.TryParse(propValue.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue) || DateTime.TryParse(propValue.ToString(), out dateValue))
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0} {1}",
                        dateValue.ToString("d", Thread.CurrentThread.CurrentCulture),
                        dateValue.ToString("H:mm", Thread.CurrentThread.CurrentCulture));
                }
            }

            if (propValue != null)
            {
                return propValue.ToString();
            }
            return string.Empty;
        }

        public static decimal? GetPropertyValueDecimal(object value, string propName)
        {
            if (value == null)
            {
                return null;
            }

            var objType = value.GetType();
            var pInfo = objType.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (pInfo == null)
            {
                return null;
            }

            var propValue = pInfo.GetValue(value, BindingFlags.GetProperty, null, null, null);
            if (propValue != null && propValue.GetType().ToString() == typeof(decimal).ToString())
            {
                decimal decimalValue;
                if (decimal.TryParse(propValue.ToString().Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimalValue))
                {
                    return decimalValue;
                }
            }
            return null;
        }
    }
}
