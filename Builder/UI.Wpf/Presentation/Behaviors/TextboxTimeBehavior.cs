using System.Windows;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    internal class TextboxTimeBehavior : TextBoxBaseBehavior<TimeValidator>
    {
        public TextboxTimeBehavior() { }


        public string TimeFormat
        {
            get { return (string)GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }

        public static readonly DependencyProperty TimeFormatProperty =
    DependencyProperty.Register("TimeFormat", typeof(string), typeof(TextboxTimeBehavior), new PropertyMetadata(null, TimeFormatChanged));


        private static void TimeFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TextboxTimeBehavior)d;
            instance.InputValidationStrategy.TimeFormat = (string)e.NewValue;
        }

    }
}
