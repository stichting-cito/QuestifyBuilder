using System.Collections.Generic;
using Enums;
using Questify.Builder.Logic.Service.Interfaces;
using ItemLayoutTemplateResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemLayoutTemplateResourceDto;

namespace Questify.Builder.Logic.Service.Decorators
{
    public abstract class BaseItemLayoutTemplateDtoServiceDecorator : BaseResourceDtoServiceDecorator<ItemLayoutTemplateResourceDto>, IItemlayoutTemplateResourceDtoRepository
    {
        public BaseItemLayoutTemplateDtoServiceDecorator(IItemlayoutTemplateResourceDtoRepository decoree)
            : base(decoree)
        {
        }

        public virtual IEnumerable<ItemLayoutTemplateResourceDto> GetListWithItemTypeFilter(int bankId, IEnumerable<ItemTypeEnum> itemTypes, bool excluded)
        {
            var returnValue = ((IItemlayoutTemplateResourceDtoRepository)Decoree).GetListWithItemTypeFilter(bankId, itemTypes, excluded);
            return returnValue;
        }
    }
}
