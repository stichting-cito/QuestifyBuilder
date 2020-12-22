using System;
using System.Collections.Generic;
using System.ServiceModel;
using Cito.Tester.Common;
using ItemResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IItemResourceDtoRepository : IResourceDtoRepository<ItemResourceDto>
    {
        [OperationContract]
        IEnumerable<ItemResourceDto> GetPauseItemList(int bankId);
        [OperationContract]
        IEnumerable<ItemResourceDto> GetItemsListWithSearchOptions(int bankId, string searchKeyWords, bool includeSubbanks, bool searchInBankProperties, bool searchInItemText, Guid testContextResourceId, int maxRecords);
        [OperationContract]
        IEnumerable<ItemResourceDto> GetItemsByCode(IEnumerable<string> itemCodeList, int bankId, ItemResourceRequestDTO request);
    }
}
