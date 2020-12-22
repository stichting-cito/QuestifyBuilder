namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.DataTemplates
{
    internal class OperatorEditorSelector : DataTemplateSelectorByTypeName
    {
        public OperatorEditorSelector()
        {
            DefaultTemplate = "DefaultOperatorEditor";
        }

        protected override string ConstructResourceName(object item, string className)
        {
            return string.Format("{0}_OperatorEditor", base.ConstructResourceName(item, className));
        }
    }
}