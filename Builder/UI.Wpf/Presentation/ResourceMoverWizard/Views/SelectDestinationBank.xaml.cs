using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.SelectDestinationBankWorkSpace, typeof(SelectDestinationBank))]
    public partial class SelectDestinationBank : UserControl, IWorkSpaceAware
    {
        public SelectDestinationBank()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(SelectDestinationBank),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

        private void BankTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            tbSelectectBankId.Text = BankTree.SelectedValue.ToString();
        }

    }
}
