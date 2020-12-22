using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.concrete
{
    [ViewnameToViewLookupKeyMetadata(Constants.ScoreEditor.TimeGap, typeof(TimeGapScoreEditor))]
    public partial class TimeGapScoreEditor : UserControl, IWorkSpaceAware
    {
        public TimeGapScoreEditor()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(TimeGapScoreEditor),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
