
''' <summary>
''' Gives access to some unit tests settings
''' </summary>
Public NotInheritable Class UnitTestSettings
    Private Shared _config As New System.Configuration.AppSettingsReader
    Private Shared _client As String
    Private Shared _username As String
  
    ''' <summary>
    ''' Initializes a new instance of the <see cref="UnitTestSettings" /> class.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Gets the test client.
    ''' </summary>
    ''' <value>The test client.</value>
    Public Shared ReadOnly Property TestClient() As String
        Get
            If _client Is Nothing Then
                _client = GetValue("UnitTestClient")
            End If
            Return _client
        End Get
    End Property

    ''' <summary>
    ''' Gets the test username.
    ''' </summary>
    ''' <value>The test username.</value>
    Public Shared ReadOnly Property TestUsername() As String
        Get
            If _username Is Nothing Then
                _username = GetValue("UnitTestUsername")
            End If
            Return _username
        End Get
    End Property

    ''' <summary>
    ''' Gets the config value.
    ''' </summary>
    ''' <param name="key">The key.</param>
    Private Shared Function GetValue(ByVal key As String) As String
        Return DirectCast(_config.GetValue(key, GetType(String)), String)
    End Function

End Class
