using System.Collections.Generic;
using Questify.Builder.Logic.Service.Interfaces;
using DataSourceResourceDto = Questify.Builder.Logic.Service.Model.Entities.DataSourceResourceDto;

namespace Questify.Builder.Logic.Service.Decorators
{
    public abstract class BaseDatasourceResourceDtoServiceDecorator : BaseResourceDtoServiceDecorator<DataSourceResourceDto>,
    IDataSourceResourceDtoRepository
    {
        public BaseDatasourceResourceDtoServiceDecorator(IDataSourceResourceDtoRepository decoree)
            : base(decoree)
        {
        }

        public IEnumerable<DataSourceResourceDto> GetListWithFilter(int bankId, bool? isTemplate, params string[] behaviours)
        {
            var returnValue = ((IDataSourceResourceDtoRepository)Decoree).GetListWithFilter(bankId, isTemplate, behaviours);
            return returnValue;
        }
    }
}
