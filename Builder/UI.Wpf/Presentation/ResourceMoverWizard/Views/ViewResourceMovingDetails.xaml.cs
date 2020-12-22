using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.ViewResourceMovingDetails, typeof(ViewResourceMovingDetails))]
    public partial class ViewResourceMovingDetails : UserControl, IWorkSpaceAware
    {
        public ViewResourceMovingDetails()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(ViewResourceMovingDetails),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }
    }
}
