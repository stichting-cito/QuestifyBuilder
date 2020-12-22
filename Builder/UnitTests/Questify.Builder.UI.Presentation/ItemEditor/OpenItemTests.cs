
using System;
using Cinch;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class OpenItemTests : ItemEditorTestBase
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.DefaultObjects)]
        public void OnLoad_UpdatedEvent_IsRaised()
        {
            var b = false;
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.Updated += (s, e) => { b = true; };

            ItemEditorVm.ItemId.DataValue = Guid.NewGuid(); DispatcherUtil.DoEvents();

            Assert.IsTrue(b, "Update event not raised.");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void OnChangeAndExit_DialogChangedRaised()
        {
            var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();

            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            ItemEditorVm.AssessmentItem.DataValue.Title = "Something else"; var args = new EventToCommandArgs(this, null, null, new System.ComponentModel.CancelEventArgs());

            ItemEditorVm.WindowClosing.Execute(args);
            A.CallTo(() => fake_MsgBoxServ.ShowYesNoCancel(A<string>.Ignored, A<string>.Ignored, A<CustomDialogIcons>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum, BankProperties = ItemEditorBankObjectStrategy.StaticExample1)]
        public void ItemEditor_LoadItemWithConceptInBank_HasBankConcepts()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid()); var result = ItemEditorVm.IsConceptDefinedOnBankBranch;
            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum, BankProperties = ItemEditorBankObjectStrategy.StaticExample1)]
        public void ItemEditor_LoadItemWithConceptInBank_WasLazyLoaded()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid()); var initialized_1 = ItemEditorVm.CustomBankPropertiesRetrieved;
            var result = ItemEditorVm.IsConceptDefinedOnBankBranch;
            var initialized_2 = ItemEditorVm.CustomBankPropertiesRetrieved;
            Assert.IsFalse(initialized_1);
            Assert.IsTrue(result);
            Assert.IsTrue(initialized_2);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void CreateNewItem_ItemEditorContextIsUpdated()
        {
            var ctx = new CurrentItemEditorContext();

            var itemEditorVM = new ItemEditorViewModel();

            itemEditorVM.DoActualLoadOnItemId(Guid.NewGuid());
            Assert.AreEqual(1, ctx.BankIdentifier);

            bool noTitle = ctx.Title.StartsWith("Title (Identifier) - Item");
            Assert.IsTrue(noTitle, "For new item no title was expected");
            Assert.AreEqual(true, ctx.IsActive, "Item Editor is active");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void ItemEditor_MoveBack()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());

            ItemEditorVm.MoveBackInList.Execute(null);
            Assert.IsFalse(ItemEditorVm.CanMoveBackInList.DataValue);
            Assert.IsFalse(ItemEditorVm.CanMoveNextInList.DataValue);
            Assert.IsFalse(ItemEditorVm.HasError.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void ItemEditor_MoveNext()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());

            ItemEditorVm.MoveNextInList.Execute(null);
            Assert.IsFalse(ItemEditorVm.CanMoveNextInList.DataValue);
            Assert.IsFalse(ItemEditorVm.CanMoveBackInList.DataValue);
            Assert.IsFalse(ItemEditorVm.HasError.DataValue);
        }
    }
}
