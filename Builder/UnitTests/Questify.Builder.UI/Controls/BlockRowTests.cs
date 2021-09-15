

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls
{
    [TestClass]
    public class BlockRowTests : BlockGridTestBase 
    {

        [TestMethod]        
        public void TestCellGenerationToBeEqualToColumns()
        {
            //Arrange
            var row = Create_BlockRow_WithXCells(3);
            //Act                
            row.Prepare("object x", CreateBlockInRow());
            //Assert
            Assert.AreEqual(3, row.Items.Count);
        }

        [TestMethod]
        public void WhenRowIsPreparedRowIsNotEmpty()
        {
            //Arrange
            var row = Create_BlockRow_WithXCells(3);
            //Act                
            row.Prepare("object x", CreateBlockInRow());
            //Assert
            Assert.AreEqual(false, row.IsEmptyRow);
        }

        [TestMethod]
        public void WhenRowIsPreparedWithoutObject_RowIsEmpty()
        {
            //Arrange
            var row = Create_BlockRow_WithXCells(3);
            //Act                

            row.Prepare(null, CreateBlockInRow());

            //Assert
            Assert.AreEqual(true, row.IsEmptyRow);
        }
    }
}
