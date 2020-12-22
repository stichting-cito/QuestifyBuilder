Imports System.Configuration
Imports System.Diagnostics.CodeAnalysis

Public NotInheritable Class ApplicationSettings

    Private Shared ReadOnly _config As New AppSettingsReader

    Private Sub New()
    End Sub

    <SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")>
    Public Shared ReadOnly Property SecurityWebServiceLocation As String
        Get
            Try
                Return _config.GetValue("SecurityWebServiceLocation", GetType(String)).ToString()
            Catch ex As InvalidCastException
                Throw New InvalidCastException(My.Resources.ApplicationSettings_InvalidConfigValueCastException)
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            End Try
        End Get
    End Property

    <SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")>
    Public Shared ReadOnly Property BankWebServiceLocation As String
        Get
            Try
                Return _config.GetValue("BankWebServiceLocation", GetType(String)).ToString()
            Catch ex As InvalidCastException
                Throw New InvalidCastException(My.Resources.ApplicationSettings_InvalidConfigValueCastException)
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            End Try
        End Get
    End Property

    <SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")>
    Public Shared ReadOnly Property ResourceWebServiceLocation As String
        Get
            Try
                Return _config.GetValue("ResourceWebServiceLocation", GetType(String)).ToString()
            Catch ex As InvalidCastException
                Throw New InvalidCastException(My.Resources.ApplicationSettings_InvalidConfigValueCastException)
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            End Try
        End Get
    End Property

    <SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")>
    Public Shared ReadOnly Property AuthorizationWebServiceLocation As String
        Get
            Try
                Return _config.GetValue("AuthorizationWebServiceLocation", GetType(String)).ToString()
            Catch ex As InvalidCastException
                Throw New InvalidCastException(My.Resources.ApplicationSettings_InvalidConfigValueCastException)
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            End Try
        End Get
    End Property

    Public Shared Sub SaveUserSettings()
        My.Settings.Save()
    End Sub
End Class
