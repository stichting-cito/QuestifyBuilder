Imports System.ComponentModel
Imports System.IO
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.UI

Public Class PackageExportOptionsControl


    Private ReadOnly _options As PackageExportOptionsDataEntity



    Public Sub New()
        InitializeComponent()

        _options = New PackageExportOptionsDataEntity
    End Sub



    Private ReadOnly Property Wizard() As WizardBase
        Get
            Return DirectCast(Me.Parent.Parent.Parent.Parent.Parent, WizardBase)
        End Get
    End Property



    Public ReadOnly Property Options() As PackageExportOptionsDataEntity
        Get
            Return _options
        End Get
    End Property



    Private Sub BrowseButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BrowseButton.Click
        Dim sfd As New SaveFileDialog

        If Not String.IsNullOrEmpty(Me.PackageUrlTextBox.Text) Then
            sfd.FileName = Path.GetFileName(Me.PackageUrlTextBox.Text)
        End If

        If sfd.ShowDialog = DialogResult.OK Then
            _options.OverwriteExisting = True
            _options.PackageUrl = sfd.FileName

            PackageExportOptionsDataEntityBindingSource.ResetCurrentItem()
            TestBuilderClientSettings.ExportLocation = Path.GetDirectoryName(_options.PackageUrl)
        End If
    End Sub

    Private Sub PackageOutputOptionsControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim bankName = BankFactory.Instance.GetBankName(Me.Wizard.BankId)
        If String.IsNullOrEmpty(TestBuilderClientSettings.ExportLocation) Then
            Me.PackageUrlTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                                        $"{RemoveInvalidFileNameCharacters(bankName)}.export")
            Me._options.PackageUrl = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                                     $"{RemoveInvalidFileNameCharacters(bankName)}.export")
        Else
            Me.PackageUrlTextBox.Text = Path.Combine(TestBuilderClientSettings.ExportLocation, $"{bankName}.export")
            Me._options.PackageUrl = Path.Combine(TestBuilderClientSettings.ExportLocation, $"{bankName}.export")
        End If

        PackageExportOptionsDataEntityBindingSource.DataSource = _options

        Dim exportBank As Boolean
        If Me.ConfigurationOptions.ContainsKey("exportBank") AndAlso Boolean.TryParse(Me.ConfigurationOptions("exportBank"), exportBank) Then
            ExportSubBanksCheckBox.Enabled = exportBank
        End If
    End Sub

    Private Sub PackageOutputOptionsControl_Validating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles MyBase.Validating
        If _options IsNot Nothing Then
            Dim dataErrorInfo As IDataErrorInfo = DirectCast(_options, IDataErrorInfo)
            Me.ErrorMessage = dataErrorInfo.Error
        End If
    End Sub


    Private Function RemoveInvalidFileNameCharacters(ByVal proposedFileName As String) As String
        Dim illegalChars() As Char = Path.GetInvalidFileNameChars
        Dim newFileName As String = proposedFileName
        For Each invalidChar As Char In illegalChars
            newFileName = newFileName.Replace(invalidChar, "_")
        Next
        Return newFileName
    End Function


End Class