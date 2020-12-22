using System;
using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class MatrixBlockRowViewModelFactory : BaseBlockRowViewModelFactory<MatrixScoringParameter, IChoiceArrayScoringManipulator>
    {
        protected override IChoiceArrayScoringManipulator CreateScoringManipulator(MatrixScoringParameter scoringParameter, Solution solution)
        {
            return scoringParameter.GetScoreManipulator(solution);
        }

        protected override List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber)
        {
            var createdInstances = new List<IBlockRowViewModel>();


            var manipulator = CreateTargetedScoreManipulator(setNumber);
            createdInstances.Add(new MatrixBlockRowViewModel(ScoreParameter, manipulator, scoreKey));

            return createdInstances;
        }


        protected override IBlockRowViewModel DoInsertInstance(string key, int? insertAtFactSetNumber, int insertAfterIndex)
        {
            throw new NotImplementedException();
        }
    }
}
