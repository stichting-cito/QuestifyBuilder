using System;
using System.Windows;
using System.Windows.Controls;
using Cinch;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.SourceWorkSpace, typeof(SourceViewerControl))]
    public partial class SourceViewerControl : UserControl, ISourceControl
    {

        private AssessmentItem _assessmentItem;



        public SourceViewerControl()
        {
            InitializeComponent();
        }



        public void SetAssessmentItem(AssessmentItem assessmentItem)
        {
            _assessmentItem = assessmentItem;
            ItemSourceCtrl.AssessmentItem = _assessmentItem;
            ItemSourceCtrl.RenderSource(true);
        }



        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(SourceViewerControl),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        private bool _disposed;

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

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (ItemSourceCtrl != null)
                    {
                        ItemSourceCtrl.Dispose();

                        host.Child = null;
                        host.Dispose();
                    }
                }
                _disposed = true;
            }
        }
    }
}
