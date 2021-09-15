
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    /// <summary>
    /// Self Test 4 ItemEditorTestBase
    /// </summary>
    [TestClass]
    public class ItemEditorShouldBeCollectableTests : ItemEditorTestBase
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Memory")]
        public void Test_ItemEditorTestBase_Services()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            var weakRef = new WeakReference(itemEditorVm);

            //Act
            itemEditorVm.Dispose();
            itemEditorVm = null;
            DESTROY_ALL_INSTANCE();

            //Assert
            Assert.IsFalse(weakRef.IsAlive);
        }

        private void DESTROY_ALL_INSTANCE()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
