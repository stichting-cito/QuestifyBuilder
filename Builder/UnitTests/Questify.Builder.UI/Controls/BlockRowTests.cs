

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls
{
    [TestClass]
    public class BlockRowTests : BlockGridTestBase
    {

        [TestMethod]
        public void TestCellGenerationToBeEqualToColumns()
        {
            var row = Create_BlockRow_WithXCells(3);
            row.Prepare("object x", CreateBlockInRow());
            Assert.AreEqual(3, row.Items.Count);
        }

        [TestMethod]
        public void WhenRowIsPreparedRowIsNotEmpty()
        {
            var row = Create_BlockRow_WithXCells(3);
            row.Prepare("object x", CreateBlockInRow());
            Assert.AreEqual(false, row.IsEmptyRow);
        }

        [TestMethod]
        public void WhenRowIsPreparedWithoutObject_RowIsEmpty()
        {
            var row = Create_BlockRow_WithXCells(3);

            row.Prepare(null, CreateBlockInRow());

            Assert.AreEqual(true, row.IsEmptyRow);
        }
    }
}
