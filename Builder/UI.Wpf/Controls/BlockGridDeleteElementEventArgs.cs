using System.Windows;

namespace Questify.Builder.UI.Wpf.Controls
{

    public class BlockGridDeleteElementEventArgs : BlockGridCanDeleteElementEventArgs
    {
        private BlockGridElement _typeOfElementActuallyDeleted;

        public BlockGridDeleteElementEventArgs(RoutedEvent routedEvent, BlockRowDataCoordinates blockRowDataCoords, BlockGridElement typeOfElementToDelete, RoutedEventArgs deletionEventArgs) : base(routedEvent, blockRowDataCoords, typeOfElementToDelete, deletionEventArgs)
        {
            _typeOfElementActuallyDeleted = typeOfElementToDelete;
        }

        public BlockGridElement TypeOfElementActuallyDeleted
        {
            get { return _typeOfElementActuallyDeleted; }
            set { _typeOfElementActuallyDeleted = value; }
        }

    }
}
