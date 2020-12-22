using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.HelperFunctions;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenDatasourceTemplateResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<DataSourceResourceDto>, IDataSourceTemplateResourceDtoRepository
    {
        public LlblGenDatasourceTemplateResourceDtoServiceAdapter(IResourceDtoRepository<DataSourceResourceDto> decoree)
            : base(decoree)
        {
        }

        public override IEnumerable<DataSourceResourceDto> GetResourcesForBank(int id)
        {
            List<DataSourceResourceDto> returnList;
            using (var list = ResourceFactoryWithoutPermissionCheck.Instance.GetDataSourcesForBank(id, true, null))
            {
                returnList = list == null ? null : list.Select(r => ((DataSourceResourceEntity)r).ConvertResourceEntityToDto<DataSourceResourceEntity, DataSourceResourceDto>()).ToList();
            }
            return returnList;
        }
    }
}
