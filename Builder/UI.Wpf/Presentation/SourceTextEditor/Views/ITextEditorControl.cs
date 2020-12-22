using Cinch;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.ViewModels;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.Views
{
    interface ITextEditorControl : IWorkSpaceAware, ICommandSupport
    {
        void SetSourceTextEditorViewModel(ISourceTextEditorViewModel sourcetextVM);
    }
}
