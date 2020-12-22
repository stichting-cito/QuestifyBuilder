
Imports System.Xml
Imports System.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class SetCellBordeStyleHandler_SeperatedStrategyTests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetStyleToCellAssertStyleOnNodeIsCorrect()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)
        Dim strategy As New SetCellBordeStyleHandler_SeperatedStrategy(New TableBounds(0, 0))
        Dim cell = t.GetCellByCoords(0, 0)

        strategy.SetLeft_BorderStyle(SolidStyle(1), cell, True)
        strategy.SetTop_BorderStyle(SolidStyle(1), cell, True)
        cell.ApplyStyles()

        Dim x = DirectCast(cell.Node, XmlElement)
        Dim s As String = x.Attributes("style").Value
        Assert.IsTrue(s.Contains("border-left"))
        Assert.IsTrue(s.Contains("border-top"))
        Assert.AreEqual(2, s.ToCharArray().Count(Function(c) c = ";"c))
    End Sub

    Friend Function SolidStyle(w As Integer) As CssBorder
        Return New CssBorder() With {.BorderStyle = "Solid", .Width = w}
    End Function

End Class
