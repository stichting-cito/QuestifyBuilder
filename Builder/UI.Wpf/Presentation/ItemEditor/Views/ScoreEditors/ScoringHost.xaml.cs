using System;
using System.Windows;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    [ViewnameToViewLookupKeyMetadata(Constants.ScoringWorkSpaceHost, typeof(ScoringHost))]
    public partial class ScoringHost : IDisposable, IWorkSpaceAware
    {
        private bool _disposed;

        public ScoringHost()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(ScoringHost),
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
            if (!_disposed)
            {
                if (disposing)
                {
                }
                _disposed = true;
            }
        }
    }
}
