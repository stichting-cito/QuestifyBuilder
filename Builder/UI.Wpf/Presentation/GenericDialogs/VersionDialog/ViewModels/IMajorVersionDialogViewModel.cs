using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.VersionDialog.ViewModels
{
    internal interface IMajorVersionDialogViewModel
    {
        SimpleCommand<object, object> OkCommand { get; }
        SimpleCommand<object, object> CancelCommand { get; }

        DataWrapper<string> Label { get; }
    }
}
