
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class Table_HSplitTests

    Private Shared NewEmptyNode As String = ChrW(&HA0).ToString()

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal_4x3Table()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableSimple(ns)
        Dim c = t.Rows(1).Cells(1)

        c.SplitHorizontal()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(6.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[3]/def:td[1]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal_WithRowSpan_gt_1()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan(ns)
        Dim c = t.Rows(0).Cells(0)

        c.SplitHorizontal()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual("1", t.Node.SelectSingleNode("//def:tr[1]/def:td[1]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[1]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal2_WithRowSpan_gt_1()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan(ns)
        Dim c = t.Rows(1).Cells(2)

        c.SplitHorizontal()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[3]/def:td[3]", ns).InnerText)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal3x6_WithRowSpan_gt_3()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan3x6(ns)
        Dim c = t.Rows(1).Cells(0)

        c.SplitHorizontal()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(6.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[5]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[6]/def:td)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(6.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[5]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[5]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[6]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[6]/def:td/@rowspan)", ns))
        Assert.AreEqual("4", t.Node.SelectSingleNode("//def:tr[2]/def:td[1]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[4]/def:td[1]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal3x7_WithRowSpan_gt_3()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan3x7(ns)
        Dim c = t.Rows(1).Cells(2)


        c.SplitHorizontal()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(7.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[5]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[6]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[7]/def:td)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(8.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[5]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[5]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[6]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[6]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[7]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[7]/def:td/@rowspan)", ns))
        Assert.AreEqual("10", t.Node.SelectSingleNode("//def:tr[2]/def:td[3]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[5]/def:td[2]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontal4x4_2x2Cell()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan4x4(ns)
        Dim c = t.Rows(1).Cells(1)

        c.SplitHorizontal()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[3]/def:td[2]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellHorizontalWithColspan()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithColspan(ns)
        Dim c = t.Rows(0).Cells(1)

        c.SplitHorizontal()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        Assert.AreEqual("2", t.Node.SelectSingleNode("//def:tr[1]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[1]", ns).InnerText)
    End Sub

    Private Function GetTableSimple(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstData.ToString(), ns)
    End Function

    Private Function GetTableWithRowSpan(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan.ToString(), ns)
    End Function

    Private Function GetTableWithRowSpan3x6(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan3x6.ToString(), ns)
    End Function

    Private Function GetTableWithRowSpan3x7(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan3x7.ToString(), ns)
    End Function

    Private Function GetTableWithRowSpan4x4(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstData4x4.ToString(), ns)
    End Function

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
