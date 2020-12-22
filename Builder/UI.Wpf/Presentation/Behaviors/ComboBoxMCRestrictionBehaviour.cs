using System.Windows;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    internal class ComboBoxMCRestrictionBehaviour : ComboBoxBaseBehaviour<AlphaNumericRangeValidator>
    {


        protected override void OnAttached()
        {
            base.OnAttached();

            InputValidationStrategy.NumericIdentifiers = NumericIdentifiers;
            InputValidationStrategy.AlternativesCount = AlternativesCount;
        }



        public bool NumericIdentifiers
        {
            get { return (bool)GetValue(NumericIdentifiersProperty); }
            set { SetValue(NumericIdentifiersProperty, value); }
        }

        public static readonly DependencyProperty NumericIdentifiersProperty =
    DependencyProperty.Register("NumericIdentifiers", typeof(bool), typeof(ComboBoxMCRestrictionBehaviour), new PropertyMetadata(false, NumericIdentifiersChanged));

        private static void NumericIdentifiersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (ComboBoxMCRestrictionBehaviour)d;
            instance.InputValidationStrategy.NumericIdentifiers = (bool)e.NewValue;
        }

        public int AlternativesCount
        {
            get { return (int)GetValue(AlternativesCountProperty); }
            set { SetValue(AlternativesCountProperty, value); }
        }

        public static readonly DependencyProperty AlternativesCountProperty =
    DependencyProperty.Register("AlternativesCount", typeof(int), typeof(ComboBoxMCRestrictionBehaviour), new PropertyMetadata(2, AlternativesCountChanged));

        private static void AlternativesCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (ComboBoxMCRestrictionBehaviour)d;
            instance.InputValidationStrategy.AlternativesCount = (int)e.NewValue;

        }

    }

}
