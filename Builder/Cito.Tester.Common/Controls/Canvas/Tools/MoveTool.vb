Imports System.Windows.Forms
Namespace Controls.Canvas.Tools
    Friend Class MoveTool
        Inherits CanvasTool

        Private _start As Point = Point.Empty


        Public Sub New(decoree As ITool(Of ICanvas))
            MyBase.New(decoree)
        End Sub

        Public Overrides Sub MouseDown(sender As ICanvas, e As MouseEventArgs)
            If (sender.EditItem IsNot Nothing) Then
                _start = sender.EditItem.AnchorPoint
            End If
            MyBase.MouseDown(sender, e)
        End Sub

        Public Overrides Sub MouseMove(sender As ICanvas, e As MouseEventArgs)
            If (sender.EditItem IsNot Nothing AndAlso _start <> Point.Empty) Then
                sender.Invalidate(sender.EditItem)
                sender.EditItem.AnchorPoint = e.Location
                sender.Invalidate(sender.EditItem)
            Else
                _start = Point.Empty
            End If
            MyBase.MouseMove(sender, e)
        End Sub

        Public Overrides Sub MouseUp(sender As ICanvas, e As MouseEventArgs)
            _start = Point.Empty
            MyBase.MouseUp(sender, e)
        End Sub

        Public Overrides Sub KeyDown(sender As ICanvas, e As KeyEventArgs)
            If (sender.EditItem IsNot Nothing) Then
                Dim delta As Integer = If(e.Shift, 5, 1)
                Select Case e.KeyCode
                    Case Keys.Up
                        MoveItem(0, -delta, sender) : e.Handled = True
                    Case Keys.Down
                        MoveItem(0, delta, sender) : e.Handled = True
                    Case Keys.Left
                        MoveItem(-delta, 0, sender) : e.Handled = True
                    Case Keys.Right
                        MoveItem(delta, 0, sender) : e.Handled = True
                    Case Else

                End Select
            End If
            MyBase.KeyDown(sender, e)
        End Sub


        Private Sub MoveItem(dX As Integer, dY As Integer, C As ICanvas)
            C.Invalidate(C.EditItem)
            Dim p = C.EditItem.AnchorPoint
            p.X = p.X + dX
            p.Y = p.Y + dY
            C.EditItem.AnchorPoint = p
            C.Invalidate(C.EditItem)
        End Sub




    End Class
End Namespace