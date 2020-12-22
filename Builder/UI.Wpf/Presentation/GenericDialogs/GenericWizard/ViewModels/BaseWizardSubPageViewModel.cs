using System.ComponentModel.Composition;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public abstract class BaseWizardSubPageViewModel : BaseWizardPageViewModel
    {
        protected IWizardParentPageViewModel _parentViewModel;

        [ImportingConstructor]
        public BaseWizardSubPageViewModel(IViewAwareStatus viewAwareStatus) : base(viewAwareStatus)
        {
            _viewAwareStatus = viewAwareStatus;
            _viewAwareStatus.ViewLoaded += _viewAwareStatusService_ViewLoaded;
        }

        private void _viewAwareStatusService_ViewLoaded()
        {
            var view = (IWorkSpaceAware)_viewAwareStatus.View;
            _parentViewModel = ((WizardSubPageWorkspaceData)view.WorkSpaceContextualData).ParentViewModel;
            _parentViewModel.NotifySubPageViewLoaded();
            _wizardViewModel.NotifyPageViewLoaded();
        }
    }
}
