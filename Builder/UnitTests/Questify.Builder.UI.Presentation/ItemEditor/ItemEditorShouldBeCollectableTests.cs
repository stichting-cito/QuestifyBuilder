
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemEditorShouldBeCollectableTests : ItemEditorTestBase
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Memory")]
        public void Test_ItemEditorTestBase_Services()
        {
            var itemEditorVm = new ItemEditorViewModel();
            var weakRef = new WeakReference(itemEditorVm);

            itemEditorVm.Dispose();
            itemEditorVm = null;
            DESTROY_ALL_INSTANCE();

            Assert.IsFalse(weakRef.IsAlive);
        }

        private void DESTROY_ALL_INSTANCE()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
