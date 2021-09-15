
Imports Questify.Builder.Security

<TestClass()> _
Public Class TestBuilderPermissionEntryCollectionTest

    '''<summary>
    '''A test for GetEntryByKey(ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub TestBuilderPermissionEntryCollection_GetEntryByKeyTest()
        'Arrange
        Dim target As TestBuilderPermissionEntryCollection = New TestBuilderPermissionEntryCollection
        target.Add(New TestBuilderPermissionEntry(TestBuilderPermissionTarget.AllTargets, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.AddNew))
        Dim expected As New TestBuilderPermissionEntry(TestBuilderPermissionTarget.ItemEntity, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.AddDependency)
        Dim key As String = expected.Key
        target.Add(expected)
        target.Add(New TestBuilderPermissionEntry(TestBuilderPermissionTarget.BankEntity, TestBuilderPermissionNamedTask.EditXhtmlParameterSource, TestBuilderPermissionAccess.DALCreate))

        'Act
        Dim actual As TestBuilderPermissionEntry = target.GetEntryByKey(key)

        'Assert
        Assert.AreEqual(expected, actual, "Cito.TestBuilder.Security.TestBuilderPermissionEntryCollection.GetEntryByKey did not return the expected value.")
    End Sub

End Class
