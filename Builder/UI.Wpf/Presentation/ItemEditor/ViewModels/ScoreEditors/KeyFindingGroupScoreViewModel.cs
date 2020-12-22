using System.Collections.ObjectModel;
using System.ComponentModel;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.Service.HelperFunctions;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    class KeyFindingGroupScoreViewModel : ValidatingViewModelBase
    {
        private CreateObjectJIT<KeyFinding> _JITKeyFinding;
        private bool _skipFirstNotification = true;

        public KeyFindingGroupScoreViewModel(CreateObjectJIT<KeyFinding> JITKeyFinding)
        {
            _JITKeyFinding = JITKeyFinding;


            ShowScoringMethod = new DataWrapper<bool>(this, ShowScoringMethodArgs) { DataValue = true };
            SelectedScoringMethod = new DataWrapper<EnumScoringMethod>(this, SelectedScoringMethodArgs, () =>
            {

                if (!_skipFirstNotification || _JITKeyFinding.CurrentValue != null) _JITKeyFinding.GetEnsuredValue().Method = SelectedScoringMethod.DataValue;
                _skipFirstNotification = false;
            });
            SelectedScoringMethod.DataValue = (_JITKeyFinding.CurrentValue != null && _JITKeyFinding.CurrentValue.Method != EnumScoringMethod.None) ? _JITKeyFinding.CurrentValue.Method : EnumScoringMethod.Dichotomous;

            ScoreEditorsViews = new ObservableCollection<WorkspaceData>();
        }

        public DataWrapper<bool> ShowScoringMethod { get; private set; }
        public DataWrapper<EnumScoringMethod> SelectedScoringMethod { get; private set; }

        public ObservableCollection<WorkspaceData> ScoreEditorsViews { private set; get; }

        public string KeyFindingId { get { return _JITKeyFinding.GetEnsuredValue().Id; } }

        static PropertyChangedEventArgs ScoreEditorsArgs = ObservableHelper.CreateArgs<KeyFindingGroupScoreViewModel>(x => x.ScoreEditorsViews); static PropertyChangedEventArgs ShowScoringMethodArgs = ObservableHelper.CreateArgs<KeyFindingGroupScoreViewModel>(x => x.ShowScoringMethod); static PropertyChangedEventArgs SelectedScoringMethodArgs = ObservableHelper.CreateArgs<KeyFindingGroupScoreViewModel>(x => x.SelectedScoringMethod);
    }
}
