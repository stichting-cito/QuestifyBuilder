using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class GeogebraBlockRowViewModelFactory : GapBlockRowViewModelFactoryBase<string, GeogebraBlockRowViewModel, GeogebraScoringParameter, IGapScoringManipulator<string>>
    {
        protected override GapValue<string> GetEmptyValue()
        {
            return new GapValue<string>(string.Empty);
        }
    }
}
