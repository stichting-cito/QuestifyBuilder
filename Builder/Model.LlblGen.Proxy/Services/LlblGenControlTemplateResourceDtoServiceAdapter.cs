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
    public class LlblGenControlTemplateResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<ControlTemplateResourceDto>, IControlTemplateResourceDtoRepository
    {
        public LlblGenControlTemplateResourceDtoServiceAdapter(IResourceDtoRepository<ControlTemplateResourceDto> decoree)
            : base(decoree)
        {
        }

        public override IEnumerable<ControlTemplateResourceDto> GetResourcesForBank(int id)
        {
            List<ControlTemplateResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetControlTemplatesForBank(id))
            {
                returnValue = (list == null) ? null : list.Select(r => ((ControlTemplateResourceEntity)r).ConvertResourceEntityToDto<ControlTemplateResourceEntity, ControlTemplateResourceDto>()).ToList();
            }
            return returnValue;
        }
    }
}
