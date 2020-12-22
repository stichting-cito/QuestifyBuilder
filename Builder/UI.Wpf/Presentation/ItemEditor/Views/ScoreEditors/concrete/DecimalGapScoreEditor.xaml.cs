using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.concrete
{
    [ViewnameToViewLookupKeyMetadata(Constants.ScoreEditor.DecimalGap, typeof(DecimalGapScoreEditor))]
    public partial class DecimalGapScoreEditor : UserControl, IWorkSpaceAware
    {
        public DecimalGapScoreEditor()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(DecimalGapScoreEditor),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
