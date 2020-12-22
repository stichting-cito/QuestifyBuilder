using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    internal abstract class BaseBlockRowViewModelFactory<TScoringParameter, TScoreManipulator> :
    IBlockRowViewModelFactory
    where TScoringParameter : ScoringParameter
    where TScoreManipulator : IScoreManipulator
    {

        private TScoringParameter _scoringParameter;
        private TScoreManipulator _scoringManipulator;
        private Solution _solution;
        private ScoringMapKey _scoringMapKey;


        public List<IBlockRowViewModel> Create(ScoringMapKey scoringMapKey, int? setNumber, Solution solution)
        {
            _scoringMapKey = scoringMapKey;
            _scoringParameter = (TScoringParameter)_scoringMapKey.ScoringParameter;
            _solution = solution;
            _scoringManipulator = CreateScoringManipulator(_scoringParameter, solution);

            return CreateBlockRowViewModels(_scoringMapKey.ScoreKey, setNumber);
        }

        public IBlockRowViewModel InsertInstance(ScoringParameter scoringParameter, string insertForScoringKey,
            int? insertAtFactSetNumber, int insertAfterIndex, Solution solution)
        {
            _scoringParameter = (TScoringParameter)scoringParameter;
            _solution = solution;
            _scoringManipulator = CreateScoringManipulator(_scoringParameter, solution);

            var factKeys = _scoringManipulator.GetManipulatableKeys();

            Debug.Assert(factKeys.Contains(insertForScoringKey),
                "InsertForScoringKey should be present in the list of factKeys");

            return DoInsertInstance(insertForScoringKey, insertAtFactSetNumber, insertAfterIndex);
        }


        public TScoreManipulator ScoreManipulator
        {
            get { return _scoringManipulator; }
        }

        public TScoringParameter ScoreParameter
        {
            get { return _scoringParameter; }
        }



        protected abstract TScoreManipulator CreateScoringManipulator(TScoringParameter scoringParameter,
            Solution solution);

        protected abstract List<IBlockRowViewModel> CreateBlockRowViewModels(string scoreKey, int? setNumber);

        protected abstract IBlockRowViewModel DoInsertInstance(string key, int? insertAtFactSetNumber,
            int insertAfterIndex);


        internal TScoreManipulator CreateTargetedScoreManipulator(int? factSetNumber)
        {
            var scoreManipulator = CreateScoringManipulator(ScoreParameter, _solution);

            scoreManipulator.SetFactSetTarget(factSetNumber);

            return scoreManipulator;
        }
    }
}