Public Class ProgressEventArgs
    Inherits EventArgs

    Public Property Message() As String
    Public Property Value() As Integer
    Public ReadOnly Property Maximum() As Integer
    Public ReadOnly Property Minimum() As Integer

    Public Sub New(maximum As Integer)
        Me.Maximum = maximum
    End Sub
End Class