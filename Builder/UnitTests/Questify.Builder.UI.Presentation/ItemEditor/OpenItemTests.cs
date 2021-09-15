
using System;
using Cinch;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    /// <summary>
    /// Verifies behavior of opening items.
    /// </summary>
    [TestClass]
    public class OpenItemTests : ItemEditorTestBase
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.DefaultObjects)]
        public void OnLoad_UpdatedEvent_IsRaised()
        {
            //Arrange
            var b = false; 
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.Updated += (s, e) => { b = true; };
            
            //Act
            ItemEditorVm.ItemId.DataValue = Guid.NewGuid(); //This triggers a Async Load
            DispatcherUtil.DoEvents(); 

            //Assert            
            Assert.IsTrue(b, "Update event not raised.");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void OnChangeAndExit_DialogChangedRaised()
        {
            //Arrange
            var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();

            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            ItemEditorVm.AssessmentItem.DataValue.Title = "Something else"; //This will invalidate object, so we need to save.
            var args = new EventToCommandArgs(this,null,null,new System.ComponentModel.CancelEventArgs());

            //Act
            ItemEditorVm.WindowClosing.Execute(args);
            //Assert
            A.CallTo(() => fake_MsgBoxServ.ShowYesNoCancel(A<string>.Ignored, A<string>.Ignored, A<CustomDialogIcons>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum, BankProperties = ItemEditorBankObjectStrategy.StaticExample1)]
        public void ItemEditor_LoadItemWithConceptInBank_HasBankConcepts()
        {
            //Arrange
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid()); //Gets default objects.
            //Act
            var result = ItemEditorVm.IsConceptDefinedOnBankBranch;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum, BankProperties = ItemEditorBankObjectStrategy.StaticExample1)]
        public void ItemEditor_LoadItemWithConceptInBank_WasLazyLoaded()
        {
            //Arrange
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid()); //Gets default objects.
            //Act
            var initialized_1 = ItemEditorVm.CustomBankPropertiesRetrieved;
            var result = ItemEditorVm.IsConceptDefinedOnBankBranch;
            var initialized_2 = ItemEditorVm.CustomBankPropertiesRetrieved;
            //Assert
            Assert.IsFalse(initialized_1);
            Assert.IsTrue(result);
            Assert.IsTrue(initialized_2);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void CreateNewItem_ItemEditorContextIsUpdated()
        {
            //Arrange
            var ctx = new CurrentItemEditorContext();
            
            var itemEditorVM = new ItemEditorViewModel();
            
            //Act
            itemEditorVM.DoActualLoadOnItemId(Guid.NewGuid()); //Gets default objects.

            //Assert
            Assert.AreEqual(1, ctx.BankIdentifier);
            //Hello developer,.. if this unit test breaks because the title of the item editor has changed, please
            //update the value below. 

            bool noTitle = ctx.Title.StartsWith("Title (Identifier) - Item");
            Assert.IsTrue(noTitle, "For new item no title was expected"); 
            Assert.AreEqual(true, ctx.IsActive, "Item Editor is active");
        }

		[TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
		public void ItemEditor_MoveBack()
		{
			//Arrange
			var ItemEditorVm = new ItemEditorViewModel();
			ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());

			//Act
			ItemEditorVm.MoveBackInList.Execute(null);
			//Assert
			Assert.IsFalse(ItemEditorVm.CanMoveBackInList.DataValue);
			Assert.IsFalse(ItemEditorVm.CanMoveNextInList.DataValue);
			Assert.IsFalse(ItemEditorVm.HasError.DataValue);
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
		public void ItemEditor_MoveNext()
		{
			//Arrange
			var ItemEditorVm = new ItemEditorViewModel();
			ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());

			//Act
			ItemEditorVm.MoveNextInList.Execute(null);
			//Assert
			Assert.IsFalse(ItemEditorVm.CanMoveNextInList.DataValue);
			Assert.IsFalse(ItemEditorVm.CanMoveBackInList.DataValue);
			Assert.IsFalse(ItemEditorVm.HasError.DataValue);
		}
	}
}
