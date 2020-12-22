using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenDatasourceProxy : LlblGenResourceProxy<DataSourceResourceDto>, IConvertResource<DataSourceResourceDto, DataSourceResourceEntity>
    {
        public DataSourceResourceDto Convert(DataSourceResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var datasource = base.GetResource(new DataSourceResourceDto(), resourceEntity, customProperties);
            datasource.IsTemplate = resourceEntity.IsTemplate;
            datasource.DataSourceType = resourceEntity.DataSourceType;
            return datasource;
        }

        public DataSourceResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var datasource = (DataSourceResourceEntity)resourceEntity;
            return datasource == null ? null : Convert(datasource, customProperties);
        }
    }
}
