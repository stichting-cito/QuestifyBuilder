
Imports System.Xml

<TestClass()>
Public Class TableCell_MergeTest
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeEmptyCellWithCellWithText()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)


        Dim c1 = t.Rows(0).Cells(0)
        Dim c2 = t.Rows(0).Cells(1)

        t.MergeCells(c1, c2)

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual("text", c1.InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub Merge2CellsWith3P()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)


        Dim c1 = t.Rows(0).Cells(2)
        Dim c2 = t.Rows(0).Cells(3)

        t.MergeCells(c1, c2)

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))

        nav = c1.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:p)", ns))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeAll()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)


        Dim c1 = t.Rows(0).Cells(0)
        Dim c2 = t.Rows(0).Cells(4)

        t.MergeCells(c1, c2)

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:p)", ns))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeCellsAndLoseObsoleteTr()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableProblem2(ns)


        Dim c1 = t.Rows(1).Cells(2)
        Dim c2 = t.Rows(2).Cells(2)

        t.MergeCells(c1, c2)

        Dim nav = t.Node.CreateNavigator()

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td[2]/def:p)", ns))

        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.IsTrue(t.TableIsBalanced())
        Assert.AreEqual("4", t.Rows(1).Cells(0).InnerText)
        Assert.AreEqual("4", t.Rows(1).Cells(1).InnerText)
        Assert.AreEqual("5" + ChrW(&HA0) + "6", t.Rows(1).Cells(2).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeCellsAndLoseObsoleteTrWithColAndRowSapn()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableProblem2(ns)


        Dim c1 = table.Rows(1).Cells(0)
        Dim c2 = table.Rows(2).Cells(2)

        table.MergeCells(c1, c2)

        Dim nav = table.Node.CreateNavigator()

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td[1]/def:p)", ns))

        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.IsTrue(table.TableIsBalanced())
        Assert.AreEqual("4" + ChrW(&HA0) + "5" + ChrW(&HA0) + "6", table.Rows(1).Cells(2).InnerText)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeCellsAndLoseObsoleteTdWithColAndRowSpan()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableProblem2(ns)


        Dim c1 = table.Rows(0).Cells(0)
        Dim c2 = table.Rows(1).Cells(0)

        table.MergeCells(c1, c2)

        Dim nav = table.Node.CreateNavigator()

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td[1]/def:p)", ns))

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:col)", ns))

        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.IsTrue(table.TableIsBalanced())
        Assert.AreEqual("1" + ChrW(&HA0) + "2" + ChrW(&HA0) + "4", table.Rows(0).Cells(0).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeAndRemoveMultipleTr()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableWithRowSpan3x6(ns)

        Dim c1 = table.Rows(1).Cells(1)
        Dim c2 = table.Rows(4).Cells(1)

        table.MergeCells(c1, c2)

        Dim nav = table.Node.CreateNavigator()

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:col)", ns))

        Assert.IsTrue(table.TableIsBalanced())
        Assert.AreEqual("5" + ChrW(&HA0) + "6" + ChrW(&HA0) + "7" + ChrW(&HA0) + "8", table.Rows(1).Cells(1).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub MergeWholeTable()
        Dim ns As XmlNamespaceManager = Nothing
        Dim table = MyBase.GetTableWithRowSpan4x4(ns)



        Dim c1 = table.Rows(0).Cells(0)
        Dim c2 = table.Rows(3).Cells(3)

        table.MergeCells(c1, c2)

        Dim nav = table.Node.CreateNavigator()

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td[1]/def:p)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:col)", ns))

        Assert.IsTrue(table.TableIsBalanced())
        Assert.AreEqual("1" + ChrW(&HA0) + "2" + ChrW(&HA0) + "3" + ChrW(&HA0) + "4" + ChrW(&HA0) +
                        "5" + ChrW(&HA0) + "6" + ChrW(&HA0) + "7" + ChrW(&HA0) + "8" + ChrW(&HA0) +
                        "9" + ChrW(&HA0) + "10" + ChrW(&HA0) + "11" + ChrW(&HA0) + "12" + ChrW(&HA0) +
                        "13", table.Rows(0).Cells(0).InnerText)
    End Sub

End Class
