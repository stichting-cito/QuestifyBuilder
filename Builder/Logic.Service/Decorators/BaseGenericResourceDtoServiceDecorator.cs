using System.Collections.Generic;
using Questify.Builder.Logic.Service.Interfaces;
using GenericResourceDto = Questify.Builder.Logic.Service.Model.Entities.GenericResourceDto;

namespace Questify.Builder.Logic.Service.Decorators
{
    public abstract class BaseGenericResourceDtoServiceDecorator : BaseResourceDtoServiceDecorator<GenericResourceDto>,
    IGenericResourceDtoRepository
    {
        public BaseGenericResourceDtoServiceDecorator(IGenericResourceDtoRepository decoree)
            : base(decoree)
        {
        }

        public IEnumerable<GenericResourceDto> GetListWithFilter(int bankId, string filter, string filePrefix, bool templatesOnly)
        {
            var returnValue = ((IGenericResourceDtoRepository)Decoree).GetListWithFilter(bankId, filter, filePrefix, templatesOnly);
            return returnValue;
        }
    }
}
