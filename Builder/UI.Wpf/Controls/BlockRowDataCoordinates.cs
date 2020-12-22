namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockRowDataCoordinates
    {
        internal BlockRowDataCoordinates(object blockGridItemsSource, BlockGridCell blockGridCell, bool determineRowIndexes)
        {
            BlockGridDataItem = blockGridItemsSource;

            if (blockGridCell != null)
            {
                BlockRow blockRow = blockGridCell.BlockGridCellOwner;

                BlockGridBlockRowDataItem = blockRow.BlockRowOwner.BlockInRowOwner.DataContext;
                BlockInRowDataItem = blockRow.BlockRowOwner.DataContext;
                BlockRowDataItem = blockRow.DataContext;

                if (determineRowIndexes)
                {
                    RowIndexValuesAreSet = true;

                    BlockGridBlockRowIndex = blockRow.BlockRowOwner.BlockGrid.Items.IndexOf(BlockGridBlockRowDataItem);
                    BlockInRowIndex = blockRow.BlockRowOwner.BlockInRowOwner.Items.IndexOf(BlockInRowDataItem);
                    BlockRowIndex = blockRow.BlockRowOwner.IndexOf(BlockRowDataItem);
                }
            }
        }

        internal BlockRowDataCoordinates(BlockRow blockRow)
            : this(blockRow.BlockRowOwner.BlockInRowOwner.BlockGridBlockRowOwner.ItemsSource, blockRow.CellTrackingTail.Container, false)
        {

        }

        public object BlockGridDataItem { get; }
        public object BlockGridBlockRowDataItem { get; }
        public object BlockInRowDataItem { get; }
        public object BlockRowDataItem { get; }

        public bool RowIndexValuesAreSet { get; }

        public int BlockGridBlockRowIndex { get; } = -1;
        public int BlockInRowIndex { get; } = -1;
        public int BlockRowIndex { get; } = -1;

        public override int GetHashCode()
        {
            return BlockRowDataItem.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return BlockRowDataItem.Equals(obj);
        }
    }
}
