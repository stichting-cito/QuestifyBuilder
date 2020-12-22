using System;
using System.Globalization;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class TimeBlockRowViewModel : GapBlockRowViewModelBase<string, TimeScoringParameter, IGapScoringManipulator<string>>
    {

        public TimeBlockRowViewModel(TimeScoringParameter scoringParameter, IGapScoringManipulator<string> scoreManipulator, string scoreKey, int index) : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
            TimeFormat = scoringParameter.TimeFormat;

            Value.AddRule(GapValidation_DateFormatRule("DataValue", "Error"));
        }


        public string TimeFormat { get; set; }

        internal SimpleRule GapValidation_DateFormatRule(string property, string msg)
        {
            return new SimpleRule(property, msg, vm =>
            {
                var prop = (DataWrapper<string>)vm;
                var ci = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                ci.DateTimeFormat.TimeSeparator = ":";
                DateTime rs;
                var valid = DateTime.TryParseExact(prop.DataValue, ConvertTimeFormat(TimeFormat), ci, System.Globalization.DateTimeStyles.None, out rs);

                return !valid;
            });
        }

        private string ConvertTimeFormat(string format)
        {
            return format.Replace("h", "H");
        }
    }
}
