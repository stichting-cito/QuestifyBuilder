
Public NotInheritable Class UnitTestSettings
    Private Shared _config As New System.Configuration.AppSettingsReader
    Private Shared _client As String
    Private Shared _username As String

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property TestClient() As String
        Get
            If _client Is Nothing Then
                _client = GetValue("UnitTestClient")
            End If
            Return _client
        End Get
    End Property

    Public Shared ReadOnly Property TestUsername() As String
        Get
            If _username Is Nothing Then
                _username = GetValue("UnitTestUsername")
            End If
            Return _username
        End Get
    End Property

    Private Shared Function GetValue(ByVal key As String) As String
        Return DirectCast(_config.GetValue(key, GetType(String)), String)
    End Function

End Class
