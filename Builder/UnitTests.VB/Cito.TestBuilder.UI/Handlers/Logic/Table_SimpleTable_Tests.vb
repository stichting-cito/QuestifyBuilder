
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class Table_SimpleTable_Tests

    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----|-----+-----+
    '|  5  |  6  |  7  |  8  |
    '+-----|-----|-----+-----+
    '|  9  |  10 |  11 |  12 |
    '+-----+-----+-----+-----+
    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetRowsFrom4x3Table()
        'Arrange
        Dim table = GetTable()
        
        'Act
        Dim result = table.GetRowCount()
       
        'Assert
        Assert.AreEqual(3, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetColumnsFrom4x3Table()
        'Arrange
        Dim table = GetTable()
     
        'Act
        Dim result = table.GetColumnCount()
    
        'Assert
        Assert.AreEqual(4, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub FindCell()
        'Arrange
        Dim doc As New XmlDocument() : doc.LoadXml(TestData.tstData.ToString())
        Dim ns As New XmlNamespaceManager(doc.NameTable) : ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim t = Table.GetTableFromNode(doc.SelectSingleNode("//def:td[2]", ns))

        'Act
        Dim result = t.GetCellByNode(doc.SelectSingleNode("//def:td[3]", ns))

        'Assert
        Assert.IsNotNull(result)
        Assert.AreEqual("3", result.InnerText)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub FindCellCoordinates()
        'Arrange
        Dim doc As New XmlDocument() : doc.LoadXml(TestData.tstData.ToString())
        Dim ns As New XmlNamespaceManager(doc.NameTable) : ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim t = Table.GetTableFromNode(doc.SelectSingleNode("//def:td[2]", ns))
        Dim cell = t.GetCellByNode(doc.SelectSingleNode("//def:td[3]", ns))
        Dim col, row As Integer
      
        'Act
        Dim result = t.GetCellCoordinates(cell, col, row)

        'Assert
        '+-----+-----+-----+-----+
        '|  1  |  2  | (3) |  4  |  <= Row 0, Column 2 (Computer counting starts @ 0) 
        '+-----|-----|-----+-----+
        Assert.IsTrue(result)
        Assert.AreEqual(2, col)
        Assert.AreEqual(0, row)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub VerifyColumnCells()
        'Arrange
        Dim table = GetTable()
        Dim expected As String() = {"3", "7", "11"}
        Dim actual As New List(Of String)
      
        'Act
        For Each c In table.Columns(2).Cells
            actual.Add(c.InnerText)
        Next
      
        'Assert
        '3rd Row = 
        '+-----+
        '|  3  |
        '|-----+
        '|  7  |
        '|-----+
        '|  11 |
        '+-----+
        Assert.AreEqual(3, actual.Count)
        For i = 0 To 2
            Assert.AreEqual(expected(i), actual(i))
        Next

    End Sub

    Private Function GetTable() As Table
        Dim doc As New XmlDocument() : doc.LoadXml(TestData.tstData.ToString())
        Dim ns As New XmlNamespaceManager(doc.NameTable) : ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim result = Table.getTableFromNode(doc.SelectSingleNode("//def:td[2]", ns))
        Return result
    End Function

End Class
