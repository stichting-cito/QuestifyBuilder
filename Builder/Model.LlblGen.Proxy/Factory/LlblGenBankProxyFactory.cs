using Questify.Builder.Logic.CustomProperties.Helpers;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Logic.Service.Model.Entities.Custom;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomProperties;
using Questify.Builder.Model.LlblGen.Proxy.Entities;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Factory
{
    internal class LlblGenBankProxyFactory
    {
        internal BankDto ConvertBank(BankEntity bankEntity)
        {
            IConvertBank converter = new LlblGenBankProxy();
            return converter.Convert(bankEntity);
        }

        internal CustomBankPropertyDto ConvertCustomProperty(CustomBankPropertyEntity customBankPropertyEntity)
        {
            var customPropertyType = CustomPropertyHelper.GetCustomBankPropertyType(customBankPropertyEntity);
            if (!customPropertyType.HasValue) return null;
            var customPropertyProxy = new LlblGenCustomPropertyProxy();
            return customPropertyProxy.Convert(customBankPropertyEntity);
        }

        internal CustomBankPropertyResourceDto ConvertCustomResourceProperty(CustomBankPropertyEntity customBankPropertyEntity)
        {
            var customPropertyType = CustomPropertyHelper.GetCustomBankPropertyType(customBankPropertyEntity);
            if (!customPropertyType.HasValue) return null;
            var customPropertyProxy = new LlblGenCustomPropertyProxy();
            var customProperty = customPropertyProxy.Convert(customBankPropertyEntity);
            return new CustomBankPropertyResourceDto()
            {
                ResourceId = customProperty.CustomBankPropertyId,
                BankId = customProperty.BankId,
                BankName = customProperty.BankName,
                Name = customProperty.Name,
                Title = customProperty.Title,
                CreatedByFullName = customProperty.CreatedByFullName,
                ModifiedByFullName = customProperty.ModifiedByFullName,
                ApplicableToMask = customProperty.ApplicableToMask,
                CustomPropertyType = customProperty.CustomPropertyType,
                ModifiedBy = customProperty.ModifiedBy,
                ModifiedDate = customProperty.ModifiedDate,
                CreatedBy = customProperty.CreatedBy,
                CreationDate = customProperty.CreationDate
            };
        }

    }
}
