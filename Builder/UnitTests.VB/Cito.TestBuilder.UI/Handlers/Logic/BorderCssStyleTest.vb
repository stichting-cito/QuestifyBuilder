
Imports System.Drawing
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class BorderCssStyleTest

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub GetColorFromBorderStyle()
        Dim style = "background-color: #00FF00;"
        Dim border As New CssBorder("red")

        Dim result = border.Color

        Assert.IsTrue(result.HasValue)
        Assert.AreEqual(Color.Red, result.Value)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub GetBorderWidth()
        Dim border As New CssBorder("12px")

        Dim result = border.Width

        Assert.IsTrue(result.HasValue)
        Assert.AreEqual(12, result.Value)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub GetBorderStyle()
        Dim border As New CssBorder("solid")

        Dim result = border.BorderStyle

        Assert.AreEqual("solid", result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub FeedResultAsInput()
        Dim border As New CssBorder("solid #FF0000 12px")
        Dim str = border.ToString()

        Dim newStyle = New CssBorder(str)

        Assert.AreEqual(Color.FromArgb(255, 0, 0), newStyle.Color)
        Assert.AreEqual(12, newStyle.Width)
        Assert.AreEqual("solid", newStyle.BorderStyle)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BordersAreEqual()
        Dim border1 As New CssBorder("solid #000000 12px")
        Dim border2 As New CssBorder("black   12px   SolId  ")

        Dim result = border1.Equals(border2)

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BordersAreNotEqual()
        Dim border1 As New CssBorder("solid #000000 12px")
        Dim border2 As New CssBorder("black      solid  ")

        Dim result = border1.Equals(border2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BordersNoValidBorder()
        Dim border1 As New CssBorder(" #000000 12px")

        Dim result = border1.hasBorderStyle()

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BordersHasValidBorder()
        Dim border1 As New CssBorder("3px solid red")

        Dim result = border1.hasBorderStyle()

        Assert.IsTrue(result)
    End Sub

End Class
