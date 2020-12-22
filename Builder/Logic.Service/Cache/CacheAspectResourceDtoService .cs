using Questify.Builder.Logic.Service.Interfaces;
using AspectResourceDto = Questify.Builder.Logic.Service.Model.Entities.AspectResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheAspectResourceDtoService : CacheResourceDtoService<AspectResourceDto>, IAspectResourceDtoRepository
    {
        private readonly IAspectResourceDtoRepository _decoree;

        public CacheAspectResourceDtoService(IAspectResourceDtoRepository decoree)
            : base(decoree)
        {
            _decoree = decoree;
        }

    }
}
