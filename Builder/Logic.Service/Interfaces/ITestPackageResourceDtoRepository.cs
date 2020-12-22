using System.ServiceModel;
using TestPackageResourceDto = Questify.Builder.Logic.Service.Model.Entities.TestPackageResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface ITestPackageResourceDtoRepository : IResourceDtoRepository<TestPackageResourceDto>
    {
    }
}
