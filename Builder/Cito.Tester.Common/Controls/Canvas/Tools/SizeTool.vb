Imports System.Windows.Forms
Namespace Controls.Canvas.Tools
    Friend Class SizeTool
        Inherits ToolBase(Of ICanvas)

        Public Sub New(decoree As ITool(Of ICanvas))
            MyBase.New(decoree)
        End Sub

        Public Overrides Sub MouseWheel(sender As ICanvas, e As MouseEventArgs)
            MyBase.MouseWheel(sender, e)
            Dim r As IDimensionManipulator = TryCast(sender.EditItem, IDimensionManipulator)
            If (r IsNot Nothing) Then
                sender.Invalidate(sender.EditItem)
                If e.Delta > 0 Then
                    r.IncWidth()
                Else
                    r.DecWidth()
                End If
                sender.Invalidate(sender.EditItem)
            End If
        End Sub

        Public Overrides Sub KeyDown(sender As ICanvas, e As KeyEventArgs)
            If (sender.EditItem IsNot Nothing AndAlso
    (e.Modifiers = Keys.Control)) Then

                Dim r As IDimensionManipulator = TryCast(sender.EditItem, IDimensionManipulator)
                If (r IsNot Nothing) Then
                    If e.KeyCode = Keys.Right OrElse e.KeyCode = Keys.Add Then
                        r.IncWidth()
                        sender.Invalidate(sender.EditItem)
                        If Not sender.BackgroundImageBounds.Contains(sender.EditItem.BoundingBox) Then
                            r.DecWidth()
                            sender.Invalidate(sender.EditItem)
                        End If
                        e.Handled = True
                    End If
                    If e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Subtract Then
                        sender.Invalidate(sender.EditItem)
                        r.DecWidth() : e.Handled = True
                    End If
                    If e.KeyCode = Keys.Down Then
                        r.IncHeight()
                        sender.Invalidate(sender.EditItem)
                        If Not sender.BackgroundImageBounds.Contains(sender.EditItem.BoundingBox) Then
                            r.DecHeight()
                            sender.Invalidate(sender.EditItem)
                        End If
                        e.Handled = True
                    End If
                    If e.KeyCode = Keys.Up Then
                        sender.Invalidate(sender.EditItem)
                        r.DecHeight() : e.Handled = True
                    End If

                End If
            End If
            MyBase.KeyDown(sender, e)
        End Sub

    End Class
End Namespace