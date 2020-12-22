using System.Windows;
using System.Windows.Controls;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.GenericControls.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.MetadataWorkSpace, typeof(MetaData))]
    public partial class MetaData : UserControl, IWorkSpaceAware
    {



        public MetaData()
        {
            InitializeComponent();
        }



        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(MetaData),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
