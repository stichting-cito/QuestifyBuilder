using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenGenericProxy : LlblGenResourceProxy<GenericResourceDto>, IConvertResource<GenericResourceDto, GenericResourceEntity>
    {
        public GenericResourceDto Convert(GenericResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var generic = base.GetResource(new GenericResourceDto(), resourceEntity, customProperties);
            generic.MediaType = resourceEntity.MediaType;
            generic.Size = resourceEntity.Size;
            generic.Dimensions = resourceEntity.Dimensions;
            generic.IsTemplate = resourceEntity.IsTemplate;
            return generic;
        }

        public GenericResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var specificType = (GenericResourceEntity)resourceEntity;
            return specificType == null ? null : Convert(specificType, customProperties);
        }
    }
}
