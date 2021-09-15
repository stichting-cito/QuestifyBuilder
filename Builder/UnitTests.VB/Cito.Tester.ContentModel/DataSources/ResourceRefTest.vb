
Imports Cito.Tester.ContentModel.Datasources

<TestClass()>
Public Class ResourceRefTest

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ResourceRef_TwoDifferentInstancesSameId_AreEquals()
        'Arrange
        Dim r1 As New ResourceRef("id123") 'Same id!
        Dim r2 As New ResourceRef("id123") 'Same id!
        
        'Assert
        Assert.IsFalse(ReferenceEquals(r1, r2)) 'Reference is different since other instance.
        Assert.AreEqual(r1, r2)
        Assert.AreEqual(r1.GetHashCode(), r2.GetHashCode()) 'When objects are equal the hash code should be the same.
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ResourceRef_TwoDifferentInstancesSameOtherId_AreEquals()
        'Arrange
        Dim r1 As New ResourceRef("id123") 'Other id!
        Dim r2 As New ResourceRef("321id") 'Other id!

        'Assert
        Assert.IsFalse(ReferenceEquals(r1, r2)) 'Reference is different since other instance.
        Assert.AreNotEqual(r1, r2)
    End Sub

End Class
