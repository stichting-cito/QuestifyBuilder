Imports System.ComponentModel
Imports Cito.Tester.Common.Controls.Canvas.Drawable.Shapes

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class TransparantEllipsePainter
        Implements IDrawableItemPainter(Of IEllipse)

        Private Sub Draw(g As Graphics, color As Color, shape As IEllipse) Implements IDrawableItemPainter(Of IEllipse).Draw
            Dim e = DirectCast(shape, EllipseShape)
            Using transWhite As New SolidBrush(Color.FromArgb(64, Color.White)), pen = New Pen(color, 2)
                g.FillEllipse(transWhite, e.BoundingBox)
                g.DrawEllipse(pen, e.CalcBB(e.HRadius - 1, e.VRadius - 1))
            End Using
        End Sub
    End Class
End Namespace