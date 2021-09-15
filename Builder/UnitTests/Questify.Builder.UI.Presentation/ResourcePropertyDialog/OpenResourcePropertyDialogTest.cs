
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
        #region ResourceEntity

        [TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void OnChangeAndExit_DialogChangedRaised()
		{
			//Arrange
			var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			var args = new EventToCommandArgs(this, null, null, new System.ComponentModel.CancelEventArgs());
   
            A.CallTo(() => FakeDal.FakeServices.FakePermissionService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored)).Returns(true);

			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ItemResourceEntity));
			resourcePropertyDialogVm.PropertyEntity.DataValue.Description = "Some description"; //This will invalidate object, so we need to save.
	
			//Act
			resourcePropertyDialogVm.WindowClosing.Execute(args);

			//Assert
			A.CallTo(() => fake_MsgBoxServ.ShowYesNoCancel(A<string>.Ignored, A<string>.Ignored, A<CustomDialogIcons>.Ignored)).MustHaveHappened();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void CorrectTabsVisible_LoadingResourceEntity()
		{
			//Arrange
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity));

			//Act
			resourcePropertyDialogVm.SetTabsVisibility();

			//Assert
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
			//Arrange
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();

			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity));

			//Act

			//Assert
			Assert.IsTrue(resourcePropertyDialogVm.SelectedTab.DataValue == 0);
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void NameFieldMustBeReadOnly()
		{
			//Arrange
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(ResourceEntity));

			//Act

			//Assert
			Assert.IsFalse(resourcePropertyDialogVm.EditNameAllowed);
		}

		#endregion

		#region CustomBankPropertyEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void OnChangeAndExit_DialogChangedRaised_CustomBankProperty()
		{
			//Arrange
			var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			var args = new EventToCommandArgs(this, null, null, new System.ComponentModel.CancelEventArgs());

            A.CallTo(() => FakeDal.FakeServices.FakePermissionService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored)).Returns(true);

            resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity));
			resourcePropertyDialogVm.PropertyEntity.DataValue.Description = "Some description"; //This will invalidate object, so we need to save.

			//Act
			resourcePropertyDialogVm.WindowClosing.Execute(args);

			//Assert
			A.CallTo(() => fake_MsgBoxServ.ShowYesNoCancel(A<string>.Ignored, A<string>.Ignored, A<CustomDialogIcons>.Ignored)).MustHaveHappened();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void CorrectTabsVisible_CustomBankPropertyEntity()
		{
			//Arrange
			var fake_MsgBoxServ = FakeCustomMessageBoxService.MakeNewFake();
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity));

			//Act
			resourcePropertyDialogVm.SetTabsVisibility();

			//Assert
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
			//Arrange
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();

			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity));

			//Act

			//Assert
			Assert.IsTrue(resourcePropertyDialogVm.SelectedTab.DataValue == 0);
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		public void NameFieldMustBeReadOnly_CustomBankPropertyEntity()
		{
			//Arrange
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(CustomBankPropertyEntity));

			//Act

			//Assert
			Assert.IsFalse(resourcePropertyDialogVm.EditNameAllowed);
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("ResourcePropertyDialog")]
        [FakeResourcePropertyDialogObjectFactoryBehavior(ResourcePropertyDialogObjectStrategy.AbsoluteValidMinimum)]
		[ExpectedException(typeof(ArgumentException), "Only types of ResourceEntity and CustomBankProperty are supported.")]
		public void BreakOnLoadingUnsupportedEntity()
		{
			//Arrange
			var resourcePropertyDialogVm = new ResourcePropertyDialogViewModel();

			//Act
			resourcePropertyDialogVm.DoActualLoadOnResourceId(Guid.NewGuid(), typeof(BankEntity)); //BankEntity does not implement IPropertyEntity.

			//Assert
		}

		#endregion
	}
}
