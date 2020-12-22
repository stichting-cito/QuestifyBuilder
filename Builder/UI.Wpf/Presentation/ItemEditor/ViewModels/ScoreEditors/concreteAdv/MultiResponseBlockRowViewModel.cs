using System.Linq;
using System.Net;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class MultiResponseBlockRowViewModel : BlockRowViewModelBase<bool, ChoiceScoringParameter, IChoiceScoringManipulator>
    {
        public MultiResponseBlockRowViewModel(ScoringParameter scoringParameter, IChoiceScoringManipulator scoreManipulator, string scoreKey)
    : base(scoringParameter, scoreManipulator, scoreKey, -1)
        {
            if (Value.DataValue == false)
            {
                scoreManipulator.SetKeyWithDefaultValue(scoreKey);
            }
        }

        protected override void UpdateScore(bool value)
        {
            if (value)
            {
                ScoreManipulator.SetKey(ScoreKey);
            }
            else
            {
                ScoreManipulator.RemoveKey(ScoreKey);
            }
        }

        protected override GapValue<bool> GetValue()
        {
            return new GapValue<bool>(ScoreManipulator.GetKeyStatus()[ScoreKey]);
        }


        protected override void DoRemoveValueFromScore()
        {
        }

        protected override string GetName()
        {
            var subParam = ScoringParameter.Value.FirstOrDefault(x => x.Id == ScoreKey);
            if (subParam != null)
            {
                var contentLabel = subParam.TryGetParameterByName<PlainTextParameter>(ScoringParameter.ElementLabelParameterName);
                if (contentLabel != null && !string.IsNullOrEmpty(contentLabel.Value))
                {
                    return WebUtility.HtmlDecode(contentLabel.Value);
                }
            }


            return string.Format("{0}.{1}", base.GetName(), ScoreKey);
        }

        public override bool IsMatch(int? factSetNumber, ScoringMapKey scoringMapKey)
        {
            return factSetNumber == FactSetNumber && Name == string.Format("{0}.{1}", scoringMapKey.ScoringParameterName, scoringMapKey.ScoreKey);
        }

        public override void SetValueOnStartingEdit()
        {
            Value.DataValue = !Value.DataValue;
        }
    }
}
