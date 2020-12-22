using System.Windows;

namespace Questify.Builder.UI.Wpf.Controls
{

    public class BlockGridCanDeleteElementEventArgs : RoutedEventArgs
    {
        private bool _cancel;
        private BlockRowDataCoordinates _blockRowDataCoords;
        private BlockGridElement _typeOfElementToDelete;


        private RoutedEventArgs _deletionEventArgs;

        public BlockGridCanDeleteElementEventArgs(RoutedEvent routedEvent, BlockRowDataCoordinates blockRowDataCoords, BlockGridElement typeOfElementToDelete, RoutedEventArgs deletionEventArgs) : base(routedEvent)
        {
            _blockRowDataCoords = blockRowDataCoords;
            _typeOfElementToDelete = typeOfElementToDelete;
            _deletionEventArgs = deletionEventArgs;
        }

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        public BlockRowDataCoordinates BlockRowDataContext
        {
            get { return _blockRowDataCoords; }
        }

        public BlockGridElement TypeOfElementToDelete
        {
            get { return _typeOfElementToDelete; }
        }

        public RoutedEventArgs DeletionEventArgs
        {
            get { return _deletionEventArgs; }
        }

    }
}
