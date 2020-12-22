using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Interfaces;
using GenericResourceDto = Questify.Builder.Logic.Service.Model.Entities.GenericResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheGenericResourceDtoService : CacheResourceDtoService<GenericResourceDto>, IGenericResourceDtoRepository
    {
        private readonly IGenericResourceDtoRepository _decoree;

        public CacheGenericResourceDtoService(IGenericResourceDtoRepository decoree)
            : base(decoree)
        {
            _decoree = decoree;
        }

        public IEnumerable<GenericResourceDto> GetListWithFilter(
            int bankId,
            string filter,
            string filePrefix,
            bool templatesOnly)
        {
            return _decoree.GetListWithFilter(bankId, filter, filePrefix, templatesOnly).ToList();
        }
    }
}
