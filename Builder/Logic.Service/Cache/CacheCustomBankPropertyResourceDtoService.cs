using Questify.Builder.Logic.Service.Interfaces;
using CustomBankPropertyResourceDto = Questify.Builder.Logic.Service.Model.Entities.Custom.CustomBankPropertyResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheCustomBankPropertyResourceDtoService : CacheResourceDtoService<CustomBankPropertyResourceDto>, ICustomBankPropertyResourceDtoRepository
    {
        private readonly ICustomBankPropertyResourceDtoRepository _decoree;

        public CacheCustomBankPropertyResourceDtoService(ICustomBankPropertyResourceDtoRepository decoree)
            : base(decoree)
        {
            _decoree = decoree;
        }
    }
}
