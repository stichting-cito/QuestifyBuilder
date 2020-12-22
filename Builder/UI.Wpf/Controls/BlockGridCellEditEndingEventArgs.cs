using System;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridCellEditEndingEventArgs : EventArgs
    {
        public BlockGridCellEditEndingEventArgs(BlockGridColumn column, BlockRow row, BlockGridEditAction editAction)
        {
            _blockGridColumn = column;
            _blockRow = row;
            _editAction = editAction;
        }

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        public BlockGridColumn Column
        {
            get { return _blockGridColumn; }
        }

        public BlockRow Row
        {
            get { return _blockRow; }
        }

        private bool _cancel;
        private BlockGridColumn _blockGridColumn;
        private BlockRow _blockRow;
        private BlockGridEditAction _editAction;
    }
}
