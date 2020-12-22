using System.Windows;

namespace Questify.Builder.UI.Wpf.Controls
{

    public class BlockGridBeginningEditEventArgs
    {
        private bool _cancel;
        private BlockGridColumn _dataGridColumn;
        private BlockRow _dataGridRow;
        private RoutedEventArgs _editingEventArgs;

        public BlockGridBeginningEditEventArgs(BlockGridColumn column, BlockRow row, RoutedEventArgs editingEventArgs)
        {
            _dataGridColumn = column;
            _dataGridRow = row;
            _editingEventArgs = editingEventArgs;
        }

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        public BlockGridColumn Column
        {
            get { return _dataGridColumn; }
        }

        public BlockRow Row
        {
            get { return _dataGridRow; }
        }

        public RoutedEventArgs EditingEventArgs
        {
            get { return _editingEventArgs; }
        }

    }
}
