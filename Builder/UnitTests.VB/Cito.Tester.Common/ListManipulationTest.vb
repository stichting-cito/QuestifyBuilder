
Imports Cito.Tester.Common


<TestClass()> _
Public Class ListManipulationTest

    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestMethod()> _
    Public Sub ListManipulation_ShuffleListTest()
        Dim originalList As New List(Of String)()
        For i As Integer = 1 To 16
            originalList.Add(String.Format("Item{0}", i))
        Next
        Dim cloneList As New List(Of String)(originalList)

        Dim actual As List(Of String) = DirectCast(ListManipulation.ShuffleList(originalList), List(Of String))

        CollectionAssert.AreEquivalent(cloneList, actual, "After shuffling the list doesn't contain the same items.")
        CollectionAssert.AreNotEqual(cloneList, actual, "The list doesn't seem to be shuffled")
    End Sub

End Class
