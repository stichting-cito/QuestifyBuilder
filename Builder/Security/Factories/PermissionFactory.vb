
Public Class PermissionFactory
    Private Shared _serviceInstance As IPermissionService

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.String.Format(System.String,System.Object)")> _
    Public Shared ReadOnly Property Instance() As IPermissionService
        Get
            If _serviceInstance Is Nothing Then
                Throw New Exception("No instance of PermissionService")
            Else
                Return _serviceInstance
            End If
        End Get
    End Property

    Private Sub New()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.String.Format(System.String,System.Object)")> _
    Public Shared Function Instantiate(ByVal service As IPermissionService) As IPermissionService
        If _serviceInstance Is Nothing Then
            _serviceInstance = service
        End If

        Return _serviceInstance
    End Function

    Public Shared Sub Destroy()
        _serviceInstance = Nothing
    End Sub

    Public Shared ReadOnly Property IsInstantiated() As Boolean
        Get
            Return _serviceInstance IsNot Nothing
        End Get
    End Property
End Class
