using System;
using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class GraphGapMatchBlockRowViewModelFactory : BaseBlockRowViewModelFactory<GraphGapMatchScoringParameter, IValidatingChoiceArrayScoringManipulator<string>>
    {
        protected override IValidatingChoiceArrayScoringManipulator<string> CreateScoringManipulator(GraphGapMatchScoringParameter scoringParameter, Solution solution)
        {
            return scoringParameter.GetScoreManipulator(solution);
        }

        protected override List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber)
        {
            List<IBlockRowViewModel> createdInstances;
            if (ScoreParameter.CanTransform)
            {
                createdInstances = new List<IBlockRowViewModel>
                {
                    new GraphGapMatchBlockRowViewModel(ScoreParameter, CreateTargetedScoreManipulator(setNumber),
                        scoreKey)
                };
            }
            else
            {
                createdInstances = new List<IBlockRowViewModel>
                {
                    new GraphGapMatchVar2BlockRowViewModel(ScoreParameter, CreateTargetedScoreManipulator(setNumber),
                        scoreKey)
                };
            }

            return createdInstances;
        }

        protected override IBlockRowViewModel DoInsertInstance(string key, int? insertAtFactSetNumber, int insertAfterIndex)
        {
            throw new NotImplementedException();
        }
    }
}