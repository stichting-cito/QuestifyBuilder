Imports System.Runtime.Serialization


<Serializable()> _
Public Class ResourceException
    Inherits TesterException

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Public Overrides ReadOnly Property UserFriendlyErrorMessage() As String
        Get
            Return My.Resources.Error_ResourceException_UserFriendlyErrorMessage_Message
        End Get
    End Property

End Class
