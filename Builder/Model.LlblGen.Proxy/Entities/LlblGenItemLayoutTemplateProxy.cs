using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenItemLayoutTemplateProxy : LlblGenResourceProxy<ItemLayoutTemplateResourceDto>, IConvertResource<ItemLayoutTemplateResourceDto, ItemLayoutTemplateResourceEntity>
    {
        public ItemLayoutTemplateResourceDto Convert(ItemLayoutTemplateResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var itemlayoutTemplate = base.GetResource(new ItemLayoutTemplateResourceDto(), resourceEntity, customProperties);
            itemlayoutTemplate.ItemType = resourceEntity.ItemType;
            return itemlayoutTemplate;
        }

        public ItemLayoutTemplateResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var specificType = (ItemLayoutTemplateResourceEntity)resourceEntity;
            return specificType == null ? null : Convert(specificType, customProperties);
        }
    }
}
