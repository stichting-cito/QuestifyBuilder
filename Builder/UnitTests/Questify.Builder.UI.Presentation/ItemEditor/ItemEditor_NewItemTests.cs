﻿
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemEditor_NewItemTests : ItemEditorTestBase
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void CreateNewItem_UpdatedEventIsFired()
        {
            var Wait = new AutoResetEvent(false);

            var itemEditorVM = new ItemEditorViewModel();
            itemEditorVM.Updated += (s, e) => { Wait.Set(); };
            itemEditorVM.CreateNewItem(Guid.Empty, 0, false, false);
            DispatcherUtil.DoEvents();
            bool isFired = false;
            isFired = AutoResetEvent.WaitAny(new[] { Wait }, 500) == 0;

            Assert.IsTrue(isFired);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void CreateNewItem_ResourceManagerIsSet()
        {
            var itemEditorVM = new ItemEditorViewModel();

            itemEditorVM.CreateNewItem(Guid.Empty, 0, false, false);
            DispatcherUtil.DoEvents();

            Assert.IsNotNull(itemEditorVM.ResourceManager.DataValue);

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void CreateNewItem_MetaDataTabIsSelected()
        {
            var Wait = new AutoResetEvent(false);

            var itemEditorVM = new ItemEditorViewModel();

            itemEditorVM.CreateNewItem(Guid.Empty, 0, false, false);
            DispatcherUtil.DoEvents();


            Assert.AreEqual(1, itemEditorVM.SelectedTab.DataValue, "When opening a new item the second tab (meta data) should display");
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void CreateNewItem_ItemEditorContextIsUpdated()
        {
            var ctx = new CurrentItemEditorContext();
            Cinch.Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.Title, "NOT TO BE SHOWN");
            var oldIsActive = ctx.IsActive;
            var itemEditorVM = new ItemEditorViewModel();

            itemEditorVM.CreateNewItem(itemLayoutTemplateId: Guid.Empty, bankId: 789, canMoveBackward: false, canMoveForward: false);
            DispatcherUtil.DoEvents();

            Assert.AreEqual(789, ctx.BankIdentifier);
            bool defaultTitle = ctx.Title == "Item Editor" || ctx.Title == "Itemeditor";
            Assert.IsTrue(defaultTitle, "For new item only default title was expected : " + ctx.Title);
            Assert.AreEqual(true, ctx.IsActive, "Item Editor is active");
            Assert.AreEqual(false, oldIsActive, "Item Editor WAS not active");
        }
    }
}
