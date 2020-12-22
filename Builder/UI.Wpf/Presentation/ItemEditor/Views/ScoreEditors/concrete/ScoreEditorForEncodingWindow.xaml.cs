using System.Windows;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.concrete
{
    [PopupNameToViewLookupKeyMetadata(Constants.EncodingEditorAdv, typeof(ScoreEditorForEncoding))]
    internal partial class ScoreEditorForEncoding : Window
    {
        public ScoreEditorForEncoding()
        {
            InitializeComponent();
        }
    }
}
