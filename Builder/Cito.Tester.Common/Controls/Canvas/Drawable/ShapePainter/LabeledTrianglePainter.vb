Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class LabeledTrianglePainter
        Implements IDrawableItemPainter(Of ITriangle)

        Private Sub Draw(g As Graphics, color As Color, shape As ITriangle) Implements IDrawableItemPainter(Of ITriangle).Draw
            Dim bb = shape.BoundingBox
            Dim centerPoint As Point
            Using pen As New Pen(color)
                Using solidBrush As New SolidBrush(color)
                    Using font As New Font("arial", 12, FontStyle.Bold)
                        centerPoint = New Point(shape.AnchorPoint.X - CInt(font.Height / 2) - 10, shape.AnchorPoint.Y - CInt(font.Height / 2) - 10)
                        g.DrawString(If(shape.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(shape.Label), shape.Label, shape.ID), font, solidBrush, centerPoint)
                    End Using

                    Using fontReg As New Font("arial", 8, FontStyle.Regular)
                        centerPoint = New Point(shape.AnchorPoint.X - CInt(fontReg.Height / 2) - 10, shape.AnchorPoint.Y - CInt(fontReg.Height / 2) - 10)
                        g.DrawString(String.Format("{0}x{1}{4}({2},{3})", bb.Width, bb.Height, bb.Left, bb.Top, vbNewLine), fontReg, solidBrush, New Point(centerPoint.X + 2, centerPoint.Y + 16))
                    End Using
                    Dim brush As New SolidBrush(Color.FromArgb(125, 255, 255, 255))
                    g.FillPolygon(brush, shape.Coordinates.ToArray)
                    g.DrawPolygon(pen, shape.Coordinates.ToArray)
                End Using
            End Using
        End Sub

    End Class

End Namespace
