using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class MathCasEqualBlockRowViewModel : MathBlockRowViewModel
    {

        public MathCasEqualBlockRowViewModel(MathCasEqualScoringParameter scoringParameter, IGapScoringManipulator<string> scoreManipulator, string scoreKey, int index)
            : base(scoringParameter, scoreManipulator, scoreKey, index)
        { }

    }
}
