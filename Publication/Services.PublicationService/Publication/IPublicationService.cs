
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Questify.Builder.Services.PublicationService.Publication
{
    /// <summary>
    /// Provides the interface for the publication service. The publication service can be used to publicize tests or testpackages using various
    /// publication handlers.
    /// </summary>
    [ServiceContract]
    public interface IPublicationService
    {
        /// <summary>
        /// Gets the available publication handlers for the specified test(s)/testpackage(s).
        /// </summary>
        /// <param name="bankId">The bank unique identifier.</param>
        /// <param name="testNames">The test names.</param>
        /// <param name="testPackageNames">The test package names.</param>
        /// <returns>A list of supported handlers for the provided test(s)/testpackage(s).</returns>
        [OperationContract]
        IList<PublicationHandlerIdentifier> GetAvailablePublicationHandlers(int bankId, IList<string> testNames, IList<string> testPackageNames);

        /// <summary>
        /// Gets the available publication handlers. (Not filtering out if they are suitable for a specific test(package))
        /// </summary>
        /// <returns>All available publication handlers.</returns>
        [OperationContract]
        IList<PublicationHandlerIdentifier> GetAllPublicationHandlers(int bankId, IList<string> testNames, IList<string> testPackageNames);

        /// <summary>
        /// Gets all test preview handlers.
        /// </summary>
        [OperationContract]
        IList<TestPreviewHandlerIdentifier> GetAllTestPreviewHandlers();


        /// <summary>
        /// Gets the available test preview handlers.
        /// </summary>
        /// <param name="bankId">The bank identifier.</param>
        /// <param name="testNames">The test names.</param>
        /// <param name="testPackages">The test packages.</param>
        [OperationContract]
        IList<TestPreviewHandlerIdentifier> GetAvailableTestPreviewHandlers(int bankId, IList<string> testNames, IList<string> testPackages);

        /// <summary>
        /// Gets the configuration options of the handler for the specified test(s)/testpackage(s).
        /// </summary>
        /// <param name="publicationHandler">The publication handler.</param>
        /// <param name="bankId">The bank unique identifier.</param>
        /// <param name="testNames">The test names.</param>
        /// <param name="testPackageNames">The test package names.</param>
        /// <returns>A dictionary with configuration options.</returns>
        [OperationContract]
        Dictionary<string, string> GetConfigurationOptions(string publicationHandlerType, int bankId, IList<string> testNames, IList<string> testPackageNames);

        /// <summary>
        /// Publicizes the specified test(s)/testpackage(s) from the specified bank using the given configuration options.
        /// </summary>
        /// <param name="publicationHandler">The publication handler.</param>
        /// <param name="configurationOptions">The configuration options.</param>
        /// <param name="bankId">The bank unique identifier.</param>
        /// <param name="testNames">The test names.</param>
        /// <param name="testPackageNames">The test package names.</param>
        /// <param name="isForPreview">if set to <c>true</c> the publication will be used for a test preview.</param>
        /// <param name="customName">Custom name for published file to use instead of package name</param>
        /// <returns>
        /// A task id which can be used to poll for progress using <see cref="GetProgress" />.
        /// </returns>
        [OperationContract]
        string Publicize(string publicationHandlerType, Dictionary<string, string> configurationOptions, int bankId, IList<string> testNames, IList<string> testPackageNames, bool isForPreview, string customName);

        /// <summary>
        /// Gets the progress of the specified task.
        /// </summary>
        /// <param name="taskId">The task's unique identifier.</param>
        /// <returns>The progress of the publication task.</returns>
        [OperationContract]
        PublicationTaskProgress GetProgress(string taskId);

        /// <summary>
        /// Finishes the specified publication task. Should be called after the client
        /// is aware that the publication has finished and the client no longer needs to poll
        /// for progress. The server can then perform some cleanup tasks.
        /// </summary>
        /// <param name="taskId">The task's unique identifier.</param>
        [OperationContract]
        void FinishPublication(string taskId);

        [OperationContract]
        String GetItemOutput(string publicationHandlerType, int bankId, string itemCode);

        /// <summary>
        /// Returns a list of ConceptProcessingReportEntry objects holding the concept related response processing QTI for the item in which attributes are included that identify the concept code and fact-id 
        /// a particular response element was created for.
        /// </summary>
        /// <returns>XElement with the concept related QTI response processing elements</returns>
        /// <param name="publicationHandler"></param>
        /// <param name="itemResourceId"></param>
        [OperationContract]
        List<ConceptProcessingLabelEntry> GetConceptRelatedResponseProcessingForReportingPurposes(string publicationHandlerType, Guid itemResourceId);
    }
}
