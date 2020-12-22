using System;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories
{
    abstract class DefaultScoreEditorFactory<TScorePrm> : IScoringParameterWorkspaceFactory where TScorePrm : Cito.Tester.ContentModel.ScoringParameter
    {
        public WorkspaceData Create(Cito.Tester.ContentModel.ScoringParameter scorePrm, Cito.Tester.ContentModel.Solution solution)
        {
            return DoCreate((TScorePrm)scorePrm, solution);
        }

        protected abstract WorkspaceData DoCreate(TScorePrm scorePrm, Cito.Tester.ContentModel.Solution solution);

        public virtual bool CanHandle(Cito.Tester.ContentModel.ScoringParameter scorePrm)
        {
            if (scorePrm == null)
                throw new ArgumentNullException(nameof(scorePrm));

            return typeof(TScorePrm).IsAssignableFrom(scorePrm.GetType());
        }
    }
}
