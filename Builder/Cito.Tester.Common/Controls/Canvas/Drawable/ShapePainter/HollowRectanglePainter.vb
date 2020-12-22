Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class HollowRectanglePainter
        Implements IDrawableItemPainter(Of IRectangle)

        Private Sub Draw(g As Graphics, color As Color, shape As IRectangle) Implements IDrawableItemPainter(Of IRectangle).Draw
            Using pen As New Pen(color)
                g.DrawRectangle(pen, shape.BoundingBox)
            End Using
        End Sub
    End Class
End Namespace

