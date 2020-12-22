using System.Windows;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    internal class TextboxDecimalBehavior : TextBoxBaseBehavior<DecimalValidator>
    {
        public TextboxDecimalBehavior() { }


        public int IntegerPartMaxLength
        {
            get { return (int)GetValue(IntegerPartMaxLengthProperty); }
            set { SetValue(IntegerPartMaxLengthProperty, value); }
        }

        public static readonly DependencyProperty IntegerPartMaxLengthProperty =
    DependencyProperty.Register("IntegerPartMaxLength", typeof(int), typeof(TextboxDecimalBehavior), new PropertyMetadata(1, IntegerPartMaxLengthChanged));

        private static void IntegerPartMaxLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TextboxDecimalBehavior)d;
            instance.InputValidationStrategy.IntegerPartMaxLength = (int)e.NewValue;
        }

        public int FractionPartMaxLength
        {
            get { return (int)GetValue(FractionPartMaxLengthProperty); }
            set { SetValue(FractionPartMaxLengthProperty, value); }
        }

        public static readonly DependencyProperty FractionPartMaxLengthProperty =
    DependencyProperty.Register("FractionPartMaxLength", typeof(int), typeof(TextboxDecimalBehavior), new PropertyMetadata(1, FractionPartMaxLengthChanged));

        private static void FractionPartMaxLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TextboxDecimalBehavior)d;
            instance.InputValidationStrategy.FractionPartMaxLength = (int)e.NewValue;
        }


    }
}
