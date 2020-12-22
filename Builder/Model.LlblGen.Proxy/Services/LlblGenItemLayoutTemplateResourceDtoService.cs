using System.Collections.Generic;
using System.Linq;
using Enums;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.HelperFunctions;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenItemLayoutTemplateResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<ItemLayoutTemplateResourceDto>, IItemlayoutTemplateResourceDtoRepository
    {
        public LlblGenItemLayoutTemplateResourceDtoServiceAdapter(IResourceDtoRepository<ItemLayoutTemplateResourceDto> decoree)
            : base(decoree)
        {
        }

        public override IEnumerable<ItemLayoutTemplateResourceDto> GetResourcesForBank(int id)
        {
            List<ItemLayoutTemplateResourceDto> returnList;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetItemLayoutTemplatesForBank(id))
            {
                returnList = ConvertItemLayoutTemplateResourceDtos(list).ToList();
            }
            return returnList;
        }

        public IEnumerable<ItemLayoutTemplateResourceDto> GetListWithItemTypeFilter(int bankId, IEnumerable<ItemTypeEnum> itemTypes, bool excluded)
        {
            List<ItemLayoutTemplateResourceDto> returnList;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetItemLayoutTemplatesForBankWithItemTypeFilter(bankId, itemTypes.ToList(), excluded))
            {
                returnList = ConvertItemLayoutTemplateResourceDtos(list).ToList();
            }
            return returnList;
        }

        private static IEnumerable<ItemLayoutTemplateResourceDto> ConvertItemLayoutTemplateResourceDtos(EntityCollection list)
        {
            return list == null
                ? null
                : list.Select(
                    r =>
                        ((ItemLayoutTemplateResourceEntity)r)
                            .ConvertResourceEntityToDto<ItemLayoutTemplateResourceEntity, ItemLayoutTemplateResourceDto>());
        }
    }
}
