Imports System.Runtime.Serialization

Public Class LoaderExceptionsException
    Inherits Exception

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Public Overridable ReadOnly Property UserFriendlyErrorMessage() As String
        Get
            Return My.Resources.Error_TesterException_UserFriendlyErrorMessage_Message
        End Get
    End Property

End Class