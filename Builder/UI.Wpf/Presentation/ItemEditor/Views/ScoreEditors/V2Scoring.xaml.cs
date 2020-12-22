using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    [ViewnameToViewLookupKeyMetadata(Constants.ScoringWorkSpaceV2, typeof(V2ScoreEditorControl))]
    public partial class V2ScoreEditorControl : UserControl, IWorkSpaceAware
    {
        public V2ScoreEditorControl()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(V2ScoreEditorControl),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
