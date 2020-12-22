using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    public class WindowPreviewKeyToRoutedUiCommand : Behavior<Window>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
          "Command", typeof(RoutedUICommand), typeof(WindowPreviewKeyToRoutedUiCommand),
          new PropertyMetadata(null));

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
            "Key", typeof(Key), typeof(WindowPreviewKeyToRoutedUiCommand), new UIPropertyMetadata(Key.None));

        public static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register(
            "Modifiers", typeof(ModifierKeys), typeof(WindowPreviewKeyToRoutedUiCommand), new UIPropertyMetadata(ModifierKeys.None));

        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyUp += AssociatedObjectPreviewKeyUp;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewKeyUp -= AssociatedObjectPreviewKeyUp;
        }

        public Key Key
        {
            get { return (Key)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public ModifierKeys Modifiers
        {
            get { return (ModifierKeys)GetValue(ModifiersProperty); }
            set { SetValue(ModifiersProperty, value); }
        }

        public RoutedUICommand Command
        {
            get { return (RoutedUICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private void AssociatedObjectPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key && IsModifierPressed(e))
            {
                if (Command.CanExecute(null, AssociatedObject))
                    Command.Execute(null, AssociatedObject);
            }
        }

        private bool IsModifierPressed(KeyEventArgs e)
        {
            bool ret = true; var m = Modifiers;
            if ((m & ModifierKeys.Alt) != 0)
                ret &= (e.KeyboardDevice.IsKeyDown(Key.LeftAlt) || e.KeyboardDevice.IsKeyDown(Key.RightAlt));

            if ((m & ModifierKeys.Control) != 0)
                ret &= (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl));

            if ((m & ModifierKeys.Shift) != 0)
                ret &= (e.KeyboardDevice.IsKeyDown(Key.LeftShift) || e.KeyboardDevice.IsKeyDown(Key.RightShift));

            return ret;
        }

    }
}
