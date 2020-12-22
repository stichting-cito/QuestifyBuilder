
Public NotInheritable Class SecurityFactory

    Private Shared _serviceInstance As ISecurityService
    Private Shared _authenticationProvider As IAuthenticationProvider

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property AuthenticationProvider() As IAuthenticationProvider
        Get
            If _authenticationProvider Is Nothing Then
                _authenticationProvider = New QuestifyRemoteAuthenticationProvider()
            End If

            Return _authenticationProvider
        End Get
    End Property

    Public Shared ReadOnly Property Instance() As ISecurityService
        Get
            If _serviceInstance Is Nothing Then
                Throw New SecurityException("SecurityService not instantiated")
            Else
                Return _serviceInstance
            End If
        End Get
    End Property

    Public Shared ReadOnly Property Isinstantiated() As Boolean
        Get
            Return _serviceInstance IsNot Nothing
        End Get
    End Property

    Public Shared Function Instantiate(ByVal service As ISecurityService) As ISecurityService
        If _serviceInstance Is Nothing Then
            _serviceInstance = service
        End If

        Return _serviceInstance
    End Function

    Public Shared Sub Destroy()
        _serviceInstance = Nothing
    End Sub


End Class
