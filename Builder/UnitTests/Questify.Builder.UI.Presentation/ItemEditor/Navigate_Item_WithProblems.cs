
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{

    [TestClass]
    public class Navigate_Item_WithProblems : ItemEditorTestBase
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_InError_NavBack_ShouldWork()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            ItemEditorVm.MoveBackInList.Execute(null);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_InError_NavForward_ShouldWork()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            ItemEditorVm.MoveNextInList.Execute(null);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void When_InErrorByException_NavBack_ShouldWork()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            ItemEditorVm.MoveBackInList.Execute(null);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void When_InErrorByException_NavForward_ShouldWork()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            ItemEditorVm.MoveNextInList.Execute(null);
        }
    }
}