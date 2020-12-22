using System;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    internal class TestCustomMessageBoxService : TestMessageBoxService
    {
        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon, string yesText, string noText)
        {
            if (this.ShowYesNoResponders.Count == 0)
            {
                throw new ApplicationException("TestMessageBoxService ShowYesNo method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");
            }
            Func<CustomDialogResults> func = this.ShowYesNoResponders.Dequeue();
            return func();
        }

        public CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon, string yesText,
                                                   string noText, string cancelText)
        {
            if (this.ShowYesNoResponders.Count == 0)
            {
                throw new ApplicationException("TestMessageBoxService ShowYesNoCancel method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");
            }
            Func<CustomDialogResults> func = this.ShowYesNoCancelResponders.Dequeue();
            return func();
        }
    }
}