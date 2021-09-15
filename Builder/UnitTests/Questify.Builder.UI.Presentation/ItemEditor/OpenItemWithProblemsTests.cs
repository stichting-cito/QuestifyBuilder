
using System;
using System.Threading;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Framework.Faketory;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class OpenItemWithProblemsTests : ItemEditorTestBase
    {
     
        #region FakeItemEditorObjectFactory returns null

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_objectFactoryReturnsNull_NoExceptionThrown()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            FakeItemEditorObjectFactory.MakeNewFake();                        
            //Act
            itemEditorVm.DoActualLoadOnItemId( Guid.NewGuid());             
            //Assert
            //..No Assert Exception 
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_objectFactoryReturnsNull_HasErrorStatus()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            FakeItemEditorObjectFactory.MakeNewFake();
            //Act
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            //Assert
            Assert.IsTrue(itemEditorVm.HasError.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_objectFactoryReturnsNull_SaveNoCrash()
        {
            //Arrange
            var fakeMsgbox = FakeCustomMessageBoxService.MakeNewFake();
            var itemEditorVm = new ItemEditorViewModel();
            FakeItemEditorObjectFactory.MakeNewFake();
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());

            //Act
            itemEditorVm.Save.Execute(null);
            //Assert
            Assert.IsTrue(itemEditorVm.HasError.DataValue);
            
            A.CallTo(() => fakeMsgbox.ShowError(A<string>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_objectFactoryReturnsNull_ID_StillSet()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            FakeItemEditorObjectFactory.MakeNewFake();
            var guidId = new Guid("B4EFA0B9-4A8B-4F3A-A27E-7DCC6734387E");
            //Act
            itemEditorVm.ItemId.DataValue = guidId;
            Thread.Sleep(500); //Stuff will be executed on background,.. wait for it.
            //Assert
           Assert.AreEqual(guidId, itemEditorVm.ItemId.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_objectFactoryReturnsNull_MessageBoxIsShown()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();            
            //Act
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            //Assert            
            A.CallTo( () => fake_MsgBoxServ.ShowError(A<string>.Ignored,A<string>.Ignored)).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_objectFactoryReturnsNull_StateReturnedToNormal()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            GiveDataObjectsValue(ref itemEditorVm);            
            //Act
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            
            //Assert
            Assert.IsFalse(itemEditorVm.IsLoading,"Object is still under the impression that it is loading");
            Assert.IsFalse(itemEditorVm.IsWorking.DataValue,"Is Working is used for view, but is still true");

            Assert.IsNull(itemEditorVm.ItemResourceEntity.DataValue);
            Assert.IsNull(itemEditorVm.AssessmentItem.DataValue);
            Assert.IsNull(itemEditorVm.ResourceManager.DataValue);
            Assert.IsNull(itemEditorVm.ParameterSetCollection.DataValue);

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void LoadItem_GetsNull_ItemEditorContextIsUpdated()
        {
            //Arrange
            var ctx = new CurrentItemEditorContext();

            var itemEditorVM = new ItemEditorViewModel();
            //Act
            try
            {
                itemEditorVM.CreateNewItem(itemLayoutTemplateId: Guid.Empty, bankId: 789, canMoveBackward: false, canMoveForward: false);
                DispatcherUtil.DoEvents();
            }
            catch
            {
                //Do nothing
            }

            //Assert
            Assert.AreEqual(0, ctx.BankIdentifier);
            Assert.IsTrue(string.IsNullOrEmpty(ctx.Title), "Error state = no title ");
            Assert.AreEqual(true, ctx.IsActive, "Item Editor is still active");
        }

        #endregion

        #region FakeItemEditorObjectFactory gives excepiotn

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void When_objectFactoryThrowsException_DoesNotCrash()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            //Act
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            //Assert
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void When_objectFactoryThrowsException_IsInErrorState()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            //Act
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            //Assert
            Assert.IsTrue(itemEditorVm.HasError.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void When_objectFactoryThrowsException_ID_StillSet()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            FakeItemEditorObjectFactory.MakeNewFake();
            var guidId = new Guid("E4EFA0B9-4A8B-4F3A-A27E-7DCC6734387E");
            //Act
            itemEditorVm.ItemId.DataValue = guidId;
            Thread.Sleep(500); //Stuff will be executed on background,.. wait for it.
            //Assert
            Assert.AreEqual(guidId, itemEditorVm.ItemId.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void When_objectFactoryThrowsException_MessageBoxIsShown()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();            
            //Act
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            //Assert
            A.CallTo(() => fake_MsgBoxServ.ShowError(A<string>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void When_objectFactoryThrowsException_StateReturnedToNormal()
        {
            //Arrange
            var itemEditorVm = new ItemEditorViewModel();
            GiveDataObjectsValue(ref itemEditorVm);
            //Act
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            //Assert
            Assert.IsFalse(itemEditorVm.IsLoading, "Object is still under the impression that it is loading");
            Assert.IsFalse(itemEditorVm.IsWorking.DataValue, "Is Working is used for view, but is still true");

            Assert.IsNull(itemEditorVm.ItemResourceEntity.DataValue);
            Assert.IsNull(itemEditorVm.AssessmentItem.DataValue);
            Assert.IsNull(itemEditorVm.ResourceManager.DataValue);
            Assert.IsNull(itemEditorVm.ParameterSetCollection.DataValue);
        }

        private void GiveDataObjectsValue(ref ItemEditorViewModel itemEditorVm)
        {
            itemEditorVm.ItemResourceEntity.DataValue = A.Fake<ItemResourceEntity>();
            itemEditorVm.AssessmentItem.DataValue = A.Fake<AssessmentItem>();
            itemEditorVm.ResourceManager.DataValue = FakeResourceManager.MakeResourceManagerBase();
            itemEditorVm.ParameterSetCollection.DataValue = A.Fake<ParameterSetCollection>();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void LoadItem_GetsException_ItemEditorContextIsUpdated()
        {
            //Arrange
            var ctx = new CurrentItemEditorContext();

            var itemEditorVM = new ItemEditorViewModel();

            //Act
            try
            {
                itemEditorVM.CreateNewItem(itemLayoutTemplateId: Guid.Empty, bankId: 789, canMoveBackward: false, canMoveForward: false);
                DispatcherUtil.DoEvents();
            }
            catch
            {
                //Do Nothing
            }

            //Assert
            Assert.AreEqual(0, ctx.BankIdentifier);
            Assert.IsTrue(string.IsNullOrEmpty(ctx.Title), "Error state = no title ");
            Assert.AreEqual(true, ctx.IsActive, "Item Editor is still active");

        }

        #endregion
    }
}
