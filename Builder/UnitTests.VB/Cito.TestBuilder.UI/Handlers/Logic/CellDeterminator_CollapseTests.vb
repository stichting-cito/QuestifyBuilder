
Imports System.Drawing
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass>
Public Class CellDeterminator_CollapseTests
    Inherits baseFakedTable


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleWhenBothStylesAreNothing()
        Dim style1 As CssBorder = Nothing
        Dim style2 As CssBorder = Nothing
        Dim determinator As New CellStyleDeterminator_collapse

        Dim result = determinator.DetermineStyle(style1, style2, False)

        Assert.IsNull(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleWhenOwnStyleIsNothing()
        Dim ownStyle As CssBorder = Nothing
        Dim adjenctStyle = BlackSolid1px()
        Dim determinator As New CellStyleDeterminator_collapse

        Dim result = determinator.DetermineStyle(ownStyle, adjenctStyle, False)

        Assert.AreSame(adjenctStyle, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleWhenAdjenctStyleIsNothing()
        Dim ownStyle = BlackSolid1px()
        Dim adjenctStyle As CssBorder = Nothing
        Dim determinator As New CellStyleDeterminator_collapse

        Dim result = determinator.DetermineStyle(ownStyle, adjenctStyle, False)

        Assert.AreSame(ownStyle, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleWhenOwnWithIsThickest()
        Dim ownStyle = New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Black, .Width = 2}
        Dim adjenctStyle = New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Black, .Width = 1}
        Dim determinator As New CellStyleDeterminator_collapse

        Dim result = determinator.DetermineStyle(ownStyle, adjenctStyle, False)

        Assert.AreSame(ownStyle, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleWhenOtherWithIsThickest()
        Dim ownStyle = New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Black, .Width = 1}
        Dim adjenctStyle = New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Black, .Width = 2}
        Dim determinator As New CellStyleDeterminator_collapse

        Dim result = determinator.DetermineStyle(ownStyle, adjenctStyle, False)

        Assert.AreSame(adjenctStyle, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AdjectBorderstyleISMorePresent_experctsAdjectStyle()
        Dim ownStyle = New CssBorder() With {.BorderStyle = "dashed", .Color = Drawing.Color.Black, .Width = 2}
        Dim adjenctStyle = New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Black, .Width = 2}
        Dim determinator As New CellStyleDeterminator_collapse

        Dim result = determinator.DetermineStyle(ownStyle, adjenctStyle, False)

        Assert.AreSame(adjenctStyle, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AdjectBorderstyleISDoubleMorePresent_experctsAdjectStyle()
        Dim ownStyle = New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Black, .Width = 2}
        Dim adjenctStyle = New CssBorder() With {.BorderStyle = "double", .Color = Drawing.Color.Black, .Width = 2}
        Dim determinator As New CellStyleDeterminator_collapse

        Dim result = determinator.DetermineStyle(ownStyle, adjenctStyle, False)

        Assert.AreSame(adjenctStyle, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub OwnBorderstyleISMorePresent_experctsAdjectStyle()
        Dim ownStyle = New CssBorder() With {.BorderStyle = "solid", .Color = Drawing.Color.Black, .Width = 2}
        Dim adjenctStyle = New CssBorder() With {.BorderStyle = "dashed", .Color = Drawing.Color.Black, .Width = 2}
        Dim determinator As New CellStyleDeterminator_collapse

        Dim result = determinator.DetermineStyle(ownStyle, adjenctStyle, False)

        Assert.AreSame(ownStyle, result)
    End Sub



    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOf3x3_LeftBorder_MustReturnMarker()
        Dim tabel = MyBase.GetFakeTable(3, 3)
        MyBase.SetUpEachSideIsOverridable(tabel, Sub(style, x, y)
                                                     If (x = 1) AndAlso (y = 1) Then style.BorderLeft = BlackDashed1px()
                                                     If (x = 0) AndAlso (y = 1) Then style.BorderRight = MARKER_DASHED_RED_3PX()
                                                 End Sub)
        Dim cell = tabel.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleLeft(cell)

        Assert.AreEqual(3, result.Item2)
        Assert.AreEqual(LineStyle.Dashed, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOf3x3_LeftBorder_MustReturSolid1px()
        Dim tabel = MyBase.GetFakeTable(3, 3)
        MyBase.SetUpEachSideIsOverridable(tabel, Sub(style, x, y)
                                                     If (x = 1) AndAlso (y = 1) Then style.BorderLeft = BlackSolid1px()
                                                     If (x = 0) AndAlso (y = 1) Then style.BorderRight = BlackDashed1px()
                                                 End Sub)
        Dim cell = tabel.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleLeft(cell)

        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOf3x3_RightBorder_MustReturnMarker()
        Dim tabel = MyBase.GetFakeTable(3, 3)
        MyBase.SetUpEachSideIsOverridable(tabel, Sub(style, x, y)
                                                     If (x = 1) AndAlso (y = 1) Then style.BorderRight = BlackDashed1px()
                                                     If (x = 2) AndAlso (y = 1) Then style.BorderLeft = MARKER_DASHED_RED_3PX()
                                                 End Sub)
        Dim cell = tabel.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleRight(cell)

        Assert.AreEqual(3, result.Item2)
        Assert.AreEqual(LineStyle.Dashed, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOf3x3_RightBorder_MustReturSolid1px()
        Dim tabel = MyBase.GetFakeTable(3, 3)
        MyBase.SetUpEachSideIsOverridable(tabel, Sub(style, x, y)
                                                     If (x = 1) AndAlso (y = 1) Then style.BorderRight = BlackSolid1px()
                                                     If (x = 2) AndAlso (y = 1) Then style.BorderLeft = BlackDashed1px()
                                                 End Sub)
        Dim cell = tabel.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleRight(cell)

        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOf3x3_TopBorder_MustReturnMarker()
        Dim tabel = MyBase.GetFakeTable(3, 3)
        MyBase.SetUpEachSideIsOverridable(tabel, Sub(style, x, y)
                                                     If (x = 1) AndAlso (y = 0) Then style.BorderBottom = MARKER_DASHED_RED_3PX()
                                                     If (x = 1) AndAlso (y = 1) Then style.BorderTop = BlackDashed1px()
                                                 End Sub)
        Dim cell = tabel.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleTop(cell)

        Assert.AreEqual(3, result.Item2)
        Assert.AreEqual(LineStyle.Dashed, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOf3x3_TopBorder_MustReturSolid1px()
        Dim tabel = MyBase.GetFakeTable(3, 3)
        MyBase.SetUpEachSideIsOverridable(tabel, Sub(style, x, y)
                                                     If (x = 1) AndAlso (y = 0) Then style.BorderBottom = BlackSolid1px()
                                                     If (x = 1) AndAlso (y = 1) Then style.BorderTop = BlackDashed1px()
                                                 End Sub)

        Dim cell = tabel.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleTop(cell)

        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOf3x3_BottomBorder_MustReturnMarker()
        Dim tabel = MyBase.GetFakeTable(3, 3)
        MyBase.SetUpEachSideIsOverridable(tabel, Sub(style, x, y)
                                                     If (x = 1) AndAlso (y = 1) Then style.BorderBottom = BlackSolid1px()
                                                     If (x = 1) AndAlso (y = 2) Then style.BorderTop = MARKER_DASHED_RED_3PX()
                                                 End Sub)
        Dim cell = tabel.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleBottom(cell)

        Assert.AreEqual(3, result.Item2)
        Assert.AreEqual(LineStyle.Dashed, result.Item1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineStyleOf3x3_BottomBorder_MustReturSolid1px()
        Dim tabel = MyBase.GetFakeTable(3, 3)
        MyBase.SetUpEachSideIsOverridable(tabel, Sub(style, x, y)
                                                     If (x = 1) AndAlso (y = 1) Then style.BorderBottom = BlackSolid1px()
                                                     If (x = 1) AndAlso (y = 2) Then style.BorderTop = BlackDashed1px()
                                                 End Sub)
        Dim cell = tabel.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleBottom(cell)

        Assert.AreEqual(1, result.Item2)
        Assert.AreEqual(LineStyle.Solid, result.Item1)
    End Sub



    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineRightSideOfCellWithColspan()
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4)
        SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y)
                                                            If (x = 1 AndAlso y = 1) Then
                                                                style.BorderLeft = BlackSolid1px()
                                                                style.BorderTop = Nothing
                                                                style.BorderRight = Nothing
                                                                style.BorderBottom = Nothing
                                                            Else
                                                                style.BorderLeft = Nothing
                                                                style.BorderTop = Nothing
                                                                style.BorderRight = Nothing
                                                                style.BorderBottom = Nothing
                                                            End If
                                                        End Sub,
                                                        MiddleIs2x2())
        Dim cell = t.GetCellByCoords(1, 1)
        Dim sd As New CellStyleDeterminator_collapse()

        Dim result = sd.GetStyleRight(cell)

        Assert.AreEqual(0, result.Item2)
        Assert.AreEqual(LineStyle.Hidden, result.Item1)
        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(1, 1)))
        Assert.IsNotNull(cellsMade(New Point(3, 1)))
    End Sub

    Private Function MiddleIs2x2() As Func(Of Integer, Integer, Dictionary(Of Point, TableCell), TableCell, TableCell)
        Return Function(x, y, cellMade, tc)
                   If ((x = 1 Or x = 2) AndAlso (y = 1 Or y = 2)) Then
                       If Not (cellMade.ContainsKey(New Point(1, 1))) Then
                           tc.ColSpan = 2
                           tc.RowSpan = 2
                           cellMade.Add(New Point(1, 1), tc)
                       End If
                       Return cellMade(New Point(1, 1))
                   End If
                   If (cellMade.ContainsKey(New Point(x, y))) Then
                       Return cellMade(New Point(x, y))
                   End If
                   cellMade.Add(New Point(x, y), tc)
                   Return tc
               End Function
    End Function


End Class
