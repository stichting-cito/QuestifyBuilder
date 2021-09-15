
''' <summary>
''' Interface for implementing security.
''' </summary>
Public Interface ISecurityService
    ''' <summary>
    ''' Interface definition for authentication.
    ''' </summary>
    ''' <param name="username">username</param>
    ''' <param name="password">password</param>
    ''' <returns>true if user is authenticated, returns false when authentication fails.</returns>
    Function Authenticate(ByVal username As String, ByVal password As String, ByVal type As String) As AuthenticationResult

    ''' <summary>
    ''' Check whether user is authenticated.
    ''' </summary>
    ''' <returns>true if user is authenticated, returns false when authentication fails.</returns>
    Function IsAuthenticated() As Boolean

    Sub Signout()

    ''' <summary>
    ''' Fetches the permissions granted to the current principal. 
    ''' </summary>
    Function FetchGrantedPermissions(bankIds As Integer()) As Security.SerializableDictionaryIntegerPermission

    ''' <summary>
    ''' Determines whether the bank is assigned to the user through a bank role.
    ''' </summary>
    ''' <param name="bankId">The bank id.</param>
    ''' <returns>
    ''' <c>true</c> if the bank is assigned to the user through a bank role; otherwise, <c>false</c>.
    ''' </returns>
    Function IsBankAssignedToUserThroughBankRole(ByVal bankId As Integer) As Boolean

End Interface
