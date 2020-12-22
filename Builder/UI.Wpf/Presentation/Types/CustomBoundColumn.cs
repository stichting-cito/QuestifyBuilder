using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    [DebuggerDisplay("{Header} : TemplateName [{TemplateName}];HeaderTemplateName [{HeaderTemplateName}]")]
    public class CustomBoundColumn : DataGridBoundColumn
    {
        public string TemplateName { get; set; }
        public string HeaderTemplateName { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var binding = new Binding(((Binding)Binding).Path.Path);
            binding.Source = dataItem;

            if (!string.IsNullOrEmpty(HeaderTemplateName) && cell.Column.HeaderTemplate == null)
            {
                cell.Column.HeaderTemplate = (DataTemplate)cell.FindResource(HeaderTemplateName);
            }

            var content = new ContentControl();

            if (!string.IsNullOrEmpty(TemplateName))
            {
                content.ContentTemplate = (DataTemplate)cell.FindResource(TemplateName);
            }

            content.SetBinding(ContentControl.ContentProperty, binding);
            return content;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}
