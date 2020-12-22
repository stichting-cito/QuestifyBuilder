
using System;
using Cinch;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog;
using Questify.Builder.Security;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ResourcePropertyDialog
{
    [TestClass]
    public class OpenResourcePropertyDialogTest : ResourcePropertyDialogTestBase
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void OnChangeAndExit_DialogChangedRaised()
        {
            var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            var args = new EventToCommandArgs(this, null, null, new System.ComponentModel.CancelEventArgs());

            A.CallTo(() => FakeDal.FakeServices.FakePermissionService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored)).Returns(true);

            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ItemResourceEntity));
            resourcePropertyDialogVm.PropertyEntity.DataValue.Description = "Some description";
            resourcePropertyDialogVm.WindowClosing.Execute(args);

            A.CallTo(() => fake_MsgBoxServ.ShowYesNoCancel(A<string>.Ignored, A<string>.Ignored, A<CustomDialogIcons>.Ignored)).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void CorrectTabsVisible_LoadingResourceEntity()
        {
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity));

            resourcePropertyDialogVm.SetTabsVisibility();

            Assert.IsTrue(resourcePropertyDialogVm.GeneralTabVisible.DataValue);
            Assert.IsTrue(resourcePropertyDialogVm.DependenciesTabVisible.DataValue);
            Assert.IsTrue(resourcePropertyDialogVm.ReferencesTabVisible.DataValue);
            Assert.IsTrue(resourcePropertyDialogVm.DataTabVisible.DataValue);
            Assert.IsTrue(resourcePropertyDialogVm.HistoryTabVisible.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void LeftMostTabMustBeActiveOnOpenening()
        {
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();

            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity));


            Assert.IsTrue(resourcePropertyDialogVm.SelectedTab.DataValue == 0);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void NameFieldMustBeReadOnly()
        {
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity));


            Assert.IsFalse(resourcePropertyDialogVm.EditNameAllowed);
        }



        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void OnChangeAndExit_DialogChangedRaised_CustomBankProperty()
        {
            var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            var args = new EventToCommandArgs(this, null, null, new System.ComponentModel.CancelEventArgs());

            A.CallTo(() => FakeDal.FakeServices.FakePermissionService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored)).Returns(true);

            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity));
            resourcePropertyDialogVm.PropertyEntity.DataValue.Description = "Some description";
            resourcePropertyDialogVm.WindowClosing.Execute(args);

            A.CallTo(() => fake_MsgBoxServ.ShowYesNoCancel(A<string>.Ignored, A<string>.Ignored, A<CustomDialogIcons>.Ignored)).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void CorrectTabsVisible_CustomBankPropertyEntity()
        {
            var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity));

            resourcePropertyDialogVm.SetTabsVisibility();

            Assert.IsTrue(resourcePropertyDialogVm.GeneralTabVisible.DataValue);
            Assert.IsFalse(resourcePropertyDialogVm.DependenciesTabVisible.DataValue);
            Assert.IsTrue(resourcePropertyDialogVm.ReferencesTabVisible.DataValue);
            Assert.IsFalse(resourcePropertyDialogVm.DataTabVisible.DataValue);
            Assert.IsFalse(resourcePropertyDialogVm.HistoryTabVisible.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void LeftMostTabMustBeActiveOnOpenening_CustomBankPropertyEntity()
        {
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();

            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity));


            Assert.IsTrue(resourcePropertyDialogVm.SelectedTab.DataValue == 0);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void NameFieldMustBeReadOnly_CustomBankPropertyEntity()
        {
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity));


            Assert.IsFalse(resourcePropertyDialogVm.EditNameAllowed);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        [ExpectedException(typeof(ArgumentException), "Only types of ResourceEntity and CustomBankProperty are supported.")]
        public void BreakOnLoadingUnsupportedEntity()
        {
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();

            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(BankEntity));
        }

    }
}
