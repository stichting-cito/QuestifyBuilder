Imports System.Runtime.Serialization


<Serializable()> _
Public Class DuplicateResourceException
    Inherits TesterException

    Private _bankId As Integer = -1
    Private _bankContextId As Integer = -1

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(message As String, bankid As Integer, bankContextId As Integer)
        MyBase.New(message)
        _bankId = bankid
        _bankContextId = bankContextId
    End Sub

    Public Overrides ReadOnly Property UserFriendlyErrorMessage() As String
        Get
            Return My.Resources.Error_ResourceException_UserFriendlyErrorMessage_Message
        End Get
    End Property

    Public ReadOnly Property BankId() As Integer
        Get
            Return _bankId
        End Get
    End Property

    Public ReadOnly Property BankContextId() As Integer
        Get
            Return _bankContextId
        End Get
    End Property

End Class
