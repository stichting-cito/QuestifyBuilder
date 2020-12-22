Imports System.Drawing

Namespace ImageGenerator

    Public MustInherit Class ImageGenerator

        Sub New()
        End Sub

        Public MustOverride Function CreateImage(width As Integer, height As Integer, text As String) As Byte()
        Public MustOverride Function CreateImage(width As Integer, height As Integer, text As String, percTransparency As Integer) As Byte()
        Public MustOverride Function CreateImage(width As Integer, height As Integer, text As String, pen As Pen, percTransparency As Integer) As Byte()
    End Class
End NameSpace