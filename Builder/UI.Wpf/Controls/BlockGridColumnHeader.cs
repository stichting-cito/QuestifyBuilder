using System.Windows;
using System.Windows.Controls.Primitives;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridColumnHeader : ButtonBase
    {


        static BlockGridColumnHeader()
        {
            var ownerType = typeof(BlockGridColumnHeader);
            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));

            ContentProperty.OverrideMetadata(typeof(BlockGridColumnHeader), new FrameworkPropertyMetadata(OnNotifyPropertyChanged, OnCoerceContent));
            HeightProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(OnNotifyPropertyChanged, OnCoerceHeight));
        }

        private static void OnNotifyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BlockGridColumnHeader)d).NotifyPropertyChanged(d, e);
        }



        private BlockGridColumn _column;
        private ContainerTracking<BlockGridColumnHeader> _tracker;



        internal BlockGridColumnHeader()
        {
            _tracker = new ContainerTracking<BlockGridColumnHeader>(this);
        }


        internal void NotifyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BlockGridColumn column = d as BlockGridColumn;
            if ((column != null) && (column != Column))
            {
                return;
            }

            if (e.Property == BlockGridColumn.HeaderProperty || e.Property == ContentProperty)
            {
                BlockGridHelper.TransferProperty(this, ContentProperty);
            }
            else if (e.Property == BlockGrid.ColumnHeaderHeightProperty)
            {
                BlockGridHelper.TransferProperty(this, HeightProperty);
            }

        }


        private static object OnCoerceContent(DependencyObject d, object baseValue)
        {
            var header = d as BlockGridColumnHeader;

            object content = BlockGridHelper.GetCoercedTransferPropertyValue(
                header,
                baseValue,
                ContentProperty,
                header.Column,
                BlockGridColumn.HeaderProperty);


            return content;
        }

        private static object OnCoerceHeight(DependencyObject d, object baseValue)
        {
            var columnHeader = (BlockGridColumnHeader)d;
            BlockGridColumn column = columnHeader.Column;
            BlockGrid blockGrid = null;

            if (column != null)
            {
                blockGrid = column.BlockGridOwner;
            }

            return BlockGridHelper.GetCoercedTransferPropertyValue(
                columnHeader,
                baseValue,
                HeightProperty,
                blockGrid,
                BlockGrid.ColumnHeaderHeightProperty);
        }



        public BlockGridColumn Column
        {
            get { return _column; }
        }

        internal ContainerTracking<BlockGridColumnHeader> Tracker
        {
            get { return _tracker; }
        }



        internal void setColumnData(BlockGridColumn column)
        {
            _column = column;

            BlockGridHelper.TransferProperty(this, ContentProperty);
            BlockGridHelper.TransferProperty(this, HeightProperty);
        }

        internal void ClearHeader()
        {
            _column = null;
        }


    }
}

