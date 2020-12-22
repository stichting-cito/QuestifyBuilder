using Questify.Builder.Logic.Service.Interfaces;
using DataSourceResourceDto = Questify.Builder.Logic.Service.Model.Entities.DataSourceResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheDataSourceTemplateResourceDtoService : CacheResourceDtoService<DataSourceResourceDto>, IDataSourceTemplateResourceDtoRepository
    {
        public CacheDataSourceTemplateResourceDtoService(IDataSourceTemplateResourceDtoRepository decoree)
            : base(decoree)
        {
        }

    }
}
