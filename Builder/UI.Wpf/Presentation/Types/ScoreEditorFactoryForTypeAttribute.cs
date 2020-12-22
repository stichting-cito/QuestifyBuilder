using System;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    internal class ScoreEditorFactoryForTypeAttribute : EditorFactoryForTypeAttribute
    {
        public ScoreEditorFactoryForTypeAttribute(Type valueHoldingType)
    : base(typeof(IScoringParameterWorkspaceFactory), valueHoldingType)
        { }
    }

}
