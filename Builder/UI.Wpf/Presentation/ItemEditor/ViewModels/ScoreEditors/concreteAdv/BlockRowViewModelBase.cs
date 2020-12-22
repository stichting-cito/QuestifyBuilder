using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    abstract class BlockRowViewModelBase<TValue, TScoreParameter, TScoreManipulator> : ValidatingViewModelBase, IBlockRowViewModel
    where TScoreParameter : ScoringParameter
    where TScoreManipulator : IScoreManipulator
    {
        const string Prefix = "ItemEditor.ScoreEditor.";

        static readonly PropertyChangedEventArgs ValueArgs = ObservableHelper.CreateArgs<BlockRowViewModelBase<TValue, TScoreParameter, TScoreManipulator>>(x => x.Value); static readonly PropertyChangedEventArgs Value2Args = ObservableHelper.CreateArgs<BlockRowViewModelBase<TValue, TScoreParameter, TScoreManipulator>>(x => x.Value2); static readonly PropertyChangedEventArgs ComparisonTypeArgs = ObservableHelper.CreateArgs<BlockRowViewModelBase<TValue, TScoreParameter, TScoreManipulator>>(x => x.ComparisonType);
        private readonly string _scoreKey;
        private readonly ScoringParameter _scoringParameter;
        private readonly TScoreManipulator _scoreManipulator;

        private readonly IList<DataWrapperBase> _cachedListOfDataWrappers;



        protected BlockRowViewModelBase(ScoringParameter scoringParameter, TScoreManipulator scoreManipulator, string scoreKey, int index)
        {
            Mediator.Instance.Register(this);
            _scoringParameter = scoringParameter;
            _scoreManipulator = scoreManipulator;
            _scoreKey = scoreKey;
            Index = index;

            ScoringParameterType = _scoringParameter.GetType();

            ComparisonType = new DataWrapper<GapComparisonType>(this, ComparisonTypeArgs);
            ComparisonType.DataValue = GapComparisonType.Equals;

            Value = new DataWrapper<TValue>(this, ValueArgs);
            Value2 = new DataWrapper<TValue>(this, Value2Args);

            var current = GetValue(); Value.DataValue = current.Value;
            Value2.DataValue = current.Value2;
            ComparisonType.DataValue = current.Comparison;

            CorrectInteractionResponseIsDefinedByOneDistinctValue = scoringParameter.IsSingleValue;
            Value.PropertyChanged += UpdateScore;
            Value2.PropertyChanged += UpdateScore;
            ComparisonType.PropertyChanged += UpdateScore;

            _cachedListOfDataWrappers = new List<DataWrapperBase> { Value };
        }



        public int Index { get; set; }

        public int? FactSetNumber
        {
            get
            {
                return _scoreManipulator.FactSetTarget;
            }
        }

        public string Caption
        {
            get
            {
                return Index > 0 ? string.Empty : Name;
            }
        }
        public string Name
        {
            get
            {
                return GetName();
            }
        }

        public DataWrapper<GapComparisonType> ComparisonType { get; private set; }

        public bool CorrectInteractionResponseIsDefinedByOneDistinctValue { get; private set; }

        public Type ScoringParameterType { get; private set; }

        public DataWrapper<TValue> Value { get; private set; }

        public DataWrapper<TValue> Value2 { get; private set; }


        public string DisplayValue
        {
            get
            {
                if (ComparisonType.DataValue == GapComparisonType.Range)
                {
                    return string.Format("[{0};{1}]", GetFormattedDisplayValue(Value.DataValue), GetFormattedDisplayValue(Value2.DataValue));
                }
                return GetFormattedDisplayValue(Value.DataValue);
            }
        }

        protected virtual string GetFormattedDisplayValue(TValue value)
        {
            return value.ToString();
        }

        public virtual string ScoreKey { get { return _scoreKey; } }

        public TScoreManipulator ScoreManipulator { get { return _scoreManipulator; } }


        public ScoringParameter ScoringParameter { get { return _scoringParameter; } }



        private void UpdateScore(object sender, PropertyChangedEventArgs e)
        {
            UpdateScore(Value.DataValue);
        }

        public void RemoveValueFromScore()
        {
            DoRemoveValueFromScore();
        }

        protected string GetFriendlyLabel(ParameterCollection sub)
        {
            string name = string.Empty;

            PlainTextParameter elementLabelParameter = sub.TryGetParameterByName<PlainTextParameter>(ScoringParameter.ElementLabelParameterName);

            if (elementLabelParameter != null && !string.IsNullOrEmpty(elementLabelParameter.Value))
            {
                name = elementLabelParameter.Value;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = sub.Id;
            }

            return WebUtility.HtmlDecode(name);
        }



        protected virtual string GetName()
        {
            return string.IsNullOrEmpty(_scoringParameter.Label) ? _scoringParameter.Name : _scoringParameter.Label;
        }



        protected abstract GapValue<TValue> GetValue();

        protected abstract void UpdateScore(TValue value);

        protected abstract void DoRemoveValueFromScore();

        public virtual bool IsMatch(int? factSetNumber, ScoringMapKey scoringMapKey)
        {
            return factSetNumber == FactSetNumber && ScoreKey == scoringMapKey.ScoreKey;
        }

        public virtual void SetValueOnStartingEdit()
        {
        }

        public override bool IsValid
        {
            get
            {
                return base.IsValid && DataWrapperHelper.AllValid(_cachedListOfDataWrappers);
            }
        }

        [MediatorMessageSink(Constants.ValidateAllScoreErrors)]
        protected void Validate(object data)
        {
            NotifyPropertyChanged(ValueArgs);
        }

        protected static string GetUiString(string key)
        {
            var _key = Prefix + key;
            return ApplicationExtensions.GetResource(_key, string.Empty);
        }

    }
}
