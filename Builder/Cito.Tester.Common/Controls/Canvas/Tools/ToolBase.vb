Imports System.Windows.Forms

Namespace Controls.Canvas.Tools
    Public Class ToolBase(Of T)
        Implements ITool(Of T)


        Private _decoree As ITool(Of T)

        Public Sub New(decoree As ITool(Of T))
            _decoree = decoree
        End Sub


        Public Overridable Sub KeyDown(sender As T, e As KeyEventArgs) Implements ITool(Of T).KeyDown
            If (_decoree IsNot Nothing AndAlso Not e.Handled) Then _decoree.KeyDown(sender, e)
        End Sub

        Public Overridable Sub KeyPress(sender As T, e As KeyPressEventArgs) Implements ITool(Of T).KeyPress
            If (_decoree IsNot Nothing AndAlso Not e.Handled) Then _decoree.KeyPress(sender, e)
        End Sub

        Public Overridable Sub KeyUp(sender As T, e As KeyEventArgs) Implements ITool(Of T).KeyUp
            If (_decoree IsNot Nothing AndAlso Not e.Handled) Then _decoree.KeyUp(sender, e)
        End Sub

        Public Overridable Sub MouseDown(sender As T, e As MouseEventArgs) Implements ITool(Of T).MouseDown
            If (_decoree IsNot Nothing) Then _decoree.MouseDown(sender, e)
        End Sub

        Public Overridable Sub MouseEnter(sender As T, e As EventArgs) Implements ITool(Of T).MouseEnter
            If (_decoree IsNot Nothing) Then _decoree.MouseEnter(sender, e)
        End Sub

        Public Overridable Sub MouseHover(sender As T, e As EventArgs) Implements ITool(Of T).MouseHover
            If (_decoree IsNot Nothing) Then _decoree.MouseHover(sender, e)
        End Sub

        Public Overridable Sub MouseLeave(sender As T, e As EventArgs) Implements ITool(Of T).MouseLeave
            If (_decoree IsNot Nothing) Then _decoree.MouseLeave(sender, e)
        End Sub

        Public Overridable Sub MouseMove(sender As T, e As MouseEventArgs) Implements ITool(Of T).MouseMove
            If (_decoree IsNot Nothing) Then _decoree.MouseMove(sender, e)
        End Sub

        Public Overridable Sub MouseUp(sender As T, e As MouseEventArgs) Implements ITool(Of T).MouseUp
            If (_decoree IsNot Nothing) Then _decoree.MouseUp(sender, e)
        End Sub

        Public Overridable Sub MouseWheel(sender As T, e As MouseEventArgs) Implements ITool(Of T).MouseWheel
            If (_decoree IsNot Nothing) Then _decoree.MouseWheel(sender, e)
        End Sub

        Public Overridable Sub EndTool(sender As T) Implements ITool(Of T).EndTool
            If (_decoree IsNot Nothing) Then _decoree.EndTool(sender)
        End Sub


    End Class
End Namespace