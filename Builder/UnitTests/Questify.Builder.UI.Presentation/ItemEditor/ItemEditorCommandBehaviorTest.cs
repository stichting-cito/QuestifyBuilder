
using System;
using System.ComponentModel;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemEditorCommandBehaviorTest : ItemEditorTestBase
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        [FakeServiceHandler()]
        public void Execute_SaveAndClose_RequestToCloseWindowIsCalled()
        {
            bool CloseRequest = false;
            var ItemEditorVm = new ItemEditorViewModel();
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid()); ItemEditorVm.CloseRequest += (s, e) => CloseRequest = true; FakeDal.Add.ItemTemplate("FakeLayoutTemplateSourceName", x => x.ResourceData = new ResourceDataEntity() { BinData = new System.Text.UTF8Encoding().GetBytes("<ItemLayoutTemplate></ItemLayoutTemplate>") });

            ItemEditorVm.SaveAndClose.Execute(new object());

            Assert.IsTrue(CloseRequest, "Close request was not fired, this is set in place to close");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        [FakeServiceHandler()]
        public void Execute_Save_NecessaryEvilIsCalled()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            var fakeIViewModel2ViewCommandSupport = A.Fake<IViewModel2ViewCommandSupport>();

            ItemEditorVm.PresentationWorkspace.DataValue.ViewModelInstance = fakeIViewModel2ViewCommandSupport;
            ItemEditorVm.MetadataWorkspace.DataValue.ViewModelInstance = fakeIViewModel2ViewCommandSupport;
            ItemEditorVm.ScoreWorkspace.DataValue.ViewModelInstance = fakeIViewModel2ViewCommandSupport;

            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            FakeDal.Add.ItemTemplate("FakeLayoutTemplateSourceName", x => x.ResourceData = new ResourceDataEntity() { BinData = new System.Text.UTF8Encoding().GetBytes("<ItemLayoutTemplate></ItemLayoutTemplate>") });

            ItemEditorVm.Save.Execute(new object());

            A.CallTo(() => fakeIViewModel2ViewCommandSupport.DoPreSaveTasks()).MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void Execute_SaveAs_NecessaryEvilIsCalled()
        {
            var ItemEditorVm = new ItemEditorViewModel();
            var fakeIViewModel2ViewCommandSupport = A.Fake<IViewModel2ViewCommandSupport>();
            FakeInputBox.MakeNewFake();

            SetFakeWorkSpaceVm(ItemEditorVm, fakeIViewModel2ViewCommandSupport);

            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            ItemEditorVm.SaveAs.Execute(new object());
            A.CallTo(() => fakeIViewModel2ViewCommandSupport.DoPreSaveTasks())
    .MustHaveHappened(Repeated.Exactly.Times(3));

        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum),
        FakeResourceExistsInBankHierarchyHandler(ResourcesContainedInBank = new[] { "FakeLayoutTemplateSourceName" })]
        public void ItemEditor_Execute_SaveAs()
        {
            var msgBox = FakeMessageBoxService.MakeNewFake();
            var ItemEditorVm = new ItemEditorViewModel();
            var fakeIViewModel2ViewCommandSupport = A.Fake<IViewModel2ViewCommandSupport>();
            var fakeInputBox = FakeInputBox.MakeNewFake();
            InputBox_ReturnsOk_NewName(fakeInputBox); var oldItem = new ItemResourceEntity() { Name = "oldItem" };
            oldItem.SetAssessmentItem(new AssessmentItem() { Identifier = "oldItem" });
            ResourceFactory.Instance.UpdateItemResource(oldItem);
            SetFakeWorkSpaceVm(ItemEditorVm, fakeIViewModel2ViewCommandSupport);
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            FakeDal.Add.ItemTemplate("FakeLayoutTemplateSourceName", x => x.ResourceData = new ResourceDataEntity() { BinData = new System.Text.UTF8Encoding().GetBytes("<ItemLayoutTemplate></ItemLayoutTemplate>") });

            ItemEditorVm.SaveAs.Execute(new object());

            A.CallTo(() => fakeInputBox.Show(A<string>.Ignored, A<bool>.Ignored, A<string>.Ignored, A<string>.Ignored, A<Func<string, string>>.Ignored))
    .MustHaveHappened(Repeated.Exactly.Times(1));
            A.CallTo(() => Fake_Factory.UpdateItemResource(A<ItemResourceEntity>.Ignored)).
                MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum),
        FakeServiceHandler(ResourceIsContainedInBank = true)]
        public void ItemEditor_Execute_SaveAs_ResourceExitstsInBank()
        {
            var msgBox = FakeCustomMessageBoxService.MakeNewFake();
            var ItemEditorVm = new ItemEditorViewModel();
            var fakeIViewModel2ViewCommandSupport = A.Fake<IViewModel2ViewCommandSupport>();
            var fakeInputBox = FakeInputBox.MakeNewFake(); InputBox_ReturnsOk_NewName(fakeInputBox);
            SetFakeWorkSpaceVm(ItemEditorVm, fakeIViewModel2ViewCommandSupport);
            ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            ItemEditorVm.SaveAs.Execute(new object());

            A.CallTo(() => fakeInputBox.Show(A<string>.Ignored, A<bool>.Ignored, A<string>.Ignored, A<string>.Ignored, A<Func<string, string>>.Ignored))
    .MustHaveHappened(Repeated.Exactly.Times(1));

            A.CallTo(() => Fake_Factory.UpdateItemResource(A<ItemResourceEntity>.Ignored)).
                MustNotHaveHappened();

            A.CallTo(msgBox).Where(x => x.Method.Name == "ShowError").MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void SaveAndClose_ItemEditorContextIsUpdated()
        {
            var ctx = new CurrentItemEditorContext();

            var itemEditorVM = new ItemEditorViewModel();
            itemEditorVM.CloseRequest += (s, e) =>
            {
                var args = new Cinch.EventToCommandArgs(null, null, null, new CancelEventArgs());
                itemEditorVM.WindowClosing.Execute(args);
                itemEditorVM.WindowClosed.Execute(null);
            };


            itemEditorVM.DoActualLoadOnItemId(Guid.Empty);
            itemEditorVM.ItemResourceEntity.DataValue.IsDirty = false;
            itemEditorVM.ItemResourceEntity.DataValue.Bank.IsDirty = false;
            itemEditorVM.ItemResourceEntity.DataValue.IsNew = false;
            itemEditorVM.ItemResourceEntity.DataValue.Bank.IsNew = false;
            var oldIsActive = ctx.IsActive;
            FakeDal.Add.ItemTemplate("FakeLayoutTemplateSourceName", x => x.ResourceData = new ResourceDataEntity() { BinData = new System.Text.UTF8Encoding().GetBytes("<ItemLayoutTemplate></ItemLayoutTemplate>") });

            itemEditorVM.SaveAndClose.Execute(new object());

            Assert.AreEqual(0, ctx.BankIdentifier);
            Assert.AreEqual(string.Empty, ctx.Title, "ItemEditor is closed so no title was expected");
            Assert.AreEqual(false, ctx.IsActive, "Item Editor is deActivated");
            Assert.AreEqual(true, oldIsActive, "Item Editor WAS active");
        }





        private void InputBox_ReturnsOk_NewName(ILegacyInputBox fakeInputBox)
        {
            A.CallTo(() => fakeInputBox.Show(A<string>.Ignored, A<bool>.Ignored, A<string>.Ignored, A<string>.Ignored, A<Func<string, string>>.Ignored))
                .ReturnsLazily(() => new InputBoxResult() { ReturnCode = System.Windows.Forms.DialogResult.OK, Text = "newName" });
        }

        private void InputBox_ReturnsCancel(ILegacyInputBox fakeInputBox)
        {
            A.CallTo(() => fakeInputBox.Show(A<string>.Ignored, A<bool>.Ignored, A<string>.Ignored, A<string>.Ignored, A<Func<string, string>>.Ignored))
                .ReturnsLazily(() => new InputBoxResult() { ReturnCode = System.Windows.Forms.DialogResult.Cancel, Text = "" });
        }

        private void SetFakeWorkSpaceVm(ItemEditorViewModel ItemEditorVm, IViewModel2ViewCommandSupport fakeIViewModel2ViewCommandSupport)
        {
            ItemEditorVm.PresentationWorkspace.DataValue.ViewModelInstance = fakeIViewModel2ViewCommandSupport;
            ItemEditorVm.MetadataWorkspace.DataValue.ViewModelInstance = fakeIViewModel2ViewCommandSupport;
            ItemEditorVm.ScoreWorkspace.DataValue.ViewModelInstance = fakeIViewModel2ViewCommandSupport;
        }
    }
}
