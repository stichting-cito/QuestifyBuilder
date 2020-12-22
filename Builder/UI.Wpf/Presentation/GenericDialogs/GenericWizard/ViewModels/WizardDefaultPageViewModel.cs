using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.ViewModelLocator;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    [ExportViewModel("WizardDefaultPageVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal class WizardDefaultPageViewModel : WizardParentPageViewModel
    {

        public string Description { get; set; }

        private static PropertyChangedEventArgs DescriptionArgs = ObservableHelper.CreateArgs<WizardDefaultPageViewModel>(x => x.Description);
        [ImportingConstructor]
        public WizardDefaultPageViewModel(IViewAwareStatus viewAwareStatus) : base(viewAwareStatus)
        {
            _viewAwareStatus.ViewLoaded += wizardDefault_viewAwareStatus_ViewLoaded;
        }

        private void wizardDefault_viewAwareStatus_ViewLoaded()
        {
            var view = (IWorkSpaceAware)_viewAwareStatus.View;
            var workspaceData = (WizardDefaultPageWorkspaceData)view.WorkSpaceContextualData;

            Description = workspaceData.Description;
            NotifyPropertyChanged(DescriptionArgs);
        }
    }
}
