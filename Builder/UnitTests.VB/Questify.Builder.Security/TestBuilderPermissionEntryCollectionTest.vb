
Imports Questify.Builder.Security

<TestClass()> _
Public Class TestBuilderPermissionEntryCollectionTest

    <TestMethod()> _
    Public Sub TestBuilderPermissionEntryCollection_GetEntryByKeyTest()
        Dim target As TestBuilderPermissionEntryCollection = New TestBuilderPermissionEntryCollection
        target.Add(New TestBuilderPermissionEntry(TestBuilderPermissionTarget.AllTargets, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.AddNew))
        Dim expected As New TestBuilderPermissionEntry(TestBuilderPermissionTarget.ItemEntity, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.AddDependency)
        Dim key As String = expected.Key
        target.Add(expected)
        target.Add(New TestBuilderPermissionEntry(TestBuilderPermissionTarget.BankEntity, TestBuilderPermissionNamedTask.EditXhtmlParameterSource, TestBuilderPermissionAccess.DALCreate))

        Dim actual As TestBuilderPermissionEntry = target.GetEntryByKey(key)

        Assert.AreEqual(expected, actual, "Cito.TestBuilder.Security.TestBuilderPermissionEntryCollection.GetEntryByKey did not return the expected value.")
    End Sub

End Class
