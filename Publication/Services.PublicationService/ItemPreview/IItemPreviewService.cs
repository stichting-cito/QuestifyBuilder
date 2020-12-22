using System.Collections.Generic;
using System.ServiceModel;
using Questify.Builder.Logic.Service.Classes;

namespace Questify.Builder.Services.PublicationService.ItemPreview
{
    [ServiceContract]
    public interface IItemPreviewService
    {
        [OperationContract]
        PublicationResult PreviewItemByCode(string itemHandlerType, string target, int bankId, string itemCode, bool isDebug, List<PublicationProperty> publicationProperties);

        [OperationContract]
        PublicationResult PreviewItemByAssessmentItem(string itemHandlerType, string target, int bankId, byte[] assessmentItem, bool isDebug, List<PublicationProperty> publicationProperties);

    }
}
