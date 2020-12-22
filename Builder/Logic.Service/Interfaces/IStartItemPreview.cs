using System.Collections.Generic;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.Service.Classes;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IStartItemPreview
    {
        PublicationResult DoPreviewFromServer(string target, AssessmentItem assessmentItem, ResourceManagerBase resourceManager, string exportPath, string url, List<PublicationProperty> publicationProperties);
        void CleanUp();
        void Validate(AssessmentItem assessmentItem, ref string warnings, ref string errors);
    }
}
