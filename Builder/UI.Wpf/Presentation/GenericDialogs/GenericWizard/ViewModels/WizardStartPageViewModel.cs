using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    [ExportViewModel("WizardStartPageVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal class WizardStartPageViewModel : BaseWizardPageViewModel
    {
        private string _header;

        private string _description;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                NotifyPropertyChanged(HeaderArgs);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged(DescriptionArgs);
            }
        }

        public override bool IsValid { get { return true; } set { } }

        private static PropertyChangedEventArgs HeaderArgs = ObservableHelper.CreateArgs<WizardStartPageViewModel>(x => x.Header); private static PropertyChangedEventArgs DescriptionArgs = ObservableHelper.CreateArgs<WizardStartPageViewModel>(x => x.Description);
        [ImportingConstructor]
        public WizardStartPageViewModel(IViewAwareStatus viewAwareStatus) : base(viewAwareStatus)
        {
            if (Designer.IsInDesignMode)
            {
                Header = "Header Text";
                Description = "Some description text describing what this wizard will do";
            }

            _viewAwareStatus.ViewLoaded += viewAwareStatusService_ViewLoaded;
        }

        void viewAwareStatusService_ViewLoaded()
        {
            var view = (IWorkSpaceAware)_viewAwareStatus.View;
            var datavalue = (WizardStartPageWorkspaceData)view.WorkSpaceContextualData;
            Header = datavalue.Header;
            Description = datavalue.Description;
        }
    }
}
