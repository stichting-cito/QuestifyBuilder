
Imports Cito.Tester.Common


'''<summary>
'''This is a test class for Cito.Tester.Common.ListManipulation and is intended
'''to contain all Cito.Tester.Common.ListManipulation Unit Tests
'''</summary>
<TestClass()> _
Public Class ListManipulationTest

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
            testContextInstance = value
        End Set
    End Property

    '''<summary>
    '''A test for ShuffleList(ByVal System.Collections.IList)
    '''</summary>
    <TestMethod()> _
    Public Sub ListManipulation_ShuffleListTest()
        'Assert
        Dim originalList As New List(Of String)()
        For i As Integer = 1 To 16 ' =   20.922.789.888.000  combinations ~ very unlikely
            originalList.Add(String.Format("Item{0}", i))
        Next
        Dim cloneList As New List(Of String)(originalList)

        'Act
        Dim actual As List(Of String) = DirectCast(ListManipulation.ShuffleList(originalList), List(Of String))
       
        'Assert
        CollectionAssert.AreEquivalent(cloneList, actual, "After shuffling the list doesn't contain the same items.")
        CollectionAssert.AreNotEqual(cloneList, actual, "The list doesn't seem to be shuffled")
    End Sub

End Class
