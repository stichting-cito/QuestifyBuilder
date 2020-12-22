Imports Questify.Builder.Logic

Namespace Controls.Logic
    Friend Class PenFactory

        Private Shared _unknowStyleColor As Color = Color.FromArgb(160, 160, 160)

        Shared Function Create(linStyle As LineStyle?, width As Integer, lineColor As Color?) As Pen
            If (linStyle.HasValue) Then
                If (linStyle.Value = LineStyle.None OrElse linStyle.Value = LineStyle.Hidden) Then
                    Return New Pen(Color.Transparent, 1)
                End If

                Dim p = New Pen(If(lineColor, Color.Black), Math.Min(width, 6))
                Select Case linStyle.Value
                    Case LineStyle.Solid
                        p.DashStyle = Drawing2D.DashStyle.Solid
                    Case LineStyle.Dotted
                        p.DashStyle = Drawing2D.DashStyle.Dot
                    Case LineStyle.Dashed
                        p.DashStyle = Drawing2D.DashStyle.Dash
                    Case LineStyle.Double
                        p.CompoundArray = New Single() {0.0F, 0.4F, 0.6F, 1.0F}

                End Select
                Return p

            Else
                Return New Pen(_unknowStyleColor, 6)
            End If
        End Function

    End Class
End Namespace
