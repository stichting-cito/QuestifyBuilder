namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    interface ICommandSupport
    {
        void DoPreSaveTasks();
        void DoTaskBeforeClosing();
        void DoPostSaveTasks();
    }


}
