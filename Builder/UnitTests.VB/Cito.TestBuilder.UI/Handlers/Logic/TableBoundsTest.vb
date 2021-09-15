
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class TableBoundsTest
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DimensionTests1x1Cell()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableSimple(ns)
        '+-----+-----+-----+-----+
        '| (1) |  2  |  3  |  4  |
        '+-----|-----|-----+-----+
        '|  5  |  6  |  7  |  8  |
        '+-----|-----|-----+-----+
        '|  9  |  10 |  11 |  12 |
        '+-----+-----+-----+-----+
        Dim c = t.Rows(0).Cells(0) '= Cell 1
     
        'Act
        Dim bounds As New TableBounds(c)
     
        'Assert
        Assert.AreEqual(0, bounds.Left)
        Assert.AreEqual(0, bounds.Top)
        Assert.AreEqual(0, bounds.Right)
        Assert.AreEqual(0, bounds.Bottom)
        Assert.AreEqual(1, bounds.Rows)
        Assert.AreEqual(1, bounds.Columns)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DimensionTests1x1Cell7()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableSimple(ns)
        '       0     1     2     3
        '    +-----+-----+-----+-----+
        '  0 |  1  |  2  |  3  |  4  |
        '    +-----|-----|-----+-----+
        '  1 |  5  |  6  | (7) |  8  |
        '    +-----|-----|-----+-----+
        '  2 |  9  |  10 |  11 |  12 |
        '    +-----+-----+-----+-----+
        Dim c = t.Rows(1).Cells(2) '= Cell 7
       
        'Act
        Dim bounds As New TableBounds(c)
      
        'Assert
        Assert.AreEqual(2, bounds.Left)
        Assert.AreEqual(1, bounds.Top)
        Assert.AreEqual(2, bounds.Right)
        Assert.AreEqual(1, bounds.Bottom)
        Assert.AreEqual(1, bounds.Rows)
        Assert.AreEqual(1, bounds.Columns)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DimensionTests4x4()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)
        '       0     1     2     3
        '    +-----+-----+-----+-----+
        '  0 |  1  |  2  |  3  |  4  |
        '    +-----|-----+-----+-----+
        '  1 |  5  |           |  7  |
        '    +-----|    (6)    |-----+
        '  2 |  8  |           |  9  |
        '    +-----+-----+-----+-----+
        '  3 |  10 |  11 |  12 |  13 |
        '    +-----+-----+-----+-----+

        Dim c = t.Rows(1).Cells(1) '= Cell 6
       
        'Act
        Dim bounds As New TableBounds(c)
     
        'Assert
        Assert.AreEqual(1, bounds.Left)
        Assert.AreEqual(1, bounds.Top)
        Assert.AreEqual(2, bounds.Right)
        Assert.AreEqual(2, bounds.Bottom)
        Assert.AreEqual(2, bounds.Rows)
        Assert.AreEqual(2, bounds.Columns)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub ContainsTest()
        'Arrange
        Dim bounds As New TableBounds(1, 1, 3, 1)
        Dim shouldContain As New TableBounds(2, 1)
      
        'Act
        Dim result = bounds.Contains(shouldContain)
     
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DoesNotContainsTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)
        '       0     1     2     3
        '    +-----+-----+-----+-----+
        '  0 |  1  |  2  |  3  |  4  |
        '    +-----|-----+-----+-----+
        '  1 |  5  |           |  7  |
        '    +-----|    (6)    |-----+
        '  2 |  8  |           |  9  |
        '    +-----+-----+-----+-----+
        '  3 |  10 |  11 |  12 |  13 |
        '    +-----+-----+-----+-----+

        Dim c = t.Rows(1).Cells(1) '= Cell 6
        Dim c2 = t.Rows(1).Cells(3) '= Cell 7
      
        'Act
        Dim result = (New TableBounds(c)).Contains(New TableBounds(c2))
      
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DoesNotContainsTest2()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)
        '       0     1     2     3
        '    +-----+-----+-----+-----+
        '  0 |  1  |  2  |  3  |  4  |
        '    +-----|-----+-----+-----+
        '  1 |  5  |           |  7  |
        '    +-----|    (6)    |-----+
        '  2 |  8  |           |  9  |
        '    +-----+-----+-----+-----+
        '  3 |  10 |  11 |  12 |  13 |
        '    +-----+-----+-----+-----+

        Dim c = t.Rows(2).Cells(0) '= Cell 8
        Dim c2 = t.Rows(3).Cells(3) '= Cell 13
        Dim c3 = t.Rows(1).Cells(1) '= Cell 6
     
        'Act
        Dim result = (New TableBounds(c2, c)).Contains(New TableBounds(c3))
      
        'Assert
        Assert.IsFalse(result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub IntersectTest()
        'Arrange
        Dim bounds As New TableBounds(1, 3, 3, 1)
        Dim shouldIntersect As New TableBounds(2, 1, 1, 5)
      
        'Act
        Dim result = bounds.Intersects(shouldIntersect)
    
        'Assert
        Assert.IsTrue(result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub IntersectTest2()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)
        '       0     1     2     3
        '    +-----+-----+-----+-----+
        '  0 |  1  |  2  |  3  |  4  |
        '    +-----|-----+-----+-----+
        '  1 |  5  |           |  7  |
        '    +-----|    (6)    |-----+
        '  2 |  8  |           |  9  |
        '    +-----+-----+-----+-----+
        '  3 |  10 |  11 |  12 |  13 |
        '    +-----+-----+-----+-----+

        Dim c = t.Rows(2).Cells(0) '= Cell 8
        Dim c2 = t.Rows(3).Cells(3) '= Cell 13
        Dim c3 = t.Rows(1).Cells(1) '= Cell 6
       
        'Act
        Dim result = (New TableBounds(c2, c)).Intersects(New TableBounds(c3))
    
        'Assert
        Assert.IsTrue(result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub TableBoundsWithRowSpanCell()
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
        Dim result = New TableBounds(c1, c2)
    
        'Assert
        Assert.AreEqual(3, result.Columns)
        Assert.AreEqual(2, result.Rows)
    End Sub

End Class
