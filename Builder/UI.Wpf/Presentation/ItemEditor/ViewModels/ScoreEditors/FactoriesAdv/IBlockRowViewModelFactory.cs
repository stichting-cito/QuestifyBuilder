using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv
{
    interface IBlockRowViewModelFactory
    {

        List<IBlockRowViewModel> Create(ScoringMapKey scoringMapKey, int? setNumber, Solution solution);

        IBlockRowViewModel InsertInstance(ScoringParameter scoringParameter, string insertForScoringKey, int? insertAtFactSetNumber, int insertAfterIndex, Solution solution);
    }
}