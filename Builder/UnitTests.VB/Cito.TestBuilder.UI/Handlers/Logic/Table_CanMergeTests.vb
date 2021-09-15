
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class Table_CanMergeTests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub CanMergeSimpleTable()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t As Table = MyBase.GetTableSimple(ns)
        '+-----+-----+-----+-----+
        '| (1) |  2  |  3  |  4  |
        '+-----|-----|-----+-----+
        '|  5  |  6  | (7) |  8  |
        '+-----|-----|-----+-----+
        '|  9  |  10 |  11 |  12 |
        '+-----+-----+-----+-----+
        Dim c1 = t.Rows(0).Cells(0) 'Cell 1
        Dim c2 = t.Rows(1).Cells(2) 'Cell 7
      
        'Act
        Dim result = t.CanMerge(c1, c2)
       
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub CanNotMergeTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t As Table = MyBase.GetTableProblem1(ns)
        '+------+----+------+----+
        '|   1  |    |      |    |
        '+------+    |  [1] |    |
        '| r[3] |    |      |    |
        '+------+  2 +------+  3 |
        '|(r[2])|    |      |    |
        '+------+    |(r[4])|    |
        '| r[1] |    |      |    |
        '+-----------------------+
        '|   4  |     5     |  6 |
        '+----------------------+
        '|   7  |     8     |  9 |
        '+-----------------------+
        Dim c1 = t.Rows(2).Cells(0) 'Cell r[2]
        Dim c2 = t.Rows(2).Cells(2) 'Cell r[4]
       
        'Act
        Dim result = t.CanMerge(c1, c2) 'Cell 2 intersects
       
        'Assert
        Assert.IsFalse(result)
    End Sub

End Class
