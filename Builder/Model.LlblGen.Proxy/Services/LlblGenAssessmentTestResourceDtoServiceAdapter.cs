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
    public class LlblGenAssessmentTestResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<AssessmentTestResourceDto>, ITestResourceDtoRepository
    {
        public LlblGenAssessmentTestResourceDtoServiceAdapter(IResourceDtoRepository<AssessmentTestResourceDto> decoree)
            : base(decoree)
        {
        }
        public override IEnumerable<AssessmentTestResourceDto> GetResourcesForBank(int id)
        {
            List<AssessmentTestResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetAssessmentTestsForBank(id))
            {
                returnValue = list.Select(r => ((AssessmentTestResourceEntity)r).ConvertResourceEntityToDto<AssessmentTestResourceEntity, AssessmentTestResourceDto>()).ToList();
            }
            return returnValue;
        }

        public IEnumerable<AssessmentTestResourceDto> GetTestsByCodes(IEnumerable<string> testCodeList, int bankId)
        {
            List<AssessmentTestResourceDto> returnValue;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetTestsByCodes(testCodeList.ToList(), bankId, true))
            {
                returnValue = list.Select(r => ((AssessmentTestResourceEntity)r).ConvertResourceEntityToDto<AssessmentTestResourceEntity, AssessmentTestResourceDto>()).ToList();
            }
            return returnValue;
        }
    }
}
