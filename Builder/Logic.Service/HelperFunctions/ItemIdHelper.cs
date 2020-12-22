using System;
using System.Configuration;

namespace Questify.Builder.Logic.Service.HelperFunctions
{

    public class ItemIdHelper
    {
        const string ALLOWEDCHARS = "23456789abcdefghjklmnpqrstuvwxyz";

        const int ALLOWEDCHARSCOUNT = 32;
        public static bool UseItemId()
        {
            bool doUseItemId;
            bool.TryParse(ConfigurationManager.AppSettings["UseItemId"], out doUseItemId);
            return doUseItemId;
        }

        public static string GetItemId(int value)
        {

            if (value < 0 || value > Math.Pow(32, 6) - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value should be between 0 and 1073741823  (= (32^6)-1)");
            }

            int remainder;
            int quotient = Math.DivRem(value, ALLOWEDCHARSCOUNT, out remainder);
            if (quotient > 0)
            {
                return GetItemId(quotient) + ValToChar(remainder);
            }

            return ValToChar(remainder).ToString();
        }

        private static char ValToChar(int value)
        {
            return ALLOWEDCHARS[value];
        }
    }
}
