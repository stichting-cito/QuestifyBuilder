using System;
using System.Collections.Generic;
using System.Linq;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.HelperFunctions;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenItemResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<ItemResourceDto>, IItemResourceDtoRepository
    {
        public LlblGenItemResourceDtoServiceAdapter(IResourceDtoRepository<ItemResourceDto> decoree)
            : base(decoree)
        {
        }

        public override IEnumerable<ItemResourceDto> GetResourcesForBank(int id)
        {
            List<ItemResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetItemsForBank(id))
            {
                returnValue = ConvertItemResourceToDtos(list);
            }
            return returnValue;
        }

        public IEnumerable<ItemResourceDto> GetPauseItemList(int bankId)
        {
            List<ItemResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetPauseItemsForBank(bankId))
            {
                returnValue = ConvertItemResourceToDtos(list);
            }
            return returnValue;
        }

        public IEnumerable<ItemResourceDto> GetItemsListWithSearchOptions(int bankId, string searchKeyWords, bool includeSubbanks, bool searchInBankProperties,
            bool searchInItemText, Guid testContextResourceId, int maxRecords)
        {
            List<ItemResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetItemsForBank(bankId, searchKeyWords, searchInBankProperties, searchInItemText, testContextResourceId, maxRecords))
            {
                returnValue = ConvertItemResourceToDtos(list);
            }
            return returnValue;
        }

        public IEnumerable<ItemResourceDto> GetItemsByCode(IEnumerable<string> itemCodeList, int bankId, ItemResourceRequestDTO request)
        {
            List<ItemResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetItemsByCodes(itemCodeList.ToList(), bankId, request))
            {
                returnValue = list.Select(r => ((ItemResourceEntity)r).ConvertResourceEntityToDto<ItemResourceEntity, ItemResourceDto>()).ToList();
            }
            return returnValue;
        }

        private static List<ItemResourceDto> ConvertItemResourceToDtos(EntityCollection list)
        {
            if (list == null)
            {
                return null;
            }
            return list.Select(r => ((ItemResourceEntity)r).ConvertResourceEntityToDto<ItemResourceEntity, ItemResourceDto>()).ToList();
        }
    }
}
