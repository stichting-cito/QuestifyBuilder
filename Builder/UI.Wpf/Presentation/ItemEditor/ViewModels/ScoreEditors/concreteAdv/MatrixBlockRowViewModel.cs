using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class MatrixBlockRowViewModel : BlockRowViewModelBase<string, ChoiceScoringParameter, IChoiceArrayScoringManipulator>
    {

        static PropertyChangedEventArgs NumericIdentifiersArgs = ObservableHelper.CreateArgs<ChoiceBlockRowViewModel>(x => x.NumericIdentifiers); static PropertyChangedEventArgs AlternativesCountArgs = ObservableHelper.CreateArgs<ChoiceBlockRowViewModel>(x => x.AlternativesCount); static PropertyChangedEventArgs ChoicesArgs = ObservableHelper.CreateArgs<ChoiceBlockRowViewModel>(x => x.Choices);

        public MatrixBlockRowViewModel(ScoringParameter scoringParameter, IChoiceArrayScoringManipulator scoreManipulator, string scoreKey)
    : base(scoringParameter, scoreManipulator, scoreKey, -1)
        {
            Choices = new DataWrapper<List<ChoiceOptionViewModel>>(this, ChoicesArgs);

            if (scoringParameter is MatrixScoringParameter)
            {
                var matrixScoringParameter = (MatrixScoringParameter)scoringParameter;

                if (matrixScoringParameter.MatrixColumnsDefinition != null && matrixScoringParameter.MatrixColumnsDefinition.Value != null)
                {
                    Choices.DataValue = matrixScoringParameter.MatrixColumnsDefinition.Value.Select(sub => new ChoiceOptionViewModel() { Id = sub.Id, Value = GetFriendlyLabel(sub) }).ToList();
                }
            }

            NumericIdentifiers = new DataWrapper<bool>(this, NumericIdentifiersArgs);
            NumericIdentifiers.DataValue = false;

            AlternativesCount = new DataWrapper<int>(this, AlternativesCountArgs);
            AlternativesCount.DataValue = scoringParameter.AlternativesCount.HasValue ? scoringParameter.AlternativesCount.Value : scoringParameter.Value.Count();
        }



        public DataWrapper<bool> NumericIdentifiers { get; set; }

        public DataWrapper<int> AlternativesCount
        {
            get;
            set;
        }

        public DataWrapper<List<ChoiceOptionViewModel>> Choices { get; set; }


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



        protected override GapValue<string> GetValue()
        {
            var keyStatus = ScoreManipulator.GetKeyStatus().FirstOrDefault(s => s.Key == ScoreKey);
            var value = keyStatus.Value;
            return new GapValue<string>((value ?? "").ToString());
        }


        protected override void UpdateScore(string value)
        {
            ScoreManipulator.SetKey(ScoreKey, value);
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
            throw new NotImplementedException();
        }

        public override bool IsMatch(int? factSetNumber, ScoringMapKey scoringMapKey)
        {
            return factSetNumber == FactSetNumber && Name == scoringMapKey.ScoringParameterName;
        }
    }
}
