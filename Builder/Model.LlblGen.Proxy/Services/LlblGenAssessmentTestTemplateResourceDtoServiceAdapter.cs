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
    public class LlblGenAssessmentTestTemplateResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<AssessmentTestResourceDto>, ITestTemplateResourceDtoRepository
    {
        public LlblGenAssessmentTestTemplateResourceDtoServiceAdapter(IResourceDtoRepository<AssessmentTestResourceDto> decoree)
            : base(decoree)
        {
        }

        public override IEnumerable<AssessmentTestResourceDto> GetResourcesForBank(int id)
        {
            List<AssessmentTestResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetAssessmentTestTemplatesForBank(id))
            {
                returnValue = list.Select(r => ((AssessmentTestResourceEntity)r).ConvertResourceEntityToDto<AssessmentTestResourceEntity, AssessmentTestResourceDto>()).ToList();
            }
            return returnValue;
        }
    }
}
