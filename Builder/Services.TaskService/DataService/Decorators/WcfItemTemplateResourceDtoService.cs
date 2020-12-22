using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Enums;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Services.TasksService.Interfaces;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfItemTemplateResourceDtoService : WcfResourceDtoServiceDecorator<ItemLayoutTemplateResourceDto>, IItemlayoutTemplateResourceDtoRepository
    {
        private readonly IConvertEnumToLocalizedString _enumConverter;


        public WcfItemTemplateResourceDtoService(IItemlayoutTemplateResourceDtoRepository decoree, IConvertEnumToLocalizedString enumConverter)
            : base(decoree)
        {
            _enumConverter = enumConverter;
        }


        public new IEnumerable<ItemLayoutTemplateResourceDto> GetResourcesForBank(int id)
        {
            var list = base.GetResourcesForBank(id).ToList();
            list.Where(r => !string.IsNullOrWhiteSpace(r.ItemType)).ToList().ForEach(r =>
                    r.ItemTypeString =
                        _enumConverter.ConvertToString(
                            (ItemTypeEnum)Enum.Parse(typeof(ItemTypeEnum), r.ItemType),
                            CultureInfo.DefaultThreadCurrentUICulture));

            var bankIds = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(ItemLayoutTemplateResourceDto)), id);
            list.ForEach(r => r.VisibleInPicker = (r.SetToInvisibleAtBankIds == null || !r.SetToInvisibleAtBankIds.Any()) || !bankIds.Intersect(r.SetToInvisibleAtBankIds).Any());

            return list;
        }


        public IEnumerable<ItemLayoutTemplateResourceDto> GetListWithItemTypeFilter(int bankId, IEnumerable<ItemTypeEnum> itemTypes, bool excludeTheOnesThatOccurInItemTypes)
        {
            if (itemTypes == null) return new List<ItemLayoutTemplateResourceDto>();
            var itemTypeList = itemTypes.Select(x => x.ToString()).ToList();

            var listToReturn = GetResourcesForBank(bankId).Where(r => r.VisibleInPicker && ((excludeTheOnesThatOccurInItemTypes && !itemTypeList.Contains(r.ItemType)) || (!excludeTheOnesThatOccurInItemTypes && itemTypeList.Contains(r.ItemType))));

            return listToReturn;
        }
    }
}