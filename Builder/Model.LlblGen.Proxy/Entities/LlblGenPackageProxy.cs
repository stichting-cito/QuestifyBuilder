using System.Collections.Generic;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    internal class LlblGenPackageProxy : LlblGenResourceProxy<PackageResourceDto>, IConvertResource<PackageResourceDto, PackageResourceEntity>
    {
        public PackageResourceDto Convert(PackageResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            return base.GetResource(new PackageResourceDto(), resourceEntity, customProperties);
        }

        public PackageResourceDto Convert(ResourceEntity resourceEntity, IEnumerable<CustomBankPropertyDto> customProperties)
        {
            var specificType = (PackageResourceEntity)resourceEntity;
            return specificType == null ? null : Convert(specificType, customProperties);
        }
    }
}
