Imports System.ComponentModel
Imports Questify.Builder.Configuration

Public Class ExcelImportOptionsControl

    Private ReadOnly _options As ExcelImportOptionsDataEntity

    Public Sub New()

        InitializeComponent()

        _options = New ExcelImportOptionsDataEntity
    End Sub

    Public ReadOnly Property Options() As ExcelImportOptionsDataEntity
        Get
            Return _options
        End Get
    End Property

    Private Sub BrowseButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BrowseButton.Click
        If String.IsNullOrEmpty(_options.Url) Then
            ExcelOpenFileDialog.InitialDirectory = TestBuilderClientSettings.ImportLocation
        Else
            ExcelOpenFileDialog.FileName = _options.Url
        End If

        Dim result As DialogResult = ExcelOpenFileDialog.ShowDialog()

        If result = DialogResult.OK Then
            _options.Url = ExcelOpenFileDialog.FileName
            ExcelImportOptionsDataEntityBindingSource.ResetCurrentItem()
        End If
    End Sub

    Private Sub ExcelImportOptionsControl_Validating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles MyBase.Validating
        If _options IsNot Nothing Then
            Dim dataErrorInfo As IDataErrorInfo = DirectCast(_options, IDataErrorInfo)
            Me.ErrorMessage = dataErrorInfo.Error
        End If
    End Sub

    Private Sub ExcelOutputOptionsControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ExcelImportOptionsDataEntityBindingSource.DataSource = _options

        ExcelUrlTextBox.Text = String.Empty
    End Sub

End Class
