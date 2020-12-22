Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class HollowTrianglePainter
        Implements IDrawableItemPainter(Of ITriangle)

        Private Sub Draw(g As Graphics, color As Color, shape As ITriangle) Implements IDrawableItemPainter(Of ITriangle).Draw
            Using pen As New Pen(color)
                g.DrawPolygon(pen, shape.Coordinates.ToArray)
            End Using
        End Sub
    End Class

End Namespace
