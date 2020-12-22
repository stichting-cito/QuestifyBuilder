using System.ServiceModel;
using DataSourceResourceDto = Questify.Builder.Logic.Service.Model.Entities.DataSourceResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IDataSourceTemplateResourceDtoRepository : IResourceDtoRepository<DataSourceResourceDto>
    {
    }
}
