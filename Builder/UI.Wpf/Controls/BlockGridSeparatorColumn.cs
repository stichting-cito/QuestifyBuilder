using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridSeparatorColumn : BlockGridContentColumn<BlockInRowSeparator>
    {


        private static Style _defaultElementStyle;
        private static Style _defaultEditingElementStyle;



        public static Style DefaultElementStyle
        {
            get
            {
                if (_defaultElementStyle == null)
                {
                    Style style = new Style(typeof(TextBlock));

                    style.Setters.Add(new Setter(TextBlock.MarginProperty, new Thickness(2.0, 0.0, 2.0, 0.0)));

                    style.Seal();
                    _defaultElementStyle = style;
                }

                return _defaultElementStyle;
            }
        }

        public static Style DefaultEditingElementStyle
        {
            get
            {
                if (_defaultEditingElementStyle == null)
                {
                    Style style = new Style(typeof(TextBox));

                    style.Setters.Add(new Setter(TextBox.BorderThicknessProperty, new Thickness(0.0)));
                    style.Setters.Add(new Setter(TextBox.PaddingProperty, new Thickness(0.0)));

                    style.Seal();
                    _defaultEditingElementStyle = style;
                }

                return _defaultEditingElementStyle;
            }
        }



        public DataTemplate SeparatorTemplate
        {
            get { return (DataTemplate)GetValue(SeparatorTemplateProperty); }
            set { SetValue(SeparatorTemplateProperty, value); }
        }

        public static readonly DependencyProperty SeparatorTemplateProperty = DependencyProperty.Register(
                                                                    "SeparatorTemplate",
                                                                    typeof(DataTemplate),
                                                                    typeof(BlockGridSeparatorColumn),
                                                                    new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BlockGridSeparatorColumn.NotifyPropertyChangeForRefreshContent)));

        public DataTemplateSelector SeparatorTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(SeparatorTemplateSelectorProperty); }
            set { SetValue(SeparatorTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty SeparatorTemplateSelectorProperty = DependencyProperty.Register(
                                                                            "SeparatorTemplateSelector",
                                                                            typeof(DataTemplateSelector),
                                                                            typeof(BlockGridSeparatorColumn),
                                                                            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BlockGridSeparatorColumn.NotifyPropertyChangeForRefreshContent)));


        public static readonly DependencyProperty FontFamilyProperty =
        TextElement.FontFamilyProperty.AddOwner(
                typeof(BlockGridSeparatorColumn),
                new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.Inherits, BlockGridSeparatorColumn.NotifyPropertyChangeForRefreshContent));

        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly DependencyProperty FontSizeProperty =
        TextElement.FontSizeProperty.AddOwner(
                typeof(BlockGridSeparatorColumn),
                new FrameworkPropertyMetadata(SystemFonts.MessageFontSize, FrameworkPropertyMetadataOptions.Inherits, BlockGridSeparatorColumn.NotifyPropertyChangeForRefreshContent));

        [TypeConverter(typeof(FontSizeConverter))]
        [Localizability(LocalizationCategory.None)]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty FontStyleProperty =
        TextElement.FontStyleProperty.AddOwner(
                typeof(BlockGridSeparatorColumn),
                new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle, FrameworkPropertyMetadataOptions.Inherits, BlockGridSeparatorColumn.NotifyPropertyChangeForRefreshContent));

        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        public static readonly DependencyProperty FontWeightProperty =
        TextElement.FontWeightProperty.AddOwner(
                typeof(BlockGridSeparatorColumn),
                new FrameworkPropertyMetadata(SystemFonts.MessageFontWeight, FrameworkPropertyMetadataOptions.Inherits, BlockGridSeparatorColumn.NotifyPropertyChangeForRefreshContent));

        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty =
        TextElement.ForegroundProperty.AddOwner(
                typeof(BlockGridSeparatorColumn),
                new FrameworkPropertyMetadata(SystemColors.ControlTextBrush, FrameworkPropertyMetadataOptions.Inherits, BlockGridSeparatorColumn.NotifyPropertyChangeForRefreshContent));

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }



        private FrameworkElement LoadTemplateContent(bool isEditing, object dataItem, BlockInRowSeparator blockInRowSeparator)
        {
            if (SeparatorTemplate != null || SeparatorTemplateSelector != null)
            {
                ContentPresenter contentPresenter = new ContentPresenter();
                contentPresenter.Content = blockInRowSeparator;
                contentPresenter.ContentTemplate = SeparatorTemplate;
                contentPresenter.ContentTemplateSelector = SeparatorTemplateSelector;
                return contentPresenter;
            }

            return null;
        }

        protected override FrameworkElement GenerateElement(ContentControl contentControl, object dataItem)
        {
            BlockInRowSeparator blockInRowSeparator = (BlockInRowSeparator)contentControl;
            return LoadTemplateContent(false, dataItem, blockInRowSeparator);
        }

        protected override FrameworkElement GenerateEditingElement(ContentControl contentControl, object dataItem)
        {
            BlockInRowSeparator blockInRowSeparator = (BlockInRowSeparator)contentControl;
            return LoadTemplateContent(true, dataItem, blockInRowSeparator);
        }



        protected internal override void RefreshCellContent(FrameworkElement element, string propertyName)
        {
            BlockGridCell cell = element as BlockGridCell;

            if (cell != null)
            {
                FrameworkElement textElement = cell.Content as FrameworkElement;

                if (textElement != null)
                {
                    switch (propertyName)
                    {
                        case "FontFamily":
                            BlockGridHelper.SyncColumnProperty(this, textElement, TextElement.FontFamilyProperty, FontFamilyProperty);
                            break;
                        case "FontSize":
                            BlockGridHelper.SyncColumnProperty(this, textElement, TextElement.FontSizeProperty, FontSizeProperty);
                            break;
                        case "FontStyle":
                            BlockGridHelper.SyncColumnProperty(this, textElement, TextElement.FontStyleProperty, FontStyleProperty);
                            break;
                        case "FontWeight":
                            BlockGridHelper.SyncColumnProperty(this, textElement, TextElement.FontWeightProperty, FontWeightProperty);
                            break;
                        case "Foreground":
                            BlockGridHelper.SyncColumnProperty(this, textElement, TextElement.ForegroundProperty, ForegroundProperty);
                            break;
                    }
                }
            }

            base.RefreshCellContent(element, propertyName);
        }

    }
}
