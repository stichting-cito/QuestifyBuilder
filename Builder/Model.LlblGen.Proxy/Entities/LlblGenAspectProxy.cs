using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenAspectProxy : LlblGenResourceProxy<AspectResourceDto>, IConvertResource<AspectResourceDto, AspectResourceEntity>
    {
        public AspectResourceDto Convert(AspectResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var aspect = base.GetResource(new AspectResourceDto(), resourceEntity, customProperties);
            aspect.RawScore = resourceEntity.RawScore;
            return aspect;
        }

        public AspectResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var aspectEntity = (AspectResourceEntity)resourceEntity;
            return aspectEntity == null ? null : Convert(aspectEntity, customProperties);
        }
    }
}
