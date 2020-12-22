using System.ServiceModel;
using AssessmentTestResourceDto = Questify.Builder.Logic.Service.Model.Entities.AssessmentTestResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface ITestTemplateResourceDtoRepository : IResourceDtoRepository<AssessmentTestResourceDto>
    {
    }
}
