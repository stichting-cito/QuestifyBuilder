using System.Diagnostics;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    [DebuggerVisualizer("Title [{Title}] , Icon [{Icon}] , Tag [{Tag}]")]
    class MenuItemViewModel
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public ICommand Command { get; set; }
    }
}
