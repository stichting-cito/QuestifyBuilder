using System.Collections.Generic;
using System.ServiceModel;
using DataSourceResourceDto = Questify.Builder.Logic.Service.Model.Entities.DataSourceResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IDataSourceResourceDtoRepository : IResourceDtoRepository<DataSourceResourceDto>
    {
        [OperationContract]
        IEnumerable<DataSourceResourceDto> GetListWithFilter(int bankId, bool? isTemplate, params string[] behaviours);
    }
}
