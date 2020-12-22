namespace Questify.Builder.Logic.Service.Interfaces.UI
{
    public class Handlers
    {
        public delegate void IsButtonCheckedChangedEventHandler(object sender, ButtonCheckChangedEventArgs e);

        public delegate void CurrentStyleChangedEventHandler(object sender, System.EventArgs e);

        public delegate void CurrentLanguageChangedEventHandler(object sender, System.EventArgs e);

        public delegate void SelectionChangedEventHandler(object sender, System.EventArgs e);

    }
}
