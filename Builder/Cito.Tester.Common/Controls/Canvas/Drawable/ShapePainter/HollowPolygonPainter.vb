Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class HollowPolygonPainter
        Implements IDrawableItemPainter(Of IPolygon)

        Private Sub Draw(g As Graphics, color As Color, shape As IPolygon) Implements IDrawableItemPainter(Of IPolygon).Draw
            Using pen As New Pen(color)
                g.DrawPolygon(pen, shape.Coordinates.ToArray)
            End Using
        End Sub
    End Class
End Namespace
