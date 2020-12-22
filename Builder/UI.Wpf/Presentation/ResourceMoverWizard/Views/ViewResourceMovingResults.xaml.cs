using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.ViewResourceMovingResults, typeof(ViewResourceMovingResults))]
    public partial class ViewResourceMovingResults : UserControl, IWorkSpaceAware
    {
        public ViewResourceMovingResults()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(ViewResourceMovingResults),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }
    }
}
