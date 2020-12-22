using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenItemProxy : LlblGenResourceProxy<ItemResourceDto>, IConvertResource<ItemResourceDto, ItemResourceEntity>
    {
        public ItemResourceDto Convert(ItemResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var item = base.GetResource(new ItemResourceDto(), resourceEntity, customProperties);
            item.AlternativesCount = resourceEntity.AlternativesCount;
            item.ItemLayoutTemplateUsedName = resourceEntity.ItemLayoutTemplateUsedName;
            item.ItemTypeFromItemLayoutTemplate = resourceEntity.ItemTypeFromItemLayoutTemplate.ToString();
            item.ResponseCount = resourceEntity.ResponseCount;
            item.KeyValues = resourceEntity.KeyValues;
            item.RawScore = resourceEntity.RawScore;
            item.MaxScore = resourceEntity.MaxScore;
            item.ItemId = resourceEntity.ItemId;
            if (DtoFactory.IsInstantiated)
            {
                var dataSourceResources = DtoFactory.Datasource.GetResourcesForBank(resourceEntity.BankId); var inAndExclusionGroups = dataSourceResources.Where(x => x.DependentResourceIds.Contains(item.ResourceId) && (x.DataSourceType.Equals("exclusion") || x.DataSourceType.Equals("inclusion"))).Select(y => new { dataSourceType = y.DataSourceType, name = y.Name });
                item.ExclusionGroupCode = string.Join(",",
                            inAndExclusionGroups.Where(x => x.dataSourceType == "exclusion").Select(y => y.name));
                item.InclusionGroupCode = string.Join(",",
                            inAndExclusionGroups.Where(x => x.dataSourceType == "inclusion").Select(y => y.name));
            }
            return item;
        }

        public ItemResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var specificType = (ItemResourceEntity)resourceEntity;
            return specificType == null ? null : Convert(specificType, customProperties);
        }
    }
}
