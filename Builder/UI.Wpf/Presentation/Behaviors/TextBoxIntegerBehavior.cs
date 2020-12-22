using System.Windows;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    internal class TextBoxIntegerBehavior : TextBoxBaseBehavior<IntegerValidator>
    {
        public TextBoxIntegerBehavior() { }




        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public bool PositiveValueOnly
        {
            get { return (bool)GetValue(PositiveValueOnlyProperty); }
            set { SetValue(PositiveValueOnlyProperty, value); }
        }

        public static readonly DependencyProperty MaxLengthProperty =
    DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBoxIntegerBehavior), new PropertyMetadata(1, MaxLenghtChanged));

        private static void MaxLenghtChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TextBoxIntegerBehavior)d;
            instance.InputValidationStrategy.MaxLength = (int)e.NewValue;
        }

        public static readonly DependencyProperty PositiveValueOnlyProperty =
            DependencyProperty.Register("PositiveValueOnly", typeof(bool), typeof(TextBoxIntegerBehavior), new PropertyMetadata(false, PositiveValueOnlyChanged));

        private static void PositiveValueOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TextBoxIntegerBehavior)d;
            instance.InputValidationStrategy.PositiveValueOnly = (bool)e.NewValue;
        }


    }
}
