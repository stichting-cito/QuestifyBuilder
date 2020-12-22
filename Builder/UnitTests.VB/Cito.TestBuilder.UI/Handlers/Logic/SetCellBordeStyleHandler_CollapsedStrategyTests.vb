
Imports FakeItEasy
Imports System.Drawing
Imports System.Diagnostics
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class SetCellBordeStyleHandler_CollapsedStrategyTests
    Inherits baseFakedTable


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetLeftStyle_ToTopLeftCell()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(0, 0))
        Dim t = GetFakeTable() : SetUpForEachCellSolidBorder(t)
        Dim cell = t.GetCellByCoords(0, 0)

        strategy.SetLeft_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetTopStyle_ToTopLeftCell()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(0, 0))
        Dim t = GetFakeTable() : SetUpForEachCellSolidBorder(t)
        Dim cell = t.GetCellByCoords(0, 0)

        strategy.SetTop_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetRightStyle_ToBottomRightCell()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(0, 0))
        Dim t = GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(t)
        Dim cell = t.GetCellByCoords(0, 0)

        strategy.SetRight_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetBottomtStyle_ToBottomRightCell()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(0, 0))
        Dim t = GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(t)
        Dim cell = t.GetCellByCoords(0, 0)

        strategy.SetBottom_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub




    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetLeftStyle_To2x2CellInMiddle()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) cellsMade.Add(New Point(x, y), Nothing), MiddleIs2x2())

        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetLeft_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(0, 1)).Style.BorderRight)
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(0, 2)).Style.BorderRight)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetTopStyle_To2x2CellInMiddle()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) cellsMade.Add(New Point(x, y), Nothing), MiddleIs2x2())
        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetTop_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(1, 0)).Style.BorderBottom)
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(2, 0)).Style.BorderBottom)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetRightStyle_To2x2CellInMiddle()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) cellsMade.Add(New Point(x, y), Nothing), MiddleIs2x2())
        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetRight_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(3, 1)).Style.BorderLeft)
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(3, 2)).Style.BorderLeft)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetBottomtStyle_To2x2CellInMiddle()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) cellsMade.Add(New Point(x, y), Nothing), MiddleIs2x2())
        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetBottom_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))

        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(1, 3)).Style.BorderTop)
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(2, 3)).Style.BorderTop)

        A.CallTo(Sub() cellsMade(New Point(1, 3)).ApplyStyles()).MustHaveHappened()
        A.CallTo(Sub() cellsMade(New Point(2, 3)).ApplyStyles()).MustHaveHappened()
    End Sub




    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetLeftStyle_To2x2CellInMiddle_adjancentCellhasRowSpan2()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs2x2LeftRight1x2TopBottom4x1())

        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetLeft_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(2, cellsMade.Count)

        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(0, 1)).Style.BorderRight)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetTopStyle_To2x2CellInMiddle_adjancentCellhasColspan4()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs2x2LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetTop_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(0, 0)).Style.BorderBottom)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetRightStyle_To2x2CellInMiddle_adjancentCellhasRowSpan2()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs2x2LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetRight_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(2, cellsMade.Count)
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(3, 1)).Style.BorderLeft)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetBottomtStyle_To2x2CellInMiddle_adjancentCellhasColspan4()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs2x2LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetBottom_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(0, 3)).Style.BorderTop)
    End Sub




    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetLeftStyle_To1x1CellInMiddle_adjancentCellhasRowSpan2()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs1x1LeftRight1x2TopBottom4x1())

        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetLeft_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        Assert.AreEqual(2, cellsMade.Count)
        Assert.AreEqual(BlackSolid1px(), cellsMade(New Point(0, 1)).Style.BorderRight)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetTopStyle_To1x1CellInMiddle_adjancentCellhasColspan4()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs1x1LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(1, 1)

        strategy.SetTop_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(0, 0)).Style.BorderBottom)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetRightStyle_To1x1CellInMiddle_adjancentCellhasRowSpan2()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs1x1LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(2, 1)

        strategy.SetRight_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        Assert.AreEqual(2, cellsMade.Count)
        Assert.AreEqual(BlackSolid1px(), cellsMade(New Point(3, 1)).Style.BorderLeft)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetBottomtStyle_To1x1CellInMiddle_adjancentCellhasColspan4()
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs1x1LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(2, 2)

        strategy.SetBottom_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)

        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(0, 3)).Style.BorderTop)
    End Sub


    Private Function MiddleIs2x2() As Func(Of Integer, Integer, Dictionary(Of Point, TableCell), TableCell, TableCell)
        Return Function(x, y, cellMade, tc)
                   If (x = 1 AndAlso y = 1) Then
                       tc.ColSpan = 2
                       tc.RowSpan = 2
                   End If
                   cellMade(New Point(x, y)) = tc
                   Return tc
               End Function
    End Function

    Private Function MiddleIs2x2LeftRight1x2TopBottom4x1() As Func(Of Integer, Integer, Dictionary(Of Point, TableCell), TableCell, TableCell)
        Return Function(x, y, cellMade, tc)
                   Dim ret As TableCell = Nothing
                   If (x = 1 AndAlso y = 1) Then
                       tc.ColSpan = 2
                       tc.RowSpan = 2
                       cellMade.Add(New Point(1, 1), tc)
                       ret = tc
                   End If

                   If (x = 0 Or x = 3) AndAlso (y = 1 Or y = 2) Then
                       Dim key = New Point(x, 1)
                       If Not cellMade.ContainsKey(key) Then
                           tc.RowSpan = 2
                           tc.RowNumber = 1
                           cellMade.Add(key, tc)
                       End If
                       ret = cellMade(key)
                   End If

                   If (y = 0 Or y = 3) Then
                       Dim key = New Point(0, y)
                       If Not cellMade.ContainsKey(key) Then
                           tc.ColSpan = 4
                           tc.ColNumber = 0
                           cellMade.Add(key, tc)
                       End If
                       ret = cellMade(key)
                   End If

                   Debug.Assert(ret IsNot Nothing)
                   Return ret
               End Function
    End Function

    Private Function MiddleIs1x1LeftRight1x2TopBottom4x1() As Func(Of Integer, Integer, Dictionary(Of Point, TableCell), TableCell, TableCell)
        Return Function(x, y, cellMade, tc)
                   Dim ret As TableCell = Nothing
                   If (x = 1 AndAlso y = 1) Then
                       cellMade.Add(New Point(x, y), tc)
                       ret = tc
                   End If
                   If (x = 1 AndAlso y = 2) Then
                       cellMade.Add(New Point(x, y), tc)
                       ret = tc
                   End If
                   If (x = 2 AndAlso y = 1) Then
                       cellMade.Add(New Point(x, y), tc)
                       ret = tc
                   End If
                   If (x = 2 AndAlso y = 2) Then
                       cellMade.Add(New Point(x, y), tc)
                       ret = tc
                   End If

                   If (x = 0 Or x = 3) AndAlso (y = 1 Or y = 2) Then
                       Dim key = New Point(x, 1)
                       If Not cellMade.ContainsKey(key) Then
                           tc.RowSpan = 2
                           tc.RowNumber = 1
                           cellMade.Add(key, tc)
                       End If
                       ret = cellMade(key)
                   End If

                   If (y = 0 Or y = 3) Then
                       Dim key = New Point(0, y)
                       If Not cellMade.ContainsKey(key) Then
                           tc.ColSpan = 4
                           tc.ColNumber = 0
                           cellMade.Add(key, tc)
                       End If
                       ret = cellMade(key)
                   End If

                   Debug.Assert(ret IsNot Nothing)
                   Return ret
               End Function
    End Function

    Sub DoNothing()
    End Sub


End Class
