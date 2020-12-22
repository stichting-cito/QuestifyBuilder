using System.ComponentModel;
using System.Windows;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.ConceptStructureWorkSpace, typeof(ItemConceptStructureView))]
    public partial class ItemConceptStructureView : IWorkSpaceAware
    {
        public ItemConceptStructureView()
        {
            InitializeComponent();
        }

        ~ItemConceptStructureView()
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
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(ItemConceptStructureView),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }
    }
}
