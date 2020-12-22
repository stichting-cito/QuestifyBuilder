using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [ExportViewModel("ItemEditor.ScoreEditors.DecimalScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class DecimalGapViewModel : BaseGapViewModel<MultiType, DecimalScoringParameter>
    {
        public int IntegerPartMaxLength { private set; get; }
        public int FractionPartMaxLength { private set; get; }


        [ImportingConstructor]
        public DecimalGapViewModel(IViewAwareStatus viewAwareStatusService)
            : base(viewAwareStatusService)
        {
            if (Designer.IsInDesignMode)
            {
                ScorableItems = new SortedDictionary<string, List<GapValueViewModel<MultiType>>>(StringComparer.CurrentCultureIgnoreCase);
                ScorableItems.Add("Finding A", new List<GapValueViewModel<MultiType>>(toIndex("Finding A", new[] { new GapValue<MultiType>(1.2m), new GapValue<MultiType>(20.2m), new GapValue<MultiType>(6.1m) })));
                IntegerPartMaxLength = 3;
                FractionPartMaxLength = 2;
                Mask = "#" + "999" + "." + "99";
            }
        }


        protected override void Init(ScoringParameter scorePrm, Solution solution)
        {
            base.Init(scorePrm, solution);
            IntegerPartMaxLength = ((DecimalScoringParameter)scorePrm).IntegerPartMaxLength;
            FractionPartMaxLength = ((DecimalScoringParameter)scorePrm).FractionPartMaxLength;
            Mask = "#" + string.Empty.PadLeft(IntegerPartMaxLength, '9') + "." + string.Empty.PadRight(FractionPartMaxLength, '9');
        }

        protected override void CreateScoringManipulator(DecimalScoringParameter scoreParam, Solution solution)
        {
            _scoringManipulator = scoreParam.GetScoreManipulator(solution);
        }

        protected override GapValue<MultiType> GetEmptyItem()
        {
            return new GapValue<MultiType>(null);
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
                var sgVM = (GapValueViewModel<MultiType>)vm;

                return sgVM.ComparisonType == GapComparisonType.Range && sgVM.Value > sgVM.Value2;
            });
        }
    }
}
