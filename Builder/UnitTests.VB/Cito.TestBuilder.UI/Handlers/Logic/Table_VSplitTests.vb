
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class Table_VSplitTests

    Private Shared NewEmptyNode As String = ChrW(&HA0).ToString()

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellVertical_4x3Table()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableSimple(ns)
        Dim c = t.Rows(1).Cells(1)

        c.SplitVertical()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(5.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[3]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellWithColumnspan_Gt_1()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithColspan(ns)
        Dim c = t.Rows(0).Cells(1)

        c.SplitVertical()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual("2", t.Node.SelectSingleNode("//def:tr[1]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[1]/def:td[3]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCell_InColumnCellWithColumnspan_Gt_1()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithColspan(ns)
        Dim c = t.Rows(1).Cells(2)

        c.SplitVertical()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(5.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[3]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[4]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellVertical4x4_2x2Cell()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan4x4(ns)
        Dim c = t.Rows(1).Cells(1)

        c.SplitVertical()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("count(//def:tr[4]/def:td)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[4]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[4]/def:td/@rowspan)", ns))

        Assert.AreEqual("6", t.Node.SelectSingleNode("//def:tr[2]/def:td[2]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[2]/def:td[3]", ns).InnerText)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SplitCellVerticalWithRowSpan()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = GetTableWithRowSpan(ns)
        Dim c = t.Rows(0).Cells(0)

        c.SplitVertical()

        Dim nav = t.Node.CreateNavigator()
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr)", ns))

        Assert.AreEqual(5.0, nav.Evaluate("count(//def:tr[1]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[2]/def:td)", ns))
        Assert.AreEqual(3.0, nav.Evaluate("count(//def:tr[3]/def:td)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[1]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("count(//def:tr[1]/def:td/@rowspan)", ns))
        Assert.AreEqual(4.0, nav.Evaluate("sum(//def:tr[1]/def:td/@rowspan)", ns))

        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[2]/def:td/@colspan)", ns))
        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[2]/def:td/@rowspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[2]/def:td/@rowspan)", ns))

        Assert.AreEqual(1.0, nav.Evaluate("count(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(2.0, nav.Evaluate("sum(//def:tr[3]/def:td/@colspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("count(//def:tr[3]/def:td/@rowspan)", ns))
        Assert.AreEqual(0.0, nav.Evaluate("sum(//def:tr[3]/def:td/@rowspan)", ns))

        Assert.AreEqual("1", t.Node.SelectSingleNode("//def:tr[1]/def:td[1]", ns).InnerText)
        Assert.AreEqual(NewEmptyNode, t.Node.SelectSingleNode("//def:tr[1]/def:td[2]", ns).InnerText)
    End Sub

    Private Function GetTableSimple(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstData.ToString(), ns)
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

    Private Function GetTableWithRowSpan4x4(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstData4x4.ToString(), ns)
    End Function

    Private Function GetTableWithRowSpan(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan.ToString(), ns)
    End Function

End Class
