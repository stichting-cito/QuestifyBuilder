Imports System.Runtime.Serialization


<Serializable()> _
Public Class ItemLogicControllerException
    Inherits TesterException

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property UserFriendlyErrorMessage() As String
        Get
            Return My.Resources.Error_ItemLogicControllerException_UserFriendlyErrorMessage_Message
        End Get
    End Property

End Class
