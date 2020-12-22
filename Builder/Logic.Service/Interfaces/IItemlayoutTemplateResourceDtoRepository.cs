using System.Collections.Generic;
using System.ServiceModel;
using Enums;
using ItemLayoutTemplateResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemLayoutTemplateResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IItemlayoutTemplateResourceDtoRepository : IResourceDtoRepository<ItemLayoutTemplateResourceDto>
    {
        [OperationContract]
        IEnumerable<ItemLayoutTemplateResourceDto> GetListWithItemTypeFilter(int bankId, IEnumerable<ItemTypeEnum> itemTypes, bool excluded);
    }
}
