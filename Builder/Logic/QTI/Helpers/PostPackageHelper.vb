Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Security
Imports System.Text.RegularExpressions
Imports RestSharp

Namespace QTI.Helpers

    Public Class PostPackageHelper
        Implements IPostPackageHelper

        Public ReadOnly Property Errors As List(Of String) Implements IPostPackageHelper.Errors
            Get
                Return _errors
            End Get
        End Property

        Private ReadOnly _errors As List(Of String) = New List(Of String)

        Private ReadOnly _htmlRegex As New Regex("<.*?>", RegexOptions.Compiled)

        Public Overridable Function PostPackage(postLocation As String, packageLocation As String, parameterName As String) As Uri Implements IPostPackageHelper.PostPackage
            Return PostPackage(postLocation, packageLocation, parameterName, False)
        End Function

        Public Overridable Function PostPackage(postLocation As String, packageLocation As String, parameterName As String, urlFormContent As Boolean) As Uri Implements IPostPackageHelper.PostPackage

            Errors.Clear()
            Dim uri = New Uri("about:blank")
            Try
                ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf CertificateValidationCallBack)
                Dim client As New RestClient(postLocation)
                Dim request As New RestRequest(Method.POST)

                request.AddParameter(parameterName, Path.GetFileName(packageLocation))
                request.AddHeader("Content-Type", "application/x-zip-compressed")
                request.AddFile(parameterName, packageLocation)

                Dim response As IRestResponse = client.Execute(request)
                uri = response.ResponseUri

                If urlFormContent Then
                    uri = New Uri(New Uri(postLocation), GetUrlFromReturnPage(response.Content))
                ElseIf uri Is Nothing Then
                    Errors.Add("No response received.")
                    If response.ErrorException IsNot Nothing Then
                        Errors.Add(response.ErrorException.Message)
                    End If
                ElseIf uri.ToString.Equals(postLocation, StringComparison.OrdinalIgnoreCase) Then
                    If response.Headers IsNot Nothing AndAlso response.Headers.Where(Function(p) p.Name.Equals("location", StringComparison.OrdinalIgnoreCase)).Count = 1 Then
                        uri = New Uri(response.Headers.Where(Function(p) p.Name.Equals("location", StringComparison.OrdinalIgnoreCase))(0).Value.ToString)
                    End If
                Else
                    Errors.Add($"Unexpected statuscode received: {response.StatusCode}")
                    If response.ErrorException IsNot Nothing Then
                        Errors.Add(response.ErrorException.Message)
                    End If
                End If

                If uri Is Nothing OrElse uri.ToString.Equals(postLocation, StringComparison.OrdinalIgnoreCase) AndAlso response.Content IsNot Nothing AndAlso Not String.IsNullOrEmpty(response.Content) Then
                    Errors.Add(StripTagsRegex(response.Content))
                End If
                If uri Is Nothing Then
                    uri = New Uri("about:blank")
                End If
            Catch ex As Exception
                Debug.Assert(False, $"An error occurred while uploading the package to the Server: {ex.Message}")
                Errors.Add(ex.ToString)
            End Try
            Return uri
        End Function

        Public Overridable Function PostPackageWithAuthentication(postLocation As String, packageLocation As String, parameterName As String, certName As String) As Uri Implements IPostPackageHelper.PostPackageWithAuthentication
            Throw New NotImplementedException()
        End Function

        Public Overridable Function PostPackageWithAuthenticationAndReturnId(ByVal postLocation As String, ByVal packageLocation As String, ByVal parameterName As String, ByVal certName As String) As String Implements IPostPackageHelper.PostPackageWithAuthenticationAndReturnId
            Throw New NotImplementedException()
        End Function

        Public Function PostPackageAndReturnId(ByVal postLocation As String, ByVal packageLocation As String, ByVal parameterName As String) As String Implements IPostPackageHelper.PostPackageAndReturnId
            Throw New NotImplementedException()
        End Function

        Protected Function StripTagsRegex(source As String) As String
            Return _htmlRegex.Replace(source, String.Empty)
        End Function

        Protected Overridable Function CertificateValidationCallBack(sender As Object, certificate As System.Security.Cryptography.X509Certificates.X509Certificate, chain As System.Security.Cryptography.X509Certificates.X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
            Return True
        End Function

        Private Shared Function GetUrlFromReturnPage(htmlPage As String) As String
            Dim returnValue As String = String.Empty
            Const pattern As String = "(?<=<a.*?href="").*?examenVariantId.*?[^""]*"
            Dim input As String = htmlPage
            For Each match As Match In Regex.Matches(input, pattern, RegexOptions.IgnoreCase)
                returnValue = match.Value
            Next
            Return returnValue
        End Function

    End Class
End NameSpace