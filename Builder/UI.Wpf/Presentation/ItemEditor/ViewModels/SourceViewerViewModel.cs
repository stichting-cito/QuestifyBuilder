using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    [ExportViewModel("ItemEditor.SourceViewerVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal class SourceViewerViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {

        private readonly IViewAwareStatus _viewAwareStatusService;
        private IItemEditorViewModel _itemEditorVm; private ISourceControl _view;


        [ImportingConstructor]
        public SourceViewerViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }

        private void viewAwareStatusService_ViewLoaded()
        {
            if (Designer.IsInDesignMode) return;

            var view = _viewAwareStatusService.View;
            var workspaceData = (IWorkSpaceAware)view;
            _view = (ISourceControl)view;
            _itemEditorVm = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;

            if (_itemEditorVm.HasError.DataValue) return; _view.SetAssessmentItem(_itemEditorVm.AssessmentItem.DataValue);

            _itemEditorVm.EnableElementsOnCompletion();
        }


        public void DoPreSaveTasks()
        {
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void DoPostSaveTasks()
        {

        }

        public void KillView()
        {
            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }
        }

        protected override void OnDispose()
        {
            _itemEditorVm = null;

            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }

            base.OnDispose();
        }
    }
}
