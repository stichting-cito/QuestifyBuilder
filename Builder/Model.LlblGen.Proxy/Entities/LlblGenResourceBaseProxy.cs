using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{

    internal class LlblGenResourceBaseProxy : LlblGenResourceProxy<ResourceDto>, IConvertResource<ResourceDto, ResourceEntity>
    {

        public ResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {

            if (resourceEntity is AspectResourceEntity)
            {
                return new LlblGenAspectProxy().Convert(resourceEntity, customProperties);
            }
            if (resourceEntity is AssessmentTestResourceEntity)
            {
                return new LlblGenAssessmentTestProxy().Convert(resourceEntity, customProperties);
            }
            if (resourceEntity is ControlTemplateResourceEntity)
            {
                return new LlblGenControlTemplateProxy().Convert(resourceEntity, customProperties);
            }
            if (resourceEntity is DataSourceResourceEntity)
            {
                return new LlblGenDatasourceProxy().Convert(resourceEntity, customProperties);
            }
            if (resourceEntity is GenericResourceEntity)
            {
                return new LlblGenGenericProxy().Convert(resourceEntity, customProperties);
            }
            if (resourceEntity is ItemLayoutTemplateResourceEntity)
            {
                return new LlblGenItemLayoutTemplateProxy().Convert(resourceEntity, customProperties);
            }
            if (resourceEntity is ItemResourceEntity)
            {
                return new LlblGenItemProxy().Convert(resourceEntity, customProperties);
            }
            if (resourceEntity is PackageResourceEntity)
            {
                return new LlblGenPackageProxy().Convert(resourceEntity, customProperties);
            }
            if (resourceEntity is TestPackageResourceEntity)
            {
                return new LlblGenTestPackageProxy().Convert(resourceEntity, customProperties);
            }
            return null;
        }
    }
}
