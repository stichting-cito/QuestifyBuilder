using System;
using System.Collections.Generic;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories
{
    class ConceptScoringFactory : IConceptScoringWorkspaceFactory
    {
        public WorkspaceData Create(IEnumerable<ScoringParameter> scorePrms, Solution solution, IItemEditorViewModel itemEditorVM)
        {
            var tmp = new WorkspaceData(imagePath: string.Empty, viewLookupKey: Constants.ScoreEditor.Concept,
                dataValue: new Tuple<IEnumerable<ScoringParameter>, Solution, IItemEditorViewModel>(scorePrms, solution, itemEditorVM),
                displayText: "Concept", isCloseable: false);

            return tmp;
        }

        public bool CanHandle(IEnumerable<Cito.Tester.ContentModel.ScoringParameter> scorePrm)
        {
            if (scorePrm == null)
                throw new ArgumentNullException(nameof(scorePrm));


            return scorePrm.Count() > 0;
        }
    }
}
