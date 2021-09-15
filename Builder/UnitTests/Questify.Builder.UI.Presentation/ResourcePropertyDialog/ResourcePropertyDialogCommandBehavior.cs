
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

		#region ResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		[FakeServiceHandler()]
		public void Execute_Ok_RequestToCloseWindowIsCalled_Type_Is_ResourceEntity()
		{
			//Arrange
			bool closeRequest = false;
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity)); //Gets default objects.
			resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; //This event is expected
			//Act            
			resourcePropertyDialogVm.Ok.Execute(new object());
			//Assert
			Assert.IsTrue(closeRequest, "Close request was not fired, this is set in place to close");
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void Execute_Cancel_RequestToCloseWindowIsCalled()
		{
			//Arrange
			bool CloseRequest = false;
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity)); //Gets default objects.
			resourcePropertyDialogVm.CloseRequest += (s, e) => CloseRequest = true; //This event is expected
			//Act            
			resourcePropertyDialogVm.Cancel.Execute(new object());
			//Assert
			Assert.IsTrue(CloseRequest, "Close request was not fired, this is set in place to close");
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void Execute_Apply()
		{
			//Arrange
			bool closeRequest = false;
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity)); //Gets default objects.
			resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; //This event is expected
			//Act            
			resourcePropertyDialogVm.Apply.Execute(new object());
			//Assert
			Assert.IsFalse(closeRequest, "Close request was fired");
		}

		#endregion

		#region CustomBankPropertyEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		[FakeServiceHandler()]
		public void Execute_Ok_RequestToCloseWindowIsCalled_Type_Is_CustomBankPropertyEntity()
		{
			//Arrange
			bool closeRequest = false;
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity)); //Gets default objects.
			resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; //This event is expected
			//Act            
			resourcePropertyDialogVm.Ok.Execute(new object());
			//Assert
			Assert.IsTrue(closeRequest, "Close request was not fired, this is set in place to close");
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void Execute_Cancel_RequestToCloseWindowIsCalled_Type_Is_CustomBankPropertyEntity()
		{
			//Arrange
			bool closeRequest = false;
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity)); //Gets default objects.
			resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; //This event is expected
			//Act            
			resourcePropertyDialogVm.Cancel.Execute(new object());
			//Assert
			Assert.IsTrue(closeRequest, "Close request was not fired, this is set in place to close");
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog"), FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void Execute_Apply_Type_Is_CustomBankPropertyEntity()
		{
			//Arrange
			bool closeRequest = false;
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity)); //Gets default objects.
			resourcePropertyDialogVm.CloseRequest += (s, e) => closeRequest = true; //This event is expected
			//Act            
			resourcePropertyDialogVm.Apply.Execute(new object());
			//Assert
			Assert.IsFalse(closeRequest, "Close request was fired");
		}

		#endregion
	}
}
