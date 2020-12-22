using Questify.Builder.Logic.Service.Interfaces;
using AssessmentTestResourceDto = Questify.Builder.Logic.Service.Model.Entities.AssessmentTestResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheTestTemplateResourceDtoService : CacheResourceDtoService<AssessmentTestResourceDto>, ITestTemplateResourceDtoRepository
    {
        private readonly ITestTemplateResourceDtoRepository _decoree;

        public CacheTestTemplateResourceDtoService(ITestTemplateResourceDtoRepository decoree)
            : base(decoree)
        {
            _decoree = decoree;
        }

    }
}
