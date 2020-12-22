using System.ComponentModel.Composition;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [Export(typeof(IAddAnswerCategory))]
    class AddAnswerCategoryImpl : IAddAnswerCategory
    {
        private readonly IWPF2WinVisualizerService _uiVisualizer;


        [ImportingConstructor]
        public AddAnswerCategoryImpl(IWPF2WinVisualizerService uiVisualizer)
        {
            _uiVisualizer = uiVisualizer;
        }


        public void AddAnswerCategory(CombinedScoringMapKey combinedScoringMapKey, Solution solution)
        {
            var model = new ScoreEditorForEncodingViewModel(combinedScoringMapKey, solution);
            _uiVisualizer.ShowDialog(Constants.EncodingEditorAdv, model, true);
        }
    }
}
