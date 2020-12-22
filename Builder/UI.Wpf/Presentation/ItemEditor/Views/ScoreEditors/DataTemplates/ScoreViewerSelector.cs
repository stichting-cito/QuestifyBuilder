using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.DataTemplates
{
    internal class ScoreViewerSelector : DataTemplateSelectorByTypeName
    {
        public ScoreViewerSelector()
        {
            DefaultTemplate = "DefaultScoreViewer";
        }

        protected override bool AuxItemFilter(object item)
        {
            return item is IBlockRowViewModel;
        }

        protected override string ConstructResourceName(object item, string className)
        {
            return base.ConstructResourceName(item, className) + "_viewer";
        }
    }
}
