using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Entities;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Factory
{
    internal class LlblGenResourceProxyFactory<TResourceDto, TResource>
        where TResource : ResourceEntity
        where TResourceDto : ResourceDto
    {
        internal TResourceDto ConvertResource(TResource resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            IConvertResource<TResourceDto, TResource> converter;
            if (resourceEntity is AspectResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenAspectProxy();
            }
            else if (resourceEntity is AssessmentTestResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenAssessmentTestProxy();
            }
            else if (resourceEntity is ControlTemplateResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenControlTemplateProxy();
            }
            else if (resourceEntity is DataSourceResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenDatasourceProxy();
            }
            else if (resourceEntity is GenericResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenGenericProxy();
            }
            else if (resourceEntity is ItemLayoutTemplateResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenItemLayoutTemplateProxy();
            }
            else if (resourceEntity is ItemResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenItemProxy();
            }
            else if (resourceEntity is PackageResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenPackageProxy();
            }
            else if (resourceEntity is TestPackageResourceEntity)
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenTestPackageProxy();
            }
            else
            {
                converter = (IConvertResource<TResourceDto, TResource>)new LlblGenResourceBaseProxy();
            }
            return converter.Convert(resourceEntity, customProperties);
        }
    }
}
