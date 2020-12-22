using System;
using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class HotTextBlockRowViewModelFactory : BaseBlockRowViewModelFactory<HotTextScoringParameter, IChoiceScoringManipulator>
    {
        protected override IChoiceScoringManipulator CreateScoringManipulator(HotTextScoringParameter scoringParameter, Solution solution)
        {
            return scoringParameter.GetScoreManipulator(solution);
        }

        protected override List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber)
        {
            var createdInstances = new List<IBlockRowViewModel>();

            createdInstances.Add(new HotTextBlockRowViewModel(ScoreParameter, CreateTargetedScoreManipulator(setNumber), scoreKey));

            return createdInstances;
        }


        protected override IBlockRowViewModel DoInsertInstance(string key, int? insertAtFactSetNumber, int insertAfterIndex)
        {
            throw new NotImplementedException();
        }
    }
}
