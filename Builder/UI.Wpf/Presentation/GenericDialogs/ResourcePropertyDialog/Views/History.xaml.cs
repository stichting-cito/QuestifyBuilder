using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.HistoryWorkSpace, typeof(History))]
    public partial class History : UserControl, IWorkSpaceAware
    {
        public History()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(History),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }


    }
}
