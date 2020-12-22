

Public Class PrintFormEventArgs
    Inherits EventArgs
    Private _printForm As PrintForm

    Public Sub New(ByVal printForm As PrintForm)
        _printForm = printForm
    End Sub

    Public Property PrintForm As PrintForm
        Get
            Return _printForm
        End Get
        Set(value As PrintForm)
            _printForm = value
        End Set
    End Property

End Class
