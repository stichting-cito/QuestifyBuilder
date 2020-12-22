using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class BooleanBlockRowViewModelFactory : GapBlockRowViewModelFactoryBase<bool, BooleanBlockRowViewModel, BooleanScoringParameter, IGapScoringManipulator<bool>>
    {
        protected override GapValue<bool> GetEmptyValue()
        {
            return new GapValue<bool>(false);
        }
    }
}
