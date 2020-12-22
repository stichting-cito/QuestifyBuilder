using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    internal static class BlockRowViewModelFactory
    {
        private static readonly Dictionary<Type, IBlockRowViewModelFactory> KnownFactories;

        static BlockRowViewModelFactory()
        {
            KnownFactories = new Dictionary<Type, IBlockRowViewModelFactory>()
            {
                                {typeof (ChoiceScoringParameter), new ChoiceBlockRowViewModelFactory()},
                {typeof (IntegerScoringParameter), new IntegerBlockRowViewModelFactory()},
                {typeof (StringScoringParameter), new StringBlockRowViewModelFactory()},
                {typeof (DecimalScoringParameter), new DecimalBlockRowViewModelFactory()},
                {typeof (CurrencyScoringParameter), new CurrencyBlockRowViewModelFactory()},
                {typeof (TimeScoringParameter), new TimeBlockRowViewModelFactory()},
                {typeof (DateScoringParameter), new DateBlockRowViewModelFactory()},
                {typeof (InlineChoiceScoringParameter), new InlineChoiceBlockRowViewModelFactory()},
                {typeof (MultiChoiceScoringParameter), new ChoiceBlockRowViewModelFactory()},
                {typeof (MathScoringParameter), new MathBlockRowViewModelFactory()},
                {typeof (GapMatchScoringParameter), new GapMatchBlockRowViewModelFactory()},
                {typeof (OrderScoringParameter), new OrderBlockRowViewModelFactory()},
                {typeof (GraphGapMatchScoringParameter), new GraphGapMatchBlockRowViewModelFactory()},
                {typeof (HotspotScoringParameter), new ChoiceBlockRowViewModelFactory()},
                {typeof (MatrixScoringParameter), new MatrixBlockRowViewModelFactory()},
                {typeof (HotTextScoringParameter), new HotTextBlockRowViewModelFactory()},
                {typeof (HotTextCorrectionScoringParameter), new HotTextCorrectionBlockRowViewModelFactory()},
                {typeof (MathCasDependencyScoringParameter), new MathCasDependencyBlockRowViewModelFactory()},
                {typeof (MathCasEqualScoringParameter), new MathCasEqualBlockRowViewModelFactory()},
                {typeof (CasEqualStepsScoringParameter), new BooleanBlockRowViewModelFactory()},
                {typeof (MathCasEvaluateScoringParameter), new StringBlockRowViewModelFactory()},
                {typeof (GapMatchRichTextScoringParameter), new GapMatchRichTextBlockRowViewModelFactory()},
                {typeof (GeogebraScoringParameter), new GeogebraBlockRowViewModelFactory()},
                {typeof (SelectPointScoringParameter), new ChoiceBlockRowViewModelFactory()}
            };
        }

        public static IEnumerable<IBlockRowViewModel> CreateInstances(CombinedScoringMapKey combinedScoringMapKey,
            Solution solution, int? setNumber = null)
        {
            if (combinedScoringMapKey == null)
            {
                throw new ArgumentNullException(nameof(combinedScoringMapKey));
            }

            if (solution == null)
            {
                throw new ArgumentNullException(nameof(solution));
            }

            ScoringParameter lastScoreParameter = null;
            var ret = new List<IBlockRowViewModel>();

            Debug.Assert(
                (combinedScoringMapKey.IsGroup ^ (setNumber == null)) || combinedScoringMapKey.SetNumbers.Any(),
                "When its a group we expect setNumber to be filled");
            Debug.Assert((combinedScoringMapKey.SetNumbers.Any() ^ (setNumber == null)) || (combinedScoringMapKey.Any(smk => smk.ScoringParameter.GroupInitially)),
                "When its a group we expect setNumber to be filled");

            foreach (var scoreMapKey in combinedScoringMapKey)
            {
                if (!(lastScoreParameter != null && lastScoreParameter.IsSingleChoice &&
                    ReferenceEquals(lastScoreParameter, scoreMapKey.ScoringParameter)))
                {
                    CreatePerScoreMapKey(scoreMapKey, ref ret, setNumber, solution);
                }
                lastScoreParameter = scoreMapKey.ScoringParameter;
            }

            return ret;
        }

        private static void CreatePerScoreMapKey(ScoringMapKey scoreMapKey, ref List<IBlockRowViewModel> ret,
            int? setNumber, Solution solution)
        {
            var perScoringMap = CreateInstances(scoreMapKey, setNumber, solution);

            ret.AddRange(perScoringMap);
        }

        private static IEnumerable<IBlockRowViewModel> CreateInstances(ScoringMapKey scorinMapKey, int? setNumber,
            Solution solution)
        {
            var ret = new List<IBlockRowViewModel>();

            IBlockRowViewModelFactory factory;
            if (KnownFactories.TryGetValue(scorinMapKey.ScoringParameter.GetType(), out factory))
            {
                ret = factory.Create(scorinMapKey, setNumber, solution);
            }
            else
            {
                Debug.Assert(false, $"No factory found for :{scorinMapKey.ScoringParameter.GetType()}");
            }

            return ret;
        }

        public static IBlockRowViewModel InsertInstance(ScoringParameter scorePrm, string insertForScoreKey,
            int? insertAtfactSetNumber, int insertAfterIndex, Solution solution)
        {
            if (scorePrm == null)
            {
                throw new ArgumentNullException(nameof(scorePrm));
            }

            if (string.IsNullOrEmpty(insertForScoreKey))
            {
                throw new ArgumentException("insertForScoreKey");
            }

            if (solution == null)
            {
                throw new ArgumentNullException(nameof(solution));
            }

            IBlockRowViewModel ret = null;

            IBlockRowViewModelFactory factory;
            if (KnownFactories.TryGetValue(scorePrm.GetType(), out factory))
            {
                ret = factory.InsertInstance(scorePrm, insertForScoreKey, insertAtfactSetNumber, insertAfterIndex, solution);
            }
            else
            {
                Debug.Assert(false, $"No factory found for :{scorePrm.GetType()}");
            }

            return ret;
        }
    }
}