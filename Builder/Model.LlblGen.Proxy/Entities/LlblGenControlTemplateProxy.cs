using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenControlTemplateProxy : LlblGenResourceProxy<ControlTemplateResourceDto>, IConvertResource<ControlTemplateResourceDto, ControlTemplateResourceEntity>
    {
        public ControlTemplateResourceDto Convert(ControlTemplateResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            return base.GetResource(new ControlTemplateResourceDto(), resourceEntity, customProperties);
        }

        public ControlTemplateResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var controlTemplate = (ControlTemplateResourceEntity)resourceEntity;
            return controlTemplate == null ? null : Convert(controlTemplate, customProperties);
        }
    }
}
