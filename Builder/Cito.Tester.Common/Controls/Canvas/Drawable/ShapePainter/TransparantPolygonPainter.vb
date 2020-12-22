Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class TransparantPolygonPainter
        Implements IDrawableItemPainter(Of IPolygon)

        Private Sub Draw(g As Graphics, color As Color, shape As IPolygon) Implements IDrawableItemPainter(Of IPolygon).Draw
            Using transWhite As New SolidBrush(Color.FromArgb(64, Color.White)), pen = New Pen(color, 2)
                g.FillPolygon(transWhite, shape.Coordinates.ToArray)

                g.DrawPolygon(pen, shape.Coordinates.ToArray)
            End Using
        End Sub

    End Class
End Namespace

