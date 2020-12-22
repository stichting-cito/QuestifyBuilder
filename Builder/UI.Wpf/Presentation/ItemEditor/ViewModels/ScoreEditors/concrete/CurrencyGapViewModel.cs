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
    [ExportViewModel("ItemEditor.ScoreEditors.CurrencyScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class CurrencyGapViewModel : BaseGapViewModel<decimal?, CurrencyScoringParameter>
    {
        public int IntegerPartMaxLength { private set; get; }
        public int FractionPartMaxLength { private set; get; }

        [ImportingConstructor]
        public CurrencyGapViewModel(IViewAwareStatus viewAwareStatusService)
            : base(viewAwareStatusService)
        {
            if (Designer.IsInDesignMode)
            {
                ScorableItems = new SortedDictionary<string, List<GapValueViewModel<decimal?>>>(StringComparer.CurrentCultureIgnoreCase);
                ScorableItems.Add("Finding A", new List<GapValueViewModel<decimal?>>(toIndex("Finding A", new[] { new GapValue<decimal?>(1.2m), new GapValue<decimal?>(20.2m), new GapValue<decimal?>(6.1m) })));
                IntegerPartMaxLength = 3;
                FractionPartMaxLength = 2;
                Mask = "€ 999" + "." + "99";
            }
        }

        protected override void Init(ScoringParameter scorePrm, Solution solution)
        {
            base.Init(scorePrm, solution);
            var currencyScorePrm = scorePrm as CurrencyScoringParameter;
            IntegerPartMaxLength = ((CurrencyScoringParameter)scorePrm).IntegerPartMaxLength;
            FractionPartMaxLength = ((CurrencyScoringParameter)scorePrm).FractionPartMaxLength;

            try
            {
                if (!string.IsNullOrEmpty(currencyScorePrm.CurrencyCulture))
                {
                    Culture = CultureInfo.GetCultureInfo(currencyScorePrm.CurrencyCulture);
                }
            }
            catch
            {
            }

            Mask = "$ " + string.Empty.PadLeft(IntegerPartMaxLength, '9') + "." + string.Empty.PadRight(FractionPartMaxLength, '9');
        }

        protected override void CreateScoringManipulator(CurrencyScoringParameter scoreParam, Solution solution)
        {
            _scoringManipulator = scoreParam.GetScoreManipulator(solution);
        }

        protected override GapValue<decimal?> GetEmptyItem()
        {
            return new GapValue<decimal?>(null);
        }

        protected override IEnumerable<Rule> GetRules()
        {
            return new Rule[]
            {
                GapValidation_ValidRangeRule("Value", Msg_NoKey),
                GapValidation_ValidRangeRule("Value2", Msg_NoKey)
            };
        }
        internal SimpleRule GapValidation_ValidRangeRule(string property, string msg)
        {
            return new SimpleRule(property, msg, vm =>
            {
                var sgVM = (GapValueViewModel<decimal?>)vm;

                return sgVM.ComparisonType == GapComparisonType.Range && sgVM.Value > sgVM.Value2;
            });
        }
    }
}
