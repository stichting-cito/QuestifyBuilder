namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    interface IViewModel2ViewCommandSupport
    {
        void DoPreSaveTasks();

        void DoTaskBeforeClosing();
        void KillView();
        void DoPostSaveTasks();
    }
}
