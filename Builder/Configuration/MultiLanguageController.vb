Imports System.Collections.ObjectModel
Imports System.Configuration
Imports System.Diagnostics.CodeAnalysis
Imports System.Globalization
Imports System.Threading

Public NotInheritable Class MultiLanguageController

    Private Sub New()
    End Sub

    <SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")> _
    Public Shared Property CurrentLanguageSetting As String
        Get
            If String.IsNullOrEmpty(My.Settings.Language) Then
                My.Settings.Language = ConfigurationManager.AppSettings("DefaultLanguage")
            End If
            Return My.Settings.Language
        End Get
        Set
            My.Settings.Language = value
        End Set
    End Property


    Public Shared Function GetAvailableLanguages() As ReadOnlyCollection(Of UILanguage)
        Dim availableLanguageList As New List(Of UILanguage)

        Dim english = New CultureInfo("EN")
        availableLanguageList.Add(New UILanguage(english.Name, english.NativeName))

        Dim dutch = New CultureInfo("NL")
        availableLanguageList.Add(New UILanguage(dutch.Name, dutch.NativeName))

        Return New ReadOnlyCollection(Of UILanguage)(availableLanguageList)
    End Function

    Public Shared Sub InitializeUILanguage()
        Dim cultureIndentifier As String
        Dim cultureInfo As CultureInfo
        cultureIndentifier = CurrentLanguageSetting
        Try
            cultureInfo = New CultureInfo(cultureIndentifier)
            Thread.CurrentThread.CurrentUICulture = cultureInfo
            cultureInfo.DefaultThreadCurrentUICulture = cultureInfo
        Catch ex As Exception
            Throw New MultiLanguageException(My.Resources.MultiLanguageSettings_LanguageNotLoaded)
        End Try
    End Sub

End Class