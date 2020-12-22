using System.ServiceModel;
using AspectResourceDto = Questify.Builder.Logic.Service.Model.Entities.AspectResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IAspectResourceDtoRepository : IResourceDtoRepository<AspectResourceDto>
    {
    }
}
