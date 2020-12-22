Public Class ProgressEventArgs
    Inherits EventArgs

    Private _statusMessage As String
    Private _progressValue As Nullable(Of Integer)

    Public ReadOnly Property StatusMessage() As String
        Get
            Return _statusMessage
        End Get
    End Property

    Public ReadOnly Property ProgessValue() As Nullable(Of Integer)
        Get
            Return _progressValue
        End Get
    End Property

    Public Sub New(sMessage As String)
        _statusMessage = sMessage
    End Sub

    Public Sub New(sMessage As String, iProgressValue As Integer)
        _statusMessage = sMessage
        _progressValue = iProgressValue
    End Sub

End Class
