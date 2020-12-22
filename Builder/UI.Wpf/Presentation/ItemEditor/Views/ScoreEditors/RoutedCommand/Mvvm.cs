using System;
using System.Windows;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.RoutedCommand
{
    static class Mvvm
    {
        public static readonly DependencyProperty CommandBindingsProperty = DependencyProperty.RegisterAttached(
            "CommandBindings", typeof(MvvmCommandBindingCollection), typeof(Mvvm),
            new PropertyMetadata(null, OnCommandBindingsChanged));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static MvvmCommandBindingCollection GetCommandBindings(UIElement target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return (MvvmCommandBindingCollection)target.GetValue(CommandBindingsProperty);
        }

        public static void SetCommandBindings(UIElement target, MvvmCommandBindingCollection value)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            target.SetValue(CommandBindingsProperty, value);
        }

        private static void OnCommandBindingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiDependencyObject = d as UIElement;
            if (uiDependencyObject == null) return;

            var oldValue = e.OldValue as MvvmCommandBindingCollection;
            if (oldValue != null)
            {
                oldValue.DettachFrom(uiDependencyObject);
            }

            var newValue = e.NewValue as MvvmCommandBindingCollection;
            if (newValue != null)
            {
                newValue.AttachTo(uiDependencyObject);
            }
        }
    }
}