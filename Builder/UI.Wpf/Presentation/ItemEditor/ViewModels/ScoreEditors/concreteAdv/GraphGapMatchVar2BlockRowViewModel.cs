using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{


    class GraphGapMatchVar2BlockRowViewModel : BlockRowViewModelBase<NoValueType<string>, GraphGapMatchScoringParameter, IValidatingChoiceArrayScoringManipulator<string>>
    {

        static readonly PropertyChangedEventArgs ChoicesArgs = ObservableHelper.CreateArgs<GraphGapMatchVar2BlockRowViewModel>(x => x.Choices);

        private static string _clearEntryChoiceId = System.Guid.NewGuid().ToString();

        public GraphGapMatchVar2BlockRowViewModel(GraphGapMatchScoringParameter scoringParameter, IValidatingChoiceArrayScoringManipulator<string> scoreManipulator, string scoreKey)
            : base(scoringParameter, scoreManipulator, scoreKey, -1)
        {
            Choices = new DataWrapper<List<ChoiceViewModel>>(this, ChoicesArgs);
            if (scoringParameter.Area != null)
            {
                var shapeList = scoringParameter.Area.ShapeList;
                Debug.Assert(shapeList != null);

                var choices = shapeList.Select(sub => new ChoiceViewModel
                {
                    Id = sub.Identifier,
                    Value = string.Format("{0} {1}", GetUiString("GraphGapMatch.Hotspot"), AlphabeticIdentifierHelper.GetAlphabeticIdentifier(sub.Identifier))
                });

                Choices.DataValue = choices.ToList();
                Choices.DataValue.Insert(0, new ChoiceViewModel() { Id = _clearEntryChoiceId, Value = "" });
                ComparisonType.OnChanged(w => w.DataValue).Do(_ =>
                    Value.DataValue = ComparisonType.DataValue == GapComparisonType.NoValue
                            ? new NoValueType<string>(true)
                            : new NoValueType<string>(string.Empty));
            }
        }

        protected override string GetName()
        {
            if (ScoringParameter != null && ScoringParameter.GetType() == typeof(GraphGapMatchScoringParameter))
            {
                var x = (GraphGapMatchScoringParameter)ScoringParameter;
                if (x.Value.Any(c => c.Id == ScoreKey) && x.Value.FirstOrDefault(c => c.Id == ScoreKey).InnerParameters.Any(p => p.Name.Equals(ScoringParameter.ElementLabelParameterName, System.StringComparison.InvariantCultureIgnoreCase)))
                {
                    string labelValue = x.Value.FirstOrDefault(c => c.Id == ScoreKey).InnerParameters.FirstOrDefault(p => p.Name.Equals(ScoringParameter.ElementLabelParameterName, System.StringComparison.InvariantCultureIgnoreCase)).ToString();
                    if (!string.IsNullOrEmpty(labelValue)) return WebUtility.HtmlDecode(labelValue);
                }
            }
            return string.Format("{0} {1}", GetUiString("GraphGapMatch.Picture"), AlphabeticIdentifierHelper.GetAlphabeticIdentifier(ScoreKey));
        }

        protected override GapValue<NoValueType<string>> GetValue()
        {
            var keyStatus = ScoreManipulator.GetKeyStatus().FirstOrDefault(s => s.Key == ScoreKey);
            var value = keyStatus.Value;
            if (value != null && value.NoValueIsCorrect)
            {
                return new GapValue<NoValueType<string>>(value, GapComparisonType.NoValue);
            }
            else
            {
                var stringValue = value ?? string.Empty;
                return new GapValue<NoValueType<string>>(stringValue);
            }
        }

        protected override void UpdateScore(NoValueType<string> value)
        {
            if (!value.Equals(GetValue().Value))
            {
                if (!value.NoValueIsCorrect && value.Value != null && value.Value.Equals(_clearEntryChoiceId))
                {
                    ScoreManipulator.RemoveKey(ScoreKey);
                }
                else
                {
                    ScoreManipulator.SetKey(ScoreKey, value);
                }
            }
        }

        protected override string GetFormattedDisplayValue(NoValueType<string> value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return Choices.DataValue.First(t => t.Id == value).Value;
        }

        protected override void DoRemoveValueFromScore()
        {
        }


        public DataWrapper<List<ChoiceViewModel>> Choices
        {
            get;
            set;
        }

    }
}