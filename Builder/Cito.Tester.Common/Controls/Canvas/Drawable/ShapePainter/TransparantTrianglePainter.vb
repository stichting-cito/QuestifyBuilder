Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Public Class TransparantTrianglePainter
        Implements IDrawableItemPainter(Of ITriangle)

        Private Sub Draw(g As Graphics, color As Color, shape As ITriangle) Implements IDrawableItemPainter(Of ITriangle).Draw
            Using transWhite As New SolidBrush(Color.FromArgb(64, Color.White)), pen = New Pen(color, 2)
                g.FillPolygon(transWhite, shape.Coordinates.ToArray)
                g.DrawPolygon(pen, shape.Coordinates.ToArray)
            End Using
        End Sub

    End Class

End Namespace