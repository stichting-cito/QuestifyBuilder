using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridCellTextColumn : BlockGridCellBoundColumn
    {


        private static Style _defaultElementStyle;
        private static Style _defaultEditingElementStyle;

        static BlockGridCellTextColumn()
        {
            var ownerType = typeof(BlockGridCellTextColumn);
            ElementStyleProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }



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



        public static readonly DependencyProperty FontFamilyProperty =
        TextElement.FontFamilyProperty.AddOwner(
                typeof(BlockGridCellTextColumn),
                new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.Inherits, BlockGridCellTextColumn.NotifyPropertyChangeForRefreshContent));

        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly DependencyProperty FontSizeProperty =
        TextElement.FontSizeProperty.AddOwner(
                typeof(BlockGridCellTextColumn),
                new FrameworkPropertyMetadata(SystemFonts.MessageFontSize, FrameworkPropertyMetadataOptions.Inherits, BlockGridCellTextColumn.NotifyPropertyChangeForRefreshContent));

        [TypeConverter(typeof(FontSizeConverter))]
        [Localizability(LocalizationCategory.None)]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty FontStyleProperty =
        TextElement.FontStyleProperty.AddOwner(
                typeof(BlockGridCellTextColumn),
                new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle, FrameworkPropertyMetadataOptions.Inherits, BlockGridCellTextColumn.NotifyPropertyChangeForRefreshContent));

        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        public static readonly DependencyProperty FontWeightProperty =
        TextElement.FontWeightProperty.AddOwner(
                typeof(BlockGridCellTextColumn),
                new FrameworkPropertyMetadata(SystemFonts.MessageFontWeight, FrameworkPropertyMetadataOptions.Inherits, BlockGridCellTextColumn.NotifyPropertyChangeForRefreshContent));

        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty =
        TextElement.ForegroundProperty.AddOwner(
                typeof(BlockGridCellTextColumn),
                new FrameworkPropertyMetadata(SystemColors.ControlTextBrush, FrameworkPropertyMetadataOptions.Inherits, BlockGridCellTextColumn.NotifyPropertyChangeForRefreshContent));

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }



        protected override FrameworkElement GenerateElement(ContentControl contentControl, object dataItem)
        {
            BlockGridCell cell = (BlockGridCell)contentControl;
            TextBlock textBlock = new TextBlock();

            SyncProperties(textBlock);

            ApplyStyle(false, false, textBlock);
            ApplyBinding(textBlock, TextBlock.TextProperty);

            BlockGridHelper.RestoreFlowDirection(textBlock, cell);

            return textBlock;
        }

        protected override FrameworkElement GenerateEditingElement(ContentControl contentControl, object dataItem)
        {
            BlockGridCell cell = (BlockGridCell)contentControl;
            TextBox textBox = new TextBox();

            SyncProperties(textBox);

            ApplyStyle(true, false, textBox);
            ApplyBinding(textBox, TextBox.TextProperty);

            BlockGridHelper.RestoreFlowDirection(textBox, cell);

            return textBox;
        }

        private void SyncProperties(FrameworkElement e)
        {
            BlockGridHelper.SyncColumnProperty(this, e, TextElement.FontFamilyProperty, FontFamilyProperty);
            BlockGridHelper.SyncColumnProperty(this, e, TextElement.FontSizeProperty, FontSizeProperty);
            BlockGridHelper.SyncColumnProperty(this, e, TextElement.FontStyleProperty, FontStyleProperty);
            BlockGridHelper.SyncColumnProperty(this, e, TextElement.FontWeightProperty, FontWeightProperty);
            BlockGridHelper.SyncColumnProperty(this, e, TextElement.ForegroundProperty, ForegroundProperty);
        }

        protected internal override void RefreshCellContent(FrameworkElement element, string propertyName)
        {
            DataGridCell cell = element as DataGridCell;

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



        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            TextBox textBox = editingElement as TextBox;
            if (textBox != null)
            {
                textBox.Focus();

                string originalValue = textBox.Text;

                TextCompositionEventArgs textArgs = editingEventArgs as TextCompositionEventArgs;
                if (textArgs != null)
                {
                    string inputText = ConvertTextForEdit(textArgs.Text);
                    textBox.Text = inputText;

                    textBox.Select(inputText.Length, 0);
                }
                else
                {
                    MouseButtonEventArgs mouseArgs = editingEventArgs as MouseButtonEventArgs;
                    if ((mouseArgs == null) || !PlaceCaretOnTextBox(textBox, Mouse.GetPosition(textBox)))
                    {
                        textBox.SelectAll();
                    }
                }

                return originalValue;
            }

            return null;
        }

        private string ConvertTextForEdit(string s)
        {
            if (s == "\b")
            {
                return String.Empty;
            }

            return s;
        }

        private static bool PlaceCaretOnTextBox(TextBox textBox, Point position)
        {
            int characterIndex = textBox.GetCharacterIndexFromPoint(position, false);
            if (characterIndex >= 0)
            {
                textBox.Select(characterIndex, 0);
                return true;
            }

            return false;
        }

        internal override void OnInput(InputEventArgs e)
        {
            if (BlockGridHelper.HasNonEscapeCharacters(e as TextCompositionEventArgs))
            {
                TryBeginEdit(e, true);
            }

            else if (BlockGridHelper.IsImeProcessed(e as KeyEventArgs))
            {
                if (BlockGridOwner != null)
                {
                    BlockGridCell cell = BlockGridOwner.CurrentCellContainer;
                    if (cell != null && !cell.IsEditing)
                    {
                        Debug.Assert(e.RoutedEvent == Keyboard.PreviewKeyDownEvent, "We should only reach here on the PreviewKeyDown event because the TextBox within is expected to handle the preview event and hence trump the successive KeyDown event.");

                        TryBeginEdit(e, false);

                        Dispatcher.Invoke((Action)delegate { }, System.Windows.Threading.DispatcherPriority.Background);
                    }
                }
            }
        }


    }
}
