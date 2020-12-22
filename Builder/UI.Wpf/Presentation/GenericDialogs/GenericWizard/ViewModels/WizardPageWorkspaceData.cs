using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public class WizardPageWorkspaceData : WorkspaceData
    {
        internal IWizardViewModel WizardViewModel { get; set; }

        public WizardPageWorkspaceData(string viewLookupKey) : base(string.Empty, viewLookupKey, null, string.Empty, false)
        {
        }
    }
}
