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
    [ExportViewModel("ItemEditor.ScoreEditors.DateScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class DateGapViewModel : BaseGapViewModel<string, DateScoringParameter>
    {
        private string _dateFormat;
        private string _dateSeparator;


        [ImportingConstructor]
        public DateGapViewModel(IViewAwareStatus viewAwareStatusService)
            : base(viewAwareStatusService)
        {
            if (Designer.IsInDesignMode)
            {
                ScorableItems = new SortedDictionary<string, List<GapValueViewModel<string>>>(StringComparer.CurrentCultureIgnoreCase);
                ScorableItems.Add("Finding A", new List<GapValueViewModel<string>>(toIndex("Finding A", new[] { new GapValue<string>("1/1/2014"), new GapValue<string>("2/1/2014"), new GapValue<string>("3/1/2014") })));

                Mask = "99\\/99\\/9999";
            }
        }


        protected override void Init(ScoringParameter scorePrm, Solution solution)
        {
            base.Init(scorePrm, solution);
            var dateScorePrm = scorePrm as DateScoringParameter;
            _dateFormat = dateScorePrm.DateFormat.ToUpper();
            _dateFormat = _dateFormat.Replace("D", "d").Replace("Y", "y");

            var mask = string.Empty;
            foreach (var c in dateScorePrm.DateFormat)
            {
                if (c == 'd' || c == 'm' || c == 'y' ||
                    c == 'D' || c == 'M' || c == 'Y')
                {
                    mask += "9";
                }
                else
                {
                    mask += "\\" + c;
                    _dateSeparator = c.ToString();
                }
            }
            Mask = mask;
        }

        protected override void CreateScoringManipulator(DateScoringParameter scoreParam, Solution solution)
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
                    ci.DateTimeFormat.DateSeparator = _dateSeparator;
                    DateTime rs;
                    var valid = DateTime.TryParseExact(sgVM.Value, _dateFormat, ci, System.Globalization.DateTimeStyles.None, out rs);

                    return !valid;
                });
        }
    }
}
