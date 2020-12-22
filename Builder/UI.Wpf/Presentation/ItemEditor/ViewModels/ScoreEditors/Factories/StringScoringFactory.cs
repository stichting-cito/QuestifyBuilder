using System;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories
{

    [ScoreEditorFactoryForType(typeof(StringScoringParameter))]
    internal class StringScoringFactory : DefaultScoreEditorFactory<StringScoringParameter>
    {
        protected override WorkspaceData DoCreate(StringScoringParameter scorePrm, Solution solution)
        {
            var usableConverters = scorePrm.DesignerSettings.GetSettingValueByKey("PreprocessRules");

            if (!string.IsNullOrEmpty(usableConverters))
                scorePrm.PreprocessRules = usableConverters;

            var display = !string.IsNullOrEmpty(scorePrm.Label) ? scorePrm.Label : scorePrm.ControllerId;
            var tmp = new WorkspaceData(imagePath: string.Empty, viewLookupKey: Constants.ScoreEditor.StringGap, dataValue: new Tuple<ScoringParameter, Solution>(scorePrm, solution), displayText: display, isCloseable: false);
            return tmp;
        }

    }

}
