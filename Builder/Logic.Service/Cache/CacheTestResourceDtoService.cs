using System.Collections.Generic;
using Questify.Builder.Logic.Service.Interfaces;
using AssessmentTestResourceDto = Questify.Builder.Logic.Service.Model.Entities.AssessmentTestResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheTestResourceDtoService : CacheResourceDtoService<AssessmentTestResourceDto>, ITestResourceDtoRepository
    {
        private readonly ITestResourceDtoRepository _decoree;

        public CacheTestResourceDtoService(ITestResourceDtoRepository decoree)
            : base(decoree)
        {
            _decoree = decoree;
        }

        public IEnumerable<AssessmentTestResourceDto> GetTestsByCodes(IEnumerable<string> testCodeList, int bankId)
        {
            return _decoree.GetTestsByCodes(testCodeList, bankId);
        }
    }
}
