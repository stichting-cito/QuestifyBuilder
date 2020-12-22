Imports System.Windows.Forms

Namespace Controls.Canvas.Tools
    Friend Class SelectDragTool
        Inherits CanvasTool

        Public Sub New(decoree As ITool(Of ICanvas))
            MyBase.New(decoree)
        End Sub

        Public Overrides Sub MouseDown(sender As ICanvas, e As MouseEventArgs)
            If (Not SelectItem(e.Location, sender)) Then
                MyBase.MouseDown(sender, e)
            End If
        End Sub

        Public Overrides Sub MouseUp(sender As ICanvas, e As MouseEventArgs)
            If (Not DeSelect(sender)) Then MyBase.MouseUp(sender, e)
        End Sub

        Public Overrides Sub MouseMove(sender As ICanvas, e As MouseEventArgs)
            MyBase.MouseMove(sender, e)
            If (sender.EditItem IsNot Nothing) Then
                sender.Invalidate(sender.EditItem)
                sender.EditItem.AnchorPoint = e.Location
                sender.Invalidate(sender.EditItem)
            End If
        End Sub

    End Class
End Namespace