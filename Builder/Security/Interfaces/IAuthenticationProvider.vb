
''' <summary>
''' The interface for the authentication providers
''' </summary>
Public Interface IAuthenticationProvider

#Region " Events "

    ''' <summary>
    ''' Occurs when getting login credentials
    ''' </summary>
    Event GetLoginCredentials As EventHandler(Of AuthenticationLoginEventArgs)

#End Region

#Region " Properties "

    ''' <summary>
    ''' Gets a value indicating whether this provider can be asked to authenticate again. 
    ''' If for example the authentication failed and the provider uses variables that can change each authentication, it is
    ''' meaningful to try again.
    ''' </summary>
    ''' <value><c>true</c> if [do retry]; otherwise, <c>false</c>.</value>
    ReadOnly Property CanRetry() As Boolean

#End Region

#Region " Methodes "
    ''' <summary>
    ''' Authenticates this instance.
    ''' </summary>
    ''' <returns></returns>
    Function Authenticate() As AuthenticationResult

#End Region

End Interface

''' <summary>
''' Enum to be used for the authenticate method of a provider
''' </summary>
Public Enum AuthenticationActionState
    
    ''' <summary>
    ''' Indicate that the authentication is valid
    ''' </summary>
    Successful

    ''' <summary>
    ''' Indicate that the authentication has failed
    ''' </summary>
    Failed

    ''' <summary>
    ''' Indicate that the authentication has been canceled
    ''' </summary>
    Canceled
End Enum


