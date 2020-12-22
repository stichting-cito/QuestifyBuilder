using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridCellTemplateColumn : BlockGridCellColumn
    {

        static BlockGridCellTemplateColumn()
        {
        }



        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }

        public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.Register(
                                                                    "CellTemplate",
                                                                    typeof(DataTemplate),
                                                                    typeof(BlockGridCellTemplateColumn),
                                                                    new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BlockGridColumn.NotifyPropertyChangeForRefreshContent)));

        public DataTemplateSelector CellTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(CellTemplateSelectorProperty); }
            set { SetValue(CellTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty CellTemplateSelectorProperty = DependencyProperty.Register(
                                                                            "CellTemplateSelector",
                                                                            typeof(DataTemplateSelector),
                                                                            typeof(BlockGridCellTemplateColumn),
                                                                            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BlockGridColumn.NotifyPropertyChangeForRefreshContent)));

        public DataTemplate CellEditingTemplate
        {
            get { return (DataTemplate)GetValue(CellEditingTemplateProperty); }
            set { SetValue(CellEditingTemplateProperty, value); }
        }

        public static readonly DependencyProperty CellEditingTemplateProperty = DependencyProperty.Register(
                                                                            "CellEditingTemplate",
                                                                            typeof(DataTemplate),
                                                                            typeof(BlockGridCellTemplateColumn),
                                                                            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BlockGridColumn.NotifyPropertyChangeForRefreshContent)));

        public DataTemplateSelector CellEditingTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(CellEditingTemplateSelectorProperty); }
            set { SetValue(CellEditingTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty CellEditingTemplateSelectorProperty = DependencyProperty.Register(
                                                                                    "CellEditingTemplateSelector",
                                                                                    typeof(DataTemplateSelector),
                                                                                    typeof(BlockGridCellTemplateColumn),
                                                                                    new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BlockGridColumn.NotifyPropertyChangeForRefreshContent)));

        private void ChooseCellTemplateAndSelector(bool isEditing, out DataTemplate template, out DataTemplateSelector templateSelector)
        {
            template = null;
            templateSelector = null;

            if (isEditing)
            {
                template = CellEditingTemplate;
                templateSelector = CellEditingTemplateSelector;
            }

            if (template == null && templateSelector == null)
            {
                template = CellTemplate;
                templateSelector = CellTemplateSelector;
            }
        }



        private FrameworkElement LoadTemplateContent(bool isEditing, object dataItem, BlockGridCell cell)
        {
            DataTemplate template;
            DataTemplateSelector templateSelector;
            ChooseCellTemplateAndSelector(isEditing, out template, out templateSelector);
            if (template != null || templateSelector != null)
            {
                ContentPresenter contentPresenter = new ContentPresenter();
                BindingOperations.SetBinding(contentPresenter, ContentPresenter.ContentProperty, new Binding());
                contentPresenter.ContentTemplate = template;
                contentPresenter.ContentTemplateSelector = templateSelector;
                return contentPresenter;
            }

            return null;
        }

        protected override FrameworkElement GenerateElement(ContentControl cell, object dataItem)
        {
            return LoadTemplateContent(false, dataItem, cell as BlockGridCell);
        }

        protected override FrameworkElement GenerateEditingElement(ContentControl cell, object dataItem)
        {
            return LoadTemplateContent(true, dataItem, cell as BlockGridCell);
        }



        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            if (editingElement != null)
            {
                UIElement editingAsUIElement = editingElement as UIElement;

                if (editingAsUIElement != null)
                {
                    if (editingAsUIElement.Focusable)
                    {
                        editingAsUIElement.Focus();
                    }
                    else
                    {
                        UIElement firstChildUIElement = BlockGridHelper.FindFirstVisualChild<UIElement>(editingAsUIElement);
                        if (firstChildUIElement != null && firstChildUIElement.Focusable)
                        {
                            firstChildUIElement.Focus();
                        }
                    }
                }
            }

            return null;
        }



        protected internal override void RefreshCellContent(FrameworkElement element, string propertyName)
        {
            BlockGridCell cell = element as BlockGridCell;
            if (cell != null)
            {
                bool isCellEditing = cell.IsEditing;

                if ((!isCellEditing &&
                        ((string.Compare(propertyName, "CellTemplate", StringComparison.Ordinal) == 0) ||
                        (string.Compare(propertyName, "CellTemplateSelector", StringComparison.Ordinal) == 0))) ||
                    (isCellEditing &&
                        ((string.Compare(propertyName, "CellEditingTemplate", StringComparison.Ordinal) == 0) ||
                        (string.Compare(propertyName, "CellEditingTemplateSelector", StringComparison.Ordinal) == 0))))
                {
                    cell.BuildVisualTree();
                    return;
                }
            }

            base.RefreshCellContent(element, propertyName);
        }

    }

}
