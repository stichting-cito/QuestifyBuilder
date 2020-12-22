using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cito.Tester.Common;
using Enums;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Services.TasksService.Interfaces;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfItemResourceDtoService : WcfResourceDtoServiceDecorator<ItemResourceDto>, IItemResourceDtoRepository
    {
        private readonly IConvertEnumToLocalizedString _enumConverter;

        internal WcfItemResourceDtoService(IItemResourceDtoRepository decoree, IConvertEnumToLocalizedString enumConverter)
            : base(decoree)
        {
            _enumConverter = enumConverter;
        }

        public new IEnumerable<ItemResourceDto> GetResourcesForBank(int id)
        {
            var list = base.GetResourcesForBank(id).ToList();
            return EnrichListOfItemsWithItemLayoutTemplateString(list);
        }

        private List<ItemResourceDto> EnrichListOfItemsWithItemLayoutTemplateString(List<ItemResourceDto> items)
        {
            var knownTranslations = new Dictionary<string, string>();
            items.ForEach(itemInLoop =>
            {
                var item = itemInLoop;
                if (item.ItemTypeFromItemLayoutTemplate != null && Enum.IsDefined(typeof(ItemTypeEnum), item.ItemTypeFromItemLayoutTemplate))
                {

                    if (!knownTranslations.ContainsKey(item.ItemTypeFromItemLayoutTemplate))
                    {
                        CacheLock.EnterWriteLock();
                        try
                        {
                            if (!knownTranslations.ContainsKey(item.ItemTypeFromItemLayoutTemplate))
                            {

                                knownTranslations.Add(item.ItemTypeFromItemLayoutTemplate,
                              _enumConverter.ConvertToString(
                                  (ItemTypeEnum)Enum.Parse(typeof(ItemTypeEnum), item.ItemTypeFromItemLayoutTemplate)));
                            }
                        }
                        finally
                        {
                            CacheLock.ExitWriteLock();
                        }
                    }
                    item.ItemTypeFromItemLayoutTemplateString = knownTranslations[item.ItemTypeFromItemLayoutTemplate];
                }
                else
                {
                    item.ItemTypeFromItemLayoutTemplateString = item.ItemTypeFromItemLayoutTemplate;
                }
            });
            return items;
        }

        public IEnumerable<ItemResourceDto> GetPauseItemList(int bankId)
        {
            var list = new HashSet<ItemResourceDto>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(ItemResourceDto)), bankId);
            if (ids == null)
            {
                return list;
            }

            Parallel.ForEach(ids.ToList(), bId =>
            {
                var b = bId;
                var listPerBank = ((IItemResourceDtoRepository)Decoree).GetPauseItemList(b);
                if (listPerBank == null)
                {
                    return;
                }
                CacheLock.EnterWriteLock();
                try
                {
                    list.UnionWith(listPerBank.Where(c => c != null && c.BankId == b));
                }
                finally
                {
                    CacheLock.ExitWriteLock();
                }
            });
            return list;
        }

        public IEnumerable<ItemResourceDto> GetItemsListWithSearchOptions(int bankId, string searchKeyWords, bool includeSubBanks, bool searchInBankProperties,
            bool searchInItemText, Guid testContextResourceId, int maxRecords)
        {
            var list = new HashSet<ItemResourceDto>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(ItemResourceDto)), bankId, includeSubBanks);
            if (ids == null)
            {
                return list;
            }

            ids.ToList().ForEach(bId =>
            {
                var b = bId;
                var listPerBank = ((IItemResourceDtoRepository)Decoree).GetItemsListWithSearchOptions(b, searchKeyWords, includeSubBanks, searchInBankProperties,
                    searchInItemText, testContextResourceId, maxRecords);
                if (listPerBank == null)
                {
                    return;
                }

                CacheLock.EnterWriteLock();
                try
                {
                    list.UnionWith(listPerBank.Where(c => c != null && c.BankId == b));
                }
                finally
                {
                    CacheLock.ExitWriteLock();
                }
            });
            return EnrichListOfItemsWithItemLayoutTemplateString(list.OrderBy(r => r.Name).ToList());
        }

        public IEnumerable<ItemResourceDto> GetItemsByCode(IEnumerable<string> itemCodeList, int bankId, ItemResourceRequestDTO request)
        {
            IEnumerable<ItemResourceDto> list = new List<ItemResourceDto>();
            var ids = GetGrantedBankPermissions(Helper.ContentModelObjectToPermissionTarget(typeof(ItemResourceDto)), bankId);
            if (ids == null)
            {
                return list;
            }
            if (itemCodeList == null)
            {
                return null;
            }
            var itemCodes = itemCodeList.ToList();
            Parallel.ForEach(ids.ToList(), bId =>
            {
                var b = bId;
                IEnumerable<ItemResourceDto> listPerBank = new List<ItemResourceDto>();
                listPerBank = ((IItemResourceDtoRepository)Decoree).GetItemsByCode(itemCodes, bankId, request);

                if (listPerBank != null && listPerBank.Any())
                {
                    CacheLock.EnterWriteLock();
                    try
                    {
                        list = list.Union(listPerBank.Where(c => c != null && c.BankId == b));
                    }
                    finally
                    {
                        CacheLock.ExitWriteLock();
                    }
                }
            });
            return list.OrderBy(r => r.Name);
        }
    }
}