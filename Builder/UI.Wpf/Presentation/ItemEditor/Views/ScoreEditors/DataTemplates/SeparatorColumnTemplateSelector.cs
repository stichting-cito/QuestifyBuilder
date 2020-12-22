using System;
using System.Windows;
using System.Windows.Controls;
using Questify.Builder.UI.Wpf.Controls;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.DataTemplates
{
    internal class SeparatorColumnTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var resourceName = String.Empty;

            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is BlockInRowSeparator)
            {
                BlockInRowSeparator blockInRowSeparator = item as BlockInRowSeparator;

                if (blockInRowSeparator.IsInBetweenBlocks)
                {
                    resourceName = "BlockInRowSeparatorInBetweenBlocks";
                }
                else
                {
                    resourceName = "BlockInRowSeparatorEndingBlock";
                }

                return element.FindResource(resourceName) as DataTemplate;
            }

            return null;
        }
    }
}
