using Cinch;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    public interface IScoringParameterWorkspaceFactory
    {
        WorkspaceData Create(ScoringParameter scorePrm, Solution solution);

        bool CanHandle(ScoringParameter scorePrm);
    }
}
