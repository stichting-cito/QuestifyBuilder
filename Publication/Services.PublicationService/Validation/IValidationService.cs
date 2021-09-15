
using System.Collections.Generic;
using System.ServiceModel;

namespace Questify.Builder.Services.PublicationService.Validation
{
    [ServiceContract]
    public interface IValidationService
    {
        /// <summary>
        /// Indicates if at least 1 validation handler is available for the specified tests.
        /// </summary>
        /// <param name="bankId">The bank unique identifier.</param>
        /// <param name="testNames">The test names.</param>
        /// <returns>true if at least 1 handler is available or else false</returns>
        [OperationContract]
        bool AtLeastOneHandlerAvailable(int bankId, IList<string> testNames);

        /// <summary>
        /// Start validation of the given tests in the specified bank.
        /// </summary>
        /// <param name="bankId">The bank unique identifier.</param>
        /// <param name="testNames">The test names.</param>
        /// <returns>A list of <see cref="ValidationHandlerIdentifier"/> which can be used to poll for progress.</returns>
        [OperationContract]
        IList<ValidationHandlerIdentifier> Validate(int bankId, IList<string> testNames);

        /// <summary>
        /// Gets the progress of the specified task.
        /// </summary>
        /// <param name="taskId">The task unique identifier.</param>
        /// <returns>The progress of the specified validation task.</returns>
        [OperationContract]
        ValidationTaskProgress GetProgress(string taskId);

        /// <summary>
        /// Finishes the specified validation task. Should be called after the client
        /// is aware that the validation has finished and the client no longer needs to poll
        /// for progress. The server can then perform some cleanup tasks.
        /// </summary>
        /// <param name="taskId">The task's unique identifier.</param>
        [OperationContract]
        void FinishValidation(string taskId);

        [OperationContract]
        System.Version GetCurrentVersion();
    }
}
