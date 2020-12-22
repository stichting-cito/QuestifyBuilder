using System;
using System.Collections.Generic;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Interfaces;
using ItemResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheItemResourceDtoService : CacheResourceDtoService<ItemResourceDto>, IItemResourceDtoRepository
    {
        private IItemResourceDtoRepository _decoree;

        public CacheItemResourceDtoService(IItemResourceDtoRepository decoree) : base(decoree)
        {
            _decoree = decoree;
        }

        public IEnumerable<ItemResourceDto> GetPauseItemList(int bankId)
        {
            return _decoree.GetPauseItemList(bankId);
        }

        public IEnumerable<ItemResourceDto> GetItemsListWithSearchOptions(int bankId, string searchKeyWords, bool includeSubbanks, bool searchInBankProperties,
            bool searchInItemText, Guid testContextResourceId, int maxRecords)
        {
            return _decoree.GetItemsListWithSearchOptions(bankId, searchKeyWords, includeSubbanks, searchInBankProperties, searchInItemText, testContextResourceId, maxRecords);
        }

        public IEnumerable<ItemResourceDto> GetItemsByCode(IEnumerable<string> itemCodeList, int bankId, ItemResourceRequestDTO request)
        {
            return _decoree.GetItemsByCode(itemCodeList, bankId, request);
        }
    }
}
