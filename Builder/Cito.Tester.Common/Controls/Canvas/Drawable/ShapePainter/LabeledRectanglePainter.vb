
Imports System.ComponentModel

Namespace Controls.Canvas.Drawable.ShapePainter
    <DesignerCategory("Code")>
    Friend Class LabeledRectanglePainter
        Implements IDrawableItemPainter(Of IRectangle)

        Private Sub Draw(g As Graphics, color As Color, shape As IRectangle) Implements IDrawableItemPainter(Of IRectangle).Draw
            SetMinimumSizeOfShape(shape)

            Dim bb = shape.BoundingBox
            Dim brush As New SolidBrush(Color.FromArgb(125, 255, 255, 255))
            g.FillRectangle(brush, shape.BoundingBox)
            Using pen As New Pen(color)
                g.DrawRectangle(pen, shape.BoundingBox)
            End Using
            Using solidBrush As New SolidBrush(color)
                Using font As New Font("arial", 12, FontStyle.Bold)
                    g.DrawString(If(shape.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(shape.Label), shape.Label, shape.ID), font, solidBrush, New Point(bb.Left + 2, bb.Top + 2))
                End Using
                Using fontReg As New Font("arial", 8, FontStyle.Regular)
                    g.DrawString(String.Format("{0}x{1}{4}({2},{3})", bb.Width, bb.Height, bb.Left, bb.Top, vbNewLine), fontReg, solidBrush, New Point(bb.Left + 2, bb.Bottom - 32))
                End Using
            End Using
        End Sub

        Private Sub SetMinimumSizeOfShape(ByRef shape As IRectangle)
            Dim minimumSize As Integer = 20

            If shape.Width < minimumSize Then
                shape.Right = shape.Left + minimumSize
            End If
            If shape.Height < minimumSize Then
                shape.Bottom = shape.Top + minimumSize
            End If
        End Sub
    End Class
End Namespace
