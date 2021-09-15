
Imports System.ComponentModel
Imports Questify.Builder.Configuration
Imports Questify.Builder.Packaging
Imports Questify.Builder.UI.HelperClasses
Imports Questify.Builder.Security

Public Class PackageImportOptionsControl

    Private WithEvents RM As ResourceManifest

    ''' <summary>
    ''' Holds value whether package is encryted or not
    ''' </summary>
    Private ReadOnly _options As PackageImportOptionsDataEntity

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        PackageUrlTextBox.EnableFileNameDrop()
        ' Add any initialization after the InitializeComponent() call.
        _options = New PackageImportOptionsDataEntity
    End Sub

    Public ReadOnly Property Options() As PackageImportOptionsDataEntity
        Get
            Return _options
        End Get
    End Property

    Private Sub BrowseButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BrowseButton.Click
        'set initial path of filename
        If String.IsNullOrEmpty(_options.Url) Then
            PackageOpenFileDialog.InitialDirectory = TestBuilderClientSettings.ImportLocation
        Else
            PackageOpenFileDialog.FileName = _options.Url
        End If

        Dim result As DialogResult = PackageOpenFileDialog.ShowDialog()

        If result = DialogResult.OK Then
            _options.Url = PackageOpenFileDialog.FileName
            PackageImportOptionsDataEntityBindingSource.ResetCurrentItem()
        End If
    End Sub

    Private Sub PackageImportOptionsControl_Validating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles MyBase.Validating
        If _options IsNot Nothing Then
            Dim dataErrorInfo As IDataErrorInfo = DirectCast(_options, IDataErrorInfo)
            Me.ErrorMessage = dataErrorInfo.Error
        End If
    End Sub

    Private Sub PackageOutputOptionsControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        PackageImportOptionsDataEntityBindingSource.DataSource = _options

        'force change
        PackageUrlTextBox.Text = String.Empty
    End Sub

    Private Sub PackageUrlTextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles PackageUrlTextBox.TextChanged
        Dim tb As TextBox = CType(sender, TextBox)

        If tb.Text.EndsWith(".exportset", StringComparison.InvariantCultureIgnoreCase) AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.AddNew, TestBuilderPermissionTarget.BankEntity, 0) Then
            ImportToRootCheckBox.Visible = True
        Else
            ImportToRootCheckBox.Visible = False
        End If
    End Sub

End Class