using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class GeogebraBlockRowViewModel : GapBlockRowViewModelBase<string, GeogebraScoringParameter, IGapScoringManipulator<string>>
    {

        public GeogebraBlockRowViewModel(GeogebraScoringParameter scoringParameter, IGapScoringManipulator<string> scoreManipulator, string scoreKey, int index)
            : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
        }

    }
}
