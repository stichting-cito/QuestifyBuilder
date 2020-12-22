Imports System.Drawing

Public Class AdjustVolumeEventArgs
    Inherits EventArgs

    Public Property ButtonRectangle As Rectangle

    Public Sub New(rect As Rectangle)
        ButtonRectangle = rect
    End Sub

End Class
