using System.Collections.Generic;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    class V2AdvScoringFindingVM : V2AdvScoringFindingVMBase
    {


        private readonly CreateObjectJIT<KeyFinding> _jitKeyFinding;
        private bool _skipFirstNotification = true;
        private readonly IEnumerable<ScoringParameter> _scoringParameters;



        public V2AdvScoringFindingVM(CreateObjectJIT<KeyFinding> jitKeyFinding, IEnumerable<ScoringParameter> scoringParameters, Solution solution) : base(solution)
        {
            _jitKeyFinding = jitKeyFinding;
            _scoringParameters = scoringParameters;

            SelectedScoringMethod = new DataWrapper<EnumScoringMethod>(this, SelectedScoringMethodArgs, () =>
{
if (!_skipFirstNotification || _jitKeyFinding.CurrentValue != null) _jitKeyFinding.GetEnsuredValue().Method = SelectedScoringMethod.DataValue;
_skipFirstNotification = false;
});
            SelectedScoringMethod.DataValue = (_jitKeyFinding.CurrentValue != null && _jitKeyFinding.CurrentValue.Method != EnumScoringMethod.None) ? _jitKeyFinding.CurrentValue.Method : EnumScoringMethod.Dichotomous;
        }



        public override bool ShowSelectedScoringMethod { get { return true; } }
        public string KeyFindingId { get { return _jitKeyFinding.GetEnsuredValue().Id; } }


        protected override IEnumerable<CombinedScoringMapKey> get_ScoreMap()
        {
            return new ScoringMap(_scoringParameters, Solution).GetMap();
        }

    }
}
