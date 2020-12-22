using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    [ViewnameToViewLookupKeyMetadata(Constants.ScoreTranslationTableWorkSpace, typeof(ScoreTranslationTableEditorView))]
    public partial class ScoreTranslationTableEditorView : UserControl, IWorkSpaceAware
    {
        public ScoreTranslationTableEditorView()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(ScoreTranslationTableEditorView),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
