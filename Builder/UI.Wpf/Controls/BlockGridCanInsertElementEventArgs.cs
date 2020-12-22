using System.Windows;

namespace Questify.Builder.UI.Wpf.Controls
{

    public class BlockGridCanInsertElementEventArgs : RoutedEventArgs
    {
        private bool _cancel;
        private BlockRowDataCoordinates _blockRowDataCoords;
        private BlockGridElement _typeOfElementToInsert;


        private RoutedEventArgs _insertionEventArgs;

        public BlockGridCanInsertElementEventArgs(RoutedEvent routedEvent, BlockRowDataCoordinates BlockRowDataContext, BlockGridElement typeOfElementToInsert, RoutedEventArgs insertionEventArgs) : base(routedEvent)
        {
            _blockRowDataCoords = BlockRowDataContext;
            _typeOfElementToInsert = typeOfElementToInsert;
            _insertionEventArgs = insertionEventArgs;
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

        public BlockGridElement TypeOfElementToInsert
        {
            get { return _typeOfElementToInsert; }
        }

        public RoutedEventArgs InsertionEventArgs
        {
            get { return _insertionEventArgs; }
        }

    }
}
