using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    abstract class GapBlockRowViewModelFactoryBase<TGapType, TBaseGapBlockRowViewModel, TScoringParameter, TScoreManipulator> : BaseBlockRowViewModelFactory<TScoringParameter, TScoreManipulator>
        where TScoringParameter : ScoringParameter
        where TScoreManipulator : IGapScoringManipulator<TGapType>
    {

        protected abstract GapValue<TGapType> GetEmptyValue();



        protected override TScoreManipulator CreateScoringManipulator(TScoringParameter scoringParameter, Solution solution)
        {
            return (TScoreManipulator)scoringParameter.GetScoreManipulator(solution);
        }



        protected override List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber)
        {
            var ret = new List<IBlockRowViewModel>();
            ScoreManipulator.SetFactSetTarget(setNumber);
            var keyStatus = ScoreManipulator.GetKeyStatus();
            ret.AddRange(Create(keyStatus, scoreKey, setNumber));
            return ret;
        }

        private IEnumerable<IBlockRowViewModel> Create(IDictionary<string, IEnumerable<GapValue<TGapType>>> keyStatus, string keyFact, int? factSetNumber = null)
        {
            IList<IBlockRowViewModel> result = new List<IBlockRowViewModel>();
            IEnumerable<GapValue<TGapType>> valuesForFact;
            if (keyStatus.TryGetValue(keyFact, out valuesForFact))
            {
                int index = 0;
                foreach (var value in valuesForFact)
                {
                    result.Add(CreateGapViewModelInstance(factSetNumber, keyFact, index++));
                }
            }
            else
            {
                Debug.Assert(false, "Should NOT occur");
                throw new ArgumentException("factKeys");
            }
            return result;
        }


        protected override IBlockRowViewModel DoInsertInstance(string keyFact, int? insertAtFactSetNumber, int insertAfterIndex)
        {
            IBlockRowViewModel insertedBlockRowViewModel = null;
            if (insertAtFactSetNumber.HasValue)
            {
                ScoreManipulator.SetFactSetTarget(insertAtFactSetNumber.Value);
            }
            var keysWithValue = ScoreManipulator.GetKeyStatus();
            IEnumerable<GapValue<TGapType>> currentValues;

            if (keysWithValue.TryGetValue(keyFact, out currentValues))
            {
                if (insertAfterIndex < currentValues.Count())
                {
                    var newKeys = new List<GapValue<TGapType>>();
                    int i = 0;
                    bool emptyElementInserted = false;

                    foreach (GapValue<TGapType> val in currentValues)
                    {
                        newKeys.Add(val);
                        if (i == insertAfterIndex)
                        {
                            newKeys.Add(GetEmptyValue());
                            emptyElementInserted = true;
                        }

                        i++;
                    }

                    if (emptyElementInserted)
                    {
                        var rules = ScoreManipulator.GetPreProcessingMethods(keyFact); ScoreManipulator.RemoveKey(keyFact); ScoreManipulator.SetKeys(keyFact, newKeys);
                        ScoreManipulator.SetPreProcessingMethods(keyFact, rules);
                        insertedBlockRowViewModel = CreateGapViewModelInstance(insertAtFactSetNumber, keyFact, insertAfterIndex + 1);
                    }
                }
            }

            return insertedBlockRowViewModel;
        }


        private IBlockRowViewModel CreateGapViewModelInstance(int? factSetNumber, string keyFact, int index)
        {
            var targetedScoreManipulator = CreateTargetedScoreManipulator(factSetNumber);

            return (IBlockRowViewModel)Activator.CreateInstance(typeof(TBaseGapBlockRowViewModel), new object[] { ScoreParameter, targetedScoreManipulator, keyFact, index });
        }


    }
}
