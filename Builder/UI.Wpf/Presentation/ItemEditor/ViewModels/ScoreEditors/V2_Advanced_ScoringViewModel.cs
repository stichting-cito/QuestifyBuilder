using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.Service.HelperFunctions;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    [ExportViewModel("ItemEditor.V2_Advanced_Scoring")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class V2_Advanced_ScoringViewModel : V2ScoringBase
    {


        [ImportingConstructor]
        public V2_Advanced_ScoringViewModel(IViewAwareStatus viewAwareStatusService)
            : base(viewAwareStatusService)
        {
            FindingGroups = new ObservableCollection<V2AdvScoringFindingVM>();
        }



        public ObservableCollection<V2AdvScoringFindingVM> FindingGroups { private set; get; }


        public override void OnViewIsLoaded()
        {
            WalkScoringParameters(Solution, ScoringParameters);
        }

        private void WalkScoringParameters(Solution solution, IEnumerable<ScoringParameter> prms)
        {
            if (solution.AutoScoring)
            {
                var scoringParametersPerFindingId = GetScoringParametersPerFindingId(prms);

                AddBlockGridDataPerFindingId(scoringParametersPerFindingId, solution);
            }
        }

        private void AddBlockGridDataPerFindingId(Dictionary<string, IList<ScoringParameter>> scoringParametersPerFindingId, Solution solution)
        {
            foreach (var findingId in scoringParametersPerFindingId.Keys)
            {
                var viewModelPerFinding = FindingGroups.FirstOrDefault(fvm => fvm.KeyFindingId == findingId);
                if (viewModelPerFinding == null)
                {
                    viewModelPerFinding = CreateViewModelForFindingId(solution, findingId, scoringParametersPerFindingId);
                    FindingGroups.Add(viewModelPerFinding);
                }
                viewModelPerFinding.AddBlockGridData();
            }
        }

        private Dictionary<string, IList<ScoringParameter>> GetScoringParametersPerFindingId(IEnumerable<ScoringParameter> prms)
        {
            var scoringParametersPerFindingId = new Dictionary<string, IList<ScoringParameter>>();
            foreach (ScoringParameter scorePrm in prms.Where(sp => sp.GetType() != typeof(AspectScoringParameter)))
            {
                var findingId = !string.IsNullOrEmpty(scorePrm.FindingId) ? scorePrm.FindingId : "default";

                if (!scoringParametersPerFindingId.ContainsKey(findingId))
                {
                    scoringParametersPerFindingId.Add(findingId, new List<ScoringParameter>() { scorePrm });
                }
                else
                {
                    scoringParametersPerFindingId[findingId].Add(scorePrm);
                }
            }
            return scoringParametersPerFindingId;
        }

        private V2AdvScoringFindingVM CreateViewModelForFindingId(Solution solution, string findingId, Dictionary<string, IList<ScoringParameter>> scoringParametersPerFindingId)
        {
            solution.GetFindingOrMakeIt(findingId);
            var jit = new CreateObjectJIT<KeyFinding>(solution.Findings.FindById(findingId), () => solution.GetFindingOrMakeIt(findingId));

            return new V2AdvScoringFindingVM(jit, scoringParametersPerFindingId[findingId], solution);
        }

        protected override void OnDispose()
        {
            _ItemEditorVM = null;
            FindingGroups.Clear();
            FindingGroups = null;

            base.OnDispose();
        }
    }
}
