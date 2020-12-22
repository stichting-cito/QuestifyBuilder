using System.Collections.Generic;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public enum FinishAction
    {
        NOTHING,
        CLOSE,
        NEXT
    }

    public enum CloseMethod
    {
        CANCEL,
        CLOSE
    }

    public interface IWizardViewModel
    {
        void ExecuteNext();

        void ExecutePrevious();

        void ExecuteCancel();

        void ExecuteFinish();

        void ExecuteClose();

        object Tag { get; set; }

        void NotifyPageViewLoaded();

        void SetPages(IEnumerable<string> viewLookupKeys);

        void SetPages(IEnumerable<WizardPageWorkspaceData> workspaces);
    }
}
