using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.Service.EventArguments;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.PresentationWorkSpace, typeof(Presentation))]
    internal partial class Presentation : IPresentationControl, IDisposable
    {
        private bool _disposed;
        ResourceManagerBase _resourceManagerBase;
        AssessmentItem _assessmentItem;
        int? _contextIdentifier;
        private int _bankId;


        public Presentation()
        {
            InitializeComponent();
            preview.ItemValidatingRequired += preview_ItemValidatingRequired;
            preview.RefreshPreview += preview_Refresh;
        }

        void preview_ItemValidatingRequired(object sender, ItemValidationRequiredEventArgs e)
        {
            e.ValidationValid = _assessmentItem.Parameters.ValidateHierarchical();
        }

        void preview_Refresh(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        ~Presentation()
        {
            try
            {
                Dispatcher.InvokeIfRequired(() => ((ICinchDisposable)DataContext).Dispose());
            }
            catch (InvalidAsynchronousStateException ex)
            {
            }
            Dispose(false);
        }



        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(Presentation),
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
                    ParameterSetEditor?.Dispose();
                    if (preview != null)
                    {
                        preview.ItemValidatingRequired -= preview_ItemValidatingRequired;
                        preview.RefreshPreview -= preview_Refresh;
                        preview.StopPreview();
                        preview.Dispose();

                        if (PreviewHost != null)
                        {
                            PreviewHost.Child = null;
                            PreviewHost.Dispose();
                            PreviewHost = null;
                        }
                    }

                    PreviewerHostPanel.Children.Clear();
                    PreviewerHostPanel = null;
                    WorkSpaceContextualData = null;
                }
                _disposed = true;
            }
        }



        public void RefreshPreview()
        {
            preview.StopPreview();
            ParameterSetEditor.editor.ValidateChildren();
            preview.PreviewItem(_assessmentItem, _bankId, _contextIdentifier, _resourceManagerBase);
        }


        ResourceManagerBase IPresentationControl.ResourceManagerBase
        {
            get { return _resourceManagerBase; }
            set
            {
                _resourceManagerBase = value;
            }
        }

        ResourceEntity IPresentationControl.ResourceEntity { get; set; }
        ParameterSetCollection IPresentationControl.ParameterSetCollection { get; set; }

        AssessmentItem IPresentationControl.AssessmentItem
        {
            get { return _assessmentItem; }
            set
            {
                _assessmentItem = value;
            }
        }

        int? IPresentationControl.ContextIdentifier
        {
            get { return _contextIdentifier; }
            set
            {
                _contextIdentifier = value;
            }
        }

        int IPresentationControl.BankId
        {
            get { return _bankId; }
            set
            {
                _bankId = value;
            }
        }

        public void DoPreSaveTasks()
        {
            ParameterSetEditor.editor.PreItemSave();
            ParameterSetEditor.editor.ValidateChildren();
        }

        public void DoTaskBeforeClosing()
        {
            ParameterSetEditor.editor.FormClosing = true;
            Questify.Builder.Configuration.UserSettings.ItemEditorLeftColumnWidth = (int)this.PresentationContentGrid.ColumnDefinitions[0].Width.Value;
            Questify.Builder.Configuration.UserSettings.ItemEditorRightColumnWidth = (int)this.PresentationContentGrid.ColumnDefinitions[2].Width.Value;
        }

        public void DoPostSaveTasks()
        {
            ParameterSetEditor.editor.PostItemSave();
        }


        private void GridSplitter_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (e.OldFocus == null)
                PreviewHost.Focus();
        }




        private void Command_RefreshPreview(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            RefreshPreview();
        }

        private void Command_CanRefreshPreview(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (_assessmentItem != null) && (_resourceManagerBase != null);
        }

    }
}
