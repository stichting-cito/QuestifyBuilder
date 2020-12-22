using System.ComponentModel.Composition;
using Cinch;
using FakeItEasy;
using MEFedMVVM.ViewModelLocator;

namespace Questify.Builder.UnitTests.Fakes
{
    [PartCreationPolicy(CreationPolicy.Shared), ExportService(ServiceType.Both, typeof(IMessageBoxService))]
    public class FakeMessageBoxService : IMessageBoxService
    {


        static IMessageBoxService _fake;

        public static IMessageBoxService MakeNewFake()
        {
            _fake = A.Fake<IMessageBoxService>();
            return _fake;
        }



        public FakeMessageBoxService()
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

        public void ShowInformation(string message, string caption)
        {
            _fake.ShowInformation(message, caption);
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
            return ShowYesNoCancel(message, caption, icon);
        }

        public CustomDialogResults ShowYesNoCancel(string message, CustomDialogIcons icon)
        {
            return _fake.ShowYesNoCancel(message, icon);
        }

    }
}
