using System.ComponentModel;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels
{
    public interface IWizardPage : INotifyPropertyChanged
    {
        bool IsValid { get; set; }

        bool? CanExecuteCancel { get; set; }

        bool? CanExecuteNext { get; set; }

        bool? CanExecutePrevious { get; set; }

        bool? CanExecuteFinish { get; set; }

        bool? CanExecuteClose { get; set; }

        FinishAction FinishAction { get; set; }

        CloseMethod CloseMethod { get; set; }

        void OnNext();

        void OnPrevious();

        bool OnFinish();

        void OnCancel();

        bool OnClose();

        string NextPageOverride { get; set; }

        int? SkipPages { get; set; }
    }
}
