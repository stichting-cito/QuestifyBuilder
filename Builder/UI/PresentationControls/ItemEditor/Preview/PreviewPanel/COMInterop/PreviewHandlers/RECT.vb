Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential)> _
Public Structure RECT

    Public left As Integer
    Public top As Integer
    Public right As Integer
    Public bottom As Integer


    Public Sub New(ByVal rect__1 As System.Drawing.Rectangle)
        Me.top = CInt(rect__1.Top)
        Me.bottom = CInt(rect__1.Bottom)
        Me.left = CInt(rect__1.Left)
        Me.right = CInt(rect__1.Right)
    End Sub


End Structure