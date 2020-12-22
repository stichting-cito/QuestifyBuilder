using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.Views;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.ViewModels
{
    [ExportViewModel("SourceTextEditor.TextEditorVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class TextEditorViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {



        private ITextEditorControl _view;
        private ISourceTextEditorViewModel _sourceTextEditorVM;
        private readonly IViewAwareStatus _viewAwareStatusService;



        [ImportingConstructor]
        public TextEditorViewModel(IViewAwareStatus viewAwareStatusService)
            : base()
        {
            _viewAwareStatusService = viewAwareStatusService;

            viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            viewAwareStatusService.ViewUnloaded += ViewAwareStatusServiceViewUnloaded;
        }

        void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                _view = (ITextEditorControl)view;
                IWorkSpaceAware workspaceData = (IWorkSpaceAware)view;
                _sourceTextEditorVM = (ISourceTextEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                _view.SetSourceTextEditorViewModel(_sourceTextEditorVM);
            }
        }

        void ViewAwareStatusServiceViewUnloaded()
        {
            _view.DoPreSaveTasks();
        }





        public void DoPreSaveTasks()
        {
            ICommandSupport CmdSupp = _view as ICommandSupport;
            if (CmdSupp != null)
                CmdSupp.DoPreSaveTasks();
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void KillView() { }
        public void DoPostSaveTasks() { }


    }
}
