Imports System.Drawing

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class CellDeterminator_seperated
        Implements ICellStyleDeterminator

        Friend Function GetStyleLeft(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?) Implements ICellStyleDeterminator.GetStyleLeft
            Dim style = cel.Style.BorderLeft
            Return GetStyle(style)
        End Function

        Friend Function GetStyleTop(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?) Implements ICellStyleDeterminator.GetStyleTop
            Dim style = cel.Style.BorderTop
            Return GetStyle(style)
        End Function

        Friend Function GetStyleRight(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?) Implements ICellStyleDeterminator.GetStyleRight
            Dim style = cel.Style.BorderRight
            Return GetStyle(style)
        End Function

        Friend Function GetStyleBottom(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?) Implements ICellStyleDeterminator.GetStyleBottom
            Dim style = cel.Style.BorderBottom
            Return GetStyle(style)
        End Function

        Private Function GetStyle(style As CssBorder) As Tuple(Of LineStyle, Integer, Color?)
            If ((style IsNot Nothing) AndAlso (Not String.IsNullOrEmpty(style.BorderStyle))) Then
                Dim ls = DirectCast([Enum].Parse(GetType(LineStyle), style.BorderStyle, True), LineStyle)
                Dim w = If(style.Width, 0)
                Dim c = If(style.Color, Color.Black)

                Return New Tuple(Of LineStyle, Integer, Color?)(ls, w, c)
            End If

            Return New Tuple(Of LineStyle, Integer, Color?)(LineStyle.None, 0, Color.Black)
        End Function
    End Class

End Namespace