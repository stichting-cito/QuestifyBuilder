
Imports Questify.Builder.Security

<TestClass()> _
Public Class TestBuilderPermissionEntryTest

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_KeyTest()
        'Arrange

        'Act
        Dim target As New TestBuilderPermissionEntry(TestBuilderPermissionTarget.AllTargets, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.AddNew)

        'Assert
        Assert.IsFalse(String.IsNullOrEmpty(target.Key), "Cito.TestBuilder.Security.TestBuilderPermissionEntry.Key was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_ConstructorTest()
        Dim target As New TestBuilderPermissionEntry()
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_ConstructorTest1()
        'Arrange

        'Act
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry(TestBuilderPermissionTarget.ItemEntity, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.DALRead)

        'Assert
        Assert.AreEqual(Of TestBuilderPermissionTarget)(TestBuilderPermissionTarget.ItemEntity, target.PermissionTarget, "TestBuilderPermissionEntry.PermissionTarget was not set correctly.")
        Assert.AreEqual(Of TestBuilderPermissionNamedTask)(TestBuilderPermissionNamedTask.None, target.TargettedNamedTask, "TestBuilderPermissionEntry.TargettedNamedTask was not set correctly.")
        Assert.AreEqual(Of TestBuilderPermissionAccess)(TestBuilderPermissionAccess.DALRead, target.PermissionAccess, "TestBuilderPermissionEntry.PermissionAccess was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_ConstructorTest2()
        'Arrange

        'Act
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry(TestBuilderPermissionTarget.ItemEntity, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.DALRead, True)

        'Assert
        Assert.AreEqual(Of TestBuilderPermissionTarget)(TestBuilderPermissionTarget.ItemEntity, target.PermissionTarget, "TestBuilderPermissionEntry.PermissionTarget was not set correctly.")
        Assert.AreEqual(Of TestBuilderPermissionNamedTask)(TestBuilderPermissionNamedTask.None, target.TargettedNamedTask, "TestBuilderPermissionEntry.TargettedNamedTask was not set correctly.")
        Assert.AreEqual(Of TestBuilderPermissionAccess)(TestBuilderPermissionAccess.DALRead, target.PermissionAccess, "TestBuilderPermissionEntry.PermissionAccess was not set correctly.")
        Assert.IsTrue(target.WhenOwnerCondition)
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_PermissionAccessTest()
        'Arrange
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry

        'Act
        target.PermissionAccess = TestBuilderPermissionAccess.AddNew

        'Assert
        Assert.AreEqual(TestBuilderPermissionAccess.AddNew, target.PermissionAccess, "TestBuilderPermissionEntry.PermissionAccess was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_PermissionTargetTest()
        'Arrange
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry

        'Act
        target.PermissionTarget = TestBuilderPermissionTarget.BankEntity

        'Assert
        Assert.AreEqual(TestBuilderPermissionTarget.BankEntity, target.PermissionTarget, "TestBuilderPermissionEntry.PermissionTarget was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_TargettedNamedTaskTest()
        'Arrange
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry

        'Act
        target.TargettedNamedTask = TestBuilderPermissionNamedTask.EditXhtmlParameterSource

        'Assert
        Assert.AreEqual(TestBuilderPermissionNamedTask.EditXhtmlParameterSource, target.TargettedNamedTask, "Cito.TestBuilder.Security.TestBuilderPermissionEntry.TargettedNamedTask was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_WhenOwnerConditionTest()
        'Arrange
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry

        'Act
        target.WhenOwnerCondition = True

        'Assert
        Assert.IsTrue(target.WhenOwnerCondition, "Cito.TestBuilder.Security.TestBuilderPermissionEntry.WhenOwnerCondition was not set correctly.")
    End Sub

End Class
