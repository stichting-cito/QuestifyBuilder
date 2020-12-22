using System.Diagnostics;
using System.Windows;

namespace Questify.Builder.UI.Wpf.Controls
{
    public abstract class BlockGridColumn : DependencyObject
    {
        private const int DefaultWidth = 100;


        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
    DependencyProperty.Register(
                            "Header",
                            typeof(object),
                            typeof(BlockGridColumn),
                            new FrameworkPropertyMetadata(null, OnNotifyColumnHeaderPropertyChanged));

        private static void OnNotifyColumnHeaderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BlockGridColumn)d).NotifyPropertyChanged(d, e, BlockGridNotificationTarget.Columns | BlockGridNotificationTarget.ColumnHeaders);
        }



        public int Width
        {
            get { return (int)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly DependencyProperty WidthProperty =
    DependencyProperty.Register(
        "Width",
        typeof(int),
        typeof(BlockGridColumn),
        new FrameworkPropertyMetadata(DefaultWidth, OnWidthPropertyChanged, OnCoerceWidth));

        private static void OnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BlockGridColumn column = (BlockGridColumn)d;
            int oldWidth = (int)e.OldValue;
            int newWidth = (int)e.NewValue;
            BlockGrid blockGrid = column.BlockGridOwner;
        }

        private static object OnCoerceWidth(DependencyObject d, object baseValue)
        {
            return baseValue;
        }



        public Style CellStyle
        {
            get { return (Style)GetValue(CellStyleProperty); }
            set { SetValue(CellStyleProperty, value); }
        }

        public static readonly DependencyProperty CellStyleProperty =
    DependencyProperty.Register("CellStyle", typeof(Style), typeof(BlockGridColumn), new PropertyMetadata(null, OnNotifyCellPropertyChanged, OnCoerceCellStyle));

        private static object OnCoerceCellStyle(DependencyObject d, object baseValue)
        {
            var column = d as BlockGridColumn;
            return BlockGridHelper.GetCoercedTransferPropertyValue(
                column,
                baseValue,
                CellStyleProperty,
                column.BlockGridOwner,
                BlockGrid.CellStyleProperty);
        }



        internal static void OnNotifyCellPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BlockGridColumn)d).NotifyPropertyChanged(d, e, BlockGridNotificationTarget.Columns | BlockGridNotificationTarget.Cells);
        }







        internal void NotifyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, BlockGridNotificationTarget target)
        {
            if (BlockGridHelper.ShouldNotifyColumns(target))
            {
                if (e.Property == BlockGrid.IsReadOnlyProperty || e.Property == BlockGridCellColumn.IsReadOnlyProperty)
                {
                    BlockGridHelper.TransferProperty(this, BlockGridCellColumn.IsReadOnlyProperty);
                }
            }
        }

        internal static void NotifyPropertyChangeForRefreshContent(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.Assert(d is BlockGridColumn, "d should be a DataGridColumn");

            ((BlockGridColumn)d).NotifyPropertyChanged(e.Property.Name);
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (BlockGridOwner != null)
            {
                BlockGridOwner.NotifyPropertyChanged(this, propertyName, new DependencyPropertyChangedEventArgs(), BlockGridNotificationTarget.RefreshCellContent);
            }
        }

        protected internal virtual void RefreshCellContent(FrameworkElement element, string propertyName)
        {
        }



        internal BlockGrid BlockGridOwner { get; set; }
    }
}
