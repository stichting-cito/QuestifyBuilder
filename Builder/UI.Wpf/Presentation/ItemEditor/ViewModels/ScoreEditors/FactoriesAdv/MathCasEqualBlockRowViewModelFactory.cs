using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class MathCasEqualBlockRowViewModelFactory : GapBlockRowViewModelFactoryBase<string, MathCasEqualBlockRowViewModel, MathCasEqualScoringParameter, IGapScoringManipulator<string>>
    {
        protected override GapValue<string> GetEmptyValue()
        {
            return new GapValue<string>(string.Empty);
        }
    }
}