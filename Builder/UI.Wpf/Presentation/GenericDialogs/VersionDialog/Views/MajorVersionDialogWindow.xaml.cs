using System.Windows;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.VersionDialog.Views
{
    [PopupNameToViewLookupKeyMetadata("MajorVersionDialog", typeof(MajorVersionDialogWindow))]
    internal partial class MajorVersionDialogWindow : Window
    {
        public MajorVersionDialogWindow()
        {
            InitializeComponent();
        }
    }
}
