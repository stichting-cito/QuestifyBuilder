Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

Public Class baseTableStyleTest

    Friend Function BoxEquals(tableStyleDto As TableStyleDto, lineStyle As LineStyle, width As Integer) As Boolean
        Return (tableStyleDto.LeftVertical.Value = lineStyle) AndAlso
             (tableStyleDto.TopHorizontal.Value = lineStyle) AndAlso
             (tableStyleDto.RightVertical.Value = lineStyle) AndAlso
              (tableStyleDto.BottomHorizontal.Value = lineStyle) AndAlso
              (tableStyleDto.LeftVerticalWidth = width) AndAlso
              (tableStyleDto.TopHorizontalWidth = width) AndAlso
              (tableStyleDto.RightVerticalWidth = width) AndAlso
              (tableStyleDto.BottomHorizontalWidth = width)
    End Function

    Friend Function InnerEquals(tableStyleDto As TableStyleDto, lineStyle As LineStyle, width As Integer) As Boolean
        Dim ret As Boolean = False
        If (tableStyleDto.HasMidHorizontal) Then
            ret = tableStyleDto.MidHorizontal.HasValue AndAlso (tableStyleDto.MidHorizontal.Value = lineStyle) AndAlso (tableStyleDto.MidHorizontalWidth = width)
        End If
        If (tableStyleDto.HasMidVertical) Then
            ret = tableStyleDto.MidVertical.HasValue AndAlso (tableStyleDto.MidVertical.Value = lineStyle) AndAlso (tableStyleDto.MidVerticalWidth = width)
        End If
        Return ret
    End Function

End Class
