using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.WizardDefaultPageWorkSpace, typeof(WizardDefaultPage))]
    public partial class WizardDefaultPage : UserControl, IWorkSpaceAware
    {
        public WizardDefaultPage()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(WizardDefaultPage),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
