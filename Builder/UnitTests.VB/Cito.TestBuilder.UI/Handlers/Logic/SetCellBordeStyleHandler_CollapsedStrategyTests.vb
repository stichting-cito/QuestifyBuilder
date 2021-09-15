
Imports FakeItEasy
Imports System.Drawing
Imports System.Diagnostics
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class SetCellBordeStyleHandler_CollapsedStrategyTests
    Inherits baseFakedTable

#Region "SingleCellTable Tests"

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetLeftStyle_ToTopLeftCell()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(0, 0))
        Dim t = GetFakeTable() : SetUpForEachCellSolidBorder(t)
        Dim cell = t.GetCellByCoords(0, 0)
        
        'Act
        strategy.SetLeft_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
       
        'Assert
        'It should have detected it can not see further to the top.
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetTopStyle_ToTopLeftCell()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(0, 0))
        Dim t = GetFakeTable() : SetUpForEachCellSolidBorder(t)
        Dim cell = t.GetCellByCoords(0, 0)
        
        'Act
        strategy.SetTop_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
       
        'Assert
        'It should have detected it can not see further to the left.
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetRightStyle_ToBottomRightCell()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(0, 0))
        Dim t = GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(t)
        Dim cell = t.GetCellByCoords(0, 0)
       
        'Act
        strategy.SetRight_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
       
        'Assert
        'It should have detected it can not see further to the right.
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetBottomtStyle_ToBottomRightCell()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(0, 0))
        Dim t = GetFakeTable(1, 1) : SetUpForEachCellSolidBorder(t)
        Dim cell = t.GetCellByCoords(0, 0)
      
        'Act
        strategy.SetBottom_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
     
        'Assert
        'It should have detected it can not see further to the bottom.
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

#End Region

#Region "Set Style to cell with col-/Rowspan 2 Tests, adjacent are 1x1"

    'We set style to 
    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----+-----+-----+
    '|  5  |           |  7  |
    '+-----|     6     |-----+
    '|  8  |           |  9  |
    '+-----+-----+-----+-----+
    '|  10 |  11 |  12 |  13 |
    '+-----+-----+-----+-----+

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetLeftStyle_To2x2CellInMiddle()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) cellsMade.Add(New Point(x, y), Nothing), MiddleIs2x2())

        Dim cell = t.GetCellByCoords(1, 1)
        
        'Act
        strategy.SetLeft_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
       
        'Assert
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(0, 1)).Style.BorderRight) 'Cell 5 right border was set to nothing.
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(0, 2)).Style.BorderRight) 'Cell 8 right border was set to nothing.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetTopStyle_To2x2CellInMiddle()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) cellsMade.Add(New Point(x, y), Nothing), MiddleIs2x2())
        Dim cell = t.GetCellByCoords(1, 1)
       
        'Act
        strategy.SetTop_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
       
        'Assert
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(1, 0)).Style.BorderBottom) 'Cell 2 bottom border was set to nothing.
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(2, 0)).Style.BorderBottom) 'Cell 3 bottom border was set to nothing.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetRightStyle_To2x2CellInMiddle()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) cellsMade.Add(New Point(x, y), Nothing), MiddleIs2x2())
        Dim cell = t.GetCellByCoords(1, 1)
        
        'Act
        strategy.SetRight_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
        
        'Assert
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(3, 1)).Style.BorderLeft) 'Cell 7 left border was set to nothing.
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(3, 2)).Style.BorderLeft) 'Cell 9 left border was set to nothing.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetBottomtStyle_To2x2CellInMiddle()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) cellsMade.Add(New Point(x, y), Nothing), MiddleIs2x2())
        Dim cell = t.GetCellByCoords(1, 1)
        
        'Act
        strategy.SetBottom_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
        
        'Assert
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))

        'Nothing changed width MARKER_DASHED_RED_3PX since adjacent cells borders will get the same value to
        'prevent a gray line being shown as border within the editor:
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(1, 3)).Style.BorderTop) 'Cell 11 top border was set to nothing.
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(2, 3)).Style.BorderTop) 'Cell 12 top border was set to nothing.

        A.CallTo(Sub() cellsMade(New Point(1, 3)).ApplyStyles()).MustHaveHappened() 'Style had to be updated.
        A.CallTo(Sub() cellsMade(New Point(2, 3)).ApplyStyles()).MustHaveHappened()
    End Sub

#End Region

