using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.RoutedCommand
{
    class MvvmCommandBinding : Freezable
    {
        UIElement uiElement;

        readonly CommandBinding commandBinding;

        public MvvmCommandBinding()
        {
            commandBinding = new CommandBinding();

            commandBinding.CanExecute += OnCanExecute;
            commandBinding.Executed += OnExecute;
        }


        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(MvvmCommandBinding),
            new PropertyMetadata(null, OnCommandChanged));

        static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MvvmCommandBinding)d).OnCommandChanged((ICommand)e.NewValue);
        }

        void OnCommandChanged(ICommand newValue)
        {
            commandBinding.Command = newValue;
        }

        [Bindable(true)]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }



        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            "Target", typeof(ICommand), typeof(MvvmCommandBinding),
            new PropertyMetadata(null, OnTargetChanged));

        [Bindable(true)]
        public ICommand Target
        {
            get { return (ICommand)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MvvmCommandBinding)d).OnTargetChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        void OnTargetChanged(ICommand oldValue, ICommand newValue)
        {
            if (oldValue != null)
            {
                oldValue.CanExecuteChanged -= OnTargetCanExecuteChanged;
            }

            if (newValue != null)
            {
                newValue.CanExecuteChanged += OnTargetCanExecuteChanged;
            }

            CommandManager.InvalidateRequerySuggested();
        }



        public static readonly DependencyProperty CanExecuteChangedSuggestRequeryProperty
            = DependencyProperty.Register(
                "CanExecuteChangedSuggestRequery", typeof(bool), typeof(MvvmCommandBinding),
                new PropertyMetadata(false, OnCanExecuteChangedSuggestRequeryChanged));

        [Bindable(true)]
        public bool CanExecuteChangedSuggestRequery
        {
            get { return (bool)GetValue(CanExecuteChangedSuggestRequeryProperty); }
            set { SetValue(CanExecuteChangedSuggestRequeryProperty, value); }
        }

        static void OnCanExecuteChangedSuggestRequeryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MvvmCommandBinding)d).OnCanExecuteChangedSuggestRequeryChanged((bool)e.NewValue);
        }

        void OnCanExecuteChangedSuggestRequeryChanged(bool newValue)
        {
            if (newValue)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }



        void OnTargetCanExecuteChanged(object sender, EventArgs e)
        {
            if (CanExecuteChangedSuggestRequery)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (Target == null) return;

            e.Handled = true;
            Target.Execute(e.Parameter);
        }

        void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Target == null) return;

            e.Handled = true;
            e.CanExecute = false;

            e.CanExecute = Target.CanExecute(e.Parameter);
        }



        internal void DettachFrom(UIElement uiDependencyObject)
        {
            if (uiDependencyObject == null) throw new ArgumentNullException(nameof(uiDependencyObject));
            WritePreamble();

            if (uiDependencyObject != uiElement) return;

            Dettach();
        }

        void Dettach()
        {
            uiElement.CommandBindings.Remove(commandBinding);
            uiElement = null;
        }

        internal void AttachTo(UIElement uiDependencyObject)
        {
            if (uiDependencyObject == null) throw new ArgumentNullException(nameof(uiDependencyObject));
            WritePreamble();

            if (uiElement != null)
            {
                Dettach();
            }

            uiElement = uiDependencyObject;
            uiDependencyObject.CommandBindings.Add(commandBinding);
        }


        protected override Freezable CreateInstanceCore()
        {
            return new MvvmCommandBinding();
        }
    }
}