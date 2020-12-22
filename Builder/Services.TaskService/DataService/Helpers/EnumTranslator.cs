using System;
using System.Globalization;
using System.Threading;
using Cito.Tester.Common;
using Enums;
using Questify.Builder.Logic.Service.Model;
using Questify.Builder.Services.TasksService.Interfaces;
using Questify.Builder.Services.TasksService.Properties;

namespace Questify.Builder.Services.TasksService.DataService.Helpers
{
    public class EnumTranslator : IConvertEnumToLocalizedString
    {
        public string ConvertToString(Enum value, CultureInfo culture)
        {
            if (value is CustomPropertyType)
            {
                return GetCustomPropertyTypeValue((CustomPropertyType)value);
            }
            if (value is ResourceTypeEnum)
            {
                return GetResourceTypeName((ResourceTypeEnum)value);
            }
            return ResourceEnumConverter.ConvertToString(value, culture);
        }

        public string ConvertToString(Enum value)
        {
            var cultureInfo = CultureInfo.DefaultThreadCurrentUICulture ?? Thread.CurrentThread.CurrentUICulture;
            return ConvertToString(value, cultureInfo);
        }

        private string GetCustomPropertyTypeValue(CustomPropertyType value)
        {
            switch (value)
            {
                case CustomPropertyType.Free:
                    return My.Resources.Resources.CustomBankPropertyType_FreeValue;
                case CustomPropertyType.FreeRichText:
                    return My.Resources.Resources.CustomBankPropertyType_FreeValueRichText;
                case CustomPropertyType.ListSingle:
                    return My.Resources.Resources.CustomBankPropertyType_ListSingleSelect;
                case CustomPropertyType.ListMultiple:
                    return My.Resources.Resources.CustomBankPropertyType_ListMultipleSelect;
                case CustomPropertyType.Tree:
                    return My.Resources.Resources.CustomBankPropertyType_Tree;
                case CustomPropertyType.Concept:
                    return My.Resources.Resources.CustomBankPropertyType_Concept;
                default:
                    return String.Empty;
            }
        }

        private string GetResourceTypeName(ResourceTypeEnum value)
        {
            switch (value)
            {
                case ResourceTypeEnum.ItemResource:
                    return Resources.ResourceTypeEnum_Items;
                case ResourceTypeEnum.AssessmentTestResource:
                    return Resources.ResourceTypeEnum_Tests;
                case ResourceTypeEnum.GenericResource:
                    return Resources.ResourceTypeEnum_Media;
                default:
                    return string.Empty;
            }

        }
    }
}