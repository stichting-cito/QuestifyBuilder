using System;
using System.Windows;

namespace Questify.Builder.UI.Wpf.Controls
{

    internal static class DoubleUtil
    {
        internal const double DBL_EPSILON = 2.2204460492503131e-016; internal const float FLT_MIN = 1.175494351e-38F;
        public static bool AreClose(double value1, double value2)
        {
            double eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * DBL_EPSILON;
            double delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        public static bool AreClose(Size size1, Size size2)
        {
            return DoubleUtil.AreClose(size1.Width, size2.Width) &&
                   DoubleUtil.AreClose(size1.Height, size2.Height);
        }

        public static bool LessThan(double value1, double value2)
        {
            return (value1 < value2) && !AreClose(value1, value2);
        }

        public static bool GreaterThan(double value1, double value2)
        {
            return (value1 > value2) && !AreClose(value1, value2);
        }
    }
}
