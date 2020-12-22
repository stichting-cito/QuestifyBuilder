using System.Collections.Generic;
using Enums;
using Questify.Builder.Logic.Service.Interfaces;
using ItemLayoutTemplateResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemLayoutTemplateResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheItemLayoutResourceDtoService : CacheResourceDtoService<ItemLayoutTemplateResourceDto>, IItemlayoutTemplateResourceDtoRepository
    {
        private readonly IItemlayoutTemplateResourceDtoRepository _decoree;

        public CacheItemLayoutResourceDtoService(IItemlayoutTemplateResourceDtoRepository decoree)
            : base(decoree)
        {
            _decoree = decoree;
        }

        public IEnumerable<ItemLayoutTemplateResourceDto> GetListWithItemTypeFilter(int bankId, IEnumerable<ItemTypeEnum> itemTypes, bool excluded)
        {
            return _decoree.GetListWithItemTypeFilter(bankId, itemTypes, excluded);
        }
    }
}
