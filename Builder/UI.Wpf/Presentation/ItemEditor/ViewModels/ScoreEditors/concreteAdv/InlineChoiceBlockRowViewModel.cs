using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class InlineChoiceBlockRowViewModel : BlockRowViewModelBase<string, InlineChoiceScoringParameter, IChoiceScoringManipulator>
    {

        static PropertyChangedEventArgs ChoicesArgs = ObservableHelper.CreateArgs<InlineChoiceBlockRowViewModel>(x => x.Choices);
        public InlineChoiceBlockRowViewModel(ScoringParameter scoringParameter, IChoiceScoringManipulator scoreManipulator)
    : base(scoringParameter, scoreManipulator, "**Not applicable", -1)
        {
            Choices = new DataWrapper<List<InlineChoiceScoringViewModel.InlineScoringChoiceViewModel>>(this, ChoicesArgs);

            if (scoringParameter.Value != null)
            {
                Choices.DataValue = scoringParameter.Value.Select(sub => new InlineChoiceScoringViewModel.InlineScoringChoiceViewModel() { Id = sub.Id, Value = sub.Id }).ToList();
            }
        }




        public DataWrapper<List<InlineChoiceScoringViewModel.InlineScoringChoiceViewModel>> Choices
        {
            get; set;
        }


        protected override string GetFormattedDisplayValue(string value)
        {
            var currentValue = Choices.DataValue.FirstOrDefault(t => t.Id == value);
            if (currentValue != null)
            {
                return currentValue.Value;
            }
            return base.GetFormattedDisplayValue(value);
        }

        protected override string GetName()
        {
            string nameViaBase = base.GetName();

            return !string.IsNullOrWhiteSpace(nameViaBase) ? nameViaBase :
                   !string.IsNullOrWhiteSpace(ScoringParameter.InlineId) ? ScoringParameter.InlineId :
                   !string.IsNullOrWhiteSpace(ScoringParameter.ControllerId) ? ScoringParameter.ControllerId : null;
        }


        public override string ScoreKey
        {
            get
            {
                return Value.DataValue;
            }
        }

        protected override GapValue<string> GetValue()
        {
            var combinedKey = String.Join(string.Empty, ScoreManipulator.GetKeysAlreadyManipulated());
            return new GapValue<string>(combinedKey);
        }

        protected override void UpdateScore(string value)
        {

            ScoreManipulator.SetKey(value);
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
