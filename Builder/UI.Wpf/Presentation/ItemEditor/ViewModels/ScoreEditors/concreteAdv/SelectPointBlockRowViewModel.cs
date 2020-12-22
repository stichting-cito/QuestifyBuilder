using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class SelectPointBlockRowViewModel : BlockRowViewModelBase<string, ChoiceScoringParameter, IChoiceScoringManipulator>
    {
        public SelectPointBlockRowViewModel(ScoringParameter scoringParameter, IChoiceScoringManipulator scoreManipulator, string scoreKey)
            : base(scoringParameter, scoreManipulator, scoreKey, -1)
        {
            var value = GetValue().Value;
            scoreManipulator.RemoveKey(value);
            scoreManipulator.SetKeyWithDefaultValue(value);
        }

        protected override GapValue<string> GetValue()
        {
            string result = string.Empty;
            if (ScoringParameter.GetType() == typeof(SelectPointScoringParameter) &&
                ((SelectPointScoringParameter)ScoringParameter).Area != null &&
                ((SelectPointScoringParameter)ScoringParameter).Area.ShapeList != null &&
                ((SelectPointScoringParameter)ScoringParameter).Area.ShapeList.Any())
            {
                var shape = ((SelectPointScoringParameter)ScoringParameter).Area.ShapeList.First();
                result = ShapeHelper.GetShapeCoordinates(shape);
            }
            return new GapValue<string>(result);
        }

        protected override void UpdateScore(string value)
        {
            ScoreManipulator.SetKey(value);
        }

        protected override void DoRemoveValueFromScore()
        {
            throw new NotImplementedException();
        }
    }
}
