Imports System.Linq

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class SetCellBorderStyleHandler_CollapsedStrategy
        Implements ISetTableBorderStyleStrategy

        Private _bounds As TableBounds

        Sub New(bounds As TableBounds)
            _bounds = bounds
        End Sub



        Public Sub SetLeft_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean) Implements ISetTableBorderStyleStrategy.SetLeft_BorderStyle
            forCell.Style.BorderLeft = SetCorrectStyle(style)
            Dim lst As List(Of AdjacentCell) = GetAdjacentCellsVert(forCell, True)
            If lst.Any() Then
                If forCell.Style.BorderLeft Is Nothing OrElse String.Equals(forCell.Style.BorderLeft.BorderStyle, LineStyle.Hidden.ToString, StringComparison.InvariantCultureIgnoreCase) Then
                    ClearAdjacentBorderStyles(lst, WhenVerticalBoundsInsideBounds, forCell)
                Else
                    SetAdjacentBorderStyles(lst, WhenVerticalBoundsInsideBounds, style, forCell)
                End If
            End If
        End Sub

        Public Sub SetTop_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean) Implements ISetTableBorderStyleStrategy.SetTop_BorderStyle
            forCell.Style.BorderTop = SetCorrectStyle(style)
            Dim lst As List(Of AdjacentCell) = GetAdjacentCellsHor(forCell, True)
            If lst.Any() Then
                If forCell.Style.BorderTop Is Nothing OrElse String.Equals(forCell.Style.BorderTop.BorderStyle, LineStyle.Hidden.ToString, StringComparison.InvariantCultureIgnoreCase) Then
                    ClearAdjacentBorderStyles(lst, WhenHorizontalBoundsInsideBounds, forCell)
                Else
                    SetAdjacentBorderStyles(lst, WhenHorizontalBoundsInsideBounds, style, forCell)
                End If
            End If
        End Sub

        Public Sub SetRight_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean) Implements ISetTableBorderStyleStrategy.SetRight_BorderStyle
            forCell.Style.BorderRight = SetCorrectStyle(style)
            Dim lst As List(Of AdjacentCell) = GetAdjacentCellsVert(forCell, False)
            If lst.Any() Then
                If forCell.Style.BorderRight Is Nothing OrElse String.Equals(forCell.Style.BorderRight.BorderStyle, LineStyle.Hidden.ToString, StringComparison.InvariantCultureIgnoreCase) Then
                    ClearAdjacentBorderStyles(lst, WhenVerticalBoundsInsideBounds, forCell)
                Else
                    SetAdjacentBorderStyles(lst, WhenVerticalBoundsInsideBounds, style, forCell)
                End If
            End If
        End Sub

        Public Sub SetBottom_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean) Implements ISetTableBorderStyleStrategy.SetBottom_BorderStyle
            forCell.Style.BorderBottom = SetCorrectStyle(style)
            Dim lst As List(Of AdjacentCell) = GetAdjacentCellsHor(forCell, False)
            If lst.Any() Then
                If forCell.Style.BorderBottom Is Nothing OrElse String.Equals(forCell.Style.BorderBottom.BorderStyle, LineStyle.Hidden.ToString, StringComparison.InvariantCultureIgnoreCase) Then
                    ClearAdjacentBorderStyles(lst, WhenHorizontalBoundsInsideBounds, forCell)
                Else
                    SetAdjacentBorderStyles(lst, WhenHorizontalBoundsInsideBounds, style, forCell)
                End If
            End If
        End Sub

        Public Sub SetBackgroundColor(color As System.Drawing.Color?, forCell As TableCell) Implements ISetTableBorderStyleStrategy.SetBackgroundColor
            forCell.Style.Background_color = color
        End Sub



        Private Function GetAdjacentCellsVert(cell As TableCell, leftSide As Boolean) As List(Of AdjacentCell)
            Dim handled = New HashSet(Of AdjacentCell)

            Dim ret As New List(Of AdjacentCell)
            Dim max = cell.Table.GetColumnCount
            Dim x = cell.ColNumber + If(leftSide, -1, cell.ColSpan)
            If (x >= 0 AndAlso x < max) Then

                For y = cell.RowNumber To (cell.RowSpan + cell.RowNumber - 1)
                    Dim c = cell.Table.GetCellByCoords(x, y)
                    Dim ac = If(leftSide, AdjacentCell.MakeLeft(c), AdjacentCell.MakeRight(c))

                    If Not handled.Contains(ac) Then
                        ret.Add(ac) : handled.Add(ac)
                    End If
                Next

            End If
            Return ret
        End Function

        Private Function GetAdjacentCellsHor(cell As TableCell, topSide As Boolean) As List(Of AdjacentCell)
            Dim handled = New HashSet(Of AdjacentCell)
            Dim ret As New List(Of AdjacentCell)
            Dim max = cell.Table.GetRowCount
            Dim y = cell.RowNumber + If(topSide, -1, cell.RowSpan)
            If (y >= 0 AndAlso y < max) Then

                For x = cell.ColNumber To (cell.ColSpan + cell.ColNumber - 1)
                    Dim c = cell.Table.GetCellByCoords(x, y)
                    Dim ac = If(topSide, AdjacentCell.MakeTop(c), AdjacentCell.MakeBottom(c))
                    If Not handled.Contains(ac) Then
                        ret.Add(ac) : handled.Add(ac)
                    End If
                Next

            End If
            Return ret
        End Function

        Private Sub SetAdjacentBorderStyles(lst As List(Of AdjacentCell), canZero As Func(Of TableCell, AdjacentCell, Boolean), style As CssBorder, forCell As TableCell)
            For Each adjc In lst
                If canZero(forCell, adjc) Then
                    adjc.AdjacentBorder = style
                    adjc.Apply()
                End If
            Next
        End Sub

        Private Sub ClearAdjacentBorderStyles(lst As List(Of AdjacentCell), canZero As Func(Of TableCell, AdjacentCell, Boolean), forCell As TableCell)
            For Each adjc In lst
                If canZero(forCell, adjc) Then
                    adjc.AdjacentBorder = Nothing
                    adjc.Apply()
                End If
            Next
        End Sub

        Private Function WhenHorizontalBoundsInsideBounds() As Func(Of TableCell, AdjacentCell, Boolean)
            Return Function(cell, adjc) As Boolean
                       Return checkRange(cell.ColNumber, cell.ColNumber + cell.ColSpan - 1,
                                         adjc.ColNumber, adjc.ColNumber + adjc.ColSpan - 1)
                   End Function
        End Function

        Private Function WhenVerticalBoundsInsideBounds() As Func(Of TableCell, AdjacentCell, Boolean)
            Return Function(cell, adjc) As Boolean
                       Return checkRange(cell.RowNumber, cell.RowNumber + cell.RowSpan - 1, adjc.RowNumber, adjc.RowNumber + adjc.RowSpan - 1)
                   End Function
        End Function

        Function checkRange(a1 As Integer, a2 As Integer, b1 As Integer, b2 As Integer) As Boolean
            Return (b1 >= a1) AndAlso (b2 <= a2)
        End Function

        Private Function SetCorrectStyle(style As CssBorder) As CssBorder
            If String.Equals(style.BorderStyle, LineStyle.None.ToString(), StringComparison.InvariantCultureIgnoreCase) Then style.BorderStyle = LineStyle.Hidden.ToString()
            Return style
        End Function



        Private Class AdjacentCell

            Private ReadOnly wrappedCell As TableCell
            Private accessor As Func(Of TableCell, CssBorder)
            Private setter As Action(Of CssBorder, TableCell)

            Public Shared Function MakeLeft(cell As TableCell) As AdjacentCell
                Return New AdjacentCell(cell) With {.accessor = AccessorLeftCell(), .setter = SetterLeftCell()}
            End Function

            Public Shared Function MakeRight(cell As TableCell) As AdjacentCell
                Return New AdjacentCell(cell) With {.accessor = AccessorRightCell(), .setter = SetterRightCell()}
            End Function

            Public Shared Function MakeTop(cell As TableCell) As AdjacentCell
                Return New AdjacentCell(cell) With {.accessor = AccessorTopCell(), .setter = SetterTopCell()}
            End Function

            Public Shared Function MakeBottom(cell As TableCell) As AdjacentCell
                Return New AdjacentCell(cell) With {.accessor = AccessorBottomCell(), .setter = SetterBottomCell()}
            End Function



            Shared Function AccessorLeftCell() As Func(Of TableCell, CssBorder)
                Return Function(c As TableCell) As CssBorder
                           Return c.Style.BorderRight
                       End Function
            End Function

            Shared Function SetterLeftCell() As Action(Of CssBorder, TableCell)
                Return Sub(b, tc)
                           tc.Style.BorderRight = b
                       End Sub
            End Function

            Shared Function AccessorRightCell() As Func(Of TableCell, CssBorder)
                Return Function(c As TableCell) As CssBorder
                           Return c.Style.BorderLeft
                       End Function
            End Function

            Shared Function SetterRightCell() As Action(Of CssBorder, TableCell)
                Return Sub(b, tc)
                           tc.Style.BorderLeft = b
                       End Sub
            End Function

            Shared Function AccessorTopCell() As Func(Of TableCell, CssBorder)
                Return Function(c As TableCell) As CssBorder
                           Return c.Style.BorderBottom
                       End Function
            End Function

            Shared Function SetterTopCell() As Action(Of CssBorder, TableCell)
                Return Sub(b, tc)
                           tc.Style.BorderBottom = b
                       End Sub
            End Function

            Shared Function AccessorBottomCell() As Func(Of TableCell, CssBorder)
                Return Function(c As TableCell) As CssBorder
                           Return c.Style.BorderTop
                       End Function
            End Function

            Shared Function SetterBottomCell() As Action(Of CssBorder, TableCell)
                Return Sub(b, tc)
                           tc.Style.BorderTop = b
                       End Sub
            End Function


            Private Sub New()
            End Sub

            Private Sub New(cell As TableCell)
                wrappedCell = cell
            End Sub

            Public Property AdjacentBorder As CssBorder
                Get
                    Return accessor(wrappedCell)
                End Get
                Set(value As CssBorder)
                    setter(value, wrappedCell)
                End Set
            End Property

            Public ReadOnly Property ColNumber As Integer
                Get
                    Return wrappedCell.ColNumber
                End Get
            End Property

            Public ReadOnly Property RowNumber As Integer
                Get
                    Return wrappedCell.RowNumber
                End Get
            End Property

            Public ReadOnly Property ColSpan As Integer
                Get
                    Return wrappedCell.ColSpan
                End Get
            End Property

            Public ReadOnly Property RowSpan As Integer
                Get
                    Return wrappedCell.RowSpan
                End Get
            End Property

            Sub Apply()
                wrappedCell.ApplyStyles()
            End Sub

        End Class


    End Class

End Namespace