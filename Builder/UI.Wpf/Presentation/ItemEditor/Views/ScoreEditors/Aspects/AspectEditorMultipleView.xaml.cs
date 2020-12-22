using Cinch;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    [ViewnameToViewLookupKeyMetadata(Constants.AspectScoringAdvancedWorkSpace, typeof(AspectEditorMultipleView))]
    public partial class AspectEditorMultipleView : UserControl, IWorkSpaceAware, ICommandSupport
    {
        private bool _disposed;
        private AspectReferencesScoringViewModel _viewModel;

        public AspectEditorMultipleView()
        {
            InitializeComponent();
        }

        public void SetViewModel(AspectReferencesScoringViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
            DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(AspectEditorMultipleView),
                new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    WorkSpaceContextualData = null;
                }
                _disposed = true;
            }
        }

        public void DoPostSaveTasks() { }

        public void DoPreSaveTasks()
        {
        }

        public void DoTaskBeforeClosing()
        {
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            ((ListView)sender).SelectedItems.Clear();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModelInstance = ((AspectReferencesScoringViewModel)WorkSpaceContextualData.ViewModelInstance);

            foreach (AspectReferenceViewModel removedItem in e.RemovedItems)
                viewModelInstance.SelectedItems.Remove(removedItem);

            foreach (AspectReferenceViewModel addedItem in e.AddedItems)
                viewModelInstance.SelectedItems.Add(addedItem);

            viewModelInstance.SyncSelectedItems();
            viewModelInstance.EnableButtons();
        }
    }
}
