
Imports System.Xml

<TestClass()>
Public Class TableCell_MergeTest
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeEmptyCellWithCellWithText()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)

        '0)     <td/>
        '1)     <td>text</td>
        '2)     <td><p>1p</p></td>
        '3)     <td><p>2p _ 1</p><p>2p _ 2</p></td>
        '4)     <td><p>2p _ 1</p>out of p tag<p>2p _ 2</p></td>

        Dim c1 = t.Rows(0).Cells(0) 'Merge  <td/>
        Dim c2 = t.Rows(0).Cells(1) 'with  <td>text</td>

        'Act
        t.MergeCells(c1, c2)

        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr)", ns)) '1 row
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) 'First row has still 4 td's (!!XPATH starts count @ 1)
        Assert.AreEqual("text", c1.InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub Merge2CellsWith3P()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)

        '0)     <td/>
        '1)     <td>text</td>
        '2)     <td><p>1</p></td>
        '3)     <td><p>2</p><p>3</p></td>
        '4)     <td><p>4</p>out of p tag<p>5</p></td>

        Dim c1 = t.Rows(0).Cells(2) 'Merge  <td> <p>1</p> </td>
        Dim c2 = t.Rows(0).Cells(3) '<td> <p>2</p > <p>3</p> </td>

        'Act
        t.MergeCells(c1, c2)

        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr)", ns)) '1 row
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) 'First row has still 4 td's (!!XPATH starts count @ 1)

        'Check inner structure.
        nav = c1.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:p)", ns)) '5 p in total (whole xml); but merged down to 4
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeAll()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)

        '0)     <td/>
        '1)     <td>text</td>
        '2)     <td><p>1</p></td>
        '3)     <td><p>2</p><p>3</p></td>
        '4)     <td><p>4</p>out of p tag<p>5</p></td>

        Dim c1 = t.Rows(0).Cells(0)
        Dim c2 = t.Rows(0).Cells(4)

        'Act
        t.MergeCells(c1, c2)

        'Assert
        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr)", ns)) '1 row
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) 'Single cell left!
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:p)", ns)) '5 p in total (whole xml); but merged down to 3
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeCellsAndLoseObsoleteTr()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableProblem2(ns)

        '       0     1     2
        '    +-----+-----+-----+           +-----+-----+-----+
        '  0 |  1  |  2  |  3  |           |  1  |  2  |  3  |
        '    +-----+-----+-----+           +-----+-----+-----+
        '  1 |           | (5) |  =>       |     4     | 5 6 |
        '    +     4     +-----|           +-----------+-----+
        '  2 |           | (6) |           
        '    +-----+-----+-----+           
        ' Merge Cell 5 & 6, should end up with just 2 tr's instead of 3

        Dim c1 = t.Rows(1).Cells(2) 'Cell 5
        Dim c2 = t.Rows(2).Cells(2) 'Cell 6

        'Act
        t.MergeCells(c1, c2)

        'Assert
        Dim nav = t.Node.CreateNavigator()

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td[2]/def:p)", ns)) '1 p tag in "5 6" cell

        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr)", ns)) '2 rows!
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) '3 cells on first row (1,2,3)
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns)) '2 cells on on row 2 (4 & "5 6")
        Assert.IsTrue(t.TableIsBalanced()) 'Table SHOULD be balanced
        Assert.AreEqual("4", t.Rows(1).Cells(0).InnerText)
        Assert.AreEqual("4", t.Rows(1).Cells(1).InnerText)
        Assert.AreEqual("5" + ChrW(&HA0) + "6", t.Rows(1).Cells(2).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeCellsAndLoseObsoleteTrWithColAndRowSapn()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableProblem2(ns)

        '       0     1     2
        '    +-----+-----+-----+           +-----+-----+-----+
        '  0 |  1  |  2  |  3  |           |  1  |  2  |  3  |
        '    +-----+-----+-----+           +-----+-----+-----+
        '  1 |           |  5  |  =>       |     4 5 6        |
        '    +    (4)    +-----|           +-----------------+
        '  2 |           | (6) |           
        '    +-----+-----+-----+           
        ' Merge Cell 4, 5 & 6, should end up with just 2 tr's instead of 3

        Dim c1 = table.Rows(1).Cells(0) 'Cell 4
        Dim c2 = table.Rows(2).Cells(2) 'Cell 6

        'Act
        table.MergeCells(c1, c2)

        'Assert
        Dim nav = table.Node.CreateNavigator()

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td[1]/def:p)", ns)) '1 p tag in "4 5 6" cell

        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr)", ns)) '2 rows!
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) '3 cells on first row (1,2,3)
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns)) '2 cells on on row 2 (4 & "5 6")
        Assert.IsTrue(table.TableIsBalanced()) 'Table SHOULD be balanced
        Assert.AreEqual("4" + ChrW(&HA0) + "5" + ChrW(&HA0) + "6", table.Rows(1).Cells(2).InnerText)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeCellsAndLoseObsoleteTdWithColAndRowSpan()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableProblem2(ns)

        '       0     1     2
        '    +-----+-----+-----+      +-----+-----+     
        '  0 | (1) |  2  |  3  |      |     |  3  |     
        '    +-----+-----+-----+      +     +-----+     
        '  1 |           |  5  |  =>  |1 2 4|  5  |     
        '    +    (4)    +-----|      |     +-----|     
        '  2 |           |  6  |      |     |  6  |     
        '    +-----+-----+-----+      +-----+-----+     
        ' Merge Cell 4, 5 & 6, should end up with just 2 tr's instead of 3

        Dim c1 = table.Rows(0).Cells(0) 'Cell 1
        Dim c2 = table.Rows(1).Cells(0) 'Cell 4

        'Act
        table.MergeCells(c1, c2)

        'Assert
        Dim nav = table.Node.CreateNavigator()

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td[1]/def:p)", ns)) '1 p tag in "4 5 6" cell

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns)) '3 rows!
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:col)", ns)) '2 columns!

        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns)) '3 cells on first row (1,2,3)
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns)) '1 cell on row 2
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns)) '1 cell on row 2
        Assert.IsTrue(table.TableIsBalanced()) 'Table SHOULD be balanced
        Assert.AreEqual("1" + ChrW(&HA0) + "2" + ChrW(&HA0) + "4", table.Rows(0).Cells(0).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeAndRemoveMultipleTr()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableWithRowSpan3x6(ns)
        '+-----+-----+-----+        +-----+-----------+-----+
        '|  1  |  2  |  3  |        |  1  |     2     |  3  |
        '+-----+-----+-----+        +-----+-----------+-----+
        '|     | (5) |     |        |  4  |  5 6 7 8  |  9  |
        '+     |-----+     +  =>    +-----+-----------+-----+
        '|     |  6  |     |        |  10 |     11    |  12 |
        '+  4  |-----+  9  +        +-----+-----------+-----+
        '|     |  7  |     |
        '+     |-----+     +
        '|     | (8) |     |
        '+-----|-----+-----+
        '| 10  |  11 |  12 |
        '+-----+-----+-----+

        Dim c1 = table.Rows(1).Cells(1) 'Cell 5
        Dim c2 = table.Rows(4).Cells(1) 'Cell 8

        'Act
        table.MergeCells(c1, c2)

        'Assert
        Dim nav = table.Node.CreateNavigator()

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns)) '3 rows!
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:col)", ns)) '2 columns!

        Assert.IsTrue(table.TableIsBalanced()) 'Table SHOULD be balanced
        Assert.AreEqual("5" + ChrW(&HA0) + "6" + ChrW(&HA0) + "7" + ChrW(&HA0) + "8", table.Rows(1).Cells(1).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeWholeTable()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableWithRowSpan4x4(ns)


        '+-----+-----+-----+-----+
        '| (1) |  2  |  3  |  4  |
        '+-----|-----+-----+-----+
        '|  5  |           |  7  |    +---------------------------------+
        '+-----|     6     |-----+ => |  1 2 3 4 5 6 7 8 9 10 11 12 13  |
        '|  8  |           |  9  |    +---------------------------------+
        '+-----+-----+-----+-----+    
        '|  10 |  11 |  12 | (13)|
        '+-----+-----+-----+-----+

        Dim c1 = table.Rows(0).Cells(0) 'Cell 1
        Dim c2 = table.Rows(3).Cells(3) 'Cell 13

        'Act
        table.MergeCells(c1, c2)

        'Assert
        Dim nav = table.Node.CreateNavigator()

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td[1]/def:p)", ns)) '1 p tag 

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr)", ns)) '1 row!
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:col)", ns)) '1 column!

        Assert.IsTrue(table.TableIsBalanced()) 'Table SHOULD be balanced
        Assert.AreEqual("1" + ChrW(&HA0) + "2" + ChrW(&HA0) + "3" + ChrW(&HA0) + "4" + ChrW(&HA0) +
                        "5" + ChrW(&HA0) + "6" + ChrW(&HA0) + "7" + ChrW(&HA0) + "8" + ChrW(&HA0) +
                        "9" + ChrW(&HA0) + "10" + ChrW(&HA0) + "11" + ChrW(&HA0) + "12" + ChrW(&HA0) +
                        "13", table.Rows(0).Cells(0).InnerText)
    End Sub

End Class
