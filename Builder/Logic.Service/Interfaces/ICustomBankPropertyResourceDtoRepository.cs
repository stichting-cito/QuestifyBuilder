using System.ServiceModel;
using CustomBankPropertyResourceDto = Questify.Builder.Logic.Service.Model.Entities.Custom.CustomBankPropertyResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface ICustomBankPropertyResourceDtoRepository : IResourceDtoRepository<CustomBankPropertyResourceDto>
    {
    }
}
