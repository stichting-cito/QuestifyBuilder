using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{

    internal abstract class ComboBoxBaseBehaviour<TInputValidator> : Behavior<ComboBox>
        where TInputValidator : IInputValidationStrategy, new()
    {

        private TInputValidator _inputValidationStrategy;
        private string _original;


        internal ComboBoxBaseBehaviour()
        {
            _inputValidationStrategy = new TInputValidator();
        }

        internal TInputValidator InputValidationStrategy
        {
            get { return _inputValidationStrategy; }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewTextInput += AssociatedObject_PreviewTextInput;
            AssociatedObject.GotFocus += AssociatedObject_GotFocus;
            AssociatedObject.LostFocus += AssociatedObject_LostFocus;
            DataObject.AddPastingHandler(AssociatedObject, OnClipboardPaste);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewTextInput -= AssociatedObject_PreviewTextInput;
            AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
            AssociatedObject.LostFocus -= AssociatedObject_LostFocus;

            DataObject.RemovePastingHandler(AssociatedObject, OnClipboardPaste);
        }

        private void AssociatedObject_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var source = (TextBox)e.Source;

            if (source != null && !InsertAndValidate(source, e.Text))
                e.Handled = true;
        }

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            var source = (TextBox)e.Source;
            _original = source.Text;
        }

        private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source is TextBox)
            {
                var source = (TextBox)e.Source;
                string valueToValidate = source.Text;
                if (sender.GetType() == typeof(ComboBox) && ((ComboBox)sender).SelectedValue != null)
                {
                    valueToValidate = ((ComboBox)sender).SelectedValue.ToString();
                }
                if (!InputValidationStrategy.IsInputValid(valueToValidate)) { source.Text = _original; }
            }
        }

        private void OnClipboardPaste(object sender, DataObjectPastingEventArgs e)
        {
            var source = (TextBox)e.Source;
            string text = e.SourceDataObject.GetData(e.FormatToApply) as string;

            if (source != null && !string.IsNullOrEmpty(text) && !InsertAndValidate(source, text))
                e.CancelCommand();
        }

        private bool InsertAndValidate(TextBox tb, string newContent)
        {
            string testString = string.Empty;
            if (!string.IsNullOrEmpty(tb.SelectedText))
            {
                string pre = tb.Text.Substring(0, tb.SelectionStart);
                string after = tb.Text.Substring(tb.SelectionStart + tb.SelectionLength,
                    tb.Text.Length - (tb.SelectionStart + tb.SelectionLength));
                testString = pre + newContent + after;
            }
            else
            {
                string pre = tb.Text.Substring(0, tb.CaretIndex);
                string after = tb.Text.Substring(tb.CaretIndex, tb.Text.Length - tb.CaretIndex);
                testString = pre + newContent + after;
            }

            return InputValidationStrategy.IsInputAllowed(testString);
        }
    }
}
