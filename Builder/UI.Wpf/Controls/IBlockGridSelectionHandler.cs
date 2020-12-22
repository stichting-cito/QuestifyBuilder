using System.Windows;

namespace Questify.Builder.UI.Wpf.Controls
{
    interface IBlockGridSelectionHandler
    {
        void DeselectAllCells();

        void SelectCell(BlockGridCell cell, RoutedEventArgs e);

        BlockGridCell CurrentSelectedCell { get; set; }
    }
}
