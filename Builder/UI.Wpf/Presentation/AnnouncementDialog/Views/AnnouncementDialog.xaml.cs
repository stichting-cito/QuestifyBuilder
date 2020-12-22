using System.Windows;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.AnnouncementDialog.Views
{
    [PopupNameToViewLookupKeyMetadata("AnnouncementDialog", typeof(AnnouncementDialog))]
    internal partial class AnnouncementDialog : Window
    {
        public AnnouncementDialog()
        {
            InitializeComponent();
        }
    }
}
