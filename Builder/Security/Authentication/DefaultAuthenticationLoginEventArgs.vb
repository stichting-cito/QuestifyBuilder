Public Class AuthenticationLoginEventArgs
    Inherits EventArgs

    Public Property Username() As String

    Public Property Password() As String

    Public Property Client() As String

    Public Property Cancel As Boolean
End Class
