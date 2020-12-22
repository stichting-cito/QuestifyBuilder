Imports System.Drawing

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class CellStyleDeterminator_collapse
        Implements ICellStyleDeterminator


        Friend Function GetStyleLeft(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?) Implements ICellStyleDeterminator.GetStyleLeft
            Dim style = cel.Style.BorderLeft
            Dim otherStyle = GetCellLeft(cel)
            Dim subResult = DetermineStyle(style, otherStyle, False)
            Return GetStyle(subResult)
        End Function

        Friend Function GetStyleTop(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?) Implements ICellStyleDeterminator.GetStyleTop
            Dim style = cel.Style.BorderTop
            Dim otherStyle = GetCellAbove(cel)
            Dim subResult = DetermineStyle(style, otherStyle, False)
            Return GetStyle(subResult)

        End Function

        Friend Function GetStyleRight(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?) Implements ICellStyleDeterminator.GetStyleRight
            Dim style = cel.Style.BorderRight
            Dim otherStyle = GetCellRight(cel)
            Dim subResult = DetermineStyle(style, otherStyle, False)
            Return GetStyle(subResult)
        End Function

        Friend Function GetStyleBottom(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?) Implements ICellStyleDeterminator.GetStyleBottom
            Dim style = cel.Style.BorderBottom
            Dim otherStyle = GetCellBelow(cel)
            Dim subResult = DetermineStyle(style, otherStyle, False)
            Return GetStyle(subResult)
        End Function


        Private Function GetStyle(style As CssBorder) As Tuple(Of LineStyle, Integer, Color?)
            If ((style IsNot Nothing) AndAlso (Not String.IsNullOrEmpty(style.BorderStyle))) Then
                Dim ls = DirectCast([Enum].Parse(GetType(LineStyle), style.BorderStyle, True), LineStyle)
                Dim w = If(style.Width, 0)
                Dim c = If(style.Color, Color.Black)

                Return New Tuple(Of LineStyle, Integer, Color?)(ls, w, c)
            End If

            Return New Tuple(Of LineStyle, Integer, Color?)(LineStyle.Hidden, 0, Color.Black)
        End Function



        Friend Function DetermineStyle(ownStyle As CssBorder, adjecentStyle As CssBorder, OwnStyleIsTopOrLeftMost As Boolean) As CssBorder
            If ((adjecentStyle Is Nothing) OrElse (Not adjecentStyle.hasBorderStyle)) Then Return ownStyle
            If ((ownStyle Is Nothing) OrElse (Not ownStyle.hasBorderStyle)) Then Return adjecentStyle
            Dim eq As Integer = DetermineByWidth(ownStyle, adjecentStyle)
            If (eq <> 0) Then
                Return If(eq > 0, ownStyle, adjecentStyle)
            Else
                Dim eq2 = DertermineByBorderStyle(ownStyle, adjecentStyle)
                If (eq2 <> 0) Then
                    Return If(eq2 > 0, ownStyle, adjecentStyle)
                Else
                    Return If(OwnStyleIsTopOrLeftMost, ownStyle, adjecentStyle)
                End If
            End If
        End Function

        Private Function DetermineByWidth(ownStyle As CssBorder, adjecentStyle As CssBorder) As Integer
            Return ownStyle.Width.GetValueOrDefault().CompareTo(adjecentStyle.Width.GetValueOrDefault())
        End Function

        Private Function DertermineByBorderStyle(ownStyle As CssBorder, adjecentStyle As CssBorder) As Integer
            Debug.Assert(ownStyle.hasBorderStyle AndAlso adjecentStyle.hasBorderStyle)
            Dim bsOwn As Integer = DirectCast([Enum].Parse(GetType(LineStyle), ownStyle.BorderStyle, True), Integer)
            Dim bsother As Integer = DirectCast([Enum].Parse(GetType(LineStyle), adjecentStyle.BorderStyle, True), Integer)
            Return bsOwn.CompareTo(bsother)
        End Function



        Private Function GetCellLeft(cel As TableCell) As CssBorder
            Dim x = cel.ColNumber - 1
            Dim y = cel.RowNumber
            Dim c = GetAdjentcCel(x, y, cel)
            If (c IsNot Nothing) Then
                Debug.Assert(Not Object.ReferenceEquals(c, cel))
                Return c.Style.BorderRight
            End If
            Return Nothing
        End Function

        Private Function GetCellRight(cel As TableCell) As CssBorder
            Dim x = cel.ColNumber + cel.ColSpan
            Dim y = cel.RowNumber
            Dim c = GetAdjentcCel(x, y, cel)
            If (c IsNot Nothing) Then
                Debug.Assert(Not Object.ReferenceEquals(c, cel))
                Return c.Style.BorderLeft
            End If
            Return Nothing
        End Function

        Private Function GetCellAbove(cel As TableCell) As CssBorder
            Dim x = cel.ColNumber
            Dim y = cel.RowNumber - 1
            Dim c = GetAdjentcCel(x, y, cel)
            If (c IsNot Nothing) Then
                Debug.Assert(Not Object.ReferenceEquals(c, cel))
                Return c.Style.BorderBottom
            End If
            Return Nothing
        End Function

        Private Function GetCellBelow(cel As TableCell) As CssBorder
            Dim x = cel.ColNumber
            Dim y = cel.RowNumber + cel.RowSpan
            Dim c = GetAdjentcCel(x, y, cel)
            If (c IsNot Nothing) Then
                Debug.Assert(Not Object.ReferenceEquals(c, cel))
                Return c.Style.BorderTop
            End If

            Return Nothing
        End Function

        Private Function GetAdjentcCel(x As Integer, y As Integer, cel As TableCell) As TableCell
            Dim t = cel.Table
            If (x >= 0) AndAlso (x < t.GetColumnCount) AndAlso (y >= 0) AndAlso (y < t.GetRowCount) Then
                Return t.GetCellByCoords(x, y)
            End If
            Return Nothing
        End Function

    End Class

End Namespace