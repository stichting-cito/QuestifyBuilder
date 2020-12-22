
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ResourcePropertyDialog
{
    [TestClass]
    public class ResourcePropertyDialogCommandBehavior : ResourcePropertyDialogTestBase
    {


        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        [FakeServiceHandler()]
        public void Execute_Ok_RequestToCloseWindowIsCalled_Type_Is_ResourceEntity()
        {
            bool closeRequest = false;
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity)); resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; resourcePropertyDialogVm.Ok.Execute(new object());
            Assert.IsTrue(closeRequest, "Close request was not fired, this is set in place to close");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void Execute_Cancel_RequestToCloseWindowIsCalled()
        {
            bool CloseRequest = false;
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity)); resourcePropertyDialogVm.CloseRequest += (s, e) => CloseRequest = true; resourcePropertyDialogVm.Cancel.Execute(new object());
            Assert.IsTrue(CloseRequest, "Close request was not fired, this is set in place to close");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void Execute_Apply()
        {
            bool closeRequest = false;
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity)); resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; resourcePropertyDialogVm.Apply.Execute(new object());
            Assert.IsFalse(closeRequest, "Close request was fired");
        }



        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        [FakeServiceHandler()]
        public void Execute_Ok_RequestToCloseWindowIsCalled_Type_Is_CustomBankPropertyEntity()
        {
            bool closeRequest = false;
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity)); resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; resourcePropertyDialogVm.Ok.Execute(new object());
            Assert.IsTrue(closeRequest, "Close request was not fired, this is set in place to close");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void Execute_Cancel_RequestToCloseWindowIsCalled_Type_Is_CustomBankPropertyEntity()
        {
            bool closeRequest = false;
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity)); resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; resourcePropertyDialogVm.Cancel.Execute(new object());
            Assert.IsTrue(closeRequest, "Close request was not fired, this is set in place to close");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
        public void Execute_Apply_Type_Is_CustomBankPropertyEntity()
        {
            bool closeRequest = false;
            var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity)); resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; resourcePropertyDialogVm.Apply.Execute(new object());
            Assert.IsFalse(closeRequest, "Close request was fired");
        }

    }
}
