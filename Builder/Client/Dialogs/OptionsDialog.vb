Imports System.Globalization
Imports Questify.Builder.Configuration

Public Class OptionsDialog

    Private Sub DialogCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DialogCancelButton.Click
        Me.Close()
    End Sub

    Private Sub DialogOkButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DialogOkButton.Click
        If IsLanguageSettingChanged() Then
            Me.DialogResult = MessageBox.Show(My.Resources.OptionsForm_RestartLanguage_Text, My.Resources.OptionsForm_RestartLanguage_Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            If Me.DialogResult = DialogResult.OK Then
                SaveSettings()
            End If
        Else
            Me.DialogResult = DialogResult.Cancel
        End If

        Me.Close()
    End Sub

    Private Sub OptionsDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        CurrentLanguageTextBox.Text = New CultureInfo(MultiLanguageController.CurrentLanguageSetting).NativeName
        With availableLanguagesList
            .DataSource = MultiLanguageController.GetAvailableLanguages()
            .SelectedValue = -1
        End With
    End Sub

    Private Function IsLanguageSettingChanged() As Boolean
        Dim currentLanguageSetting As String = MultiLanguageController.CurrentLanguageSetting
        Dim selectedLanguage As UILanguage = CType(availableLanguagesList.SelectedItem, UILanguage)

        If selectedLanguage IsNot Nothing Then
            Return currentLanguageSetting <> selectedLanguage.CultureIdentifier
        Else
            Return False
        End If
    End Function


    Private Sub SaveSettings()
        If Not (availableLanguagesList.SelectedItem Is Nothing) Then MultiLanguageController.CurrentLanguageSetting = DirectCast(availableLanguagesList.SelectedValue, String)
        ApplicationSettings.SaveUserSettings()
    End Sub
End Class