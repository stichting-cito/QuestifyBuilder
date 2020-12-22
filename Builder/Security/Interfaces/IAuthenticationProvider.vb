
Public Interface IAuthenticationProvider


    Event GetLoginCredentials As EventHandler(Of AuthenticationLoginEventArgs)



    ReadOnly Property CanRetry() As Boolean


    Function Authenticate() As AuthenticationResult


End Interface

Public Enum AuthenticationActionState

    Successful

    Failed

    Canceled
End Enum


