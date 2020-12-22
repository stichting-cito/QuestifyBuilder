using System;
using System.Collections.Generic;
using System.Linq;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    class ChoiceBlockRowViewModelFactory : BaseBlockRowViewModelFactory<ChoiceScoringParameter, IChoiceScoringManipulator>
    {
        protected override IChoiceScoringManipulator CreateScoringManipulator(ChoiceScoringParameter scoringParameter, Solution solution)
        {
            return scoringParameter.GetScoreManipulator(solution);
        }

        protected override List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber)
        {
            var createdInstances = new List<IBlockRowViewModel>();


            if (ScoreParameter.GetType() == typeof(SelectPointScoringParameter))
            {
                createdInstances.Add(new SelectPointBlockRowViewModel(ScoreParameter, CreateTargetedScoreManipulator(setNumber), scoreKey));
            }
            else if (ScoreParameter.IsSingleChoice)
            {
                ScoreManipulator.SetFactSetTarget(setNumber);
                var keyStatus = ScoreManipulator.GetKeyStatus();
                createdInstances.AddRange(Create(keyStatus, scoreKey, setNumber));
            }
            else
            {
                createdInstances.Add(new MultiResponseBlockRowViewModel(ScoreParameter, CreateTargetedScoreManipulator(setNumber), scoreKey));
            }

            return createdInstances;
        }

        private IEnumerable<IBlockRowViewModel> Create(IDictionary<string, bool> keyStatus, string keyFact, int? factSetNumber = null)
        {
            IList<IBlockRowViewModel> result = new List<IBlockRowViewModel>();
            int index = 0;
            foreach (var value in keyStatus.Where(v => v.Value).Select(k => k.Key))
            {
                result.Add(CreateViewModelInstance(factSetNumber, value, index++));
            }
            if (!result.Any())
            {
                result.Add(new ChoiceBlockRowViewModel(ScoreParameter, CreateTargetedScoreManipulator(factSetNumber)));
            }

            return result;
        }

        protected override IBlockRowViewModel DoInsertInstance(string key, int? insertAtFactSetNumber, int insertAfterIndex)
        {
            IBlockRowViewModel insertedBlockRowViewModel = null;
            if (insertAtFactSetNumber.HasValue)
            {
                ScoreManipulator.SetFactSetTarget(insertAtFactSetNumber.Value);
            }
            insertedBlockRowViewModel = CreateViewModelInstance(insertAtFactSetNumber, string.Empty, insertAfterIndex + 1);
            return insertedBlockRowViewModel;
        }

        private IBlockRowViewModel CreateViewModelInstance(int? factSetNumber, string keyFact, int index)
        {
            var targetedScoreManipulator = CreateTargetedScoreManipulator(factSetNumber);

            return (IBlockRowViewModel)Activator.CreateInstance(typeof(ChoiceBlockRowViewModel), new object[] { ScoreParameter, targetedScoreManipulator, keyFact, index });
        }
    }
}
