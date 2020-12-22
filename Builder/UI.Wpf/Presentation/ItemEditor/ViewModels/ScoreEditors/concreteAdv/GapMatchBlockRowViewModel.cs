using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class GapMatchBlockRowViewModel : BlockRowViewModelBase<NoValueType<string>, GapMatchScoringParameter, IValidatingChoiceArrayScoringManipulator<string>>
    {
        static readonly PropertyChangedEventArgs ChoicesArgs = ObservableHelper.CreateArgs<GapMatchBlockRowViewModel>(x => x.Choices);

        protected static string _clearEntryChoiceId = System.Guid.NewGuid().ToString();

        public GapMatchBlockRowViewModel(GapMatchScoringParameter scoringParameter, IValidatingChoiceArrayScoringManipulator<string> scoreManipulator, string scoreKey)
            : base(scoringParameter, scoreManipulator, scoreKey, -1)
        {
            Choices = new DataWrapper<List<ChoiceViewModel>>(this, ChoicesArgs);
            if (scoringParameter.Value != null)
            {
                Choices.DataValue = scoringParameter.Gaps.Select(sub => new ChoiceViewModel
                {
                    Id = sub.Key,
                    Value = GetFriendlyNameOfMovableElement(sub)
                }).ToList();
            }
            Choices.DataValue.Insert(0, new ChoiceViewModel() { Id = _clearEntryChoiceId, Value = "" });
            ComparisonType.DataValue = scoreManipulator.GetBaseValueForKey(scoreKey) is NoValue ? GapComparisonType.NoValue : GapComparisonType.Equals;
            ComparisonType.OnChanged(w => w.DataValue).Do(_ =>
                Value.DataValue = ComparisonType.DataValue == GapComparisonType.NoValue
                        ? new NoValueType<string>(true)
                        : new NoValueType<string>(string.Empty));


            var matchMaxRule = GapMatchValidationMatchMaxRule("DataValue", GetUiString("GapMatch.ValidateMatchMax"));
            Value.AddRule(matchMaxRule);
        }


        protected override string GetName()
        {
            var name = (from par in ScoringParameter.Value where par.Id == ScoreKey select ((GapTextParameter)par.InnerParameters.FirstOrDefault(i => i.Name == GapMatchScoringParameter.GapControlName)).Value).FirstOrDefault();
            return name;
        }

        protected override GapValue<NoValueType<string>> GetValue()
        {
            var keyStatus = ScoreManipulator.GetKeyStatus().FirstOrDefault(s => s.Key == ScoreKey);
            var value = keyStatus.Value;
            return new GapValue<NoValueType<string>>(value);
        }

        protected override void UpdateScore(NoValueType<string> value)
        {
            if (value != null && value.Value != null && value.Value.Equals(_clearEntryChoiceId))
            {
                ScoreManipulator.RemoveKey(ScoreKey);
            }
            else
            {
                ScoreManipulator.SetKey(ScoreKey, value);
            }

            Mediator.Instance.NotifyColleagues(Constants.ValidateAllScoreErrors, new EventArgs());
        }

        protected override string GetFormattedDisplayValue(NoValueType<string> value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

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
                name = WebUtility.HtmlDecode(sub.Value.FirstOrDefault(t => t.Key == GapMatchScoringParameter.GapMatchValue).Value);
            }
            return WebUtility.HtmlDecode(name);
        }

    }

}