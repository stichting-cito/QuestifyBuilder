using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Controls;
using Questify.Builder.UnitTests.Questify.Builder.UI.Controls.HelperClasses;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls
{
    [TestClass]
    public abstract class BlockGridTestBase
    {
        private BlockGrid _currentBlockGrid;

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void DeInitialize()
        {
            _currentBlockGrid = null;
        }

        public BlockRow Create_BlockRow_WithXCells(int columnsToAdd)
        {
            var blockRow = new BlockRow();



            var columnsAlreadyPresent = CurrentBlockGrid.Columns.Count;
            var remainingColumnsToMake = (columnsToAdd - columnsAlreadyPresent);

            if (remainingColumnsToMake != 0 && remainingColumnsToMake != columnsToAdd)
                Assert.Fail("Columns are shared over all blockRows, this is a rule, and thus it's blocked to create such a situation in a test.");

            for (int i = 0; i < remainingColumnsToMake; i++)
                CurrentBlockGrid.Columns.Add(new BlockGridTestColumn());

            return blockRow;
        }

        internal IEnumerable<BlockGridCell> Create_BlockGridCells(BlockRow row)
        {
            var root = row.CellTrackingTail;
            int columnIndex = 0;
            foreach (var item in row.ItemsSource)
            {
                var newCell = new BlockGridCell();
                newCell.PrepareCell(item, row, CurrentBlockGrid.Columns[columnIndex] as BlockGridCellColumn);
                newCell.Tracker.StartTracking(ref root);
                yield return newCell;
                columnIndex++;
            }
            row.SetCellTrakingTail(root);
        }

        public BlockInRow CreateBlockInRow()
        {
            var blockInRow = new BlockInRow();
            blockInRow.BlockInRowOwner = new BlockGridBlockRow();
            blockInRow.BlockInRowOwner.BlockGridBlockRowOwner = CurrentBlockGrid;
            return blockInRow;
        }

        public BlockGrid CurrentBlockGrid
        {
            get
            {
                if (_currentBlockGrid == null)
                    _currentBlockGrid = new BlockGrid(); return _currentBlockGrid;
            }
            set { _currentBlockGrid = value; }
        }



        internal BlockGridColumn CreateTextCellColumn()
        {
            return new BlockGridCellTextColumn();
        }

        internal BlockGridSeparatorColumn CreateSeparatorColumn()
        {
            return new BlockGridSeparatorColumn();
        }

    }
}
