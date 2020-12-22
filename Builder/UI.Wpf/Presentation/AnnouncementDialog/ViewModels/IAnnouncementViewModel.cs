using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.AnnouncementDialog.ViewModels
{
    internal interface IAnnouncementViewModel
    {
        SimpleCommand<object, object> Close { get; }

        DataWrapper<int> SelectedTab { get; }
    }
}
