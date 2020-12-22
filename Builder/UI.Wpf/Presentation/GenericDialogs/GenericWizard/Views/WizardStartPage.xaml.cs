using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.WizardStartPageWorkSpace, typeof(WizardStartPage))]
    public partial class WizardStartPage : UserControl, IWorkSpaceAware
    {
        public WizardStartPage()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(WizardStartPage),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
