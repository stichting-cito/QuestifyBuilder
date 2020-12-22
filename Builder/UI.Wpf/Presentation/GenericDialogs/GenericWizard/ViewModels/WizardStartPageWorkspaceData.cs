namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public class WizardStartPageWorkspaceData : WizardPageWorkspaceData
    {
        internal string Header { get; set; }

        internal string Description { get; set; }

        public WizardStartPageWorkspaceData(string header, string description) : base(Constants.WizardStartPageWorkSpace)
        {
            Header = header;
            Description = description;
        }
    }
}
