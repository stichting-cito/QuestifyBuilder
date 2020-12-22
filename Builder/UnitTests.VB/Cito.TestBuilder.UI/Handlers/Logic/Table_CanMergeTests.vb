
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class Table_CanMergeTests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub CanMergeSimpleTable()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t As Table = MyBase.GetTableSimple(ns)
        Dim c1 = t.Rows(0).Cells(0)
        Dim c2 = t.Rows(1).Cells(2)

        Dim result = t.CanMerge(c1, c2)

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub CanNotMergeTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t As Table = MyBase.GetTableProblem1(ns)
        Dim c1 = t.Rows(2).Cells(0)
        Dim c2 = t.Rows(2).Cells(2)

        Dim result = t.CanMerge(c1, c2)

        Assert.IsFalse(result)
    End Sub

End Class
