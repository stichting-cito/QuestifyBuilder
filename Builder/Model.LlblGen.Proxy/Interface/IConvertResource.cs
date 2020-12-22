using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.Model.LlblGen.Proxy.Interface
{
    internal interface IConvertResource<out TResourceDto, in TResource>
        where TResource : ResourceEntity
        where TResourceDto : ResourceDto
    {
        TResourceDto Convert(TResource resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties);
        TResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties);
    }
}
