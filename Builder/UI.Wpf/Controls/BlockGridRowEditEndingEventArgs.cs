using System;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridRowEditEndingEventArgs : EventArgs
    {
        public BlockGridRowEditEndingEventArgs(BlockRow row, BlockGridEditAction editAction)
        {
            _blockRow = row;
            _editAction = editAction;
        }

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        public BlockRow Row
        {
            get { return _blockRow; }
        }

        public BlockGridEditAction EditAction
        {
            get { return _editAction; }
        }

        private bool _cancel;
        private BlockRow _blockRow;
        private BlockGridEditAction _editAction;
    }
}
