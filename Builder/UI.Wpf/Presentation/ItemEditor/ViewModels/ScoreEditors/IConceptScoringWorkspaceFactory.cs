using System.Collections.Generic;
using Cinch;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    interface IConceptScoringWorkspaceFactory
    {
        WorkspaceData Create(IEnumerable<ScoringParameter> scorePrms, Solution solution, IItemEditorViewModel itemEditorVM);

        bool CanHandle(IEnumerable<ScoringParameter> scorePrm);
    }
}
