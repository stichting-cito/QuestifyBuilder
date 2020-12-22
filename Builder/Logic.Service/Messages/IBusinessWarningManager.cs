using System.Collections.Generic;

namespace Questify.Builder.Logic.Service.Messages
{
    public interface IBusinessWarningManager
    {
        void HandleBusinessWarning(IEnumerable<BusinessWarning> warnings);
    }
}
