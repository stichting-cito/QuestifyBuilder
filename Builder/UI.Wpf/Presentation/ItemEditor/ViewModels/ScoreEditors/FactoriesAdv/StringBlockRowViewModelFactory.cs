using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class StringBlockRowViewModelFactory : GapBlockRowViewModelFactoryBase<string, StringBlockRowViewModel, StringScoringParameter, IGapScoringManipulator<string>>
    {
        protected override List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber)
        {
            var usableConverters = ScoreParameter.DesignerSettings.GetSettingValueByKey("PreprocessRules");

            if (!string.IsNullOrEmpty(usableConverters))
            {
                ScoreParameter.PreprocessRules = usableConverters;
            }

            return base.CreateBlockRowViewModels(scoreKey, setNumber);
        }

        protected override GapValue<string> GetEmptyValue()
        {
            return new GapValue<string>(string.Empty);
        }

    }
}
