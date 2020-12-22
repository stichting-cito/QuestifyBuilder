using Cito.Tester.ContentModel;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class DecimalBlockRowViewModelFactory : GapBlockRowViewModelFactoryBase<MultiType, DecimalBlockRowViewModel, DecimalScoringParameter, IGapScoringManipulator<MultiType>>
    {

        protected override GapValue<MultiType> GetEmptyValue()
        {
            return new GapValue<MultiType>(null);
        }

    }
}
