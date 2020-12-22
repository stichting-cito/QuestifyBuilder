
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Questify.Builder.Services.PublicationService.Publication
{
    [ServiceContract]
    public interface IPublicationService
    {
        [OperationContract]
        IList<PublicationHandlerIdentifier> GetAvailablePublicationHandlers(int bankId, IList<string> testNames, IList<string> testPackageNames);

        [OperationContract]
        IList<PublicationHandlerIdentifier> GetAllPublicationHandlers(int bankId, IList<string> testNames, IList<string> testPackageNames);

        [OperationContract]
        IList<TestPreviewHandlerIdentifier> GetAllTestPreviewHandlers();


        [OperationContract]
        IList<TestPreviewHandlerIdentifier> GetAvailableTestPreviewHandlers(int bankId, IList<string> testNames, IList<string> testPackages);

        [OperationContract]
        Dictionary<string, string> GetConfigurationOptions(string publicationHandlerType, int bankId, IList<string> testNames, IList<string> testPackageNames);

        [OperationContract]
        string Publicize(string publicationHandlerType, Dictionary<string, string> configurationOptions, int bankId, IList<string> testNames, IList<string> testPackageNames, bool isForPreview, string customName);

        [OperationContract]
        PublicationTaskProgress GetProgress(string taskId);

        [OperationContract]
        void FinishPublication(string taskId);

        [OperationContract]
        String GetItemOutput(string publicationHandlerType, int bankId, string itemCode);

        [OperationContract]
        List<ConceptProcessingLabelEntry> GetConceptRelatedResponseProcessingForReportingPurposes(string publicationHandlerType, Guid itemResourceId);
    }
}
