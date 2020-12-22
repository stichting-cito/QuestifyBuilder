Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class TransparantRectanglePainter
        Implements IDrawableItemPainter(Of IRectangle)

        Private Sub Draw(g As Graphics, color As Color, shape As IRectangle) Implements IDrawableItemPainter(Of IRectangle).Draw
            Using transWhite As New SolidBrush(Color.FromArgb(64, Color.White)), pen = New Pen(color, 2)
                g.FillRectangle(transWhite, shape.BoundingBox)
                g.DrawRectangle(pen, shape.BoundingBox)
            End Using
        End Sub

    End Class
End Namespace

