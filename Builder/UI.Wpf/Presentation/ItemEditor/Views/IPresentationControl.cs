using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    interface IPresentationControl : IWorkSpaceAware, ICommandSupport
    {
        void RefreshPreview();
        ResourceManagerBase ResourceManagerBase { get; set; }
        ResourceEntity ResourceEntity { get; set; }
        ParameterSetCollection ParameterSetCollection { get; set; }
        AssessmentItem AssessmentItem { get; set; }
        int? ContextIdentifier { get; set; }
        int BankId { get; set; }
        void Dispose();
    }
}
