Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class LabeledPolygonPainter
        Implements IDrawableItemPainter(Of IPolygon)

        Private Sub Draw(g As Graphics, color As Color, shape As IPolygon) Implements IDrawableItemPainter(Of IPolygon).Draw
            Dim bb = shape.BoundingBox
            Using pen As New Pen(color)
                Using solidBrush As New SolidBrush(color)
                    Using font As New Font("arial", 12, FontStyle.Bold)
                        Dim centerPoint = New Point(shape.AnchorPoint.X + CInt(shape.BoundingBox.Width / 2) - CInt(font.Height * 2.5), shape.AnchorPoint.Y + CInt(shape.BoundingBox.Height / 2) - CInt(font.Height * 2.5))
                        g.DrawString(If(shape.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(shape.Label), shape.Label, shape.ID), font, solidBrush, centerPoint)
                    End Using

                    Using fontReg As New Font("arial", 8, FontStyle.Regular)
                        Dim centerPoint = New Point(shape.AnchorPoint.X + CInt(shape.BoundingBox.Width / 2) - CInt(fontReg.Height * 3.6), shape.AnchorPoint.Y + CInt(shape.BoundingBox.Height / 2) - CInt(fontReg.Height * 3))
                        g.DrawString((String.Format("{0}x{1}{4}({2},{3})", bb.Width, bb.Height, bb.Left, bb.Top, vbNewLine)), fontReg, solidBrush, New Point(centerPoint.X, centerPoint.Y + 12))
                    End Using
                    Dim brush As New SolidBrush(Color.FromArgb(125, 255, 255, 255))
                    g.FillPolygon(brush, shape.Coordinates.ToArray)
                    g.DrawPolygon(pen, shape.Coordinates.ToArray)
                End Using
            End Using
        End Sub
    End Class
End Namespace
