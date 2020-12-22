using System.Windows;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;
using Questify.Builder.UI.Wpf.Properties;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    internal class TextBoxDecimalIntervalBehavior : TextBoxBaseBehavior<DecimalIntervalValidator>
    {
        public TextBoxDecimalIntervalBehavior() { }


        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ToolTip = Resources.TextBoxIntegerIntervalBehavior_ToolTip;
        }



        public int IntegerPartMaxLength
        {
            get { return (int)GetValue(IntegerPartMaxLengthProperty); }
            set { SetValue(IntegerPartMaxLengthProperty, value); }
        }

        public static readonly DependencyProperty IntegerPartMaxLengthProperty =
    DependencyProperty.Register("IntegerPartMaxLength", typeof(int), typeof(TextBoxDecimalIntervalBehavior), new PropertyMetadata(1, IntegerPartMaxLengthChanged));

        private static void IntegerPartMaxLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TextBoxDecimalIntervalBehavior)d;
            instance.InputValidationStrategy.IntegerPartMaxLength = (int)e.NewValue;
        }


        public int FractionPartMaxLength
        {
            get { return (int)GetValue(FractionPartMaxLengthProperty); }
            set { SetValue(FractionPartMaxLengthProperty, value); }
        }

        public static readonly DependencyProperty FractionPartMaxLengthProperty =
    DependencyProperty.Register("FractionPartMaxLength", typeof(int), typeof(TextBoxDecimalIntervalBehavior), new PropertyMetadata(1, FractionPartMaxLengthChanged));

        private static void FractionPartMaxLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TextBoxDecimalIntervalBehavior)d;
            instance.InputValidationStrategy.FractionPartMaxLength = (int)e.NewValue;
        }


    }
}
