using System;
using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class GapMatchRichTextBlockRowViewModelFactory : BaseBlockRowViewModelFactory<GapMatchRichTextScoringParameter, IValidatingChoiceArrayScoringManipulator<string>>
    {

        protected override IValidatingChoiceArrayScoringManipulator<string> CreateScoringManipulator(GapMatchRichTextScoringParameter scoringParameter,
            Solution solution)
        {
            return scoringParameter.GetScoreManipulator(solution);
        }

        protected override List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber)
        {
            var createdInstances = new List<IBlockRowViewModel>
            {
                new GapMatchRichTextBlockRowViewModel(ScoreParameter, CreateTargetedScoreManipulator(setNumber), scoreKey)
            };

            return createdInstances;
        }

        protected override IBlockRowViewModel DoInsertInstance(string key, int? insertAtFactSetNumber, int insertAfterIndex)
        {
            throw new NotImplementedException();
        }
    }
}
