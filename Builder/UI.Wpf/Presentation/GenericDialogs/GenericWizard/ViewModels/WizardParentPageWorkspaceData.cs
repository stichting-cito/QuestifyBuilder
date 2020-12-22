namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public class WizardParentPageWorkspaceData : WizardPageWorkspaceData
    {
        internal WizardSubPageWorkspaceData SubPageWorkspaceData { get; set; }

        public WizardParentPageWorkspaceData(string keyLookup, WizardSubPageWorkspaceData subPageWorkspaceData)
    : base(keyLookup)
        {
        }
    }
}
