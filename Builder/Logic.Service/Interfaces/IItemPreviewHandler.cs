using System.Collections.Generic;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.Service.Classes;
using Questify.Builder.Logic.Service.Enums;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IItemPreviewHandler
    {
        PublicationResult SetupItemPreview(int bankId, AssessmentItem assessmentItem, bool isDebug, List<PublicationProperty> publicationProperties);
        void CleanUp();
        void Validate(AssessmentItem assessmentItem, ref string warnings, ref string errors);
        ResourceManagerBase ResourceManager { get; set; }
        PreviewControl PreviewControl { get; }
        string PreviewTarget { get; }
        string PublicationLocation { get; }
        string UserFriendlyName { get; }
        Dictionary<string, System.Drawing.Size> Dimensions { get; }
        bool ShowTestMonitor { get; }
        string ServiceName { get; }
    }
}
