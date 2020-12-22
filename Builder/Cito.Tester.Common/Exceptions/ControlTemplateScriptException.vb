Imports System.Runtime.Serialization
Imports System.Security.Permissions


<Serializable()> _
Public Class ControlTemplateScriptException
    Inherits TesterException

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(message As String, sourceCode As String)
        MyBase.New(message)
        Me.SourceCode = sourceCode
    End Sub

    Public Sub New(message As String, sourceCode As String, innerException As Exception)
        MyBase.New(message, innerException)
        Me.SourceCode = sourceCode
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)

        If info IsNot Nothing Then
            SourceCode = info.GetString("sourceCode")
        End If
    End Sub

    Public Property SourceCode() As String

    Public Overrides ReadOnly Property UserFriendlyErrorMessage() As String
        Get
            Return My.Resources.Error_ControlTemplateScriptException_UserFriendlyErrorMessage_Message
        End Get
    End Property

    <SecurityPermission(SecurityAction.Demand, SerializationFormatter:=True)> _
    <SecurityPermission(SecurityAction.LinkDemand, flags:=SecurityPermissionFlag.SerializationFormatter)> _
    Public Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
        If info IsNot Nothing Then

            info.AddValue("sourceCode", SourceCode)

            MyBase.GetObjectData(info, context)
        Else
            Throw New SerializationException(My.Resources.Error_ControlTemplateScriptException_GetObjectData_CannotGetData)
        End If
    End Sub

End Class
