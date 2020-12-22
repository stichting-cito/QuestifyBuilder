using System.Collections.Generic;
using Questify.Builder.Logic.Service.Messages;

namespace Questify.Builder.Logic.Service.Domain.AppServices
{
    public interface IBusinessNotifier
    {
        void AddWarning(BusinessWarningEnum warningType, string message);
        bool HasWarnings { get; }
        IEnumerable<BusinessWarning> RetrieveWarnings();
    }
}
