using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Cinch;
using MEFedMVVM.Common;
using Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.Helpers;
using Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.Views
{
    [ViewnameToViewLookupKeyMetadata(Constants.TextEditorWorkSpace, typeof(TextEditor))]
    internal partial class TextEditor : UserControl, ITextEditorControl
    {

        void parentViewModelUpdated(object sender, StringEventArgs e)
        {
            if (!Designer.IsInDesignMode)
            {
                switch (e.StringValue)
                {
                    case "DoUpdate":
                    case "DoStyleSheetUpdate":
                        UpdateXhtmlEditor();
                        break;
                }
            }
        }



        private bool disposed;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        object _last = new object();

        private ISourceTextEditorViewModel _sourceTextEditorViewModel;



        public TextEditor()
        {
            InitializeComponent();
            if (!Designer.IsInDesignMode)
            {
                Loaded += (s, e) => { Window.GetWindow(this).Closing += (s2, e2) => { if (!e2.Cancel) FocusedEditor.Dispose(); }; };

                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }
        }

        ~TextEditor()
        {
            try
            {
                this.Dispatcher.InvokeIfRequired(() =>
                {
                    ((ICinchDisposable)this.DataContext).Dispose();
                });
            }
            catch (InvalidAsynchronousStateException ex)
            {
            }
            Dispose(false);
        }



        void ITextEditorControl.SetSourceTextEditorViewModel(ISourceTextEditorViewModel sourceTextEditorViewModel)
        {
            _sourceTextEditorViewModel = sourceTextEditorViewModel;

            if (sourceTextEditorViewModel != null)
            {
                sourceTextEditorViewModel.Updated += parentViewModelUpdated;
                if (!_sourceTextEditorViewModel.IsLoading)
                {
                    UpdateXhtmlEditor();
                }
            }
        }

        private void UpdateXhtmlEditor()
        {
            GenericResourceEditorBehaviour behaviour = new GenericResourceEditorBehaviour((GenericResourceEntity)_sourceTextEditorViewModel.GenericResourceEntity.DataValue, _sourceTextEditorViewModel.ResourceManager.DataValue, _sourceTextEditorViewModel.ContextIdentifier.DataValue, true);

            FocusedEditor.Initialize(behaviour);
        }


        public void DoPreSaveTasks()
        {
            FocusedEditor.HtmlEditor.StopEditor();
            FocusedEditor.HtmlEditor.Validate();
            _last = null;
            _sourceTextEditorViewModel.EditorChange(null);
        }

        public void DoTaskBeforeClosing()
        {
            if (FocusedEditor != null && FocusedEditor.HtmlEditor != null)
            {
                FocusedEditor.HtmlEditor.FormClosing = true;
            }
        }

        public void DoPostSaveTasks() { }




        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(TextEditor),
        new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }


        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (FocusedEditor.HtmlEditor.ContainsFocus)
            {
                var focused = FocusedControlHelper.GetFocusedControl();
                object _now = FocusedControlHelper.FindFocusedControl2(focused, 6);
                if (_now != null && !object.ReferenceEquals(_last, _now))
                {
                    _last = _now;
                    _sourceTextEditorViewModel.EditorChange(_now);
                }
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dispatcherTimer.Stop();
                    dispatcherTimer = null;
                }
                disposed = true;
            }
        }

    }
}
