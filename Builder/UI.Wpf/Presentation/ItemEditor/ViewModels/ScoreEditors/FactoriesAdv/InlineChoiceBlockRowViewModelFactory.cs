using System;
using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class InlineChoiceBlockRowViewModelFactory : BaseBlockRowViewModelFactory<InlineChoiceScoringParameter, IChoiceScoringManipulator>
    {
        protected override IChoiceScoringManipulator CreateScoringManipulator(InlineChoiceScoringParameter scoringParameter, Solution solution)
        {
            return scoringParameter.GetScoreManipulator(solution);
        }

        protected override List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber)
        {
            var createdInstances = new List<IBlockRowViewModel>();

            bool isScoreKeySingleChar = scoreKey.Length == 1;
            if (!isScoreKeySingleChar) throw new NotSupportedException("Unable to create MC Block View model. Not all possible keys are single char.");

            createdInstances.Add(new InlineChoiceBlockRowViewModel(ScoreParameter, CreateTargetedScoreManipulator(setNumber)));

            return createdInstances;
        }

        protected override IBlockRowViewModel DoInsertInstance(string key, int? insertAtFactSetNumber, int insertAfterIndex)
        {
            throw new NotImplementedException();
        }
    }
}
