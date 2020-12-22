namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public class WizardSubPageWorkspaceData : WizardPageWorkspaceData
    {
        public IWizardParentPageViewModel ParentViewModel { get; set; }

        public WizardSubPageWorkspaceData(string viewLookupKey) : base(viewLookupKey)
        {

        }
    }
}
