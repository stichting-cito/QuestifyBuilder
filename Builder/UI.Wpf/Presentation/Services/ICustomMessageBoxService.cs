using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    public interface ICustomMessageBoxService : IMessageBoxService
    {
        CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon, string yesText,
                                      string noText);

        CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon, string yesText,
                                      string noText, string cancelText);

        void ShowInformation(string message, string caption, CustomDialogIcons icon);
        void ShowWarning(string message, string caption, CustomDialogIcons icon);
        void ShowError(string message, string caption, CustomDialogIcons icon);
    }
}