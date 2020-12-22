using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    class GraphGapMatchBlockRowViewModel : BlockRowViewModelBase<NoValueType<string>, GraphGapMatchScoringParameter, IValidatingChoiceArrayScoringManipulator<string>>
    {
        static readonly PropertyChangedEventArgs ChoicesArgs = ObservableHelper.CreateArgs<GraphGapMatchBlockRowViewModel>(x => x.Choices);

        private static string _clearEntryChoiceId = System.Guid.NewGuid().ToString();

        public GraphGapMatchBlockRowViewModel(GraphGapMatchScoringParameter scoringParameter, IValidatingChoiceArrayScoringManipulator<string> scoreManipulator, string scoreKey)
            : base(scoringParameter, scoreManipulator, scoreKey, -1)
        {
            Choices = new DataWrapper<List<ChoiceViewModel>>(this, ChoicesArgs);
            if (scoringParameter.Value != null)
            {
                Choices.DataValue = scoringParameter.Gaps.Select(sub => new ChoiceViewModel { Id = sub.Key, Value = GetFriendlyNameOfMovableElement(sub) }).ToList();
            }
            Choices.DataValue.Insert(0, new ChoiceViewModel() { Id = _clearEntryChoiceId, Value = "" });
            ComparisonType.OnChanged(w => w.DataValue).Do(_ =>
                Value.DataValue = ComparisonType.DataValue == GapComparisonType.NoValue
                        ? new NoValueType<string>(true)
                        : new NoValueType<string>(string.Empty));

            var matchMaxRule = GapMatchValidationMatchMaxRule("DataValue", GetUiString("GapMatch.ValidateMatchMax"));
            Value.AddRule(matchMaxRule);
        }

        protected override string GetName()
        {
            string name = (from par in ScoringParameter.Value
                           where par.Id == ScoreKey
                           select
                               ((GapImageParameter)
                                   par.InnerParameters.FirstOrDefault(i => i.Name == GapMatchScoringParameter.GapControlName))
                                   .Id).FirstOrDefault();

            if (string.IsNullOrEmpty(name) == false)
                name = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(name);
            return string.Format("{0} {1}", GetUiString("GraphGapMatch.Hotspot"), name);
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
                var stringValue = (value != null && value.Value != null) ? value.Value : string.Empty;
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

                Mediator.Instance.NotifyColleagues(Constants.ValidateAllScoreErrors, new EventArgs());
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

        internal SimpleRule GapMatchValidationMatchMaxRule(string property, string msg)
        {
            return new SimpleRule(property, msg, data =>
            {
                var value = (DataWrapper<NoValueType<string>>)data;
                var result = ScoreManipulator.IsValid(value.DataValue);
                return !result;
            });
        }



        public DataWrapper<List<ChoiceViewModel>> Choices
        {
            get;
            set;
        }


        private string GetFriendlyNameOfMovableElement(KeyValuePair<string, Dictionary<string, string>> sub)
        {
            string name = string.Empty;
            name = WebUtility.HtmlDecode(sub.Value.FirstOrDefault(t => t.Key == GapMatchScoringParameter.GapMatchLabel).Value);
            if (string.IsNullOrWhiteSpace(name))
            {
                name = WebUtility.HtmlDecode(sub.Value.FirstOrDefault(t => t.Key == GapMatchScoringParameter.GapMatchName).Value + " " + AlphabeticIdentifierHelper.GetAlphabeticIdentifier(sub.Key));
            }
            return WebUtility.HtmlDecode(name);
        }

    }

}