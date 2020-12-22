Imports System.Security.Principal

Public Class TestBuilderPrincipal
    Inherits GenericPrincipal


    Public Sub New(ByVal identity As TestBuilderIdentity)
        MyBase.New(identity, Nothing)
    End Sub




    Public Overrides Function IsInRole(ByVal role As String) As Boolean
        Return True
    End Function



    Public Shared Function GetAuthPrincipal(ByVal identity As TestBuilderIdentity) As TestBuilderPrincipal
        If identity Is Nothing Then
            Throw New ArgumentNullException("identity")
        End If

        If identity.IsAuthenticated Then
            Return New TestBuilderPrincipal(identity)
        Else
            Return Nothing
        End If

    End Function


End Class
