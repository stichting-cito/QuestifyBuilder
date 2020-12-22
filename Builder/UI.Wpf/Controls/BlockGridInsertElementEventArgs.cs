using System.Windows;

namespace Questify.Builder.UI.Wpf.Controls
{

    public class BlockGridInsertElementEventArgs : BlockGridCanInsertElementEventArgs
    {
        private object _insertedDataItem;

        public BlockGridInsertElementEventArgs(RoutedEvent routedEvent, BlockRowDataCoordinates blockRowDataCoords, BlockGridElement typeOfElementToInsert, RoutedEventArgs insertionEventArgs) : base(routedEvent, blockRowDataCoords, typeOfElementToInsert, insertionEventArgs)
        {
        }

        public object InsertedDataItem
        {
            get { return _insertedDataItem; }
            set { _insertedDataItem = value; }
        }
    }
}
