using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public abstract class WizardParentPageViewModel : BaseWizardPageViewModel, IWizardParentPageViewModel
    {
        protected IWizardPage SubPageViewModel { get; set; }

        public WizardSubPageWorkspaceData SubPageWorkspace { get; set; }

        public override bool? CanExecuteCancel { get { return SubPageViewModel != null ? SubPageViewModel.CanExecuteCancel : base.CanExecuteCancel; } }
        public override bool? CanExecutePrevious { get { return SubPageViewModel != null ? SubPageViewModel.CanExecutePrevious : base.CanExecutePrevious; } }
        public override bool? CanExecuteNext { get { return SubPageViewModel != null ? SubPageViewModel.CanExecuteNext : base.CanExecuteNext; } }
        public override bool? CanExecuteFinish { get { return SubPageViewModel != null ? SubPageViewModel.CanExecuteFinish : base.CanExecuteFinish; } }
        public override bool? CanExecuteClose { get { return SubPageViewModel != null ? SubPageViewModel.CanExecuteClose : base.CanExecuteClose; } }
        public override FinishAction FinishAction { get { return SubPageViewModel != null ? SubPageViewModel.FinishAction : base.FinishAction; } }
        public override CloseMethod CloseMethod { get { return SubPageViewModel != null ? SubPageViewModel.CloseMethod : base.CloseMethod; } }
        public override bool IsValid { get { return SubPageViewModel != null ? SubPageViewModel.IsValid : false; } set { if (SubPageViewModel != null) SubPageViewModel.IsValid = value; } }
        public override string NextPageOverride { get { return SubPageViewModel != null ? SubPageViewModel.NextPageOverride : base.NextPageOverride; } }
        public override int? SkipPages { get { return SubPageViewModel != null ? SubPageViewModel.SkipPages : base.SkipPages; } }
        public override bool OnClose() { return SubPageViewModel != null ? SubPageViewModel.OnClose() : base.OnClose(); }
        public override bool OnFinish() { return SubPageViewModel != null ? SubPageViewModel.OnFinish() : base.OnFinish(); }

        private static PropertyChangedEventArgs SubPageWorkspaceArgs = ObservableHelper.CreateArgs<WizardDefaultPageViewModel>(x => x.SubPageWorkspace);
        [ImportingConstructor]
        public WizardParentPageViewModel(IViewAwareStatus viewAwareStatus) : base(viewAwareStatus)
        {
            _viewAwareStatus.ViewLoaded += wizardParentPage_viewAwareStatus_ViewLoaded;
        }

        private void wizardParentPage_viewAwareStatus_ViewLoaded()
        {
            var view = (IWorkSpaceAware)_viewAwareStatus.View;
            var workspaceData = (WizardParentPageWorkspaceData)view.WorkSpaceContextualData;
            SubPageWorkspace = workspaceData.SubPageWorkspaceData;


            SubPageWorkspace.WizardViewModel = workspaceData.WizardViewModel;
            SubPageWorkspace.ParentViewModel = this;

            NotifyPropertyChanged(SubPageWorkspaceArgs);
        }

        public void NotifySubPageViewLoaded()
        {
            SubPageViewModel = (IWizardPage)SubPageWorkspace.ViewModelInstance;
            SubPageViewModel.PropertyChanged += (s, e) =>
                {
                    NotifyPropertyChanged(e.PropertyName);
                };
        }
    }
}
