Public Class ResourceDialogRequestEventArgs
    Inherits EventArgs

    Public ReadOnly Property Filter() As String

    Public ReadOnly Property ResourceType() As String

    Public Property ReturnedResourceName() As String

    Public Sub New(resourceType As String, filter As String)
        Me.ResourceType = resourceType
        Me.Filter = filter
    End Sub
End Class