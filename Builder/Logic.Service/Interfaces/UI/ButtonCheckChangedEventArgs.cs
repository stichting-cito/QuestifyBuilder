namespace Questify.Builder.Logic.Service.Interfaces.UI
{
    public class ButtonCheckChangedEventArgs : System.EventArgs
    {
        public Button Button { get; set; }

        public bool IsChecked { get; set; }

        public ButtonCheckChangedEventArgs(Button btn, bool @checked)
        {
            Button = btn;
            IsChecked = @checked;
        }
    }
}
