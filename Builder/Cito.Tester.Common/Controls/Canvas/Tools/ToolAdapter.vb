Imports System.Windows.Forms

Namespace Controls.Canvas.Tools
    Friend Class ToolAdapter(Of F As Class, T As Class)
        Implements ITool(Of F)


        Private _adaptee As ITool(Of T)

        Public Sub New(adaptee As ITool(Of T))
            _adaptee = adaptee
        End Sub

        Public Property Adaptee As ITool(Of T)
            Get
                Return _adaptee
            End Get
            Set(value As ITool(Of T))
                _adaptee = value
            End Set
        End Property



        Public Sub KeyDown(sender As F, e As KeyEventArgs) Implements ITool(Of F).KeyDown
            _adaptee.KeyDown(F2T(sender), e)
        End Sub

        Public Sub KeyPress(sender As F, e As KeyPressEventArgs) Implements ITool(Of F).KeyPress
            _adaptee.KeyPress(F2T(sender), e)
        End Sub

        Public Sub KeyUp(sender As F, e As KeyEventArgs) Implements ITool(Of F).KeyUp
            _adaptee.KeyUp(F2T(sender), e)
        End Sub

        Public Sub MouseDown(sender As F, e As MouseEventArgs) Implements ITool(Of F).MouseDown
            _adaptee.MouseDown(F2T(sender), e)
        End Sub

        Public Sub MouseEnter(sender As F, e As EventArgs) Implements ITool(Of F).MouseEnter
            _adaptee.MouseEnter(F2T(sender), e)
        End Sub

        Public Sub MouseHover(sender As F, e As EventArgs) Implements ITool(Of F).MouseHover
            _adaptee.MouseHover(F2T(sender), e)
        End Sub

        Public Sub MouseLeave(sender As F, e As EventArgs) Implements ITool(Of F).MouseLeave
            _adaptee.MouseLeave(F2T(sender), e)
        End Sub

        Public Sub MouseMove(sender As F, e As MouseEventArgs) Implements ITool(Of F).MouseMove
            _adaptee.MouseMove(F2T(sender), e)
        End Sub

        Public Sub MouseUp(sender As F, e As MouseEventArgs) Implements ITool(Of F).MouseUp
            _adaptee.MouseUp(F2T(sender), e)
        End Sub

        Public Sub MouseWheel(sender As F, e As MouseEventArgs) Implements ITool(Of F).MouseWheel
            _adaptee.MouseWheel(F2T(sender), e)
        End Sub

        Public Sub EndTool(sender As F) Implements ITool(Of F).EndTool
            _adaptee.EndTool(F2T(sender))
        End Sub


        Private Function F2T(sender As F) As T
            Dim ret As T = TryCast(sender, T)
            Return ret
        End Function


    End Class
End Namespace