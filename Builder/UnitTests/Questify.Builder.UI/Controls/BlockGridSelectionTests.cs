using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls
{
    [TestClass]
    public class BlockGridSelectionTests : BlockGridTestBase
    {

        [TestMethod]
        public void SelectACell_WholeRowsIsSelected()
        {
            var row = Create_BlockRow_WithXCells(3);
            row.Prepare("object x", CreateBlockInRow());
            var cells = base.Create_BlockGridCells(row).ToList();
            cells[0].SimulateSelect();
            foreach (var cell in cells)
            {
                Assert.IsTrue(cell.IsSelected);
            }
        }
    }
}
