using System;
using System.Globalization;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class DateBlockRowViewModel : GapBlockRowViewModelBase<string, DateScoringParameter, IGapScoringManipulator<string>>
    {

        public DateBlockRowViewModel(DateScoringParameter scoringParameter, IGapScoringManipulator<string> scoreManipulator, string scoreKey, int index) : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
            DateFormat = scoringParameter.DateFormat;


        }


        public string DateFormat { get; set; }

        protected override string GetFormattedDisplayValue(string value)
        {
            return base.GetFormattedDisplayValue(value);
        }

        internal SimpleRule GapValidation_DateFormatRule(string property, string msg)
        {
            return new SimpleRule(property, msg, vm =>
            {
                var prop = (DataWrapper<string>)vm;
                DateTime rs;
                var valid = DateTime.TryParseExact(prop.DataValue, "d", CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out rs);

                return !valid;
            });
        }

    }

}
