using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Entities;
using Questify.Builder.Model.LlblGen.Proxy.Factory;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.HelperFunctions
{
    public static class ResourceExtensions
    {
        public static TResourceDto ConvertResourceEntityToDto<TResource, TResourceDto>(this TResource resourceEntity, bool customPropertiesIncluded = true)
            where TResource : ResourceEntity
            where TResourceDto : ResourceDto
        {
            if (resourceEntity == null) return null;

            var customProperties = new List<CustomBankPropertyDto>();
            if (customPropertiesIncluded) customProperties = DtoFactory.CustomBankProperty.GetCustomBankPropertiesForBranch(resourceEntity.BankId).ToList();
            var proxy = new LlblGenResourceProxyFactory<TResourceDto, TResource>();
            return proxy.ConvertResource(resourceEntity, customProperties);
        }

        public static ResourceDto ConvertResourceEntityToResourceDto(this ResourceEntity resourceEntity, bool customPropertiesIncluded = true)
        {
            if (resourceEntity == null) return null;
            var customProperties = new List<CustomBankPropertyDto>();
            if (customPropertiesIncluded) customProperties = DtoFactory.CustomBankProperty.GetCustomBankPropertiesForBranch(resourceEntity.BankId).ToList(); var converter = (IConvertResource<ResourceDto, ResourceEntity>)new LlblGenResourceBaseProxy();
            return converter.Convert(resourceEntity, customProperties);
        }
    }
}
