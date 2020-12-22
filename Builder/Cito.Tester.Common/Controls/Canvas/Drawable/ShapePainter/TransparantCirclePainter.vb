Imports System.ComponentModel
Imports Cito.Tester.Common.Controls.Canvas.Drawable.Shapes

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class TransparantCirclePainter
        Implements IDrawableItemPainter(Of ICircle)

        Private Sub Draw(g As Graphics, color As Color, shape As ICircle) Implements IDrawableItemPainter(Of ICircle).Draw
            Using transWhite As New SolidBrush(Color.FromArgb(64, Color.White)), pen = New Pen(color, 2)
                g.FillEllipse(transWhite, shape.BoundingBox)
                Dim s = DirectCast(shape, CircleShape)
                g.DrawEllipse(pen, s.CalcBB(s.Radius - 1))
            End Using
        End Sub

    End Class
End Namespace
