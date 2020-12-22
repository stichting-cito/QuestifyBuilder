using System;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories
{
    [ScoreEditorFactoryForType(typeof(MultiChoiceScoringParameter))]
    internal class MCScoringVWFactory : DefaultScoreEditorFactory<MultiChoiceScoringParameter>
    {
        protected override WorkspaceData DoCreate(MultiChoiceScoringParameter scorePrm, Solution solution)
        {
            var display = !string.IsNullOrEmpty(scorePrm.Label) ? scorePrm.Label : scorePrm.ControllerId;
            var tmp = new WorkspaceData(imagePath: string.Empty, viewLookupKey: Constants.ScoreEditor.MultiChoice, dataValue: new Tuple<ScoringParameter, Solution>(scorePrm, solution), displayText: display, isCloseable: false);
            return tmp;
        }
    }

}
