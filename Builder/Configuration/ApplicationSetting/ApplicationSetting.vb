Imports System.Configuration

Public Class ApplicationSetting

    Private ReadOnly _settingName As String = String.Empty
    Private ReadOnly _settingNameDefault As String = String.Empty
    Private ReadOnly _settingValueDefault As String = String.Empty

    Public Sub New(settingName As String, settingValueDefault As String)
        _settingName = $"setting_{settingName}"
        _settingNameDefault = $"setting_default_{settingName}"
        _settingValueDefault = settingValueDefault
    End Sub

    Public Function GetValue() As String

        If DefaultHasChanged() Then
            SaveSettingValueDefault(_settingValueDefault)
            SaveSettingValue(_settingValueDefault)
        End If

        Return GetSettingValueFromUserSettings(_settingName)
    End Function

    Public Sub SaveSettingValue(settingValue As String)
        CreateSettingWhenNotExists(_settingName)
        My.Settings(_settingName) = settingValue
    End Sub

    Private Function DefaultHasChanged() As Boolean
        Dim settingValueDefault As String = GetSettingValueFromUserSettings(_settingNameDefault)
        Return settingValueDefault <> _settingValueDefault
    End Function

    Private Sub SaveSettingValueDefault(settingValueDefault As String)
        CreateSettingWhenNotExists(_settingNameDefault)
        My.Settings(_settingNameDefault) = settingValueDefault
    End Sub

    Private Function GetSettingValueFromUserSettings(settingName As String) As String
        Dim returnValue As String
        CreateSettingWhenNotExists(settingName)
        returnValue = My.Settings(settingName).ToString()
        Return returnValue
    End Function

    Private Sub CreateSettingWhenNotExists(settingName As String)
        If My.Settings.Properties.Item(settingName) Is Nothing Then
            Dim previewProperty As SettingsProperty = New SettingsProperty(My.Settings.Properties("Base"))
            previewProperty.Name = settingName
            My.Settings.Properties.Add(previewProperty)
            My.Settings.Reload()
        End If
    End Sub

End Class
