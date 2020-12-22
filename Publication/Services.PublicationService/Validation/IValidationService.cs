
using System.Collections.Generic;
using System.ServiceModel;

namespace Questify.Builder.Services.PublicationService.Validation
{
    [ServiceContract]
    public interface IValidationService
    {
        [OperationContract]
        bool AtLeastOneHandlerAvailable(int bankId, IList<string> testNames);

        [OperationContract]
        IList<ValidationHandlerIdentifier> Validate(int bankId, IList<string> testNames);

        [OperationContract]
        ValidationTaskProgress GetProgress(string taskId);

        [OperationContract]
        void FinishValidation(string taskId);

        [OperationContract]
        System.Version GetCurrentVersion();
    }
}
