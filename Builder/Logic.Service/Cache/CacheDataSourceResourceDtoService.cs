using System.Collections.Generic;
using Questify.Builder.Logic.Service.Interfaces;
using DataSourceResourceDto = Questify.Builder.Logic.Service.Model.Entities.DataSourceResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheDataSourceResourceDtoService : CacheResourceDtoService<DataSourceResourceDto>, IDataSourceResourceDtoRepository
    {
        private readonly IDataSourceResourceDtoRepository _decoree;

        public CacheDataSourceResourceDtoService(IDataSourceResourceDtoRepository decoree)
            : base(decoree)
        {
            _decoree = decoree;
        }

        public IEnumerable<DataSourceResourceDto> GetListWithFilter(int bankId, bool? isTemplate, params string[] behaviours)
        {
            return _decoree.GetListWithFilter(bankId, isTemplate, behaviours);
        }
    }
}
