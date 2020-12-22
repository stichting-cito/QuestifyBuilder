using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.AnnouncementDialog.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.SendAnnouncementWorkspace, typeof(SendAnnouncementView))]
    public partial class SendAnnouncementView : UserControl, IWorkSpaceAware
    {
        public SendAnnouncementView()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(SendAnnouncementView),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
