
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class Table_VSplitTests

    Private Shared NewEmptyNode As String = ChrW(&HA0).ToString()

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellVertical_4x3Table()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableSimple(ns) 'ns will be set
        Dim c = t.Rows(1).Cells(1) '= Cell 6
        
        'Act
        '+-----+-----+-----+-----+      +-----+---------+-----+-----+
        '|  1  |  2  |  3  |  4  |      |  1  |    2    |  3  |  4  |
        '+-----|-----|-----+-----+      +-----|---------|-----+-----+
        '|  5  | (6) |  7  |  8  |  =>  |  5  | 6 | New |  7  |  8  |
        '+-----|-----|-----+-----+      +-----|---------|-----+-----+
        '|  9  |  10 |  11 |  12 |      |  9  |    10   |  11 |  12 |
        '+-----+-----+-----+-----+      +-----+---------+-----+-----+
        c.SplitVertical()
       
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns)) 'Still 3 rows
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) 'First row has still 4 td's (!!XPATH starts count @ 1)
        Assert.AreEqual(5.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns)) 'Second row has  5 td's due to split
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns)) 'Third row has still 4 td's 

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns)) 'Number of td with colspan attribute @ first row (!!XPATH starts count @ 1)
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns)) 'Summation of "colspan" combined form first row

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns)) 'Number of td with colspan attribute second row (!!XPATH starts count @ 1)

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns)) 'Number of td with colspan attribute @ first row (!!XPATH starts count @ 1)
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns)) 'Summation of "colspan" combined form second row
        'Verify old and new cell
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[3]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellWithColumnspan_Gt_1()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithColspan(ns) 'ns will be set
        Dim c = t.Rows(0).Cells(1) '= Table Cell 2
       
        'Act
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '|  1  |    (2)    |  3  |      |  1  |  2  | New |  3  |
        '+-----|-----+-----+-----+      +-----|-----+-----+-----+
        '|  4  |  5  |  6  |  7  |  =>  |  4  |  5  |  6  |  7  |
        '+-----|-----|-----+-----+      +-----|-----|-----+-----+
        '|  8  |  9  |  10 |  11 |      |  8  |  9  |  10 |  11 |
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        c.SplitVertical()
       
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns)) 'Still 3 rows
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) 'First row has 4 td's due to split (!!XPATH starts count @ 1)
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns)) 'Second row has  4 td's 
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns)) 'Third row has still 4 td's 

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns)) 'Number of td with colspan attribute @ first row (!!XPATH starts count @ 1)
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns)) 'Summation of "colspan" combined form first row

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns)) 'Number of td with colspan attribute second row (!!XPATH starts count @ 1)

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns)) 'Number of td with colspan attribute @ first row (!!XPATH starts count @ 1)
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns)) 'Summation of "colspan" combined form second row
        'Verify old and new cell
        Assert.AreEqual("2", t.Node.SelectSingleNode("//def:tr[1]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[1]/def:td[3]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCell_InColumnCellWithColumnspan_Gt_1()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithColspan(ns) 'ns will be set
        Dim c = t.Rows(1).Cells(2) '= TableCell 6
        
        'Act
        '+-----+-----+-----+-----+      +-----+-----------------+-----+
        '|  1  |     2     |  3  |      |  1  |        2        |  3  |
        '+-----|-----+-----+-----+      +-----|-----+-----+-----+-----+
        '|  4  |  5  | (6) |  7  |  =>  |  4  |  5  |  6  | New |  7  |
        '+-----|-----|-----+-----+      +-----|-----|-----+-----+-----+
        '|  8  |  9  |  10 |  11 |      |  8  |  9  |    10     |  11 |
        '+-----+-----+-----+-----+      +-----+-----+-----------+-----+
        c.SplitVertical()
       
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns)) 'Still 3 rows
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) 'First row has 4 td's due to colspan of cell 2 (!!XPATH starts count @ 1)
        Assert.AreEqual(5.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns)) 'Second row has 5 td's  due to split
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns)) 'Third row has still 4 td's 

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns)) 'Number of td with colspan attribute @ first row (!!XPATH starts count @ 1)
        Assert.AreEqual(3.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns)) 'Summation of "colspan" combined form first row

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns)) 'Number of td with colspan attribute second row (!!XPATH starts count @ 1)

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns)) 'Number of td with colspan attribute @ first row (!!XPATH starts count @ 1)
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns)) 'Summation of "colspan" combined form second row
        'Verify old and new cell
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[3]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[4]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellVertical4x4_2x2Cell()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan4x4(ns) 'ns will be set
        Dim c = t.Rows(1).Cells(1) '= Cell 6
      
        'Act
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '|  1  |  2  |  3  |  4  |      |  1  |  2  |  3  |  4  |
        '+-----|-----+-----+-----+      +-----|-----+-----+-----+
        '|  5  |           |  7  |      |  5  |     |     |  7  |
        '+-----|    (6)    |-----+  =>  +-----|  6  | New |-----+
        '|  8  |           |  9  |      |  8  |     |     |  9  |
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        '|  10 |  11 |  12 |  13 |      |  10 |  11 |  12 |  13 |
        '+-----+-----+-----+-----+      +-----+-----+-----+-----+
        c.SplitVertical()
      
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr)", ns)) 'Still 7 rows

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))

        'First row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        '2nd row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

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
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[3]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellVerticalWithRowSpan()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan(ns) 'ns will be set
        Dim c = t.Rows(0).Cells(0) '= Cell 1
        
        'Act
        '+-----+-----+-----+-----+     +-----+-----+-----+-----+-----+
        '|     |  2  |  3  |  4  |     |     |     |  2  |  3  |  4  |
        '+ (1) +-----+-----+-----+     +  1  | New +-----+-----+-----+
        '|     |  5  |     |  7  |  => |     |     |  5  |     |  7  |
        '+-----|-----+  6  +-----+     +-----+-----|-----+  6  +-----+
        '|  8  |  9  |     |  10 |     |     8     |  9  |     |  10 |
        '+-----+-----+-----+-----+     +-----------+-----+-----+-----+
        c.SplitVertical()
        
        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns)) 'Still 7 rows

        Assert.AreEqual(5.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))

        'First row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        '2nd row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        '3th row Count & Sum of Col-/Rowspan attribute
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        'Verify old and new cell
        Assert.AreEqual("1", t.Node.SelectSingleNode("//def:tr[1]/def:td[1]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[1]/def:td[2]", ns).InnerText)
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
    '|     |  2  |  3  |  4  |
    '+  1  +-----+-----+-----+
    '|     |  5  |     |  7  |
    '+-----|-----+  6  +-----+
    '|  8  |  9  |     |  10 |
    '+-----+-----+-----+-----+
    Private Function GetTableWithRowSpan(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan.ToString(), ns)
    End Function

End Class
