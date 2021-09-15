
Imports System.Drawing
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class BorderCssStyleTest

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub GetColorFromBorderStyle()
        'Arrange
        Dim style = "background-color: #00FF00;"
        Dim border As New CssBorder("red")
        
        'Act
        Dim result = border.Color
        
        'Assert
        Assert.IsTrue(result.HasValue)
        Assert.AreEqual(Color.Red, result.Value)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub GetBorderWidth()
        'Arrange
        Dim border As New CssBorder("12px")
        
        'Act
        Dim result = border.Width
        
        'Assert
        Assert.IsTrue(result.HasValue)
        Assert.AreEqual(12, result.Value)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub GetBorderStyle()
        'Arrange
        Dim border As New CssBorder("solid")
        
        'Act
        Dim result = border.BorderStyle
        
        'Assert
        Assert.AreEqual("solid", result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub FeedResultAsInput()
        'Arrange
        Dim border As New CssBorder("solid #FF0000 12px")
        Dim str = border.ToString() 'Css StyleName is not part of ToString.
       
        'Act
        Dim newStyle = New CssBorder(str)
        
        'Assert
        Assert.AreEqual(Color.FromArgb(255, 0, 0), newStyle.Color)
        Assert.AreEqual(12, newStyle.Width)
        Assert.AreEqual("solid", newStyle.BorderStyle)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BordersAreEqual()
        'Arrange
        Dim border1 As New CssBorder("solid #000000 12px")
        Dim border2 As New CssBorder("black   12px   SolId  ")

        'Act
        Dim result = border1.Equals(border2)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BordersAreNotEqual()
        'Arrange
        Dim border1 As New CssBorder("solid #000000 12px")
        Dim border2 As New CssBorder("black      solid  ")

        'Act
        Dim result = border1.Equals(border2)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BordersNoValidBorder()
        'Arrange
        Dim border1 As New CssBorder(" #000000 12px")
        
        'Act
        Dim result = border1.hasBorderStyle()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BordersHasValidBorder()
        'Arrange
        Dim border1 As New CssBorder("3px solid red")
        
        'Act
        Dim result = border1.hasBorderStyle()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

End Class
