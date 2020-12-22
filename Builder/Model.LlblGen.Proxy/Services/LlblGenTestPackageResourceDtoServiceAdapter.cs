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
    public class LlblGenTestPackageResourceDtoServiceAdapter : BaseResourceDtoServiceDecorator<TestPackageResourceDto>, ITestPackageResourceDtoRepository
    {
        public LlblGenTestPackageResourceDtoServiceAdapter(IResourceDtoRepository<TestPackageResourceDto> decoree)
            : base(decoree)
        {
        }

        public override IEnumerable<TestPackageResourceDto> GetResourcesForBank(int id)
        {
            var list = ResourceFactoryWithoutPermissionCheck.Instance.GetTestPackagesForBank(id);
            return list == null ? null : list.Select(r => ((TestPackageResourceEntity)r).ConvertResourceEntityToDto<TestPackageResourceEntity, TestPackageResourceDto>());
        }

    }
}
