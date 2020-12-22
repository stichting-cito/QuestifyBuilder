Imports System.Windows.Forms

Namespace Controls.Canvas.Tools
    Friend Class WithinBoundsTool
        Inherits CanvasTool

        Public Sub New(decoree As ITool(Of ICanvas))
            MyBase.New(decoree)
        End Sub

        Public Overrides Sub MouseMove(sender As ICanvas, e As MouseEventArgs)
            If (sender.EditItem IsNot Nothing) Then
                Dim offsetX = e.X - sender.EditItem.AnchorPoint.X
                Dim offsetY = e.Y - sender.EditItem.AnchorPoint.Y

                Dim rect = sender.EditItem.BoundingBox
                rect.Offset(offsetX, offsetY)

                If Not sender.BackgroundImageBounds.Contains(rect) Then
                    Dim newX As Integer = e.X
                    Dim newY As Integer = e.Y

                    If (rect.X < 0) Then
                        newX = newX - rect.X
                    ElseIf (rect.Right > sender.BackgroundImageBounds.Right) Then
                        newX = newX - (rect.Right - sender.BackgroundImageBounds.Right)
                    End If

                    If (rect.Y < 0) Then
                        newY = newY - rect.Y
                    ElseIf (rect.Bottom > sender.BackgroundImageBounds.Bottom) Then
                        newY = newY - (rect.Bottom - sender.BackgroundImageBounds.Bottom)
                    End If

                    e = New MouseEventArgs(e.Button, e.Clicks, newX, newY, e.Delta)
                End If

                MyBase.MouseMove(sender, e)
            End If
        End Sub

        Public Overrides Sub KeyDown(sender As ICanvas, e As KeyEventArgs)
            If (sender.EditItem IsNot Nothing) Then
                Dim offsetX = 0
                Dim offsetY = 0
                Dim delta As Integer = If(e.Shift, 5, 1)
                Select Case e.KeyCode
                    Case Keys.Up
                        offsetY = delta * -1
                    Case Keys.Down
                        offsetY = delta
                    Case Keys.Left
                        offsetX = delta * -1
                    Case Keys.Right
                        offsetX = delta
                End Select

                Dim rect = sender.EditItem.BoundingBox
                rect.Offset(offsetX, offsetY)

                If Not sender.BackgroundImageBounds.Contains(rect) Then
                    e.Handled = True
                End If

                MyBase.KeyDown(sender, e)
            End If
        End Sub
    End Class
End Namespace

