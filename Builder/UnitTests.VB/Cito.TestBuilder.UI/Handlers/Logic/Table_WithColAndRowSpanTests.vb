
Imports System.Xml

<TestClass()>
Public Class Table_WithColAndRowSpanTests
    Inherits TableBaseTests


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetRowsFromTable()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableWithRowSpan4x4(ns)

        Dim result = table.GetRowCount()

        Assert.AreEqual(4, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetColumnsFromTable()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableWithRowSpan4x4(ns)

        Dim result = table.GetColumnCount()

        Assert.AreEqual(4, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub VerifyCells()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableWithRowSpan4x4(ns)

        Dim expected As String(,) = {{"1", "2", "3", "4"},
                                      {"5", "6", "6", "7"},
                                      {"8", "6", "6", "9"},
                                      {"10", "11", "12", "13"}}

        For r = 0 To 3
            Dim row = table.Rows(r)
            Assert.AreEqual(4, row.Cells.Count)

            For c = 0 To 3
                Dim cell = row.Cells(c)

                Assert.AreEqual(expected(r, c), cell.InnerText)
            Next
        Next
    End Sub

End Class
