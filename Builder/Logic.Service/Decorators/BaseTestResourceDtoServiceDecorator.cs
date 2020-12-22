using System.Collections.Generic;
using Questify.Builder.Logic.Service.Interfaces;
using AssessmentTestResourceDto = Questify.Builder.Logic.Service.Model.Entities.AssessmentTestResourceDto;

namespace Questify.Builder.Logic.Service.Decorators
{
    public abstract class BaseTestResourceDtoServiceDecorator : BaseResourceDtoServiceDecorator<AssessmentTestResourceDto>,
    ITestResourceDtoRepository
    {
        public BaseTestResourceDtoServiceDecorator(ITestResourceDtoRepository decoree)
            : base(decoree)
        {
        }

        public virtual IEnumerable<AssessmentTestResourceDto> GetTestsByCodes(IEnumerable<string> testCodeList, int bankId)
        {
            var returnValue = ((ITestResourceDtoRepository)Decoree).GetTestsByCodes(testCodeList, bankId);
            return returnValue;
        }
    }
}
