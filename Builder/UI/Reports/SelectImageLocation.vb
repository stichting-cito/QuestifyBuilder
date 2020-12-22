Imports System.Windows.Forms
Imports Questify.Builder.Configuration

Public Class SelectImageLocation


    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseButton.Click
        If Not String.IsNullOrEmpty(FileNameTextBox.Text) Then
            FolderBrowserDialog.SelectedPath = FileNameTextBox.Text
        End If
        If FolderBrowserDialog.ShowDialog = DialogResult.OK Then
            FileNameTextBox.Text = FolderBrowserDialog.SelectedPath
            ReportSettings.WordReport = FileNameTextBox.Text
        End If
        Me.ValidateChildren()
    End Sub




    Public Sub New()
        InitializeComponent()


    End Sub



End Class
