using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.AnimateResourceMovingProcess, typeof(AnimateResourceMovingProcess))]
    public partial class AnimateResourceMovingProcess : UserControl, IWorkSpaceAware
    {
        public AnimateResourceMovingProcess()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(AnimateResourceMovingProcess),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }
    }
}
