
Imports System.Xml
Imports System.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class SetCellBordeStyleHandler_SeperatedStrategyTests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetStyleToCellAssertStyleOnNodeIsCorrect()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)
        Dim strategy As New SetCellBordeStyleHandler_SeperatedStrategy(New TableBounds(0, 0))
        Dim cell = t.GetCellByCoords(0, 0)
       
        'Act
        strategy.SetLeft_BorderStyle(SolidStyle(1), cell, True)
        strategy.SetTop_BorderStyle(SolidStyle(1), cell, True)
        cell.ApplyStyles()
       
        'Assert
        Dim x = DirectCast(cell.Node, XmlElement)
        Dim s As String = x.Attributes("style").Value
        '"border-left: 1px Solid;border-top: 1px Solid;"
        Assert.IsTrue(s.Contains("border-left"))
        Assert.IsTrue(s.Contains("border-top"))
        Assert.AreEqual(2, s.ToCharArray().Count(Function(c) c = ";"c)) 'Two ";" found
    End Sub

    Friend Function SolidStyle(w As Integer) As CssBorder
        Return New CssBorder() With {.BorderStyle = "Solid", .Width = w}
    End Function

End Class
