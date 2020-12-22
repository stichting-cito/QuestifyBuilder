Imports Cito.TestBuilder.ContentModel.EntityClasses
Imports Cito.TestBuilder.ContentModel.DatabaseSpecific
Imports System.Security.Principal

<TestClass()> _
Public Class DataExecuterAdapterTest

#Region "Privates"

	Private _principal As IPrincipal

#End Region

#Region "Test setup"

	<TestInitialize()>
	Public Sub Initialize()
		_principal = System.Threading.Thread.CurrentPrincipal
	End Sub

	<TestCleanup()>
	Public Sub CleanUp()
		System.Threading.Thread.CurrentPrincipal = _principal
	End Sub

#End Region

#Region "General tests"

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_CreatedByTestBuilderPrincipal_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()
		Dim userId = 2

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True

		System.Threading.Thread.CurrentPrincipal = New Cito.TestBuilder.Security.TestBuilderPrincipal(New Cito.TestBuilder.Security.TestBuilderIdentity(1, UnitTestSettings.TestClient, userId, UnitTestSettings.TestUsername, "unittest"))

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.CreatedBy, 2)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_CreatedByUnknownPrincipal_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()
		Dim userId = 1

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.CreatedBy, 1)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_ModifiedByTestBuilderPrincipal_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()
		Dim userId = 2

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True

		System.Threading.Thread.CurrentPrincipal = New Cito.TestBuilder.Security.TestBuilderPrincipal(New Cito.TestBuilder.Security.TestBuilderIdentity(1, UnitTestSettings.TestClient, userId, UnitTestSettings.TestUsername, "unittest"))

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.ModifiedBy, 2)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_ModifiedByUnknownPrincipal_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()
		Dim userId = 1

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.ModifiedBy, 1)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_ModifiedDateTestBuilderPrincipal_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True
		itemResourceEntity.ModifiedDate = DateTime.MinValue

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreNotEqual(itemResourceEntity.ModifiedDate, DateTime.MinValue)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_CreationDateUnknownPrincipal_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreNotEqual(itemResourceEntity.CreationDate, DateTime.MinValue)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_CreationDateTestBuilderPrincipal_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True
		itemResourceEntity.CreationDate = DateTime.MinValue

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreNotEqual(itemResourceEntity.CreationDate, DateTime.MinValue)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_ModifiedByTestBuilderPrincipal_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()
		Dim userId = 2

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False

		System.Threading.Thread.CurrentPrincipal = New Cito.TestBuilder.Security.TestBuilderPrincipal(New Cito.TestBuilder.Security.TestBuilderIdentity(1, UnitTestSettings.TestClient, userId, UnitTestSettings.TestUsername, "unittest"))

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.ModifiedBy, 2)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_ModifiedByUnknownPrincipal_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()
		Dim userId = 1

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.ModifiedBy, 1)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_ModifiedDateTestBuilderPrincipal_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False
		itemResourceEntity.ModifiedDate = DateTime.MinValue

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreNotEqual(itemResourceEntity.ModifiedDate, DateTime.MinValue)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_CreationDateUnknownPrincipal_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False
		itemResourceEntity.CreationDate = DateTime.MinValue

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.CreationDate, DateTime.MinValue)
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_CreationDateTestBuilderPrincipal_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False
		itemResourceEntity.CreationDate = DateTime.MinValue

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.CreationDate, DateTime.MinValue)
	End Sub

#End Region

#Region "Version tests"

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_NoVersion_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.Version, "0.1")
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_HasVersion_IsNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = True
		itemResourceEntity.Version = "0.4"

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.Version, "0.4")
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_NoVersion_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.Version, "0.1")
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_IncrementMinorVersion_NoVersion_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.Version, "0.1")
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_IncrementMinorVersion_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False
		itemResourceEntity.Version = "1.4"

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.Version, "1.5")
	End Sub

	<TestMethod(), Owner("remcor")>
	Public Sub PerformModificationsOnEntity_IncrementMajorVersion_IsNotNewTest()
		'Arrange
		Dim itemResourceEntity As New ItemResourceEntity()

		itemResourceEntity.IsDirty = True
		itemResourceEntity.IsNew = False
		itemResourceEntity.Version = "2"

		'Act
		DataAccessAdapterExecuter.PerformModificationsOnEntity(itemResourceEntity)

		'Assert
		Assert.AreEqual(itemResourceEntity.Version, "2.0")
	End Sub

#End Region

End Class
