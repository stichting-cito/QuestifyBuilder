
using System;
using Cito.Tester.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using ResourceHistoryCreator = Questify.Builder.Logic.Service.Direct.ResourceHistoryCreator;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ResourcePropertyDialog
{
	[TestClass]
	public class VersionReverterTest : ResourcePropertyDialogTestBase
	{
		private static System.Globalization.CultureInfo _oldUICulture;

	    [ClassInitialize]
	    public static void ClassInitialize(TestContext context)
	    {
            _oldUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

	    }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = _oldUICulture;
        }
        
		#region ItemResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges_ItemResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<ItemResourceEntity>();
		}
		
		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_ItemResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_ItemResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_ItemResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevert_ItemResourceEntity()
		{
			DoCanRevertToPreviousVersion_RevertResultsInNoChanges<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_ItemResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_ItemResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased_ItemResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved_ItemResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion_ItemResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<ItemResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore_ItemResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<ItemResourceEntity>();
		}

		#endregion

		#region AssessmentTestResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges_AssessmentTestResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_AssessmentTestResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_AssessmentTestResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_AssessmentTestResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevert_AssessmentTestResourceEntity()
		{
			DoCanRevertToPreviousVersion_RevertResultsInNoChanges<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_AssessmentTestResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_AssessmentTestResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased_AssessmentTestResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved_AssessmentTestResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion_AssessmentTestResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<AssessmentTestResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore_AssessmentTestResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<AssessmentTestResourceEntity>();
		}

		#endregion

		#region AspectResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges_AspectResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_AspectResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_AspectResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_AspectResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevert_AspectResourceEntity()
		{
			DoCanRevertToPreviousVersion_RevertResultsInNoChanges<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_AspectResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_AspectResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased_AspectResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved_AspectResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion_AspectResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<AspectResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore_AspectResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<AspectResourceEntity>();
		}

		#endregion

		#region DataSourceResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges_DataSourceResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_DataSourceResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_DataSourceResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_DataSourceResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevert_DataSourceResourceEntity()
		{
			DoCanRevertToPreviousVersion_RevertResultsInNoChanges<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_DataSourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_dataSourceResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased_DataSourceResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved_DataSourceResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion_DataSourceResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<DataSourceResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore_DataSourceResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<DataSourceResourceEntity>();
		}

		#endregion

		#region TestPackageResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges_TestPackageResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_TestPackageResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_TestPackageResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_TestPackageResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevert_TestPackageResourceEntity()
		{
			DoCanRevertToPreviousVersion_RevertResultsInNoChanges<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_TestPackageResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_TestPackageResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased_TestPackageResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved_TestPackageResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion_TestPackageResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<TestPackageResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore_TestPackageResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<TestPackageResourceEntity>();
		}

		#endregion

		#region ItemLayoutTemplateResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges_ItemLayoutTemplateResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_ItemLayoutTemplateResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_ItemLayoutTemplateResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_ItemLayoutTemplateResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevert_ItemLayoutTemplateResourceEntity()
		{
			DoCanRevertToPreviousVersion_RevertResultsInNoChanges<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_ItemLayoutTemplateResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_ItemLayoutTemplateResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased_ItemLayoutTemplateResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved_ItemLayoutTemplateResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion_ItemLayoutTemplateResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<ItemLayoutTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore_ItemLayoutTemplateResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<ItemLayoutTemplateResourceEntity>();
		}

		#endregion

		#region ControlTemplateResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges_ControlTemplateResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_ControlTemplateResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_ControlTemplateResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_ControlTemplateResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevert_ControlTemplateResourceEntity()
		{
			DoCanRevertToPreviousVersion_RevertResultsInNoChanges<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_ControlTemplateResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_ControlTemplateResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased_ControlTemplateResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved_ControlTemplateResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion_ControlTemplateResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<ControlTemplateResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore_ControlTemplateResourceEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<ControlTemplateResourceEntity>();
		}

		#endregion

		#region GenericResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges_GenericResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<GenericResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_GenericResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<GenericResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_GenericResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<GenericResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_GenericResourceEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<GenericResourceEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		public void CanRevert_GenericResourceEntity()
		{
            CannotRevert_BecauseDefinedOnType<GenericResourceEntity>();
		}

		#endregion

		#region CustomBankPropertyResourceEntity

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void RevertToPreviousResourceHistoryVersion_UnsupportedType_CustomBankPropertyEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_CustomBankPropertyEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_CustomBankPropertyEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void RevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved_CustomBankPropertyEntity()
		{
			DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void CanRevert_CustomBankPropertyEntity()
		{
			DoCanRevertToPreviousVersion_RevertResultsInNoChanges<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged_CustomBankPropertyEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void CanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded_CustombankPropertyResourceEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased_CustomBankEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void CanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved_CustomBankPropertyEntity()
		{
			DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion_CustomBankPropertyEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<CustomBankPropertyEntity>();
		}

		[TestMethod, TestCategory("ViewModel"), TestCategory("VersionReverter")]
		[ExpectedException(typeof(ArgumentException))]
		public void CannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore_CustomBankPropertyEntity()
		{
			DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<CustomBankPropertyEntity>();
		}

		#endregion

		#region CanRevert

		/// <summary>
		/// Does the can revert to previous version_ revert results in no changes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private void DoCanRevertToPreviousVersion_RevertResultsInNoChanges<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
            CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);
            var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");
			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Act
			var errorMessage = default(string);
			var canRevert = versionreverter.CanRevert(ref errorMessage);

			//Assert
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(canRevert);
			Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
		}

        private void CannotRevert_BecauseDefinedOnType<T>() where T : new()
        {
            //Arrange
            var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
            CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);
            var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");
            var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

            previousResourceHistory.Resource = propertyEntity as ResourceEntity;
            currentResourceHistory.Resource = propertyEntity as ResourceEntity;

            var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);
            
            //Act
            var errorMessage = default(string);
            var canRevert = versionreverter.CanRevert(ref errorMessage);

            //Assert
            Assert.IsFalse(canRevert);
        }

        /// <summary>
        /// Does the can revert to previous resource history version_ revert results in changes because title was changed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
        private void DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
            var dependentResource = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);

            var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			propertyEntity.Title = "Title has been changed.";
			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Act
			var errorMessage = default(string);
			var canRevert = versionreverter.CanRevert(ref errorMessage);

			//Assert
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(canRevert);
			Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
		}

		/// <summary>
		/// Does the can revert to previous resource history version_ revert results in changes because dependent resource was added.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private void DoCanRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
            var dependentResource = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);
            var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");
			var dependentResource2 = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);
			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Act
			var errorMessage = default(string);
			var canRevert = versionreverter.CanRevert(ref errorMessage);

			//Assert
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 2);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(canRevert);
			Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
		}

		/// <summary>
		/// Does the can revert to previous resource history version_ dependent resource was removed.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private void DoCanRevertToPreviousResourceHistoryVersion_DependentResourceWasRemoved<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
			var dependentResource = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);
			var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			propertyEntity.DependentResourceCollection.Remove(dependentResource);

			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Act
			var errorMessage = default(string);
			var canRevert = versionreverter.CanRevert(ref errorMessage);

			//Assert
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(canRevert);
			Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
		}

		/// <summary>
		/// Does the cannot revert to previous resource history version_ because dependent resource was added and its version was increased.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private void DoCannotRevertToPreviousResourceHistoryVersion_BecauseDependentResourceWasAddedAndItsVersionWasIncreased<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
			var dependentResource = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);
			var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			dependentResource.DependentResource.Version = "0.2";

			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Act
			var errorMessage = default(string);
			var canRevert = versionreverter.CanRevert(ref errorMessage);

			//Assert
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsFalse(canRevert);
			Assert.IsTrue(errorMessage.Contains("Used in most recent version: 0.2, Used in this version: 0.1"));
		}

		private void DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotMatchCurrentDependentResourceVersion<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
			var dependentResource = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.2", true);
			var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");
			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Act
			var errorMessage = default(string);
			var canRevert = versionreverter.CanRevert(ref errorMessage);

			//Assert
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData[0].Version == "0.1");
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData[0].Version == "0.1");
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsFalse(canRevert);
			Assert.IsTrue(errorMessage.Contains("Used in most recent version: 0.2, Used in this version: 0.1"));
		}

        private void DoCannotRevertToPreviousResourceHistoryVersion_BecauseVersionOfDependentResourceDoesNotExistAnymore<T>() where T : new()
        {
            //Arrange
            var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
            var dependentResource = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", false);
            var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");
            var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

            previousResourceHistory.Resource = propertyEntity as ResourceEntity;
            currentResourceHistory.Resource = propertyEntity as ResourceEntity;

            var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);
            var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
            var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

            //Act
            var errorMessage = default(string);
            var canRevert = versionreverter.CanRevert(ref errorMessage);

            //Assert
            Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
            Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
            Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 1);
            Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData[0].Version == "0.1");
            Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 1);
            Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData[0].Version == "0.1");
            Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
            Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
            Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
            Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
            Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
            Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
            Assert.IsFalse(canRevert);
            Assert.IsTrue(errorMessage.Contains("Used in most recent version: Resource does not exist, Used in this version: 0.1"));
        }

		#endregion

		#region Revert

		/// <summary>
		/// Does the revert to previous resource history version_ revert results in no changes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private void DoRevertToPreviousResourceHistoryVersion_RevertResultsInNoChanges<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
			var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");
			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);

			//Act
			var revertedResource = versionreverter.Revert();
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Assert
			Assert.IsFalse(revertedResource.IsDirty);
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsInstanceOfType(revertedResource, typeof(T));
			Assert.AreEqual(propertyEntity.Version, revertedResource.Version); //VersionReverter does not modify the number of the version. Instead, the DataAccessAdapterExecuter does it when a changed entity is saved.
		}

		/// <summary>
		/// Does the revert to previous resource history version_ revert results in changes because title was changed.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private void DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseTitleWasChanged<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
			var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			propertyEntity.Title = "Title has been changed.";
			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);

			//Act
			var revertedResource = versionreverter.Revert();
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Assert
			Assert.IsTrue(revertedResource.IsDirty);
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsInstanceOfType(revertedResource, typeof(T));
			Assert.AreEqual(propertyEntity.Version, revertedResource.Version); //VersionReverter does not modify the number of the version. Instead, the DataAccessAdapterExecuter does it when a changed entity is saved.
		}

		/// <summary>
		/// Does the revert to previous resource history version_ revert results in changes because dependent resource was added.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private void DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasAdded<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
			var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");
			var dependentResource = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);
			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);

			//Act
			var revertedResource = versionreverter.Revert();
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Assert
			Assert.IsFalse(revertedResource.IsDirty); //The entity itself isn't dirty!
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsInstanceOfType(revertedResource, typeof(T));
			Assert.AreEqual(propertyEntity.Version, revertedResource.Version); //VersionReverter does not modify the number of the version. Instead, the DataAccessAdapterExecuter does it when a changed entity is saved.
		}

		/// <summary>
		/// Does the revert to previous resource history version_ revert results in changes because dependent resource was removed.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private void DoRevertToPreviousResourceHistoryVersion_RevertResultsInChangesBecauseDependentResourceWasRemoved<T>() where T : new()
		{
			//Arrange
			var propertyEntity = CreatePropertyEntity<T>(Guid.NewGuid());
			var dependentResource = CreateAndAddDependentResource(propertyEntity, Guid.NewGuid(), "0.1", true);
			var previousResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			propertyEntity.DependentResourceCollection.Remove(dependentResource);

			var currentResourceHistory = ResourceHistoryCreator.CreateResourceHistoryEntity((IVersionable)propertyEntity, "remcor");

			previousResourceHistory.Resource = propertyEntity as ResourceEntity;
			currentResourceHistory.Resource = propertyEntity as ResourceEntity;

			var versionreverter = new VersionReverter(currentResourceHistory, previousResourceHistory);

			//Act
			var revertedResource = versionreverter.Revert();
			var metaDataPreviousVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(previousResourceHistory.MetaData, typeof(Versioning.MetaData));
			var metaDataCurrentVersion = (Versioning.MetaData)SerializeHelper.XmlDeserializeFromByteArray(currentResourceHistory.MetaData, typeof(Versioning.MetaData));

			//Assert
			Assert.IsFalse(revertedResource.IsDirty);
			Assert.IsTrue(metaDataPreviousVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataCurrentVersion.PropertyEntityMetaData.Count > 0);
			Assert.IsTrue(metaDataPreviousVersion.DependentResourcesMetaData.Count == 1);
			Assert.IsTrue(metaDataCurrentVersion.DependentResourcesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.ConceptStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.CustomPropertiesMetaData.Count == 0);
			Assert.IsTrue(metaDataPreviousVersion.TreeStructureMetaData.Count == 0);
			Assert.IsTrue(metaDataCurrentVersion.TreeStructureMetaData.Count == 0);
			Assert.IsInstanceOfType(revertedResource, typeof(T));
			Assert.AreEqual(propertyEntity.Version, revertedResource.Version); //VersionReverter does not modify the number of the version. Instead, the DataAccessAdapterExecuter does it when a changed entity is saved.
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Creates the property entity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id">The identifier.</param>
		/// <exception cref="System.ArgumentException">Must implement IVersionable</exception>
		private IPropertyEntity CreatePropertyEntity<T>(Guid id) where T : new()
		{
			IVersionable versionable = new T() as IVersionable;

			if (versionable == null)
				throw new ArgumentException("Must implement IVersionable");

			var propertyEntity = (IPropertyEntity)versionable;

			propertyEntity.Id = id;
			propertyEntity.Version = "0.1";
			propertyEntity.ResourceData = new ResourceDataEntity();
			propertyEntity.ResourceData.IsDirty = false;
			propertyEntity.ResourceData.IsNew = false;
			propertyEntity.IsDirty = false;
			propertyEntity.IsNew = false;

			return propertyEntity;
		}

		/// <summary>
		/// Creates the dependent resource.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyEntity">The property entity.</param>
		private DependentResourceEntity CreateAndAddDependentResource(IPropertyEntity propertyEntity, Guid guidOfItemResourceEntityToBeAdded, string versionOfItemResourceEntityToBeAdded, bool alsoAddToFakeDal)
		{
			var dependentResource = propertyEntity.DependentResourceCollection.AddNew();

			if (propertyEntity is ResourceEntity) //Do this check because CustomBankProperty's do not inherit from ResourceEntity.
			{
				dependentResource.ResourceId = propertyEntity.Id;

				var dependentResourceEntity = CreatePropertyEntity<ItemResourceEntity>(guidOfItemResourceEntityToBeAdded);
				dependentResource.DependentResource = (ResourceEntity)dependentResourceEntity;
			}

			if (alsoAddToFakeDal)
				CreateAndStoreItemResourceEntity(guidOfItemResourceEntityToBeAdded, versionOfItemResourceEntityToBeAdded); //Add the dependent resource to the FakeDal.

			return dependentResource;
		}

		#endregion

	}
}
