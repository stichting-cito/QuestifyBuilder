using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.HelperFunctions;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenDatasourceResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<DataSourceResourceDto>, IDataSourceResourceDtoRepository
    {
        public LlblGenDatasourceResourceDtoServiceAdapter(IResourceDtoRepository<DataSourceResourceDto> decoree)
            : base(decoree)
        {
        }
        public override IEnumerable<DataSourceResourceDto> GetResourcesForBank(int id)
        {
            List<DataSourceResourceDto> returnList;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetDataSourcesForBank(id, false, null))
            {
                returnList = ConvertResourceToDtos(list).ToList();
            }
            return returnList;
        }
        public IEnumerable<DataSourceResourceDto> GetListWithFilter(int bankId, bool? isTemplate, params string[] behaviors)
        {
            List<DataSourceResourceDto> returnList;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetDataSourcesForBank(bankId, isTemplate, behaviors))
            {
                returnList = ConvertResourceToDtos(list).ToList();
            }
            return returnList;
        }

        private static IEnumerable<DataSourceResourceDto> ConvertResourceToDtos(EntityCollection list)
        {
            return list == null
                ? null
                : list.Select(
                    r => ((DataSourceResourceEntity)r).ConvertResourceEntityToDto<DataSourceResourceEntity, DataSourceResourceDto>());
        }
    }
}
