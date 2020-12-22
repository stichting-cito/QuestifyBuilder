Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter

    <DesignerCategory("Code")>
    Friend Class HollowCirclePainter
        Implements IDrawableItemPainter(Of ICircle)

        Private Sub Draw(g As Graphics, color As Color, shape As ICircle) Implements IDrawableItemPainter(Of ICircle).Draw
            Using pen As New Pen(color)
                g.DrawEllipse(pen, shape.BoundingBox)
            End Using
        End Sub

    End Class

End Namespace