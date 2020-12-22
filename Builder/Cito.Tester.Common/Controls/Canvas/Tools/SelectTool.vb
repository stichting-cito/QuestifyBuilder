Imports System.Windows.Forms
Namespace Controls.Canvas.Tools
    Friend Class SelectTool
        Inherits CanvasTool

        Public Sub New(decoree As ITool(Of ICanvas))
            MyBase.New(decoree)
        End Sub

        Public Overrides Sub KeyDown(sender As ICanvas, e As KeyEventArgs)

            If sender.EditItem IsNot Nothing AndAlso e.KeyCode = Keys.Escape Then
                DeSelect(sender)
            End If
            MyBase.KeyDown(sender, e)

        End Sub

        Public Overrides Sub MouseDown(sender As ICanvas, e As MouseEventArgs)
            If (sender.EditItem IsNot Nothing) Then
                If Not sender.EditItem.BoundingBox.Contains(e.Location) Then
                    DeSelect(sender)
                    SelectItem(e.Location, sender)
                End If
            Else
                SelectItem(e.Location, sender)
            End If
            MyBase.MouseDown(sender, e)
        End Sub

        Public Overrides Sub EndTool(sender As ICanvas)
            DeSelect(sender)
        End Sub

    End Class
End Namespace