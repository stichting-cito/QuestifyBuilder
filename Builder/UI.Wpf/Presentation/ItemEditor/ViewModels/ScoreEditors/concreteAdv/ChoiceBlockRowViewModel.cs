using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class ChoiceBlockRowViewModel : BlockRowViewModelBase<string, ChoiceScoringParameter, IChoiceScoringManipulator>
    {

        static PropertyChangedEventArgs NumericIdentifiersArgs = ObservableHelper.CreateArgs<ChoiceBlockRowViewModel>(x => x.NumericIdentifiers); static PropertyChangedEventArgs AlternativesCountArgs = ObservableHelper.CreateArgs<ChoiceBlockRowViewModel>(x => x.AlternativesCount); static PropertyChangedEventArgs ChoicesArgs = ObservableHelper.CreateArgs<ChoiceBlockRowViewModel>(x => x.Choices);
        private static string _clearEntryChoiceId = System.Guid.NewGuid().ToString();
        private string _previousValue;


        public ChoiceBlockRowViewModel(ScoringParameter scoringParameter, IChoiceScoringManipulator scoreManipulator, string scoreKey, int index)
           : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
            Choices = new DataWrapper<List<ChoiceOptionViewModel>>(this, ChoicesArgs);

            if (scoringParameter.Value != null)
            {
                Choices.DataValue = scoringParameter.Value.Select(sub => new ChoiceOptionViewModel() { Id = sub.Id, Value = GetFriendlyLabel(sub) }).ToList();
                Choices.DataValue.Insert(0, new ChoiceOptionViewModel() { Id = _clearEntryChoiceId, Value = "" });
            }

            NumericIdentifiers = new DataWrapper<bool>(this, NumericIdentifiersArgs);
            NumericIdentifiers.DataValue = false;

            AlternativesCount = new DataWrapper<int>(this, AlternativesCountArgs);
            AlternativesCount.DataValue = scoringParameter.AlternativesCount.HasValue ? scoringParameter.AlternativesCount.Value : scoringParameter.Value.Count();

            _previousValue = scoreKey;

            var noDuplicateKeysRule = NoDuplicateKeys("DataValue", GetUiString("Choice.NoDuplicateKeys"));
            Value.AddRule(noDuplicateKeysRule);
        }

        public ChoiceBlockRowViewModel(ScoringParameter scoringParameter, IChoiceScoringManipulator scoreManipulator)
            : this(scoringParameter, scoreManipulator, "**Not applicable", -1)
        {
        }



        public DataWrapper<bool> NumericIdentifiers { get; set; }

        public DataWrapper<int> AlternativesCount
        {
            get;
            set;
        }

        public override string ScoreKey
        {
            get
            {
                string result = base.ScoreKey;
                if (string.IsNullOrEmpty(result))
                {
                    result = _clearEntryChoiceId;
                }
                return result;
            }
        }

        public DataWrapper<List<ChoiceOptionViewModel>> Choices { get; set; }



        protected override GapValue<string> GetValue()
        {
            if (string.IsNullOrEmpty(_previousValue))
            {
                return new GapValue<string>(ScoreKey);
            }
            else
            {
                return new GapValue<string>(_previousValue);
            }
        }

        protected override void UpdateScore(string value)
        {
            if (!value.Equals(GetValue().Value))
            {
                if (value.Equals(_clearEntryChoiceId))
                {
                    ScoreManipulator.RemoveKey(GetValue().Value);
                    _previousValue = string.Empty;
                }
                else
                {
                    ScoreManipulator.RemoveKey(GetValue().Value);
                    ScoreManipulator.SetKey(value);
                    _previousValue = value;
                }
            }

            Mediator.Instance.NotifyColleagues(Constants.ValidateAllScoreErrors, new EventArgs());
        }

        protected override string GetFormattedDisplayValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (Choices.DataValue.Any(c => c.Id == value))
            {
                return Choices.DataValue.First(c => c.Id == value).Value;
            }

            return base.GetFormattedDisplayValue(value);
        }

        protected override void DoRemoveValueFromScore()
        {
            var keysAndValues = ScoreManipulator.GetKeyStatus();

            foreach (string key in keysAndValues.Where(v => v.Value).Select(k => k.Key))
            {
                if (key.Equals(ScoreKey))
                {
                    if (keysAndValues.ContainsKey(key))
                    {
                        ScoreManipulator.RemoveKey(key);
                    }
                }
            }
        }

        public override bool IsMatch(int? factSetNumber, ScoringMapKey scoringMapKey)
        {
            return factSetNumber == FactSetNumber && Name == scoringMapKey.ScoringParameterName;
        }

        internal SimpleRule NoDuplicateKeys(string property, string msg)
        {
            return new SimpleRule(property, msg, data =>
            {
                var value = (DataWrapper<string>)data;
                var result = ScoreManipulator.IsValid(value.DataValue);
                return !result;
            });
        }
    }
}
