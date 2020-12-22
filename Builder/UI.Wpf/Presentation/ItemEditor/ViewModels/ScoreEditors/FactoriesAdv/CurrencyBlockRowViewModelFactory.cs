using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class CurrencyBlockRowViewModelFactory : GapBlockRowViewModelFactoryBase<decimal?, CurrencyBlockRowViewModel, CurrencyScoringParameter, IGapScoringManipulator<decimal?>>
    {

        protected override GapValue<decimal?> GetEmptyValue()
        {
            return new GapValue<decimal?>(null);
        }

    }
}
