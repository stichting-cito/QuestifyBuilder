using System.ComponentModel.Composition;
using Questify.Builder.Logic.Service.Messages;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{

    [Export(typeof(IBusinessExceptionManager))]
    class BusinessExceptionManager
        : IBusinessExceptionManager
    {
        public void HandleBusinessException(BusinessExceptionDto exceptionDto)
        {
        }
    }
}
