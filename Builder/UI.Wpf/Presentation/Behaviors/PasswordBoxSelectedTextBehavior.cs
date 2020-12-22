using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    public class PasswordBoxSelectedTextBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += AssociatedObjectGotKeyboardFocus;
        }

        void AssociatedObjectGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            AssociatedObject.SelectAll();
        }
    }
}