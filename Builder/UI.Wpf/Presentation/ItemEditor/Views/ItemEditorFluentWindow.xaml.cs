using Cinch;
using Questify.Builder.UI.Wpf.Presentation.Behaviors;
using Questify.Builder.UI.Wpf.Presentation.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views
{
    [PopupNameToViewLookupKeyMetadata(Constants.ItemEditorFluentView, typeof(ItemEditorFluentWindow))]
    internal partial class ItemEditorFluentWindow : IItemEditorWindow, IRibbonFocus
    {
        public ItemEditorFluentWindow()
        {
            InitializeComponent();
            DocumentEditor.RegisterCommandHandlers(this.GetType());

        }
        public bool RibbonSelected
        {
            get { return ribbon.IsKeyboardFocusWithin; }
        }
    }
}
