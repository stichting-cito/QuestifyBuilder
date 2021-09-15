
Imports System.Xml

<TestClass()>
Public Class Table_WithColAndRowSpanTests
    Inherits TableBaseTests

    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----+-----+-----+
    '|  5  |           |  7  |
    '+-----|     6     |-----+
    '|  8  |           |  9  |
    '+-----+-----+-----+-----+
    '|  10 |  11 |  12 |  13 |
    '+-----+-----+-----+-----+

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetRowsFromTable()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing 'Will be assigned @ next line
        Dim table = MyBase.GetTableWithRowSpan4x4(ns) '= ns is given ByRef.
      
        'Act
        Dim result = table.GetRowCount()
       
        'Assert
        Assert.AreEqual(4, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetColumnsFromTable()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing 'Will be assigned @ next line
        Dim table = MyBase.GetTableWithRowSpan4x4(ns) '= ns is given ByRef.
        
        'Act
        Dim result = table.GetColumnCount()
       
        'Assert
        Assert.AreEqual(4, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub VerifyCells()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing 'Will be assigned @ next line
        Dim table = MyBase.GetTableWithRowSpan4x4(ns) '= ns is given ByRef.
        '+-----+-----+-----+-----+
        '|  1  |  2  |  3  |  4  |
        '+-----|-----+-----+-----+
        '|  5  |           |  7  |
        '+-----|     6     |-----+
        '|  8  |           |  9  |
        '+-----+-----+-----+-----+
        '|  10 |  11 |  12 |  13 |
        '+-----+-----+-----+-----+

        Dim expected As String(,) = {{"1", "2", "3", "4"},
                                      {"5", "6", "6", "7"},
                                      {"8", "6", "6", "9"},
                                      {"10", "11", "12", "13"}}
        'Act
        'No act, just assert.

        'Assert
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
