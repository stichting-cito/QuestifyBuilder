
Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class LabeledCirclePainter
        Implements IDrawableItemPainter(Of ICircle)

        Private Sub Draw(g As Graphics, color As Color, shape As ICircle) Implements IDrawableItemPainter(Of ICircle).Draw
            SetMinimumSizeOfShape(shape)
            Using solidBrush As New SolidBrush(color)
                Using font As New Font("Arial", 12, FontStyle.Bold)
                    g.DrawString(If(shape.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(shape.Label), shape.Label, shape.ID), font, solidBrush, New Point(shape.AnchorPoint.X - CInt(font.Height / 2), shape.AnchorPoint.Y - CInt(font.Height / 2) - 6))
                End Using
                Dim bb = shape.BoundingBox
                Using fontReg As New Font("arial", 8, FontStyle.Regular)
                    Dim centerPoint = New Point(shape.AnchorPoint.X - CInt(fontReg.Height), shape.AnchorPoint.Y - CInt(fontReg.Height / 2) + 6)
                    g.DrawString(String.Format("Ø{0}{3}({1},{2}", shape.Radius, bb.Left, bb.Top, vbNewLine), fontReg, solidBrush, centerPoint)
                End Using
            End Using
            Using pen As New Pen(color)
                Dim brush As New SolidBrush(Color.FromArgb(125, 255, 255, 255))
                g.FillEllipse(brush, shape.BoundingBox)
                g.DrawEllipse(pen, shape.BoundingBox)
            End Using

        End Sub

        Private Sub SetMinimumSizeOfShape(ByRef shape As ICircle)
            Dim minimumSize As Integer = 13

            If shape.Radius < minimumSize Then
                shape.Radius = minimumSize
            End If
        End Sub
    End Class
End Namespace