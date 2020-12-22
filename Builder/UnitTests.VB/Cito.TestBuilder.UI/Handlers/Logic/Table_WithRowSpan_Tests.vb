
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class Table_WithRowSpan_Tests


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetRowsFromTable()
        Dim table = GetTable()

        Dim result = table.GetRowCount()

        Assert.AreEqual(3, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetColumnsFromTable()
        Dim table = GetTable()

        Dim result = table.GetColumnCount()

        Assert.AreEqual(4, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub VerifyCellsForMiddleRow()
        Dim table = GetTable()


        Dim expected As String() = {"1", "5", "6", "7"}
        Dim actual As New List(Of String)

        For Each c In table.Rows(1).Cells
            actual.Add(c.InnerText)
        Next

        Assert.AreEqual(4, actual.Count)
        For i = 0 To 3
            Assert.AreEqual(expected(i), actual(i))
        Next
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub VerifyCellsForLastRow()
        Dim table = GetTable()

        Dim expected As String() = {"8", "9", "6", "10"}
        Dim actual As New List(Of String)

        For Each c In table.Rows(2).Cells
            actual.Add(c.InnerText)
        Next

        Assert.AreEqual(4, actual.Count)
        For i = 0 To 3
            Assert.AreEqual(expected(i), actual(i))
        Next
    End Sub

    Private Function GetTable() As Table
        Dim doc As New XmlDocument() : doc.LoadXml(TestData.tstDataRowspan.ToString())
        Dim ns As New XmlNamespaceManager(doc.NameTable) : ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim result = Table.getTableFromNode(doc.SelectSingleNode("//def:td[2]", ns))
        Return result
    End Function

End Class
