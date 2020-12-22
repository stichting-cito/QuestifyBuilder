using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.DataTemplates
{
    internal class ScoreEditorSelector : DataTemplateSelectorByTypeName
    {
        public ScoreEditorSelector()
        {
            DefaultTemplate = "DefaultScoreEditor";
        }

        protected override bool AuxItemFilter(object item)
        {
            return item is IBlockRowViewModel;
        }

        protected override string ConstructResourceName(object item, string className)
        {
            return string.Format("{0}_editor", base.ConstructResourceName(item, className));
        }
    }

}
