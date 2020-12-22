namespace Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies
{
    internal interface IInputValidationStrategy
    {
        bool IsInputAllowed(string input);
        bool IsInputValid(string input);
    }
}
