using System.Collections.Generic;
using System.ServiceModel;
using AssessmentTestResourceDto = Questify.Builder.Logic.Service.Model.Entities.AssessmentTestResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface ITestResourceDtoRepository : IResourceDtoRepository<AssessmentTestResourceDto>
    {
        [OperationContract]
        IEnumerable<AssessmentTestResourceDto> GetTestsByCodes(IEnumerable<string> testCodeList, int bankId);
    }
}
