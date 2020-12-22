
Imports Questify.Builder.Security

<TestClass()> _
Public Class TestBuilderPermissionEntryTest

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_KeyTest()

        Dim target As New TestBuilderPermissionEntry(TestBuilderPermissionTarget.AllTargets, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.AddNew)

        Assert.IsFalse(String.IsNullOrEmpty(target.Key), "Cito.TestBuilder.Security.TestBuilderPermissionEntry.Key was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_ConstructorTest()
        Dim target As New TestBuilderPermissionEntry()
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_ConstructorTest1()

        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry(TestBuilderPermissionTarget.ItemEntity, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.DALRead)

        Assert.AreEqual(Of TestBuilderPermissionTarget)(TestBuilderPermissionTarget.ItemEntity, target.PermissionTarget, "TestBuilderPermissionEntry.PermissionTarget was not set correctly.")
        Assert.AreEqual(Of TestBuilderPermissionNamedTask)(TestBuilderPermissionNamedTask.None, target.TargettedNamedTask, "TestBuilderPermissionEntry.TargettedNamedTask was not set correctly.")
        Assert.AreEqual(Of TestBuilderPermissionAccess)(TestBuilderPermissionAccess.DALRead, target.PermissionAccess, "TestBuilderPermissionEntry.PermissionAccess was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_ConstructorTest2()

        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry(TestBuilderPermissionTarget.ItemEntity, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.DALRead, True)

        Assert.AreEqual(Of TestBuilderPermissionTarget)(TestBuilderPermissionTarget.ItemEntity, target.PermissionTarget, "TestBuilderPermissionEntry.PermissionTarget was not set correctly.")
        Assert.AreEqual(Of TestBuilderPermissionNamedTask)(TestBuilderPermissionNamedTask.None, target.TargettedNamedTask, "TestBuilderPermissionEntry.TargettedNamedTask was not set correctly.")
        Assert.AreEqual(Of TestBuilderPermissionAccess)(TestBuilderPermissionAccess.DALRead, target.PermissionAccess, "TestBuilderPermissionEntry.PermissionAccess was not set correctly.")
        Assert.IsTrue(target.WhenOwnerCondition)
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_PermissionAccessTest()
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry

        target.PermissionAccess = TestBuilderPermissionAccess.AddNew

        Assert.AreEqual(TestBuilderPermissionAccess.AddNew, target.PermissionAccess, "TestBuilderPermissionEntry.PermissionAccess was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_PermissionTargetTest()
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry

        target.PermissionTarget = TestBuilderPermissionTarget.BankEntity

        Assert.AreEqual(TestBuilderPermissionTarget.BankEntity, target.PermissionTarget, "TestBuilderPermissionEntry.PermissionTarget was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_TargettedNamedTaskTest()
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry

        target.TargettedNamedTask = TestBuilderPermissionNamedTask.EditXhtmlParameterSource

        Assert.AreEqual(TestBuilderPermissionNamedTask.EditXhtmlParameterSource, target.TargettedNamedTask, "Cito.TestBuilder.Security.TestBuilderPermissionEntry.TargettedNamedTask was not set correctly.")
    End Sub

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntry_WhenOwnerConditionTest()
        Dim target As TestBuilderPermissionEntry = New TestBuilderPermissionEntry

        target.WhenOwnerCondition = True

        Assert.IsTrue(target.WhenOwnerCondition, "Cito.TestBuilder.Security.TestBuilderPermissionEntry.WhenOwnerCondition was not set correctly.")
    End Sub

End Class
