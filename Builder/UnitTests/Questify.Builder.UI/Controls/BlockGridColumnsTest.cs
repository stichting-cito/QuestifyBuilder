using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Controls;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls
{
    [TestClass]
    public class BlockGridColumnsTest : BlockGridTestBase
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddColumnAfterSeparator_Disallowed()
        {
            BlockGrid bg = new BlockGrid();
            bg.Columns.Add(base.CreateTextCellColumn());
            bg.Columns.Add(base.CreateSeparatorColumn());
            bg.Columns.Add(base.CreateTextCellColumn());
            Assert.Fail();
        }

    }
}
