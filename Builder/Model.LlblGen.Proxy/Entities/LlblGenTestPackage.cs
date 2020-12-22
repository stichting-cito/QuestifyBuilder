using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenTestPackageProxy : LlblGenResourceProxy<TestPackageResourceDto>, IConvertResource<TestPackageResourceDto, TestPackageResourceEntity>
    {
        public TestPackageResourceDto Convert(TestPackageResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            return base.GetResource(new TestPackageResourceDto(), resourceEntity, customProperties);
        }

        public TestPackageResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var specificType = (TestPackageResourceEntity)resourceEntity;
            return specificType == null ? null : Convert(specificType, customProperties);
        }
    }
}
