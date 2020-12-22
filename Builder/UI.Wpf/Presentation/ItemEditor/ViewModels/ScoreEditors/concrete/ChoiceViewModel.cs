using System.Diagnostics;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [DebuggerDisplay("Value={Value} ID={Id}")]
    class ChoiceViewModel
    {
        public string Id { get; set; }

        public string Value { get; set; }
    }
}
