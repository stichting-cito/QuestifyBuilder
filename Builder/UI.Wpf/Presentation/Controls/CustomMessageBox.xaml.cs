using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Questify.Builder.UI.Wpf.Presentation.Controls
{
    public partial class CustomMessageBox
    {
        internal CustomMessageBox(string message)
        {
            InitializeComponent();

            Message = message;
            Image_MessageBox.Visibility = Visibility.Collapsed;
            DisplayButtons(MessageBoxButton.OK);
        }

        internal CustomMessageBox(string message, string caption)
        {
            InitializeComponent();

            Message = message;
            Caption = caption;
            Image_MessageBox.Visibility = Visibility.Collapsed;
            DisplayButtons(MessageBoxButton.OK);
        }

        internal CustomMessageBox(string message, string caption, MessageBoxButton button)
        {
            InitializeComponent();

            Message = message;
            Caption = caption;
            Image_MessageBox.Visibility = Visibility.Collapsed;

            DisplayButtons(button);
        }

        internal CustomMessageBox(string message, string caption, MessageBoxImage image)
        {
            InitializeComponent();

            Message = message;
            Caption = caption;
            DisplayImage(image);
            DisplayButtons(MessageBoxButton.OK);
        }

        internal CustomMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage image)
        {
            InitializeComponent();

            Message = message;
            Caption = caption;
            Image_MessageBox.Visibility = Visibility.Collapsed;

            DisplayButtons(button);
            DisplayImage(image);
        }

        internal string Caption
        {
            get { return Title; }
            set { Title = value; }
        }

        internal string Message
        {
            get { return TextBlock_Message.Text; }
            set { TextBlock_Message.Text = value; }
        }

        internal string OkButtonText
        {
            get { return Label_Ok.Content.ToString(); }
            set { Label_Ok.Content = value.TryAddKeyboardAccellerator(); }
        }

        internal string CancelButtonText
        {
            get { return Label_Cancel.Content.ToString(); }
            set { Label_Cancel.Content = value.TryAddKeyboardAccellerator(); }
        }

        internal string YesButtonText
        {
            get { return Label_Yes.Content.ToString(); }
            set { Label_Yes.Content = value.TryAddKeyboardAccellerator(); }
        }

        internal string NoButtonText
        {
            get { return Label_No.Content.ToString(); }
            set { Label_No.Content = value.TryAddKeyboardAccellerator(); }
        }

        public MessageBoxResult Result { get; set; }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            Util.RemoveIcon(this);
        }

        private void DisplayButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OKCancel:
                    Button_OK.Visibility = Visibility.Visible;
                    Button_Cancel.Visibility = Visibility.Visible;
                    Button_Cancel.IsDefault = true;

                    Button_Yes.Visibility = Visibility.Collapsed;
                    Button_No.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    Button_Yes.Visibility = Visibility.Visible;
                    Button_Yes.IsDefault = true;
                    Button_No.Visibility = Visibility.Visible;

                    Button_OK.Visibility = Visibility.Collapsed;
                    Button_Cancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNoCancel:
                    Button_Yes.Visibility = Visibility.Visible;
                    Button_Yes.IsDefault = true;
                    Button_No.Visibility = Visibility.Visible;
                    Button_Cancel.Visibility = Visibility.Visible;

                    Button_OK.Visibility = Visibility.Collapsed;

                    break;
                default:
                    Button_OK.Visibility = Visibility.Visible;
                    Button_OK.IsDefault = true;

                    Button_Yes.Visibility = Visibility.Collapsed;
                    Button_No.Visibility = Visibility.Collapsed;
                    Button_Cancel.Visibility = Visibility.Collapsed;
                    break;
            }

            FocusDefaultButton();
        }

        void FocusDefaultButton()
        {
            if (Button_OK.IsDefault) Button_OK.Focus();
            if (Button_Yes.IsDefault) Button_Yes.Focus();
            if (Button_No.IsDefault) Button_No.Focus();
            if (Button_Cancel.IsDefault) Button_Cancel.Focus();
        }

        private void DisplayImage(MessageBoxImage image)
        {
            Icon icon;

            switch (image)
            {
                case MessageBoxImage.Exclamation:
                    icon = SystemIcons.Exclamation;
                    break;
                case MessageBoxImage.Error:
                    icon = SystemIcons.Hand;
                    break;
                case MessageBoxImage.Information:
                    icon = SystemIcons.Information;
                    break;
                case MessageBoxImage.Question:
                    icon = SystemIcons.Question;
                    break;
                default:
                    icon = SystemIcons.Information;
                    break;
            }

            Image_MessageBox.Source = icon.ToImageSource();
            Image_MessageBox.Visibility = Visibility.Visible;
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
        }

        private void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
        }

        private void Button_No_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
        }
    }

    internal static class Util
    {
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                   int x, int y, int width, int height, uint flags);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_DLGMODALFRAME = 0x0001;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_FRAMECHANGED = 0x0020;
        const uint WM_SETICON = 0x0080;

        public static void RemoveIcon(Window window)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);

            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE |
      SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }

        internal static ImageSource ToImageSource(this Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }

        internal static string TryAddKeyboardAccellerator(this string input)
        {
            const string accellerator = "_";
            if (input.Contains(accellerator)) return input;

            return accellerator + input;
        }
    }
}