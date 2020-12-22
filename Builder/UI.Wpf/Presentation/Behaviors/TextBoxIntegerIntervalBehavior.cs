using System.Windows;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;
using Questify.Builder.UI.Wpf.Properties;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    internal class TextBoxIntegerIntervalBehavior : TextBoxBaseBehavior<IntegerIntervalValidator>
    {

        public TextBoxIntegerIntervalBehavior() { }


        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ToolTip = Resources.TextBoxIntegerIntervalBehavior_ToolTip;
        }



        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly DependencyProperty MaxLengthProperty =
    DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBoxIntegerIntervalBehavior), new PropertyMetadata(1, MaxLenghtChanged));

        private static void MaxLenghtChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TextBoxIntegerIntervalBehavior)d;
            instance.InputValidationStrategy.MaxLength = (int)e.NewValue;
        }


    }
}
