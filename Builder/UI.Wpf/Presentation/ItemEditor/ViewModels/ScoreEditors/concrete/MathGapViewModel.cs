using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [ExportViewModel("ItemEditor.ScoreEditors.MathScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class MathGapViewModel : BaseGapViewModel<string, MathScoringParameter>
    {


        [ImportingConstructor]
        public MathGapViewModel(IViewAwareStatus viewAwareStatusService)
            : base(viewAwareStatusService)
        {
            if (Designer.IsInDesignMode)
            {
                ScorableItems = new SortedDictionary<string, List<GapValueViewModel<string>>>(StringComparer.CurrentCultureIgnoreCase);
                ScorableItems.Add("Finding A", new List<GapValueViewModel<string>>(toIndex("Finding A", new[] { new GapValue<string>(""), new GapValue<string>(""), new GapValue<string>("") })));
            }
        }


        protected override GapValueViewModel<string> GetGap(string key, int i, GapValue<string> e)
        {
            MathGapValueViewModel gap;
            try
            {
                gap = new MathGapValueViewModel(key, e, i);
            }
            catch (Exception)
            {
                Debug.Assert(false, "Invalid MathML: " + e.Value);
                gap = new MathGapValueViewModel(key, GetEmptyItem(), i);
                e.Value = string.Empty;
            }

            return gap;
        }

        protected override void Init(ScoringParameter scorePrm, Solution solution)
        {
            base.Init(scorePrm, solution);
            var mathScorePrm = scorePrm as MathScoringParameter;
        }

        protected override void CreateScoringManipulator(MathScoringParameter scoreParam, Solution solution)
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
            };
        }
    }
}
