namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public class WizardDefaultPageWorkspaceData : WizardParentPageWorkspaceData
    {
        internal string Description { get; set; }

        public WizardDefaultPageWorkspaceData(string description, WizardSubPageWorkspaceData subPageWorkspaceData)
    : base(Constants.WizardDefaultPageWorkSpace, subPageWorkspaceData)
        {
            Description = description;
            SubPageWorkspaceData = subPageWorkspaceData;
        }
    }
}
