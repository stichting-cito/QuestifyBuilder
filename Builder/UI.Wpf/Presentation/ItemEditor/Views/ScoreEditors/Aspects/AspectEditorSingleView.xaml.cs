using System;
using System.Windows;
using System.Windows.Controls;
using Cinch;
using MEFedMVVM.Common;
using Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior;
using Questify.Builder.UI.Wpf.Presentation.Helpers;
using Questify.Builder.UI.Wpf.Presentation.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    [ViewnameToViewLookupKeyMetadata(Constants.AspectScoringSingleWorkSpace, typeof(AspectEditorSingleView))]
    public partial class AspectEditorSingleView : UserControl, IWorkSpaceAware, ICommandSupport
    {
        private bool _disposed;
        private AspectReferenceScoringViewModel _viewModel;
        System.Windows.Threading.DispatcherTimer _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        object _last = new object();

        public static readonly DependencyProperty AspectEditControlUpdatedProperty =
    DependencyProperty.Register("AspectEditControlUpdated", typeof(object), typeof(AspectEditorSingleView), new PropertyMetadata(null));

        public AspectEditorSingleView()
        {
            InitializeComponent();

            if (!Designer.IsInDesignMode)
            {
                _dispatcherTimer.Tick += DispatcherTimer_Tick;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                _dispatcherTimer.Start();
            }
        }

        public void SetViewModel(AspectReferenceScoringViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void UpdateXhtmlEditor(AspectReferenceEditorBehavior behaviour)
        {
            FocusedEditor.Initialize(behaviour);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var resetControls = false;
            if (FocusedEditor.HtmlEditor.ContainsFocus || MaxScoreTextBox.IsFocused)
            {
                var focused = FocusedControlHelper.GetFocusedControl();
                var now = FocusedControlHelper.FindFocusedControl2(focused, 6);
                resetControls = now == null;
                if (!ReferenceEquals(_last, now))
                {
                    _last = now;
                    AspectEditControlUpdated = now;
                }
            }
            else
            {
                if (AspectEditControlUpdated != null)
                {
                    resetControls = true;
                }
            }

            var ribbonFocusInterface = WpfHelper.GetByDependencyObjectByInterface<IRibbonFocus>(this);
            var ribbonSelected = ribbonFocusInterface != null && ribbonFocusInterface.RibbonSelected;

            if (!resetControls || (Application.Current.MainWindow != null && !Application.Current.MainWindow.IsActive) || ribbonSelected)
            {
                return;
            }

            _last = null;
            AspectEditControlUpdated = null;
        }

        public object AspectEditControlUpdated
        {
            get { return GetValue(AspectEditControlUpdatedProperty); }
            set
            {
                SetValue(AspectEditControlUpdatedProperty, value);
                _viewModel.EditControl.DataValue = value;
            }
        }


        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
    DependencyProperty.Register("WorkSpaceContextualData", typeof(WorkspaceData), typeof(AspectEditorSingleView),
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
                    FocusedEditor?.Dispose();
                    WorkSpaceContextualData = null;
                }
                _disposed = true;
            }
        }



        public void DoPreSaveTasks()
        {
            FocusedEditor.HtmlEditor.StopEditor();
            FocusedEditor.HtmlEditor.Validate();
            _last = null;
        }

        public void DoTaskBeforeClosing()
        {
            if (FocusedEditor != null && FocusedEditor.HtmlEditor != null)
            {
                FocusedEditor.HtmlEditor.FormClosing = true;
            }
        }

        public void DoPostSaveTasks() { }


        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.MaxScoreUpdate();
        }
    }
}
