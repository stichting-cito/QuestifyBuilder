namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.LoginDialog.Views
{
    public partial class LoginDialogView
    {
        public LoginDialogView()
        {
            InitializeComponent();

            Loaded += (sender, args) => Activate();
        }
    }
}
