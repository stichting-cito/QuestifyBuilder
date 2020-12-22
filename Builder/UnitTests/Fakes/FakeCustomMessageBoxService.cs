using System.ComponentModel.Composition;
using Cinch;
using FakeItEasy;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UnitTests.Fakes
{
    [PartCreationPolicy(CreationPolicy.Shared), ExportService(ServiceType.Both, typeof(ICustomMessageBoxService))]
    public class FakeCustomMessageBoxService : ICustomMessageBoxService
    {

        static ICustomMessageBoxService _fake;

        public static ICustomMessageBoxService MakeNewFake()
        {
            return _fake;
        }



        static FakeCustomMessageBoxService()
        {
            if (_fake == null)
                _fake = A.Fake<ICustomMessageBoxService>();
        }

        public FakeCustomMessageBoxService()
        {
        }



        public void ShowError(string message, string caption)
        {
            _fake.ShowError(message, caption);
        }

        public void ShowError(string message)
        {
            _fake.ShowError(message);
        }

        public void ShowError(string message, string caption, CustomDialogIcons icon)
        {
            _fake.ShowError(message, caption, icon);
        }

        public void ShowInformation(string message, string caption)
        {
            _fake.ShowInformation(message, caption);
        }

        public void ShowInformation(string message, string caption, CustomDialogIcons icon)
        {
            _fake.ShowInformation(message, caption, icon);
        }

        public void ShowInformation(string message)
        {
            _fake.ShowInformation(message);
        }

        public CustomDialogResults ShowOkCancel(string message, string caption, CustomDialogIcons icon, CustomDialogResults defaultResult)
        {
            return _fake.ShowOkCancel(message, caption, icon, defaultResult);
        }

        public CustomDialogResults ShowOkCancel(string message, string caption, CustomDialogIcons icon)
        {
            return _fake.ShowOkCancel(message, caption, icon);
        }

        public CustomDialogResults ShowOkCancel(string message, CustomDialogIcons icon)
        {
            return _fake.ShowOkCancel(message, icon);
        }

        public void ShowWarning(string message, string caption)
        {
            _fake.ShowWarning(message, caption);
        }

        public void ShowWarning(string message)
        {
            _fake.ShowWarning(message);
        }

        public void ShowWarning(string message, string caption, CustomDialogIcons icon)
        {
            _fake.ShowWarning(message, caption, icon);
        }

        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon, CustomDialogResults defaultResult)
        {
            return _fake.ShowYesNo(message, caption, icon, defaultResult);
        }

        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon)
        {
            return _fake.ShowYesNo(message, caption, icon);
        }

        public CustomDialogResults ShowYesNo(string message, CustomDialogIcons icon)
        {
            return _fake.ShowYesNo(message, icon);
        }

        public CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon, CustomDialogResults defaultResult)
        {
            return _fake.ShowYesNoCancel(message, caption, icon, defaultResult);
        }

        public CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon)
        {
            return _fake.ShowYesNoCancel(message, caption, icon);
        }

        public CustomDialogResults ShowYesNoCancel(string message, CustomDialogIcons icon)
        {
            return _fake.ShowYesNoCancel(message, icon);
        }

        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon, string yesText, string noText)
        {
            return _fake.ShowYesNo(message, caption, icon, yesText, noText);
        }

        public CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon, string yesText,
                                                   string noText, string cancelText)
        {
            return _fake.ShowYesNoCancel(message, caption, icon, yesText, noText, cancelText);
        }

    }
}
