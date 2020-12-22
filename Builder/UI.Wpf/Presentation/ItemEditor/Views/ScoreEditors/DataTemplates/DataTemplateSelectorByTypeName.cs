using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.DataTemplates
{

    internal class DataTemplateSelectorByTypeName : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var resourceName = string.Empty;

            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && AuxItemFilter(item))
            {
                var className = item.GetType().Name;
                resourceName = ConstructResourceName(item, className);
                var dataTemplate = element.TryFindResource(resourceName) as DataTemplate;

                return (dataTemplate == null) ? GetDefaultTemplate(element) : dataTemplate;
            }

            return null;
        }

        private DataTemplate GetDefaultTemplate(FrameworkElement element)
        {
            if (!string.IsNullOrEmpty(DefaultTemplate))
                return (DataTemplate)element.FindResource(DefaultTemplate);

            return null;
        }

        protected virtual bool AuxItemFilter(object item)
        {
            return true;
        }

        protected virtual string ConstructResourceName(object item, string className)
        {
            return string.Format("{0}", className);
        }

        public string DefaultTemplate { get; set; }
    }
}
