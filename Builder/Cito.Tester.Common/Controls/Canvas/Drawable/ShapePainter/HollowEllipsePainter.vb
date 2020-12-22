Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class HollowEllipsePainter
        Implements IDrawableItemPainter(Of IEllipse)

        Private Sub Draw(g As Graphics, color As Color, shape As IEllipse) Implements IDrawableItemPainter(Of IEllipse).Draw
            Using pen As New Pen(color)
                g.DrawEllipse(pen, shape.BoundingBox)
            End Using
        End Sub

    End Class
End Namespace