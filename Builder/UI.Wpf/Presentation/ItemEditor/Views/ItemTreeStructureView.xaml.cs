using System.ComponentModel;
using System.Windows;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.TreeStructureWorkSpace, typeof(ItemTreeStructureView))]
    public partial class ItemTreeStructureView : IWorkSpaceAware
    {
        public ItemTreeStructureView()
        {
            InitializeComponent();
        }

        ~ItemTreeStructureView()
        {
            try
            {
                Dispatcher.InvokeIfRequired(() => ((ICinchDisposable)DataContext).Dispose());
            }
            catch (InvalidAsynchronousStateException ex)
            {
            }
        }

        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(ItemTreeStructureView),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
