using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class BooleanBlockRowViewModel : GapBlockRowViewModelBase<bool, BooleanScoringParameter, IGapScoringManipulator<bool>>
    {
        public BooleanBlockRowViewModel(ScoringParameter scoreParameter, IGapScoringManipulator<bool> scoreManipulator, string scoreKey, int index) : base(scoreParameter, scoreManipulator, scoreKey, index)
        {
        }
    }
}
