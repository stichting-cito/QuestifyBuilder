using Questify.Builder.Logic.CustomProperties.Helpers;
using Questify.Builder.Logic.Service.Model;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.Model.LlblGen.Proxy.CustomProperties
{
    internal class LlblGenCustomPropertyProxy
    {

        public CustomBankPropertyDto Convert(CustomBankPropertyEntity customBankProperty)
        {
            var customPropertyType = CustomPropertyHelper.GetCustomBankPropertyType(customBankProperty);
            if (!customPropertyType.HasValue)
            {
                return null;
            }
            var customPropertyHelper = new CustomPropertyHelper();
            var customPropertyConverter = customPropertyHelper.GetCustomBankConverter(customPropertyType.Value);
            var customPropertyDto = new CustomBankPropertyDto
            {
                CustomBankPropertyId = customBankProperty.CustomBankPropertyId,
                ApplicableToMask = customBankProperty.ApplicableToMask,
                BankId = customBankProperty.BankId,
                Code = customBankProperty.Code,
                CreatedBy = customBankProperty.CreatedBy,
                CreatedByFullName = customBankProperty.CreatedByFullName,
                Description = customBankProperty.Description,
                ModifiedBy = customBankProperty.ModifiedBy,
                ModifiedByFullName = customBankProperty.CreatedByFullName,
                Name = customBankProperty.Name,
                CreationDate = customBankProperty.CreationDate,
                ModifiedDate = customBankProperty.ModifiedDate,
                Publishable = customBankProperty.Publishable,
                Scorable = customBankProperty.Scorable,
                StateId = customBankProperty.StateId,
                Title = customBankProperty.Title,
                Version = customBankProperty.Version,
            };
            if (customBankProperty.Bank != null)
            {
                customPropertyDto.BankName = customBankProperty.Bank.Name;
            }
            var ctype = CustomPropertyHelper.GetCustomBankPropertyType(customBankProperty);
            if (ctype != null)
            {
                customPropertyDto.CustomPropertyType = ctype.Value;
                if (ctype.Value != CustomPropertyType.Free && customPropertyConverter != null)
                {
                    customPropertyDto.Values = customPropertyConverter.GetValuesFromCustomBankProperty(customBankProperty);
                }
            }
            return customPropertyDto;
        }
    }
}
