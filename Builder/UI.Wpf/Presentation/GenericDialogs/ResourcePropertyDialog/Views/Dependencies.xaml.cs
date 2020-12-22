using System.Windows;
using System.Windows.Controls;
using Cinch;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.DependenciesWorkSpace, typeof(Dependencies))]
    public partial class Dependencies : UserControl, IWorkSpaceAware
    {
        public Dependencies()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(Dependencies),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModelInstance = ((DependenciesViewModel)WorkSpaceContextualData.ViewModelInstance);

            foreach (DependentResourceViewModel removedItem in e.RemovedItems)
                viewModelInstance.SelectedItems.Remove(removedItem.Entity);

            foreach (DependentResourceViewModel addedItem in e.AddedItems)
                viewModelInstance.SelectedItems.Add(addedItem.Entity);
        }
    }
}
