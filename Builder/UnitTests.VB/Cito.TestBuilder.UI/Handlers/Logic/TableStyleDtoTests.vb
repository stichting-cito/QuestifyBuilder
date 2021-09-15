
Imports System.Drawing
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class TableStyleDtoTests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleLeftSide()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
       
        'Act
        Dim result = dto.GetLeftVert(True) 'Borders = yes
       
        'Assert
        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Dashed.ToString(),
                                              .Width = 3,
                                              .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleMidVert_AsLeftSide()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
      
        'Act
        Dim result = dto.GetLeftVert(False) 'Borders = no
       
        'Assert
        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Solid.ToString(),
                                              .Width = 2,
                                              .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleMidVert_AsRightSide()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
       
        'Act
        Dim result = dto.GetRightVert(False) 'Borders = no
       
        'Assert
        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Solid.ToString(),
                                              .Width = 2,
                                              .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleMidVert_AsRightSide_shouldEqual_AsLeftSide()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
       
        'Act
        Dim resultRight = dto.GetRightVert(False) 'Borders = no
        Dim resultLeft = dto.GetLeftVert(False) 'Borders = no
     
        'Assert
        Assert.AreEqual(resultLeft, resultRight)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleRightSide()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
       
        'Act
        Dim result = dto.GetRightVert(True) 'Borders = yes
     
        'Assert
        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Double.ToString(),
                                              .Width = 13,
                                              .Color = Color.Black}, result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleTopSide()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
      
        'Act
        Dim result = dto.GetTopHorizontal(True) 'Borders = yes
      
        'Assert
        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Double.ToString(),
                                              .Width = 11,
                                              .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetMidHor_asTop()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
       
        'Act
        Dim result = dto.GetTopHorizontal(False) 'Borders = no
       
        'Assert
        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Solid.ToString(),
                                              .Width = 7,
                                              .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetMidHor_asBottom()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
       
        'Act
        Dim result = dto.GetBottomHorizontal(False) 'Borders = no
       
        'Assert
        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Solid.ToString(),
                                              .Width = 7,
                                              .Color = Color.Black}, result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetMidHor_asBottom_eqausl_asTop()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
      
        'Act
        Dim resultbottom = dto.GetBottomHorizontal(False) 'Borders = no
        Dim resulttop = dto.GetTopHorizontal(False) 'Borders = no
      
        'Assert
        Assert.AreEqual(resultbottom, resulttop)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetBottomSide()
        'Arrange
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()
      
        'Act
        Dim result = dto.GetBottomHorizontal(True) 'Borders = no
      
        'Assert
        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Dotted.ToString(),
                                              .Width = 5,
                                              .Color = Color.Black}, result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub CheckIfOuter_BoxAndInnerSameStyle_WhenIsNothing()
        'Arrange
        Dim dto As TableStyleDto = New TableStyleDto
      
        'Act
        Dim result = dto.BoxAndInnerSameStyle()
      
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub CheckIfOuter_BoxIsSameStyle_WhenIsNothing()
        'Arrange
        Dim dto As TableStyleDto = New TableStyleDto
     
        'Act
        Dim result = dto.BoxIsSameStyle()
     
        'Assert
        Assert.IsFalse(result)
    End Sub

    Private Function GetStyleDtoForIncludingMidSections() As TableStyleDto
        Dim ret As New TableStyleDto()
        ret.TopHorizontal = LineStyle.Double
        ret.TopHorizontalWidth = 11

        ret.HasMidHorizontal = True
        ret.MidHorizontal = LineStyle.Solid
        ret.MidHorizontalWidth = 7

        ret.BottomHorizontal = LineStyle.Dotted
        ret.BottomHorizontalWidth = 5

        ret.LeftVertical = LineStyle.Dashed
        ret.LeftVerticalWidth = 3

        ret.HasMidVertical = True
        ret.MidVertical = LineStyle.Solid
        ret.MidVerticalWidth = 2

        ret.RightVertical = LineStyle.Double
        ret.RightVerticalWidth = 13

        Return ret
    End Function

End Class
