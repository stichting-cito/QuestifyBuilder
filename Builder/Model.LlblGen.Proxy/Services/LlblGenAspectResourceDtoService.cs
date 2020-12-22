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
    public class LlblGenAspectResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<AspectResourceDto>, IAspectResourceDtoRepository
    {
        public LlblGenAspectResourceDtoServiceAdapter(IResourceDtoRepository<AspectResourceDto> decoree)
            : base(decoree)
        {
        }

        public override IEnumerable<AspectResourceDto> GetResourcesForBank(int id)
        {
            List<AspectResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetAspectsForBank(id))
            {
                returnValue = (list == null) ? null : list.Select(r => ((AspectResourceEntity)r).ConvertResourceEntityToDto<AspectResourceEntity, AspectResourceDto>()).ToList();
            }
            return returnValue;
        }

    }
}
