using System;
using System.Windows;
using System.Windows.Input;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Logic;
using Questify.Builder.UI.Wpf.Presentation.Helpers;
using Questify.Builder.UI.Wpf.Presentation.Interfaces;
using Control = System.Windows.Forms.Control;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    internal partial class ParameterSetControl : IDisposable
    {
        private bool _disposed;


        public static readonly DependencyProperty ResourceManagerBaseProperty =
    DependencyProperty.Register("ResourceManagerBase", typeof(ResourceManagerBase), typeof(ParameterSetControl), new FrameworkPropertyMetadata(null, UpdateResourceManagerBase));

        public static readonly DependencyProperty ResourceEntityProperty =
    DependencyProperty.Register("ResourceEntity", typeof(ResourceEntity), typeof(ParameterSetControl), new PropertyMetadata(null, UpdateResourceEntity));

        public static readonly DependencyProperty ParameterSetCollectionProperty =
    DependencyProperty.Register("ParameterSetCollection", typeof(ParameterSetCollection), typeof(ParameterSetControl), new PropertyMetadata(null, UpdateParameterSetCollection));

        public static readonly DependencyProperty ContextIdentifierProperty =
    DependencyProperty.Register("ContextIdentifier", typeof(int?), typeof(ParameterSetControl), new PropertyMetadata(null, UpdateContextIdentifier));

        public static readonly DependencyProperty EditControlUpdatedProperty =
    DependencyProperty.Register("EditControlUpdated", typeof(object), typeof(ParameterSetControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IsOldItemProperty =
    DependencyProperty.Register("IsOldItem", typeof(bool), typeof(ParameterSetControl), new PropertyMetadata(false, UpdateIsOldItem));

        public static readonly DependencyProperty LaunchGenericResourceEditorCommandProperty =
        DependencyProperty.Register("LaunchGenericResourceEditorCommand", typeof(ICommand), typeof(ParameterSetControl), new UIPropertyMetadata(null));

        private static void UpdateResourceManagerBase(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ParameterSetControl)d;
            c.editor.ResourceManager = (ResourceManagerBase)e.NewValue;
        }

        private static void UpdateResourceEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ParameterSetControl)d;
            c.editor.ResourceEntity = (ItemResourceEntity)e.NewValue;
        }

        private static void UpdateParameterSetCollection(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ParameterSetControl)d;
            var parametersetCollection = (ParameterSetCollection)e.NewValue;
            c.editor.ShouldSort = parametersetCollection.ShouldSort;
            c.editor.ParameterSets = parametersetCollection;
            var pvm = c.DataContext as ViewModels.PresentationViewModel;
            if (pvm != null)
            {
                c.editor.Solution = pvm.AssessmentItem.DataValue.Solution;
            }
        }

        private static void UpdateContextIdentifier(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ParameterSetControl)d;
            c.editor.ContextIdentifierForEditors = (int?)e.NewValue;
        }

        private static void UpdateIsOldItem(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ParameterSetControl)d;
            c.editor.HasLoadedOldItemLayoutTemplate = (bool)e.NewValue;
        }



        private readonly System.Windows.Threading.DispatcherTimer _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private Control _last = new Control();
        private object _lastParent = new object();


        public ParameterSetControl()
        {
            InitializeComponent();
            if (!Designer.IsInDesignMode)
            {
                _dispatcherTimer.Tick += DispatcherTimer_Tick;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                _dispatcherTimer.Start();
            }

            editor.EditResource += HandleEditor_EditResource;
        }

        private void HandleEditor_EditResource(object sender, ResourceNameEventArgs e)
        {
            LaunchGenericResourceEditorCommand?.Execute(e.ResourceName);
        }



        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var resetControls = false;
            if (editor.ContainsFocus)
            {
                var focused = FocusedControlHelper.GetFocusedControl();
                var now = FocusedControlHelper.FindFocusedControl2(focused, 6);
                resetControls = now == null;
                if (now != null && (!ReferenceEquals(_last, now) ||
                                    _lastParent != null && !ReferenceEquals(_lastParent, now.Parent)))
                {
                    _last = now;
                    _lastParent = now.Parent;
                    EditControlUpdated = now;
                }
            }
            else
            {
                if (EditControlUpdated != null)
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
            _lastParent = null;
            EditControlUpdated = null;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dispatcherTimer?.Stop();

                if (editor != null)
                {
                    editor.CleanUpEditors();
                    editor.EditResource -= HandleEditor_EditResource; editor.Dispose();
                }

                host?.Dispose();
            }

            _disposed = true;
        }

        ~ParameterSetControl()
        {
            Dispose(false);
        }



        public ResourceManagerBase ResourceManagerBase
        {
            get { return (ResourceManagerBase)GetValue(ResourceManagerBaseProperty); }
            set { SetValue(ResourceManagerBaseProperty, value); }
        }

        public ResourceEntity ResourceEntity
        {
            get { return (ResourceEntity)GetValue(ResourceEntityProperty); }
            set { SetValue(ResourceEntityProperty, value); }
        }

        public ParameterSetCollection ParameterSetCollection
        {
            get { return (ParameterSetCollection)GetValue(ParameterSetCollectionProperty); }
            set { SetValue(ParameterSetCollectionProperty, value); }
        }

        public ICommand LaunchGenericResourceEditorCommand
        {
            get { return (ICommand)GetValue(LaunchGenericResourceEditorCommandProperty); }
            set { SetValue(LaunchGenericResourceEditorCommandProperty, value); }
        }

        public int? ContextIdentifier
        {
            get { return (int?)GetValue(ContextIdentifierProperty); }
            set { SetValue(ContextIdentifierProperty, value); }
        }

        public object EditControlUpdated
        {
            get { return GetValue(EditControlUpdatedProperty); }
            set { SetValue(EditControlUpdatedProperty, value); }
        }

        public bool IsOldItem
        {
            get { return (bool)GetValue(IsOldItemProperty); }
            set { SetValue(IsOldItemProperty, value); }
        }
    }
}
