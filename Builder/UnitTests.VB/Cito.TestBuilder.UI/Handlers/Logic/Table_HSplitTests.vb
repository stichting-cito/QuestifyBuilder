
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class Table_HSplitTests

    Private Shared NewEmptyNode As String = ChrW(&HA0).ToString()

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal_4x3Table()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableSimple(ns) 'ns will be set
        Dim c = t.Rows(1).Cells(1) '= Cell 6
        
        'Act
        '+-----+-----+-----+-----+       +-----+-----+-----+-----+
        '|  1  |  2  |  3  |  4  |       |  1  |  2  |  3  |  4  |
        '+-----|-----|-----+-----+       +-----|-----|-----+-----+
        '|  5  | (6) |  7  |  8  |  =>   |     |  6  |     |     |
        '+-----|-----|-----+-----+       |  5  |-----|  7  |  8  |
        '|  9  |  10 |  11 |  12 |       |     | New |     |     |
        '+-----+-----+-----+-----+       +-----|-----|-----+-----+
        '                                |  9  |  10 |  11 |  12 |
        '                                +-----+-----+-----+-----+
        c.SplitHorizontal()
       
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr)", ns)) 'Now 4 rows

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) 'First row has still 4 td's (!!XPATH starts count @ 1)
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns)) 'Second row has 4 td's 
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns)) 'Third row has still 4 td's due to split
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns)) 'Fourth row has still 4 td's 

        'First row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        'Second row Count & Sum of Rowspan attribute
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(6.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        'Third row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        'Fourth row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))
        'Verify old and new cell
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[3]/def:td[1]", ns).InnerText) 'There is only 1 td
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal_WithRowSpan_gt_1()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan(ns) 'ns will be set
        Dim c = t.Rows(0).Cells(0) '= Cell 1
       
        'Act
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '|     |  2  |  3  |  4  |      |  1  |  2  |  3  |  4  |
        '+ (1) +-----+-----+-----+      +-----+-----+-----+-----+
        '|     |  5  |     |  7  |  =>  | New |  5  |     |  7  |
        '+-----|-----+  6  +-----+      +-----|-----+  6  +-----+
        '|  8  |  9  |     |  10 |      |  8  |  9  |     |  10 |
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        c.SplitHorizontal()
       
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns)) 'Now 4 rows

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) 'First row has still 4 td's (!!XPATH starts count @ 1)
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns)) 'Second row has 4 td's 
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns)) 'Third row has still 4 td's due to split

        'First row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        'Second row Count & Sum of Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        'Third row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))
        'Verify old and new cell
        Assert.AreEqual("1", t.Node.SelectSingleNode("//def:tr[1]/def:td[1]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[1]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal2_WithRowSpan_gt_1()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan(ns) 'ns will be set
        Dim c = t.Rows(1).Cells(2) '= Cell 6
        
        'Act
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '|     |  2  |  3  |  4  |      |     |  2  |  3  |  4  |
        '+  1  +-----+-----+-----+      +  1  +-----+-----+-----+
        '|     |  5  |     |  7  |  =>  |     |  5  |  6  |  7  |
        '+-----|-----+ (6) +-----+      +-----|-----+-----+-----+
        '|  8  |  9  |     |  10 |      |  8  |  9  | New |  10 |
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        c.SplitHorizontal()
        
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns)) 'Now 4 rows

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))

        'First row Count & Sum of Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        'Second row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        'Third row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))
        'Verify old and new cell
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[3]/def:td[3]", ns).InnerText)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal3x6_WithRowSpan_gt_3()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan3x6(ns) 'ns will be set
        Dim c = t.Rows(1).Cells(0) '= Cell 4
       
        'Act
        '                                                     ROW
        '+-----+-----+-----+        +-----+-----+-----+  
        '|  1  |  2  |  3  |        |  1  |  2  |  3  |         1
        '+-----+-----+-----+        +-----+-----+-----+
        '|     |  5  |     |        |     |  5  |     |         2
        '+     |-----+     +        +  4  |-----+     +
        '|     |  6  |     |        |     |  6  |     |         3
        '+ (4) |-----+  9  +  =>    +-----|-----+  9  +
        '|     |  7  |     |        |     |  7  |     |         4
        '+     |-----+     +        + New |-----+     +
        '|     |  8  |     |        |     |  8  |     |         5
        '+-----|-----+-----+        +-----|-----+-----+
        '| 10  |  11 |  12 |        | 10  |  11 |  12 |         6
        '+-----+-----+-----+        +-----+-----+-----+
        c.SplitHorizontal()
        
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(6.0, nav.Evaluate("count(//def:tr)", ns)) 'Still 6 rows

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[5]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[6]/def:td)", ns))

        'First row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        'Second row Count & Sum of Rowspan attribute
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(6.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        'Third row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        'Fourth row Count & Sum of Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        'Fifth row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[5]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[5]/def:td/@rowspan)", ns))

        'Last row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[6]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[6]/def:td/@rowspan)", ns))
        'Verify old and new cell
        Assert.AreEqual("4", t.Node.SelectSingleNode("//def:tr[2]/def:td[1]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[4]/def:td[1]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal3x7_WithRowSpan_gt_3()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan3x7(ns) 'ns will be set
        Dim c = t.Rows(1).Cells(2) '= Cell 10
        
        'Act
        '                                                     ROW
        '+-----+-----+-----+            +-----+-----+-----+
        '|  1  |  2  |  3  |            |  1  |  2  |  3  |     1
        '+-----+-----+-----+            +-----+-----+-----+     
        '|     |  5  |     |            |     |  5  |     |     2
        '+     |-----+     +            +     |-----+     +
        '|     |  6  |     |            |     |  6  | 10  |     3
        '+     |-----+     +            +     |-----+     +
        '|  4  |  7  |(10) |    =>      |  4  |  7  |     |     4
        '+     |-----+     +            +     |-----+-----+
        '|     |  8  |     |            |     |  8  |     |     5
        '+     |-----+     +            +     |-----+ New +
        '|     |  9  |     |            |     |  9  |     |     6
        '+-----|-----+-----+            +-----|-----+-----+
        '| 11  |  12 |  13 |            | 11  |  12 |  13 |     7
        '+-----+-----+-----+            +-----+-----+-----+

        c.SplitHorizontal()
        
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(7.0, nav.Evaluate("count(//def:tr)", ns)) 'Still 7 rows

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[5]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[6]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[7]/def:td)", ns))

        'First row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        'Second row Count & Sum of Rowspan attribute
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(8.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        'Third row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        'Fourth row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        'Fifth row Count & Sum of Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[5]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[5]/def:td/@rowspan)", ns))

        'Sixth row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[6]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[6]/def:td/@rowspan)", ns))

        'Last row Count & Sum of Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[7]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[7]/def:td/@rowspan)", ns))
        'Verify old and new cell
        Assert.AreEqual("10", t.Node.SelectSingleNode("//def:tr[2]/def:td[3]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[5]/def:td[2]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal4x4_2x2Cell()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan4x4(ns) 'ns will be set
        Dim c = t.Rows(1).Cells(1) '= Cell 6
       
        'Act
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '|  1  |  2  |  3  |  4  |      |  1  |  2  |  3  |  4  |
        '+-----|-----+-----+-----+      +-----|-----+-----+-----+
        '|  5  |           |  7  |      |  5  |     6     |  7  |
        '+-----|    (6)    |-----+  =>  +-----|-----------|-----+
        '|  8  |           |  9  |      |  8  |    New    |  9  |
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '|  10 |  11 |  12 |  13 |      |  10 |  11 |  12 |  13 |
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        c.SplitHorizontal()
       
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr)", ns)) 'Still 7 rows

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))

        'First row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        '2nd row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        '3th row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        '4th row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        'Verify old and new cell
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[3]/def:td[2]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontalWithColspan()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithColspan(ns) 'ns will be set
        Dim c = t.Rows(0).Cells(1) '= Cell 2
        
        'Act
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '|  1  |    (2)    |  3  |      |     |     2     |     |
        '+-----|-----+-----+-----+      |  1  +-----------+  3  |
        '|  4  |  5  |  6  |  7  |      |     |    New    |     |
        '+-----|-----|-----+-----+  =>  +-----|-----------+-----+
        '|  8  |  9  |  10 |  11 |      |  4  |  5  |  6  |  7  |
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '                               |  8  |  9  |  10 |  11 |
        '                               +-----+-----+-----+-----+
        c.SplitHorizontal()
       
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr)", ns)) 'Now 4

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))

        'First row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        '2nd row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        '3th row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        '4th row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        'Verify old and new cell
        Assert.AreEqual("2", t.Node.SelectSingleNode("//def:tr[1]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[1]", ns).InnerText)
    End Sub

    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----|-----+-----+
    '|  5  |  6  |  7  |  8  |
    '+-----|-----|-----+-----+
    '|  9  |  10 |  11 |  12 |
    '+-----+-----+-----+-----+
    Private Function GetTableSimple(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstData.ToString(), ns)
    End Function

    '+-----+-----+-----+-----+
    '|     |  2  |  3  |  4  |
    '+  1  +-----+-----+-----+
    '|     |  5  |     |  7  |
    '+-----|-----+  6  +-----+
    '|  8  |  9  |     |  10 |
    '+-----+-----+-----+-----+
    Private Function GetTableWithRowSpan(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan.ToString(), ns)
    End Function

    '+-----+-----+-----+
    '|  1  |  2  |  3  |
    '+-----+-----+-----+
    '|     |  5  |     |
    '+     |-----+     +
    '|     |  6  |     |
    '+  4  |-----+  9  +
    '|     |  7  |     |
    '+     |-----+     +
    '|     |  8  |     |
    '+-----|-----+-----+
    '| 10  |  11 |  12 |
    '+-----+-----+-----+
    Private Function GetTableWithRowSpan3x6(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan3x6.ToString(), ns)
    End Function

    '+-----+-----+-----+
    '|  1  |  2  |  3  |
    '+-----+-----+-----+
    '|     |  5  |     |
    '+     |-----+     +
    '|     |  6  |     |
    '+     |-----+     +
    '|  4  |  7  | 10  |
    '+     |-----+     +
    '|     |  8  |     |
    '+     |-----+     +
    '|     |  9  |     |
    '+-----|-----+-----+
    '| 11  |  12 |  13 |
    '+-----+-----+-----+
    Private Function GetTableWithRowSpan3x7(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan3x7.ToString(), ns)
    End Function

    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----+-----+-----+
    '|  5  |           |  7  |
    '+-----|     6     |-----+
    '|  8  |           |  9  |
    '+-----+-----+-----+-----+
    '|  10 |  11 |  12 |  13 |
    '+-----+-----+-----+-----+
    Private Function GetTableWithRowSpan4x4(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstData4x4.ToString(), ns)
    End Function

    '+-----+-----+-----+-----+
    '|  1  |     2     |  3  |
    '+-----|-----+-----+-----+
    '|  4  |  5  |  6  |  7  |
    '+-----|-----|-----+-----+
    '|  8  |  9  |  10 |  11 |
    '+-----+-----+-----+-----+
    Private Function GetTableWithColspan(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataColspan.ToString(), ns)
    End Function

    Private Function GetTable(xml As String, ByRef ns As XmlNamespaceManager) As Table
        Dim doc As New XmlDocument() : doc.LoadXml(xml)
        ns = New XmlNamespaceManager(doc.NameTable) : ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim result = Table.GetTableFromNode(doc.SelectSingleNode("//def:td[2]", ns))
        Return result
    End Function

End Class
