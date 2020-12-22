Public Class QuestifyRemoteAuthenticationProvider
    Implements IAuthenticationProvider


    Public Function Authenticate() As AuthenticationResult Implements IAuthenticationProvider.Authenticate
        Dim returnValue As New AuthenticationResult
        Dim loginArgs As New AuthenticationLoginEventArgs()
        RaiseEvent GetLoginCredentials(Me, loginArgs)
        Dim username As String = loginArgs.Username

        If Not loginArgs.Cancel Then
            If String.IsNullOrEmpty(loginArgs.Username) Then
                returnValue.AuthenticationActionState = AuthenticationActionState.Failed

            ElseIf Not SecurityFactory.Instance.IsAuthenticated Then
                returnValue = SecurityFactory.Instance.Authenticate(username, loginArgs.Password, "default")
                If returnValue.QuestifyBuilderIdentity IsNot Nothing AndAlso returnValue.QuestifyBuilderIdentity.IsAuthenticated Then
                    returnValue.AuthenticationActionState = AuthenticationActionState.Successful

                    Threading.Thread.CurrentPrincipal = TestBuilderPrincipal.GetAuthPrincipal(returnValue.QuestifyBuilderIdentity)
                End If
            End If
        Else
            returnValue.AuthenticationActionState = AuthenticationActionState.Canceled
        End If

        Return returnValue
    End Function

    Public ReadOnly Property CanRetry As Boolean Implements IAuthenticationProvider.CanRetry
        Get
            Return False
        End Get
    End Property

    Public Event GetLoginCredentials(sender As Object, e As AuthenticationLoginEventArgs) Implements IAuthenticationProvider.GetLoginCredentials
End Class
