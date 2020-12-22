
Imports System.Drawing
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class TableStyleDtoTests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleLeftSide()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim result = dto.GetLeftVert(True)

        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Dashed.ToString(),
                                      .Width = 3,
                                      .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleMidVert_AsLeftSide()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim result = dto.GetLeftVert(False)

        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Solid.ToString(),
                                      .Width = 2,
                                      .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleMidVert_AsRightSide()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim result = dto.GetRightVert(False)

        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Solid.ToString(),
                                      .Width = 2,
                                      .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleMidVert_AsRightSide_shouldEqual_AsLeftSide()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim resultRight = dto.GetRightVert(False)
        Dim resultLeft = dto.GetLeftVert(False)

        Assert.AreEqual(resultLeft, resultRight)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleRightSide()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim result = dto.GetRightVert(True)

        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Double.ToString(),
                                      .Width = 13,
                                      .Color = Color.Black}, result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetStyleTopSide()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim result = dto.GetTopHorizontal(True)

        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Double.ToString(),
                                      .Width = 11,
                                      .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetMidHor_asTop()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim result = dto.GetTopHorizontal(False)

        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Solid.ToString(),
                                      .Width = 7,
                                      .Color = Color.Black}, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetMidHor_asBottom()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim result = dto.GetBottomHorizontal(False)

        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Solid.ToString(),
                                      .Width = 7,
                                      .Color = Color.Black}, result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetMidHor_asBottom_eqausl_asTop()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim resultbottom = dto.GetBottomHorizontal(False)
        Dim resulttop = dto.GetTopHorizontal(False)

        Assert.AreEqual(resultbottom, resulttop)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub WithMidGetVert_GetBottomSide()
        Dim dto As TableStyleDto = GetStyleDtoForIncludingMidSections()

        Dim result = dto.GetBottomHorizontal(True)

        Assert.AreEqual(New CssBorder() With {.BorderStyle = LineStyle.Dotted.ToString(),
                                      .Width = 5,
                                      .Color = Color.Black}, result)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub CheckIfOuter_BoxAndInnerSameStyle_WhenIsNothing()
        Dim dto As TableStyleDto = New TableStyleDto

        Dim result = dto.BoxAndInnerSameStyle()

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub CheckIfOuter_BoxIsSameStyle_WhenIsNothing()
        Dim dto As TableStyleDto = New TableStyleDto

        Dim result = dto.BoxIsSameStyle()

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
