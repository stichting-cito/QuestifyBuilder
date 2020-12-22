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
    [ExportViewModel("ItemEditor.ScoreEditors.IntegerScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class IntegerGapViewModel : BaseGapViewModel<MultiType, IntegerScoringParameter>
    {
        public int MaxLength { private set; get; }


        [ImportingConstructor]
        public IntegerGapViewModel(IViewAwareStatus viewAwareStatusService)
            : base(viewAwareStatusService)
        {
            if (Designer.IsInDesignMode)
            {
                ScorableItems = new SortedDictionary<string, List<GapValueViewModel<MultiType>>>(StringComparer.CurrentCultureIgnoreCase);
                ScorableItems.Add("Finding A", new List<GapValueViewModel<MultiType>>(toIndex("Finding A", new[] { new GapValue<MultiType>(1), new GapValue<MultiType>(2), new GapValue<MultiType>(6) })));
                MaxLength = 5;
            }
        }


        protected override void Init(ScoringParameter scorePrm, Solution solution)
        {
            base.Init(scorePrm, solution);
            MaxLength = ((IntegerScoringParameter)scorePrm).MaxLength;
        }

        protected override void CreateScoringManipulator(IntegerScoringParameter scoreParam, Solution solution)
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
                var sgVM = (GapValueViewModel<int>)vm;

                return sgVM.ComparisonType == GapComparisonType.Range && sgVM.Value > sgVM.Value2;
            });
        }
    }
}
