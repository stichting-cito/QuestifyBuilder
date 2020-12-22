Imports System.Runtime.Serialization


<Serializable()> _
Public Class SecurityException
    Inherits TesterException

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Overrides ReadOnly Property UserFriendlyErrorMessage() As String
        Get
            Return "Item cannot be displayed, because a security error has occurred."
        End Get
    End Property

End Class
