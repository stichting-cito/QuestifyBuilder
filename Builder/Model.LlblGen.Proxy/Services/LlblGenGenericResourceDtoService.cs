using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.HelperFunctions;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenGenericResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<GenericResourceDto>, IGenericResourceDtoRepository
    {
        public LlblGenGenericResourceDtoServiceAdapter(IResourceDtoRepository<GenericResourceDto> decoree)
            : base(decoree)
        {
        }

        public override IEnumerable<GenericResourceDto> GetResourcesForBank(int id)
        {
            return GetListWithFilter(id, string.Empty, string.Empty, false);
        }

        public IEnumerable<GenericResourceDto> GetListWithFilter(int bankId, string filter, string filePrefix, bool templatesOnly)
        {
            List<GenericResourceDto> returnList;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetGenericResourceForBank(bankId, filter, filePrefix, templatesOnly))
            {
                returnList = list.Select(r => ((GenericResourceEntity)r).ConvertResourceEntityToDto<GenericResourceEntity, GenericResourceDto>()).ToList();
            }
            return returnList;
        }
    }
}