#Region "Set Style to cell with col-/Rowspan 2 Tests, adjacent are 1x2 vertical or 4x1 horizontal"

    'We set style to 
    '+-----+-----+-----+-----+
    '|           1           |
    '+-----+-----+-----+-----+
    '|     |           |     |
    '+  2  |   Target  |  3  +
    '|     |           |     |
    '+-----+-----+-----+-----+
    '|           4           |
    '+-----+-----+-----+-----+

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetLeftStyle_To2x2CellInMiddle_adjancentCellhasRowSpan2()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs2x2LeftRight1x2TopBottom4x1())

        Dim cell = t.GetCellByCoords(1, 1)
        
        'Act
        strategy.SetLeft_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
        
        'Assert
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(2, cellsMade.Count)

        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(0, 1)).Style.BorderRight) 'Cell 2 right border was set to same style as target.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetTopStyle_To2x2CellInMiddle_adjancentCellhasColspan4()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs2x2LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(1, 1)
        
        'Act
        strategy.SetTop_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
        
        'Assert
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(0, 0)).Style.BorderBottom) 'Cell 1 bottom border was set to nothing.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetRightStyle_To2x2CellInMiddle_adjancentCellhasRowSpan2()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs2x2LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(1, 1)
       
        'Act
        strategy.SetRight_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
      
        'Assert
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(2, cellsMade.Count)
        Assert.AreEqual(MARKER_DASHED_RED_3PX(), cellsMade(New Point(3, 1)).Style.BorderLeft) 'Cell 3 left border was set to same as target
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetBottomtStyle_To2x2CellInMiddle_adjancentCellhasColspan4()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs2x2LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(1, 1)
        
        'Act
        strategy.SetBottom_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
       
        'Assert
        A.CallTo(Function() t.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(3))
        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(0, 3)).Style.BorderTop) 'Cell 4 top border was set to nothing.
    End Sub

#End Region

#Region "Set Style to cell with col-/Rowspan 1 Tests, adjacent are 1x2 vertical or 4x1 horizontal"

    'We set style to 
    '+-----+-----+-----+-----+
    '|           1           |
    '+-----+-----+-----+-----+
    '|     |   X |  X  |     |
    '+  2  +-----+-----|  3  +
    '|     |   X |  X  |     |
    '+-----+-----+-----+-----+
    '|           4           |
    '+-----+-----+-----+-----+

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetLeftStyle_To1x1CellInMiddle_adjancentCellhasRowSpan2()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs1x1LeftRight1x2TopBottom4x1())

        Dim cell = t.GetCellByCoords(1, 1)
        
        'Act
        strategy.SetLeft_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
        
        'Assert        
        Assert.AreEqual(2, cellsMade.Count)
        'Since span is larger than cell border is being set for, the current border remains:
        Assert.AreEqual(BlackSolid1px(), cellsMade(New Point(0, 1)).Style.BorderRight)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetTopStyle_To1x1CellInMiddle_adjancentCellhasColspan4()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs1x1LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(1, 1)
       
        'Act
        strategy.SetTop_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
        
        'Assert
        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(0, 0)).Style.BorderBottom) 'Cell 1 bottom border was set to nothing.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetRightStyle_To1x1CellInMiddle_adjancentCellhasRowSpan2()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs1x1LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(2, 1)
        
        'Act
        strategy.SetRight_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
        
        'Assert
        Assert.AreEqual(2, cellsMade.Count)
        Assert.AreEqual(BlackSolid1px(), cellsMade(New Point(3, 1)).Style.BorderLeft) 'Cell 3 left border was left as is, since it is larger than the targetCell
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub SetBottomtStyle_To1x1CellInMiddle_adjancentCellhasColspan4()
        'Arrange
        Dim strategy As New SetCellBorderStyleHandler_CollapsedStrategy(New TableBounds(1, 1, 2, 2))
        Dim cellsMade As New Dictionary(Of Point, TableCell)
        Dim t = GetFakeTable(4, 4) : SetUpEachSideAndSizeIsOverridable(t, cellsMade, Sub(style, x, y) DoNothing(), MiddleIs1x1LeftRight1x2TopBottom4x1())
        Dim cell = t.GetCellByCoords(2, 2)
        
        'Act
        strategy.SetBottom_BorderStyle(MARKER_DASHED_RED_3PX(), cell, True)
       
        'Assert
        Assert.AreEqual(2, cellsMade.Count)
        Assert.IsNotNull(cellsMade(New Point(0, 3)).Style.BorderTop) 'Cell 4 top border was set to nothing.
    End Sub

#End Region

    Private Function MiddleIs2x2() As Func(Of Integer, Integer, Dictionary(Of Point, TableCell), TableCell, TableCell)
        Return Function(x, y, cellMade, tc)
                   If (x = 1 AndAlso y = 1) Then 'This is cell 6!
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
                   If (x = 1 AndAlso y = 1) Then 'This is cell 6!
                       tc.ColSpan = 2
                       tc.RowSpan = 2
                       cellMade.Add(New Point(1, 1), tc)
                       ret = tc
                   End If

                   'Left & right
                   If (x = 0 Or x = 3) AndAlso (y = 1 Or y = 2) Then
                       Dim key = New Point(x, 1)
                       If Not cellMade.ContainsKey(key) Then
                           tc.RowSpan = 2
                           tc.RowNumber = 1
                           cellMade.Add(key, tc)
                       End If
                       ret = cellMade(key)
                   End If

                   'Top & bottom
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

                   'Left & right
                   If (x = 0 Or x = 3) AndAlso (y = 1 Or y = 2) Then
                       Dim key = New Point(x, 1)
                       If Not cellMade.ContainsKey(key) Then
                           tc.RowSpan = 2
                           tc.RowNumber = 1
                           cellMade.Add(key, tc)
                       End If
                       ret = cellMade(key)
                   End If

                   'Top & bottom
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
        'Does NOTHING.
    End Sub


End Class
