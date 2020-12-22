using Questify.Builder.Logic.Service.Interfaces;
using ControlTemplateResourceDto = Questify.Builder.Logic.Service.Model.Entities.ControlTemplateResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheControlTemplateResourceDtoService : CacheResourceDtoService<ControlTemplateResourceDto>, IControlTemplateResourceDtoRepository
    {
        private readonly IControlTemplateResourceDtoRepository _decoree;

        public CacheControlTemplateResourceDtoService(IControlTemplateResourceDtoRepository decoree)
            : base(decoree)
        {
            _decoree = decoree;
        }
    }
}
