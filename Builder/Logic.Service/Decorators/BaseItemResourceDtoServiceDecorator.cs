using System;
using System.Collections.Generic;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Interfaces;
using ItemResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemResourceDto;

namespace Questify.Builder.Logic.Service.Decorators
{
    public abstract class BaseItemResourceDtoServiceDecorator : BaseResourceDtoServiceDecorator<ItemResourceDto>,
    IItemResourceDtoRepository
    {
        public BaseItemResourceDtoServiceDecorator(IItemResourceDtoRepository decoree)
            : base(decoree)
        {
        }

        public virtual IEnumerable<ItemResourceDto> GetPauseItemList(int bankId)
        {
            var returnValue = ((IItemResourceDtoRepository)Decoree).GetPauseItemList(bankId);
            return returnValue;
        }

        public virtual IEnumerable<ItemResourceDto> GetItemsListWithSearchOptions(int bankId, string searchKeyWords,
            bool includeSubbanks,
            bool searchInBankProperties,
            bool searchInItemText, Guid testContextResourceId, int maxRecords)
        {
            var returnValue = ((IItemResourceDtoRepository)Decoree).GetItemsListWithSearchOptions(bankId, searchKeyWords, includeSubbanks, searchInBankProperties,
                searchInItemText, testContextResourceId, maxRecords); ;
            return returnValue;
        }

        public virtual IEnumerable<ItemResourceDto> GetItemsByCode(IEnumerable<string> itemCodeList, int bankId, ItemResourceRequestDTO request)
        {
            var returnValue = ((IItemResourceDtoRepository)Decoree).GetItemsByCode(itemCodeList, bankId, request);
            return returnValue;
        }
    }
}
