Imports System.Windows.Forms
Namespace Controls.Canvas

    Public Interface ITool(Of T)

        Sub MouseMove(sender As T, e As MouseEventArgs)

        Sub MouseDown(sender As T, e As MouseEventArgs)

        Sub MouseUp(sender As T, e As MouseEventArgs)

        Sub MouseWheel(sender As T, e As MouseEventArgs)

        Sub MouseLeave(sender As T, e As EventArgs)

        Sub MouseEnter(sender As T, e As EventArgs)

        Sub MouseHover(sender As T, e As EventArgs)

        Sub KeyDown(sender As T, e As KeyEventArgs)

        Sub KeyUp(sender As T, e As KeyEventArgs)

        Sub KeyPress(sender As T, e As KeyPressEventArgs)

        Sub EndTool(sender As T)

    End Interface
End Namespace