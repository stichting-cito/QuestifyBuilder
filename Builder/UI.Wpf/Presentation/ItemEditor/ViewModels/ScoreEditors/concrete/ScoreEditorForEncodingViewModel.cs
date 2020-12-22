using System.Diagnostics;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    internal class ScoreEditorForEncodingViewModel : ViewModelBase
    {

        private readonly CombinedScoringMapKey _combinedScoringMapKey;
        private readonly Solution _solution;
        private readonly Solution _workingSolution;

        private V2AdvResponseCategoryScoringVM _scoreEditorVm;



        public ScoreEditorForEncodingViewModel(CombinedScoringMapKey scoringMapKey, Solution solution)
        {
            _combinedScoringMapKey = PreProcess(scoringMapKey);
            _solution = solution;
            _workingSolution = new Solution();
            InitCommands();
        }

        private CombinedScoringMapKey PreProcess(CombinedScoringMapKey scoringMapKey)
        {
            if (scoringMapKey.IsGroup)
                return CombinedScoringMapKey.Create(scoringMapKey, new[] { 0 }); return scoringMapKey;
        }


        public Solution WorkingSolution
        {
            get { return _workingSolution; }
        }

        public V2AdvResponseCategoryScoringVM FindingVM
        {
            get { EnsureFindingGroupVm(); return _scoreEditorVm; }
        }

        private void EnsureFindingGroupVm()
        {
            if (_scoreEditorVm == null)
            {
                var newFindingGroup = new V2AdvResponseCategoryScoringVM(_combinedScoringMapKey, _workingSolution);

                newFindingGroup.AddBlockGridData();

                _scoreEditorVm = newFindingGroup;
            }
        }

        internal void ProcessResult()
        {

            if (_combinedScoringMapKey.IsGroup)
            {
                var renameSet = _workingSolution.Findings[0].KeyFactsets.First();

                foreach (var key in _combinedScoringMapKey)
                {
                    var nr = _combinedScoringMapKey.GetIdForAnswerCategory(_solution.ConceptFindings.First());

                    var renameFact = renameSet.Facts.FirstOrDefault(fact => fact.Id.StartsWith(key.ScoreKey) && fact.Id.EndsWith(key.ScoringParameter.IdentifierPostFix()));

                    if (renameFact == null)
                    {
                        Debug.Assert(key.ScoringParameter.IsSingleChoice);
                    }
                    else
                    {
                        var newId = key.GetFactId(nr.ToString());
                        renameFact.Id = newId;
                    }
                }

                _combinedScoringMapKey.GetConceptManipulator(_workingSolution);

                var toMove = _workingSolution.ConceptFindings[0].KeyFactsets.First();

                _solution.ConceptFindings[0].KeyFactsets.Add(toMove);

            }
            else
            {
                var renameFact = _workingSolution.Findings[0].Facts.First();

                ScoringMapKey key = _combinedScoringMapKey.First();

                var nr = _combinedScoringMapKey.GetIdForAnswerCategory(_solution.ConceptFindings.First());

                var newId = key.GetFactId(nr.ToString());
                renameFact.Id = newId;

                _combinedScoringMapKey.GetConceptManipulator(_workingSolution);

                var toMove = _workingSolution.ConceptFindings[0].Facts.First(f => f.Id == renameFact.Id);

                _solution.ConceptFindings[0].Facts.Add(toMove);
            }


        }


        private void InitCommands()
        {
            OkCommand = new SimpleCommand<object, object>(o =>
            {
                ProcessResult();
                RaiseCloseRequest(true);
            });

            CancelCommand = new SimpleCommand<object, object>(o => RaiseCloseRequest(false));
        }

        public SimpleCommand<object, object> OkCommand { get; private set; }
        public SimpleCommand<object, object> CancelCommand { get; private set; }

    }
}
