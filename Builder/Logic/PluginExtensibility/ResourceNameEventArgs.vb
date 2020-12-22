Public Class ResourceNameEventArgs
    Inherits EventArgs

    Public ReadOnly Property ResourceName() As String

    Public Sub New(name As String)
        ResourceName = name
    End Sub

End Class
