using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class MathCasDependencyBlockRowViewModelFactory : GapBlockRowViewModelFactoryBase<string, MathCasDependencyBlockRowViewModel, MathCasDependencyScoringParameter, IGapScoringManipulator<string>>
    {
        protected override GapValue<string> GetEmptyValue()
        {
            return new GapValue<string>(string.Empty);
        }
    }
}