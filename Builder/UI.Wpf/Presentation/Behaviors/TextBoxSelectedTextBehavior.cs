using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    public class TextBoxSelectedTextBehavior : Behavior<TextBox>
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
