using System.ServiceModel;
using ControlTemplateResourceDto = Questify.Builder.Logic.Service.Model.Entities.ControlTemplateResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IControlTemplateResourceDtoRepository : IResourceDtoRepository<ControlTemplateResourceDto>
    {
    }
}
