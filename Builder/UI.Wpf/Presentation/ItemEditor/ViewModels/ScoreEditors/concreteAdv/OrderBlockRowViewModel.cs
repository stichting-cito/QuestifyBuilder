using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class OrderBlockRowViewModel : BlockRowViewModelBase<int, OrderScoringParameter, IOrderScoringManipulator>
    {
        static readonly PropertyChangedEventArgs MovableElementArgs = ObservableHelper.CreateArgs<OrderBlockRowViewModel>(x => x.MovableElements);

        public OrderBlockRowViewModel(OrderScoringParameter scoringParameter, IOrderScoringManipulator scoreManipulator, string scoreKey)
            : base(scoringParameter, scoreManipulator, scoreKey, -1)
        {
            MovableElements = new DataWrapper<List<OrderBlockRowViewModel.MovableElementViewModel>>(this, MovableElementArgs);
            if (scoringParameter.Value != null)
            {
                MovableElements.DataValue = scoringParameter.Value.Select(sub => new MovableElementViewModel { Id = scoringParameter.Value.IndexOf(sub) + 1, Value = GetFriendlyLabel(sub) }).ToList();
            }

            Value.AddRule(AssignEachElementToOnePositionOnlyRule("DataValue", GetUiString("Order.ValidateAssignToOnePositionOnly")));
        }

        protected override string GetName()
        {
            int zeroBasedIndexOfScoreKey = ScoringParameter.Value.IndexOf(ScoringParameter.Value.FirstOrDefault(sub => sub.Id == ScoreKey));
            if (zeroBasedIndexOfScoreKey > -1)
                return string.Format("{0} {1}", GetUiString("Order.PostionLabel"), zeroBasedIndexOfScoreKey + 1);

            return string.Empty;
        }

        protected override GapValue<int> GetValue()
        {
            var keyStatus = ScoreManipulator.GetKeyStatus().FirstOrDefault(s => s.Key == ScoreKey);
            var value = keyStatus.Value;
            return new GapValue<int>(value);
        }

        protected override void UpdateScore(int value)
        {
            ScoreManipulator.SetKey(ScoreKey, value);

            Mediator.Instance.NotifyColleagues(Constants.ValidateAllScoreErrors, new EventArgs());
        }

        protected override string GetFormattedDisplayValue(int value)
        {
            if (value <= 0)
                return string.Empty;

            return MovableElements.DataValue[value - 1].Value;
        }

        protected override void DoRemoveValueFromScore()
        {
        }


        public DataWrapper<List<MovableElementViewModel>> MovableElements
        {
            get;
            set;
        }



        private SimpleRule AssignEachElementToOnePositionOnlyRule(string property, string msg)
        {
            return new SimpleRule(property, msg, data =>
            {
                var value = (DataWrapper<int>)data;
                var result = ScoreManipulator.IsValid(value.DataValue);
                return !result;
            });
        }

        public class MovableElementViewModel
        {
            public int Id { get; set; }

            public string Value { get; set; }
        }
    }
}