using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [ExportViewModel("ItemEditor.ScoreEditors.TimeScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class TimeGapViewModel : BaseGapViewModel<string, TimeScoringParameter>
    {
        private string _timeFormat;
        private string _timeSeparator;


        [ImportingConstructor]
        public TimeGapViewModel(IViewAwareStatus viewAwareStatusService)
            : base(viewAwareStatusService)
        {
            if (Designer.IsInDesignMode)
            {
                ScorableItems = new SortedDictionary<string, List<GapValueViewModel<string>>>(StringComparer.CurrentCultureIgnoreCase);
                ScorableItems.Add("Finding A", new List<GapValueViewModel<string>>(toIndex("Finding A", new[] { new GapValue<string>("10:12"), new GapValue<string>("16:30"), new GapValue<string>("19:00") })));

                Mask = "99\\:99";
            }
        }


        protected override void Init(ScoringParameter scorePrm, Solution solution)
        {
            base.Init(scorePrm, solution);
            var timeScorePrm = scorePrm as TimeScoringParameter;

            if (timeScorePrm == null)
            {
                return;
            }

            _timeFormat = timeScorePrm.TimeFormat.ToUpper(CultureInfo.InvariantCulture);
            _timeFormat = _timeFormat.Replace("M", "m").Replace("S", "s");

            var mask = string.Empty;
            foreach (var c in timeScorePrm.TimeFormat)
            {
                if (c == 'h' || c == 'm' || c == 's' ||
                    c == 'H' || c == 'M' || c == 'S')
                {
                    mask += "9";
                }
                else
                {
                    mask += "\\" + c;
                    _timeSeparator = c.ToString();
                }
            }
            Mask = mask;
        }

        protected override void CreateScoringManipulator(TimeScoringParameter scoreParam, Solution solution)
        {
            _scoringManipulator = scoreParam.GetScoreManipulator(solution);
        }

        protected override GapValue<string> GetEmptyItem()
        {
            return new GapValue<string>(string.Empty);
        }

        protected override IEnumerable<Rule> GetRules()
        {
            return new Rule[]
            {
                GapValidation_DateFormatRule("Value", Msg_NoKey),
                GapValidation_DateFormatRule("Value2", Msg_NoKey),
            };
        }

        internal SimpleRule GapValidation_DateFormatRule(string property, string msg)
        {
            return new SimpleRule(property, msg, vm =>
                {
                    var sgVM = (GapValueViewModel<string>)vm;

                    var ci = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                    ci.DateTimeFormat.TimeSeparator = _timeSeparator;
                    DateTime rs;
                    var valid = DateTime.TryParseExact(sgVM.Value, _timeFormat, ci, System.Globalization.DateTimeStyles.None, out rs);

                    return !valid;
                });
        }
    }
}
