Namespace QTI.Helpers
    Public Interface IPostPackageHelper
        ReadOnly Property Errors As List(Of String)
        Function PostPackage(postLocation As String, packageLocation As String, parameterName As String) As Uri
        Function PostPackage(postLocation As String, packageLocation As String, parameterName As String, urlFormContent As Boolean) As Uri
        Function PostPackageWithAuthentication(postLocation As String, packageLocation As String, parameterName As String, certName As String) As Uri
        Function PostPackageWithAuthenticationAndReturnId(postLocation As String, packageLocation As String, parameterName As String, certName As String) As String
        Function PostPackageAndReturnId(postLocation As String, packageLocation As String, parameterName As String) As String
    End Interface
End NameSpace