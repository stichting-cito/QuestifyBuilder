using System.ComponentModel.Composition;
using System.Windows;
using Cinch;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.Controls;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [ExportService(ServiceType.Both, typeof(ICustomMessageBoxService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CustomMessageBoxService : ICustomMessageBoxService
    {
        private string _caption;

        private string Caption
        {
            get
            {
                if (string.IsNullOrEmpty(_caption))
                {
                    var findResource = Application.Current.FindResource("Application.Name");
                    if (findResource != null)
                        _caption = findResource.ToString();
                }
                return _caption;
            }
        }

        public void ShowError(string message)
        {
            ShowMessage(message, "Error", CustomDialogIcons.Stop);
        }

        public void ShowError(string message, string caption)
        {
            ShowMessage(message, caption, CustomDialogIcons.Stop);
        }

        public void ShowError(string message, string caption, CustomDialogIcons icon)
        {
            ShowMessage(message, caption, icon);
        }

        public void ShowInformation(string message)
        {
            ShowMessage(message, Caption, CustomDialogIcons.Information);
        }

        public void ShowInformation(string message, string caption)
        {
            ShowMessage(message, caption, CustomDialogIcons.Information);
        }

        public void ShowInformation(string message, string caption, CustomDialogIcons icon)
        {
            ShowMessage(message, caption, icon);
        }

        public void ShowWarning(string message)
        {
            ShowMessage(message, Caption, CustomDialogIcons.Warning);
        }

        public void ShowWarning(string message, string caption)
        {
            ShowMessage(message, caption, CustomDialogIcons.Warning);
        }

        public void ShowWarning(string message, string caption, CustomDialogIcons icon)
        {
            ShowMessage(message, caption, icon);
        }

        public CustomDialogResults ShowYesNo(string message, CustomDialogIcons icon)
        {
            return ShowQuestionWithButton(message, icon, CustomDialogButtons.YesNo);
        }

        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon)
        {
            return ShowQuestionWithButton(message, caption, icon, CustomDialogButtons.YesNo);
        }

        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon,
            CustomDialogResults defaultResult)
        {
            return ShowQuestionWithButton(message, caption, icon, CustomDialogButtons.YesNo, defaultResult);
        }

        public CustomDialogResults ShowYesNoCancel(string message, CustomDialogIcons icon)
        {
            return ShowQuestionWithButton(message, icon, CustomDialogButtons.YesNoCancel);
        }

        public CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon)
        {
            return ShowQuestionWithButton(message, caption, icon, CustomDialogButtons.YesNoCancel);
        }

        public CustomDialogResults ShowYesNoCancel(
            string message,
            string caption,
            CustomDialogIcons icon,
            CustomDialogResults defaultResult)
        {
            return ShowQuestionWithButton(message, caption, icon, CustomDialogButtons.YesNoCancel, defaultResult);
        }

        public CustomDialogResults ShowOkCancel(string message, CustomDialogIcons icon)
        {
            return ShowQuestionWithButton(message, icon, CustomDialogButtons.OKCancel);
        }

        public CustomDialogResults ShowOkCancel(string message, string caption, CustomDialogIcons icon)
        {
            return ShowQuestionWithButton(message, caption, icon, CustomDialogButtons.OKCancel);
        }

        public CustomDialogResults ShowOkCancel(
            string message,
            string caption,
            CustomDialogIcons icon,
            CustomDialogResults defaultResult)
        {
            return ShowQuestionWithButton(message, caption, icon, CustomDialogButtons.OKCancel, defaultResult);
        }

        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon, string yesText,
            string noText)
        {
            return ShowCustomYesNo(message, caption, icon, yesText, noText);
        }

        public CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon,
            string yesText,
            string noText, string cancelText)
        {
            return ShowCustomYesNoCancel(message, caption, icon, yesText, noText, cancelText);
        }

        private void ShowMessage(string message, string caption, CustomDialogIcons icon)
        {
            var cap = string.IsNullOrEmpty(caption) ? Caption : caption;
            var messageBox = new CustomMessageBox(message, cap, MessageBoxButton.OK, GetImage(icon));
            messageBox.ShowDialog();
        }

        private CustomDialogResults ShowQuestionWithButton(
            string message,
            CustomDialogIcons icon,
            CustomDialogButtons button)
        {
            var messageBox = new CustomMessageBox(message, Caption, GetButton(button), GetImage(icon));
            messageBox.ShowDialog();
            return GetResult(messageBox.Result);
        }

        private CustomDialogResults ShowCustomYesNo(
            string message,
            string caption,
            CustomDialogIcons icon,
            string yesText,
            string noText)
        {
            var cap = string.IsNullOrEmpty(caption) ? Caption : caption;
            var messageBox = new CustomMessageBox(message, cap, GetButton(CustomDialogButtons.YesNo), GetImage(icon))
            {
                YesButtonText = yesText,
                NoButtonText = noText
            };

            messageBox.ShowDialog();
            return GetResult(messageBox.Result);
        }

        private CustomDialogResults ShowCustomYesNoCancel(
            string message,
            string caption,
            CustomDialogIcons icon,
            string yesText,
            string noText,
            string cancelText)
        {
            var cap = string.IsNullOrEmpty(caption) ? Caption : caption;
            var messageBox = new CustomMessageBox(message, cap, GetButton(CustomDialogButtons.YesNoCancel),
                GetImage(icon))
            {
                YesButtonText = yesText,
                NoButtonText = noText
            };

            if (!string.IsNullOrEmpty(cancelText))
                messageBox.CancelButtonText = cancelText;

            messageBox.ShowDialog();
            return GetResult(messageBox.Result);
        }

        private CustomDialogResults ShowQuestionWithButton(
            string message,
            string caption,
            CustomDialogIcons icon,
            CustomDialogButtons button)
        {
            var cap = string.IsNullOrEmpty(caption) ? Caption : caption;
            var messageBox = new CustomMessageBox(message, cap, GetButton(button), GetImage(icon));
            messageBox.ShowDialog();
            return GetResult(messageBox.Result);
        }

        private CustomDialogResults ShowQuestionWithButton(
            string message,
            string caption,
            CustomDialogIcons icon,
            CustomDialogButtons button,
            CustomDialogResults defaultResult)
        {
            var cap = string.IsNullOrEmpty(caption) ? Caption : caption;
            var messageBox = new CustomMessageBox(message, cap, GetButton(button), GetImage(icon));
            messageBox.ShowDialog();
            return GetResult(messageBox.Result);
        }

        private MessageBoxImage GetImage(CustomDialogIcons icon)
        {
            var result = MessageBoxImage.None;
            switch (icon)
            {
                case CustomDialogIcons.Information:
                    result = MessageBoxImage.Asterisk;
                    break;
                case CustomDialogIcons.Question:
                    result = MessageBoxImage.Question;
                    break;
                case CustomDialogIcons.Exclamation:
                case CustomDialogIcons.Warning:
                    result = MessageBoxImage.Exclamation;
                    break;
                case CustomDialogIcons.Stop:
                    result = MessageBoxImage.Hand;
                    break;
            }
            return result;
        }

        private MessageBoxButton GetButton(CustomDialogButtons btn)
        {
            var result = MessageBoxButton.OK;
            switch (btn)
            {
                case CustomDialogButtons.OK:
                    result = MessageBoxButton.OK;
                    break;
                case CustomDialogButtons.OKCancel:
                    result = MessageBoxButton.OKCancel;
                    break;
                case CustomDialogButtons.YesNo:
                    result = MessageBoxButton.YesNo;
                    break;
                case CustomDialogButtons.YesNoCancel:
                    result = MessageBoxButton.YesNoCancel;
                    break;
            }
            return result;
        }

        private CustomDialogResults GetResult(MessageBoxResult result)
        {
            var result2 = CustomDialogResults.None;
            switch (result)
            {
                case MessageBoxResult.None:
                    result2 = CustomDialogResults.None;
                    break;
                case MessageBoxResult.OK:
                    result2 = CustomDialogResults.OK;
                    break;
                case MessageBoxResult.Cancel:
                    result2 = CustomDialogResults.Cancel;
                    break;
                case MessageBoxResult.Yes:
                    result2 = CustomDialogResults.Yes;
                    break;
                case MessageBoxResult.No:
                    result2 = CustomDialogResults.No;
                    break;
            }
            return result2;
        }
    }
}