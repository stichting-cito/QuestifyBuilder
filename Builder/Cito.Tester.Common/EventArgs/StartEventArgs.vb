Public Class StartEventArgs
    Inherits EventArgs

    Private _numberOfResources As Integer

    Public ReadOnly Property NumberOfResources() As Integer
        Get
            Return _numberOfResources
        End Get
    End Property

    Public Sub New(numberOfResources As Integer)
        _numberOfResources = numberOfResources
    End Sub
End Class
