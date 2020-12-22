using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.ReferencesWorkSpace, typeof(References))]
    public partial class References : UserControl, IWorkSpaceAware
    {
        public References()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(References),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
