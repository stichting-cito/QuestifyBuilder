Namespace Controls.Logic
    Friend Class DrawingMath

        Public Shared Function GetSmallerRect(rect As Rectangle, margin As Integer) As Rectangle
            Return Rectangle.FromLTRB(rect.Left + margin, rect.Top + margin, rect.Right - margin, rect.Bottom - margin)
        End Function

        Public Shared Function GetPoint(pointFrom As ContentAlignment, rect As Rectangle) As PointF
            Select Case pointFrom

                Case ContentAlignment.TopLeft
                    Return New PointF(rect.Left, rect.Top)
                Case ContentAlignment.MiddleLeft
                    Return New PointF(rect.Left, rect.Top + (rect.Height / 2.0F))
                Case ContentAlignment.BottomLeft
                    Return New PointF(rect.Left, rect.Bottom)

                Case ContentAlignment.TopCenter
                    Return New PointF(rect.Left + (rect.Width / 2.0F), rect.Top)
                Case ContentAlignment.MiddleCenter
                    Return New PointF(rect.Left + (rect.Width / 2.0F), rect.Top + (rect.Height / 2.0F))
                Case ContentAlignment.BottomCenter
                    Return New PointF(rect.Left + (rect.Width / 2.0F), rect.Bottom)

                Case ContentAlignment.TopRight
                    Return New PointF(rect.Right, rect.Top)
                Case ContentAlignment.MiddleRight
                    Return New PointF(rect.Right, rect.Top + (rect.Height / 2.0F))
                Case ContentAlignment.BottomRight
                    Return New PointF(rect.Right, rect.Bottom)
                Case Else
                    Throw New ArgumentException()
            End Select

        End Function

        Public Shared Function GetPoint(pointFrom As ContentAlignment, rect As Rectangle,
                                        offsetX As Integer, OffsetY As Integer) As PointF
            Dim p = GetPoint(pointFrom, rect)

            Return New PointF(p.X + offsetX, p.Y + OffsetY)
        End Function

        Shared Function CreateRect(p1 As PointF, p2 As PointF) As Rectangle
            Dim ret As New Rectangle(CInt(p1.X), CInt(p1.Y), CInt(Math.Abs(p2.X - p1.X)), CInt(Math.Abs(p2.Y - p1.Y)))
            Return ret
        End Function

        Shared Function CreateRect(p1 As PointF, p2 As PointF,
                           offsetLeft As Integer,
                           offsetTop As Integer,
                           offsetRight As Integer,
                           offsetBottom As Integer
                           ) As Rectangle
            Dim ret As New Rectangle(CInt(p1.X + offsetLeft), CInt(p1.Y + offsetTop),
                                     CInt(Math.Abs(p2.X - p1.X) - (offsetRight + offsetLeft)),
                                     CInt(Math.Abs(p2.Y - p1.Y) - (offsetBottom + offsetTop)))
            Return ret
        End Function

    End Class
End Namespace