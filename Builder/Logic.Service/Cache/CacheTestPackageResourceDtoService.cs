using Questify.Builder.Logic.Service.Interfaces;
using TestPackageResourceDto = Questify.Builder.Logic.Service.Model.Entities.TestPackageResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheTestPackageResourceDtoService : CacheResourceDtoService<TestPackageResourceDto>, ITestPackageResourceDtoRepository
    {
        public CacheTestPackageResourceDtoService(ITestPackageResourceDtoRepository decoree)
            : base(decoree)
        {
        }

    }
}
