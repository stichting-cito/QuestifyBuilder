
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class TableBoundsTest
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DimensionTests1x1Cell()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableSimple(ns)
        Dim c = t.Rows(0).Cells(0)

        Dim bounds As New TableBounds(c)

        Assert.AreEqual(0, bounds.Left)
        Assert.AreEqual(0, bounds.Top)
        Assert.AreEqual(0, bounds.Right)
        Assert.AreEqual(0, bounds.Bottom)
        Assert.AreEqual(1, bounds.Rows)
        Assert.AreEqual(1, bounds.Columns)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DimensionTests1x1Cell7()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableSimple(ns)
        Dim c = t.Rows(1).Cells(2)

        Dim bounds As New TableBounds(c)

        Assert.AreEqual(2, bounds.Left)
        Assert.AreEqual(1, bounds.Top)
        Assert.AreEqual(2, bounds.Right)
        Assert.AreEqual(1, bounds.Bottom)
        Assert.AreEqual(1, bounds.Rows)
        Assert.AreEqual(1, bounds.Columns)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DimensionTests4x4()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)

        Dim c = t.Rows(1).Cells(1)

        Dim bounds As New TableBounds(c)

        Assert.AreEqual(1, bounds.Left)
        Assert.AreEqual(1, bounds.Top)
        Assert.AreEqual(2, bounds.Right)
        Assert.AreEqual(2, bounds.Bottom)
        Assert.AreEqual(2, bounds.Rows)
        Assert.AreEqual(2, bounds.Columns)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub ContainsTest()
        Dim bounds As New TableBounds(1, 1, 3, 1)
        Dim shouldContain As New TableBounds(2, 1)

        Dim result = bounds.Contains(shouldContain)

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DoesNotContainsTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)

        Dim c = t.Rows(1).Cells(1)
        Dim c2 = t.Rows(1).Cells(3)

        Dim result = (New TableBounds(c)).Contains(New TableBounds(c2))

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DoesNotContainsTest2()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)

        Dim c = t.Rows(2).Cells(0)
        Dim c2 = t.Rows(3).Cells(3)
        Dim c3 = t.Rows(1).Cells(1)

        Dim result = (New TableBounds(c2, c)).Contains(New TableBounds(c3))

        Assert.IsFalse(result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub IntersectTest()
        Dim bounds As New TableBounds(1, 3, 3, 1)
        Dim shouldIntersect As New TableBounds(2, 1, 1, 5)

        Dim result = bounds.Intersects(shouldIntersect)

        Assert.IsTrue(result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub IntersectTest2()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)

        Dim c = t.Rows(2).Cells(0)
        Dim c2 = t.Rows(3).Cells(3)
        Dim c3 = t.Rows(1).Cells(1)

        Dim result = (New TableBounds(c2, c)).Intersects(New TableBounds(c3))

        Assert.IsTrue(result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub TableBoundsWithRowSpanCell()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t As Table = MyBase.GetTableProblem1(ns)
        Dim c1 = t.Rows(2).Cells(0)
        Dim c2 = t.Rows(2).Cells(2)

        Dim result = New TableBounds(c1, c2)

        Assert.AreEqual(3, result.Columns)
        Assert.AreEqual(2, result.Rows)
    End Sub

End Class
