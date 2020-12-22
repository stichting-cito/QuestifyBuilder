using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    public static class FloatingNumericTextBoxBehavior
    {

        static string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static readonly DependencyProperty IsEnabledProperty =
DependencyProperty.RegisterAttached("IsEnabled",
typeof(bool), typeof(FloatingNumericTextBoxBehavior),
    new UIPropertyMetadata(false, OnEnabledStateChanged));

        public static bool GetIsEnabled(DependencyObject source)
        {
            return (bool)source.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject source, bool value)
        {
            source.SetValue(IsEnabledProperty, value);
        }

        private static void OnEnabledStateChanged(DependencyObject sender,
    DependencyPropertyChangedEventArgs e)
        {
            TextBoxBase tbb = sender as TextBoxBase;
            if (tbb == null)
                return;

            tbb.PreviewKeyDown -= OnKeyDown;
            DataObject.RemovePastingHandler(tbb, OnClipboardPaste);

            bool b = ((e.NewValue != null && e.NewValue.GetType() == typeof(bool))) ?
                (bool)e.NewValue : false;
            if (b)
            {
                tbb.PreviewKeyDown += OnKeyDown;
                DataObject.AddPastingHandler(tbb, OnClipboardPaste);
            }
        }


        private static void OnClipboardPaste(object sender, DataObjectPastingEventArgs e)
        {
            string text = e.SourceDataObject.GetData(e.FormatToApply) as string;
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Count(ch => !Char.IsNumber(ch)) == 0)
                    return;
            }
            e.CancelCommand();
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (IsValidKey(e) || IsMinusAtPos0(sender, e) || IsDecimalSeperator(sender, e))
                return;

            e.Handled = true;
        }

        private static bool IsDecimalSeperator(object sender, KeyEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                return !tb.Text.Contains(decimalSeparator) && (e.Key == Key.Decimal);
            }
            return false;
        }

        private static bool IsMinusAtPos0(object sender, KeyEventArgs e)
        {
            var tb = sender as TextBox;

            return tb != null && tb.CaretIndex == 0 && (e.Key == Key.Subtract || e.Key == Key.OemMinus);
        }

        private static bool IsValidKey(KeyEventArgs e)
        {
            return (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                   e.Key == Key.Back || e.Key == Key.Delete ||
                   e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Tab ||
                   (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);
        }

    }
}
