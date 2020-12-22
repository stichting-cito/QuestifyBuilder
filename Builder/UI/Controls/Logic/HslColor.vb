Namespace Controls.Logic
    Public Structure HslColor
        Public Property H As Byte
        Public Property S As Byte
        Public Property L As Byte

        Public Sub New(h As Byte, s As Byte, l As Byte)
            Me.H = h
            Me.S = s
            Me.L = l
        End Sub
    End Structure
End Namespace