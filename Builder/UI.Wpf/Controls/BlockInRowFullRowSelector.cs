using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Controls
{
    internal class BlockInRowFullRowSelector : IBlockGridSelectionHandler
    {

        readonly BlockGrid _owner;
        readonly HashSet<BlockRow> _rowsWithSelectedCells;


        public BlockInRowFullRowSelector(BlockGrid owner)
        {
            this._owner = owner;
            _rowsWithSelectedCells = new HashSet<BlockRow>();
        }



        public BlockGridCell CurrentSelectedCell { get; set; }

        public void DeselectAllCells()
        {
            CurrentSelectedCell = null;
            var rows = _rowsWithSelectedCells.ToArray();

            foreach (var row in rows)
            {
                ToggleSelectionForRow(row, false);
            }
            _rowsWithSelectedCells.Clear();
        }

        public void SelectCell(BlockGridCell clickedCell, RoutedEventArgs e)
        {
            bool previousCellWasEditing = false;
            bool previousAndCurrentCellActOnSameDataItem = false;
            bool updateMultiBlockRowSelection = (clickedCell != null && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control));

            if (CurrentSelectedCell != null && CurrentSelectedCell != clickedCell)
            {
                previousCellWasEditing = CurrentSelectedCell.IsEditing;
                previousAndCurrentCellActOnSameDataItem = ((clickedCell != null) && CurrentSelectedCell.RowDataItem == clickedCell.RowDataItem);
                _owner.CommitEdit();
            }

            if (clickedCell == null)
            {
                DeselectAllCells();
                CurrentSelectedCell = null;
            }
            else
            {
                var rowOfSelectedCell = clickedCell.BlockGridCellOwner;
                bool cellEditStarted = false;

                if (updateMultiBlockRowSelection)
                {
                    ToggleSelectionForRow(rowOfSelectedCell, !_rowsWithSelectedCells.Contains(rowOfSelectedCell));
                }
                else
                {
                    foreach (var br in _rowsWithSelectedCells.ToArray())
                    {
                        if (!br.Equals(rowOfSelectedCell))
                        {
                            ToggleSelectionForRow(br, false);
                        }
                    }

                    if (CurrentSelectedCell == clickedCell && clickedCell.IsKeyboardFocusWithin && clickedCell.IsSelected && !clickedCell.IsEditing && !clickedCell.IsReadOnly)
                    {
                        cellEditStarted = _owner.TryBeginEdit(e);
                    }
                    else
                    {
                        if (!_rowsWithSelectedCells.Contains(rowOfSelectedCell))
                        {
                            ToggleSelectionForRow(rowOfSelectedCell, true);
                        }
                    }
                }

                if (!cellEditStarted)
                {
                    HandleCurrentCellSelected(clickedCell, e, updateMultiBlockRowSelection, previousCellWasEditing, previousAndCurrentCellActOnSameDataItem);
                }
            }

            _owner.SyncSelectedItems(_rowsWithSelectedCells);
        }

        private void HandleCurrentCellSelected(BlockGridCell clickedCell, RoutedEventArgs e, bool updateMultiBlockRowSelection, bool previousCellWasEditing, bool previousAndCurrentCellActOnSameDataItem)
        {
            CurrentSelectedCell = clickedCell;
            Keyboard.Focus(clickedCell);

            if (!updateMultiBlockRowSelection && previousCellWasEditing && previousAndCurrentCellActOnSameDataItem)
            {
                _owner.TryBeginEdit(e);
            }
            else
            {
                if (!clickedCell.IsFocused && clickedCell.Focusable)
                {
                    FocusManager.SetFocusedElement(FocusManager.GetFocusScope(clickedCell), clickedCell);
                }
            }
        }


        void ToggleSelectionForRow(BlockRow currentRow, bool isSelected)
        {
            var cellTracker = currentRow.CellTrackingTail;
            while (cellTracker != null)
            {
                BlockGridCell cell = cellTracker.Container;

                cell.SyncIsSelected(isSelected);

                cellTracker = cellTracker.Previous;
            }
            if (isSelected)
            {
                _rowsWithSelectedCells.Add(currentRow);
            }
            else
            {
                _rowsWithSelectedCells.Remove(currentRow);
            }
        }

    }
}
