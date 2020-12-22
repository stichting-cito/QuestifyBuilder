using System.Collections.Generic;
using System.ServiceModel;
using GenericResourceDto = Questify.Builder.Logic.Service.Model.Entities.GenericResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IGenericResourceDtoRepository : IResourceDtoRepository<GenericResourceDto>
    {
        [OperationContract]
        IEnumerable<GenericResourceDto> GetListWithFilter(int bankId, string filter, string filePrefix,
            bool templatesOnly);
    }
}
