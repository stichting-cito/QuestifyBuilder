Imports System.Drawing

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class TableStyleDeterminator

        Private _table As Table
        Private _bounds As TableBounds
        Private _c As ICellStyleDeterminator

        Public Shared Function GetStyle(table As Table, bounds As TableBounds) As TableStyleDto
            Dim r = New TableStyleDeterminator(table, bounds)
            Return r.DetermineStyle()
        End Function


        Public Sub New(table As Table, bounds As TableBounds)
            _table = table
            _bounds = bounds
            If (table.IsCollapsedStyle()) Then
                _c = New CellStyleDeterminator_collapse
            Else
                _c = New CellDeterminator_seperated
            End If
        End Sub


        Friend Function DetermineStyle() As TableStyleDto
            Dim handled As New HashSet(Of TableCell)
            Dim ret As New TableStyleDto
            Dim setColor, setLeft, setTop, setRight, setBottom, setMidHor, setMidVert As Boolean

            For r = _bounds.Top To _bounds.Bottom
                For c = _bounds.Left To _bounds.Right
                    Dim cell = _table.GetCellByCoords(c, r)
                    If Not handled.Contains(cell) Then
                        Dim bordersLeft = cell.ColNumber = _bounds.Left
                        Dim bordersTop = cell.RowNumber = _bounds.Top
                        Dim bordersRight = cell.ColNumber + (cell.ColSpan - 1) = _bounds.Right
                        Dim bordersBottom = cell.RowNumber + (cell.RowSpan - 1) = _bounds.Bottom

                        DetermineColor(cell, ret, setColor)

                        If bordersLeft Then
                            DetermineLeftStyle(cell, ret, setLeft)
                        Else
                            DetermineMidVertical(cell, ret, setMidVert, True)
                        End If

                        If bordersRight Then
                            DetermineRightStyle(cell, ret, setRight)
                        Else
                            DetermineMidVertical(cell, ret, setMidVert, False)
                        End If
                        If bordersTop Then
                            DetermineTopStyle(cell, ret, setTop)
                        Else
                            DetermineMidHorizontal(cell, ret, setMidHor, True)
                        End If

                        If bordersBottom Then
                            DetermineBottomStyle(cell, ret, setBottom)
                        Else
                            DetermineMidHorizontal(cell, ret, setMidHor, False)
                        End If

                        handled.Add(cell)
                    End If
                Next
            Next
            Return ret
        End Function


        Private Sub DetermineLeftStyle(cel As TableCell, ByRef ret As TableStyleDto, ByRef setLeft As Boolean)
            If setLeft Then
                If ret.LeftVertical.HasValue Then
                    Dim toEvaluateWith = _c.GetStyleLeft(cel)
                    Dim result As Tuple(Of LineStyle?, Integer, Color?) = EvaluateStyle(ret.LeftVertical, ret.LeftVerticalWidth, ret.LineColor, toEvaluateWith)
                    ret.LeftVertical = result.Item1
                    ret.LeftVerticalWidth = If(result.Item2 = 0, Nothing, result.Item2)
                    ret.LineColor = If(result.Item3, Color.Black)
                Else
                End If
            Else
                Dim current = _c.GetStyleLeft(cel)
                ret.LeftVertical = current.Item1
                ret.LeftVerticalWidth = current.Item2
                ret.LineColor = current.Item3
                setLeft = True
            End If
        End Sub



        Private Sub DetermineTopStyle(cel As TableCell, ByRef ret As TableStyleDto, ByRef setTop As Boolean)

            If setTop Then
                If ret.TopHorizontal.HasValue Then
                    Dim toEvaluateWith = _c.GetStyleTop(cel)
                    Dim result As Tuple(Of LineStyle?, Integer, Color?) = EvaluateStyle(ret.TopHorizontal, ret.TopHorizontalWidth, ret.LineColor, toEvaluateWith)
                    ret.TopHorizontal = result.Item1
                    ret.TopHorizontalWidth = If(result.Item2 = 0, Nothing, result.Item2)
                    ret.LineColor = If(result.Item3, Color.Black)
                Else
                End If
            Else
                Dim current = _c.GetStyleTop(cel)
                ret.TopHorizontal = current.Item1
                ret.TopHorizontalWidth = current.Item2
                ret.LineColor = current.Item3
                setTop = True
            End If
        End Sub

        Private Sub DetermineRightStyle(cel As TableCell, ByRef ret As TableStyleDto, ByRef setRight As Boolean)

            If setRight Then
                If ret.RightVertical.HasValue Then
                    Dim toEvaluateWith = _c.GetStyleRight(cel)
                    Dim result As Tuple(Of LineStyle?, Integer, Color?) = EvaluateStyle(ret.RightVertical, ret.RightVerticalWidth, ret.LineColor, toEvaluateWith)
                    ret.RightVertical = result.Item1
                    ret.RightVerticalWidth = If(result.Item2 = 0, Nothing, result.Item2)
                    ret.LineColor = If(result.Item3, Color.Black)
                Else
                End If
            Else
                Dim current = _c.GetStyleRight(cel)
                ret.RightVertical = current.Item1
                ret.RightVerticalWidth = current.Item2
                ret.LineColor = current.Item3
                setRight = True
            End If
        End Sub

        Private Sub DetermineBottomStyle(cel As TableCell, ByRef ret As TableStyleDto, ByRef setBottom As Boolean)

            If setBottom Then
                If ret.BottomHorizontal.HasValue Then
                    Dim toEvaluateWith = _c.GetStyleBottom(cel)
                    Dim result As Tuple(Of LineStyle?, Integer, Color?) = EvaluateStyle(ret.BottomHorizontal, ret.BottomHorizontalWidth, ret.LineColor, toEvaluateWith)
                    ret.BottomHorizontal = result.Item1
                    ret.BottomHorizontalWidth = If(result.Item2 = 0, Nothing, result.Item2)
                    ret.LineColor = If(result.Item3, Color.Black)
                Else
                End If
            Else
                Dim current = _c.GetStyleBottom(cel)
                ret.BottomHorizontal = current.Item1
                ret.BottomHorizontalWidth = current.Item2
                ret.LineColor = current.Item3
                setBottom = True
            End If
        End Sub


        Private Sub DetermineMidVertical(cell As TableCell, ByRef ret As TableStyleDto, ByRef setMidVert As Boolean, forLeftCellBorder As Boolean)

            If (setMidVert) Then
                If (ret.MidVertical.HasValue) Then
                    Dim toEvaluateWith = If(forLeftCellBorder, _c.GetStyleLeft(cell), _c.GetStyleRight(cell))
                    Dim result As Tuple(Of LineStyle?, Integer, Color?) = EvaluateStyle(ret.MidVertical, ret.MidVerticalWidth, ret.LineColor, toEvaluateWith)
                    ret.MidVertical = result.Item1
                    ret.MidVerticalWidth = If(result.Item2 = 0, Nothing, result.Item2)
                    ret.LineColor = If(result.Item3, Color.Black)
                Else
                End If
            Else
                Dim current = If(forLeftCellBorder, _c.GetStyleLeft(cell), _c.GetStyleRight(cell))
                ret.MidVertical = current.Item1
                ret.MidVerticalWidth = current.Item2
                ret.LineColor = current.Item3
                ret.HasMidVertical = True
                setMidVert = True
            End If
        End Sub

        Private Sub DetermineMidHorizontal(cell As TableCell, ByRef ret As TableStyleDto, ByRef setMidVert As Boolean, forTopCellBorder As Boolean)
            If (setMidVert) Then
                If (ret.MidHorizontal.HasValue) Then
                    Dim toEvaluateWith = If(forTopCellBorder, _c.GetStyleTop(cell), _c.GetStyleBottom(cell))
                    Dim result As Tuple(Of LineStyle?, Integer, Color?) = EvaluateStyle(ret.MidHorizontal, ret.MidHorizontalWidth, ret.LineColor, toEvaluateWith)
                    ret.MidHorizontal = result.Item1
                    ret.MidHorizontalWidth = If(result.Item2 = 0, Nothing, result.Item2)
                    ret.LineColor = If(result.Item3, Color.Black)
                Else
                End If
            Else
                Dim current = If(forTopCellBorder, _c.GetStyleTop(cell), _c.GetStyleBottom(cell))
                ret.MidHorizontal = current.Item1
                ret.MidHorizontalWidth = current.Item2
                ret.LineColor = current.Item3
                ret.HasMidHorizontal = True
                setMidVert = True
            End If
        End Sub

        Private Function EvaluateStyle(currentLineStyle As LineStyle?, currentLineWidth As Integer, currentLineColor As Color?, toEvaluateWith As Tuple(Of LineStyle, Integer, Color?)) As Tuple(Of LineStyle?, Integer, Color?)
            Dim retLineStle As LineStyle? = If(Not currentLineStyle.HasValue, Nothing, If(toEvaluateWith.Item1 = currentLineStyle, currentLineStyle, Nothing))
            Dim retLineW As Integer = If(Not currentLineStyle.HasValue, -1, If(toEvaluateWith.Item2 = currentLineWidth, currentLineWidth, -1))
            Dim retLineColor As Color? = If(Not currentLineStyle.HasValue, Color.Black, If(toEvaluateWith.Item3 = currentLineColor, currentLineColor, Color.Black))
            Return New Tuple(Of LineStyle?, Integer, Color?)(retLineStle, retLineW, retLineColor)
        End Function

        Private Sub DetermineColor(cell As TableCell, ByRef ret As TableStyleDto, ByRef setLeft As Boolean)
            If setLeft Then
                If cell.Style.Background_color.HasValue AndAlso ret.BackColor.HasValue Then
                    Dim c As Color = cell.Style.Background_color.Value
                    If (ret.BackColor.Value.R = c.R AndAlso
                        ret.BackColor.Value.G = c.G AndAlso
                        ret.BackColor.Value.B = c.B) Then
                    Else
                        ret.BackColor = Nothing
                    End If
                Else
                    ret.BackColor = Nothing
                End If
            Else
                If cell.Style.Background_color.HasValue Then
                    Dim c As Color = cell.Style.Background_color.Value
                    ret.BackColor = Color.FromArgb(c.R, c.G, c.B)
                Else
                    ret.BackColor = Nothing
                End If

                setLeft = True
            End If
        End Sub


    End Class
End Namespace