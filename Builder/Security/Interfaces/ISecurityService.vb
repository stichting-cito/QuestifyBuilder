
Public Interface ISecurityService
    Function Authenticate(ByVal username As String, ByVal password As String, ByVal type As String) As AuthenticationResult

    Function IsAuthenticated() As Boolean

    Sub Signout()

    Function FetchGrantedPermissions(bankIds As Integer()) As Security.SerializableDictionaryIntegerPermission

    Function IsBankAssignedToUserThroughBankRole(ByVal bankId As Integer) As Boolean

End Interface
