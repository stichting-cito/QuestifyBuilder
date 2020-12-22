using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    class V2AdvResponseCategoryScoringVM : V2AdvScoringFindingVMBase
    {
        private CombinedScoringMapKey _combinedScoringMapKey;


        public V2AdvResponseCategoryScoringVM(CombinedScoringMapKey combinedScoringMapKey, Solution solution)
            : base(solution)
        {
            _combinedScoringMapKey = combinedScoringMapKey;
        }



        protected override IEnumerable<CombinedScoringMapKey> get_ScoreMap()
        {
            return new[] { _combinedScoringMapKey };
        }

        internal override bool CanAddFactSet(IBlockRowViewModel blockRowViewModel)
        {
            return false;
        }

        protected override bool CanExecuteGroupInteractionsCommand(object c)
        {
            return false;
        }


    }
}
