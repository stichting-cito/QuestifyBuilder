
Imports System.Drawing
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class CssStyleTest

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub GetBackgroundColorByName()
        'Arrange
        Dim style = "background-color: red"
        Dim css As New CssStyleList(style)
        
        'Act
        Dim result = css.Background_color
       
        'Assert
        Assert.IsTrue(result.HasValue)
        Assert.AreEqual(Color.Red, result.Value)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub GetBackgroundColorByHexCode()
        'Arrange
        Dim style = "background-color: #00FF00;"
        Dim css As New CssStyleList(style)
       
        'Act
        Dim result = css.Background_color
       
        'Assert
        Assert.IsTrue(result.HasValue)
        Assert.AreEqual(Color.FromArgb(0, 255, 0).R, result.Value.R)
        Assert.AreEqual(Color.FromArgb(0, 255, 0).G, result.Value.G)
        Assert.AreEqual(Color.FromArgb(0, 255, 0).B, result.Value.B)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub FeedLeftBorderStyle_LeftBorderStyleIsSet()
        'Arrange
        Dim style = "background-color: #00FF00; border-left: green dashed 37px" 'Note the extra space between ";" and "border"
        Dim css As New CssStyleList(style)
        
        'Act
        Dim result = css.BorderLeft
       
        'Assert
        Assert.IsNotNull(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub FeedLeftBorderStyle_TopBorderStyleIsNothing()
        'Arrange
        Dim style = "background-color: #00FF00;border-left: green dashed 37px"
        Dim css As New CssStyleList(style)
        
        'Act
        Dim result = css.BorderTop
        
        'Assert
        Assert.IsNull(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub SetBotomStyle_StyleIsRepresentedInToString()
        'Arrange
        Dim css As New CssStyleList("")
        Dim border = New CssBorder("")
        border.Width = 23
        border.BorderStyle = "dashed"
        css.BorderBottom = border
        
        'Act
        Dim result = css.ToString()
        
        'Assert
        Assert.IsTrue(result.Contains("dashed"))
        Assert.IsTrue(result.Contains("23px"))
        Assert.IsTrue(result.Contains("border-bottom"))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BorderStyleIsCaseInsensetiveTest()
        'Arrange
        Dim style = "background-color: #00ff00; border-left: green dashed 37px"
        Dim styleUpper = style.ToUpper()
        
        'Act
        Dim cssLower As New CssStyleList(style)
        Dim cssUpper As New CssStyleList(styleUpper)
        
        'Assert
        Assert.AreEqual(cssLower.BorderLeft.Color.Value.R, cssUpper.BorderLeft.Color.Value.R)
        Assert.AreEqual(cssLower.BorderLeft.Color.Value.G, cssUpper.BorderLeft.Color.Value.G)
        Assert.AreEqual(cssLower.BorderLeft.Color.Value.B, cssUpper.BorderLeft.Color.Value.B)

        Assert.AreEqual(cssLower.BorderLeft.Width.Value, cssLower.BorderLeft.Width.Value)

        Assert.AreEqual(cssLower.BorderLeft.BorderStyle, cssLower.BorderLeft.BorderStyle)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BorderCollapsedStyle_separate_Test()
        'Arrange
        Dim style = "border-collapse : sepAratE"
        
        'Act
        Dim styleLst As New CssStyleList(style)
        
        'Assert
        Assert.AreEqual(True, styleLst.BorderCollapse.isCollapsed.HasValue)
        Assert.AreEqual(False, styleLst.BorderCollapse.isCollapsed.Value)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")>
    Public Sub BorderCollapsedStyle_collapse_Test()
        'Arrange
        'style="border-collapse :  separate ; border-collapse:collapse"
        Dim style = "border-collapse : colLaPse ;"
        
        'Act
        Dim styleLst As New CssStyleList(style)
        
        'Assert
        Assert.AreEqual(True, styleLst.BorderCollapse.isCollapsed.HasValue)
        Assert.AreEqual(True, styleLst.BorderCollapse.isCollapsed.Value)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("CssStyle")> <WorkItem(10319)> <WorkItem(10320)>
    Public Sub BorderCollapsedStyle_collapse_extra_space_Test()
        'Arrange
        'style="border-collapse :  separate ; border-collapse:collapse"
        Dim style = "border-collapse : colLaPse ; "
        
        'Act
        Dim styleLst As New CssStyleList(style)
        
        'Assert
        Assert.AreEqual(True, styleLst.BorderCollapse.isCollapsed.HasValue)
        Assert.AreEqual(True, styleLst.BorderCollapse.isCollapsed.Value)
    End Sub

End Class
