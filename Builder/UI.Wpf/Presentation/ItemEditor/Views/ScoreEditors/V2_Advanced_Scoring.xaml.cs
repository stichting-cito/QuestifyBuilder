using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    [ViewnameToViewLookupKeyMetadata(Constants.ScoringWorkSpaceV2Adv, typeof(V2_Advanced_Scoring))]
    public partial class V2_Advanced_Scoring : UserControl, IWorkSpaceAware
    {


        public V2_Advanced_Scoring()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(V2_Advanced_Scoring),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
