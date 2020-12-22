using System;
using Cinch;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    interface ISourceControl : IWorkSpaceAware, IDisposable
    {
        void SetAssessmentItem(AssessmentItem assessmentItem);
    }
}
