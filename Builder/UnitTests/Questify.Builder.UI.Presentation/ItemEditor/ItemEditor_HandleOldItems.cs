
using System;
using Cinch;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemEditor_HandleOldItems : ItemEditorTestBase 
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum, IsOldItem = true)]
        public void LoadOldItem_IsOldItem_ShouldBeTrue()
        {
            var ItemEditorVm_InError = new ItemEditorViewModel(); ItemEditorVm_InError.DoActualLoadOnItemId(Guid.NewGuid());
            var view = A.Fake<IPresentationControl>(); view.WorkSpaceContextualData.DataValue = ItemEditorVm_InError;
            var fake_MsgBoxServ = FakeMessageBoxService.MakeNewFake();
            var viewAwareStatus = new TestViewAwareStatus();
			var resouceEditor = A.Fake<IResourceEditorService>();
			var presentationVm = new PresentationViewModel(viewAwareStatus, resouceEditor);
            //Act
            viewAwareStatus.View = view;
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert            
            Assert.IsTrue(presentationVm.IsOldItem.DataValue );
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum, IsOldItem = false)]
        public void LoadContemporaryItem_IsOldItem_ShouldBeFalse()
        {
            var ItemEditorVm_InError = new ItemEditorViewModel(); ItemEditorVm_InError.DoActualLoadOnItemId(Guid.NewGuid());
            var view = A.Fake<IPresentationControl>(); view.WorkSpaceContextualData.DataValue = ItemEditorVm_InError;
            var fake_MsgBoxServ = FakeMessageBoxService.MakeNewFake();
            var viewAwareStatus = new TestViewAwareStatus();
			var resouceEditor = A.Fake<IResourceEditorService>();
			var presentationVm = new PresentationViewModel(viewAwareStatus, resouceEditor);
            //Act
            viewAwareStatus.View = view;
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert            
            Assert.IsFalse(presentationVm.IsOldItem.DataValue);
        }
    }
}
