
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.Service.Interfaces

Namespace Publication.UI

    Public Class SelectPublicationOptions

        Private ReadOnly _selectionToPublish As IPublicationSelection
        Private ReadOnly _handler As IPublicationHandler

        Private ReadOnly Property DefaultPackageName As String
            Get
                Return _selectionToPublish.DefaultPublicationName(_handler.FileExtension)
            End Get
        End Property
        Public Property PublicationPath As String

        Public Property PackageName As String

        Public Sub New(selection As IPublicationSelection, publicationHandler As IPublicationHandler)

            InitializeComponent()

            _selectionToPublish = selection
            _handler = publicationHandler
            PublicationFolderTextBox.Text = GetPublishLocation()
            TextBoxPackageName.Text = DefaultPackageName
            Me.PublicationOptionsTabContent.Task = My.Resources.PublicationOptions
            Me.PublicationOptionsTabContent.TaskDescription = ""
            Me.CheckBoxSpecifyPackageName.Text = My.Resources.SpecifyPackageName
            Me.PublicationFolderLabel.Text = My.Resources.PublicationOptions
        End Sub

        Private Sub PublicationBrowseButton_Click(sender As Object, e As EventArgs) Handles PublicationBrowseButton.Click
            If PublicationPath Is Nothing Then PublicationPath = GetPublishLocation()

            Using fbd As New FolderBrowserDialog
                If Not PublicationPath Is Nothing Then
                    fbd.SelectedPath = PublicationPath
                End If
                If fbd.ShowDialog(Me) = DialogResult.OK Then
                    If Not fbd.SelectedPath.EndsWith(Path.DirectorySeparatorChar) Then
                        fbd.SelectedPath = fbd.SelectedPath + Path.DirectorySeparatorChar
                    End If
                    PublicationFolderTextBox.Text = fbd.SelectedPath
                    PublicationPath = fbd.SelectedPath
                    SetPublishLocation(PublicationPath)
                    Me.Validate()
                End If
            End Using
        End Sub

        Public Function IsValid() As Boolean
            PublicationPath = PublicationFolderTextBox.Text
            PackageName = TextBoxPackageName.Text
            SetPublishLocation(PublicationPath)
            If Not ValidatePublicationFileName() Then
                Return False
            End If

            If String.IsNullOrWhiteSpace(PublicationPath) OrElse (String.IsNullOrWhiteSpace(PackageName) AndAlso _selectionToPublish.TestNames.Count = 1) Then
                MessageBox.Show(String.Format(My.Resources.PublicationLocationText, My.Resources.Publication, My.Resources.PleaseEnterACompleteAndValidPath))
                Return False
            End If
            Return True
        End Function
        Private Sub SetPublishLocation(strPathOnly As String)
            If String.Compare(strPathOnly, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), True) = 0 Then
                TestBuilderClientSettings.PublishLocation = ""
            Else
                TestBuilderClientSettings.PublishLocation = strPathOnly
            End If
        End Sub
        Private Function GetPublishLocation() As String
            Dim strPath As String = PublicationPath
            If String.IsNullOrWhiteSpace(strPath) Then
                strPath = TestBuilderClientSettings.PublishLocation
                If String.IsNullOrWhiteSpace(strPath) Then
                    strPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                End If
            Else
                SetPublishLocation(strPath)
            End If
            Return strPath
        End Function

        Private Function ValidatePublicationFileName() As Boolean
            If Not PublicationBrowseButton.Visible Then
                Return True
            ElseIf String.IsNullOrEmpty(PublicationFolderTextBox.Text) Then
                OptionsErrorProvider.SetError(PublicationFolderTextBox, String.Format(My.Resources.FieldMustBeFilled, PublicationFolderLabel.Text))
            ElseIf Not Directory.Exists(PublicationFolderTextBox.Text) Then
                OptionsErrorProvider.SetError(PublicationFolderTextBox, My.Resources.PleaseEnterAValidExportFilename)
            ElseIf _selectionToPublish.TestNames.Count() = 1 AndAlso String.IsNullOrEmpty(TextBoxPackageName.Text) Then
                OptionsErrorProvider.SetError(TextBoxPackageName, String.Format(My.Resources.FieldMustBeFilled, PublicationFolderLabel.Text))
            ElseIf _selectionToPublish.TestNames.Count() = 1 AndAlso TextBoxPackageName.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 Then
                OptionsErrorProvider.SetError(TextBoxPackageName, String.Format(My.Resources.TheFilenameCannotContainIllegalCharacters, PublicationFolderLabel.Text))
            ElseIf (_selectionToPublish.TestNames.Count = 1 OrElse _selectionToPublish.TestPackageNames.Count = 1) AndAlso File.Exists(Path.Combine(PublicationFolderTextBox.Text, TextBoxPackageName.Text)) Then
                OptionsErrorProvider.SetError(PublicationFolderTextBox, My.Resources.FileAlreadyExist)
            Else
                OptionsErrorProvider.SetError(PublicationFolderTextBox, String.Empty)
                Return True
            End If
            Return False
        End Function

        Friend WithEvents OptionsErrorProvider As ErrorProvider = new ErrorProvider()

        Private Sub CheckBoxSpecifyPackageName_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSpecifyPackageName.CheckedChanged
            TextBoxPackageName.Enabled = (CheckBoxSpecifyPackageName.Checked)
            If Not CheckBoxSpecifyPackageName.Checked Then
                TextBoxPackageName.Text = DefaultPackageName
            End If
        End Sub

    End Class
End NameSpace