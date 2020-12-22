Imports Microsoft.Web.Services3.Security.Tokens

Public NotInheritable Class SecurityUsernameToken

    Private Shared _usernameToken As UsernameToken
    Private Shared _policy As String = "usernameTokenSecurity"

    Public Shared Property UsernameToken() As UsernameToken
        Get
            Return _usernameToken
        End Get
        Set(ByVal value As UsernameToken)
            _usernameToken = value
        End Set
    End Property

    Public Shared Property Policy() As String
        Get
            Return _policy
        End Get
        Set(ByVal value As String)
            _policy = value
        End Set
    End Property

    Private Sub New()
    End Sub

End Class
