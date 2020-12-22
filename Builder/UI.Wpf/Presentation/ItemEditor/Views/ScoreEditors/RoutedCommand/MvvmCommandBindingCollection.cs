using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.RoutedCommand
{
    [ContentProperty("Commands")]
    class MvvmCommandBindingCollection : Freezable
    {

        static readonly DependencyPropertyKey commandsPropertyReadWrite =
            DependencyProperty.RegisterReadOnly("Commands", typeof(FreezableCollection<MvvmCommandBinding>),
            typeof(MvvmCommandBindingCollection), null);

        public static readonly DependencyProperty CommandsProperty = commandsPropertyReadWrite.DependencyProperty;

        public FreezableCollection<MvvmCommandBinding> Commands
        {
            get { return (FreezableCollection<MvvmCommandBinding>)GetValue(CommandsProperty); }
            private set { SetValue(commandsPropertyReadWrite, value); }
        }

        UIElement uiElement;

        public MvvmCommandBindingCollection()
        {
            Commands = new FreezableCollection<MvvmCommandBinding>();
            ((INotifyCollectionChanged)Commands).CollectionChanged += CommandsChanged;
        }

        void CommandsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (uiElement == null) return;

            if (e.Action == NotifyCollectionChangedAction.Add
                || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (MvvmCommandBinding command in e.NewItems)
                {
                    command.AttachTo(uiElement);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove
                || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (MvvmCommandBinding command in e.OldItems)
                {
                    command.DettachFrom(uiElement);
                }
            }
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
            foreach (var command in Commands)
            {
                command.DettachFrom(uiElement);
            }

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

            foreach (var command in Commands)
            {
                command.AttachTo(uiElement);
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new MvvmCommandBindingCollection();
        }
    }
}