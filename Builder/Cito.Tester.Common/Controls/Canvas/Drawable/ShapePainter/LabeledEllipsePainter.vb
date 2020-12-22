
Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class LabeledEllipsePainter
        Implements IDrawableItemPainter(Of ICircle)

        Private Sub Draw(g As Graphics, color As Color, shape As ICircle) Implements IDrawableItemPainter(Of ICircle).Draw
            Dim bb = shape.BoundingBox
            Using pen As New Pen(color)
                Using solidBrush As New SolidBrush(color)
                    Using font As New Font("arial", 12, FontStyle.Bold)
                        Dim centerPoint = New Point(shape.AnchorPoint.X - CInt(font.Height / 2), shape.AnchorPoint.Y - CInt(font.Height / 2))
                        g.DrawString(If(shape.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(shape.Label), shape.Label, shape.ID), font, solidBrush, centerPoint)
                        g.DrawString($"({bb.Left},{bb.Top})", font, solidBrush, New Point(centerPoint.X, centerPoint.Y - 35))
                    End Using
                    Dim brush As New SolidBrush(Color.FromArgb(125, 255, 255, 255))
                    g.FillEllipse(brush, shape.BoundingBox)
                    g.DrawEllipse(pen, shape.BoundingBox)
                End Using
            End Using
        End Sub
    End Class
End Namespace
