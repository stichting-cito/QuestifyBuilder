
Imports System.Xml

<TestClass()>
Public Class SetStyleToTableCellTests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub ApplyEmptyStyleToCell()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableForCellMerging(ns)
        Dim cell = t.GetCellByCoords(0, 0)

        cell.ApplyStyles()

        Dim x = DirectCast(cell.Node, XmlElement)
        Dim att = x.Attributes("style")
        Assert.IsNull(att)
        Assert.IsFalse(cell.Style.hasStyles)
    End Sub

End Class
