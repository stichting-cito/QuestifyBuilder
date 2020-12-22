Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Janus.Windows.GridEX
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.UI
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.CustomInteractions
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ResourceManager

Public Class AddGenericResourceWizardForm
    Private ReadOnly _validSelectedFiles As New List(Of String)
    Private ReadOnly _invalidSelectedFiles As New Dictionary(Of String, List(Of String))

    Private _selectedImportMethod As ImportMethod
    Private _selectedTemplateEntity As GenericResourceDto = Nothing

    Private ReadOnly _addedGenericResources As New List(Of GenericResourceEntity)()
    Private _addedTemplateResource As GenericResourceEntity
    Private ReadOnly _filesSizes As New Dictionary(Of Uri, Size)

    Private Const C_TESTBUILDER_SCHEME = "testbuilder"


    Public Sub New()
        InitializeComponent()
        Me.InitTabControl()
    End Sub

    Public Sub New(ByVal bankId As Integer)
        Me.New()

        Me.BankId = bankId
    End Sub



    Private Sub AddGenericResourceWizardForm_WizardBeforeTabChange(ByVal sender As Object, ByVal e As WizardBeforeTabChangeEventArgs) Handles Me.WizardBeforeTabChange
        Select Case e.CurrentTab.Tag.ToString
            Case "SelectImportMethod"
                If ImportRadioButton.Checked Then
                    _selectedImportMethod = ImportMethod.Import
                    e.NextTab = TabControlMain.TabPages("FileSelectionTab")
                ElseIf TemplatedRadioButton.Checked Then
                    _selectedImportMethod = ImportMethod.Templated
                    e.NextTab = TabControlMain.TabPages("SelectTemplateTab")
                End If

            Case "FileSelectionTab"
                If GenericGridEX.RowCount = 0 Then
                    MessageBox.Show(My.Resources.WizardForm_SelectAFileMessage, My.Resources.WizardForm_SelectAFileTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    e.Cancel = True
                Else
                    e.NextTab = TabControlMain.TabPages("SelectedFilesTab")
                End If

            Case "SelectTemplateTab"
                If _selectedTemplateEntity Is Nothing Then
                    MessageBox.Show(My.Resources.WizardForm_SelectAnObject)
                    e.Cancel = True
                End If

            Case "SelectedFilesTab"
                For Each row As GridEXRow In GenericGridEX.GetRows
                    If String.IsNullOrEmpty(row.Cells("Code").Text) Then
                        MessageBox.Show(My.Resources.WizardForm_CodeNotFilled)
                        e.Cancel = True
                        Return
                    End If
                    If String.IsNullOrEmpty(row.Cells("Title").Text) Then
                        MessageBox.Show(My.Resources.WizardForm_TitleNotFilled)
                        e.Cancel = True
                        Return
                    End If
                    If String.IsNullOrEmpty(row.Cells("MediaType").Text) Then
                        MessageBox.Show(My.Resources.WizardForm_TypeNotFilled)
                        e.Cancel = True
                        Return
                    End If

                    Dim selectedFileUri As New Uri(row.Cells("Path").Text)
                    If selectedFileUri.IsFile AndAlso Not selectedFileUri.Scheme = C_TESTBUILDER_SCHEME Then
                        Dim rawBytes As Byte() = Nothing

                        Dim checkExtension As String = LogicFileHelper.ValidateGenericResourceToBeImportedIntoBank_FileExtension(selectedFileUri, rawBytes, row.Cells("Code").Text)
                        If Not String.IsNullOrEmpty(checkExtension) Then
                            MessageBox.Show(checkExtension)
                            e.Cancel = True
                            Return
                        End If

                        Dim checkFileSize As String = LogicFileHelper.ValidateGenericResourceToBeImportedIntoBank_FileSize(selectedFileUri, rawBytes, row.Cells("MediaType").Text, _filesSizes)
                        If Not String.IsNullOrEmpty(checkFileSize) AndAlso MessageBox.Show(checkFileSize, String.Empty, MessageBoxButtons.YesNo) = DialogResult.No Then
                            e.Cancel = True
                        End If

                        rawBytes = Nothing
                    End If
                Next

                e.NextTab = Me.GetProcessingTab
        End Select
    End Sub

    Private Sub AddGenericResourceWizardForm_WizardCompleted(ByVal sender As Object, ByVal e As EventArgs) Handles Me.WizardCompleted
        Me.ResultTabContent.ResultText = String.Format(My.Resources.TheOperationIsSuccesfulyCompleted)
    End Sub

    Private Sub AddGenericResourceWizardForm_WizardDoProcess(ByVal sender As Object, ByVal e As WizardProcessEventArgs) Handles Me.WizardDoProcess
        ProcessTabContent.ProgressMinimumValueDetail = 0
        ProcessTabContent.ProgressMaximumValueDetail = GenericGridEX.GetRows.Length + 1
        ProcessTabContent.ProgressValueDetail = 0

        Application.DoEvents()

        Select Case _selectedImportMethod
            Case ImportMethod.Import
                If SaveResource() Then
                    ProcessTabContent.ProgressValueDetail = ProcessTabContent.ProgressMaximumValueDetail
                    ProcessTabContent.ProcessInfoTextDetail = My.Resources.Completed
                    Thread.Sleep(1000)
                Else
                    e.Cancel = True
                End If

            Case ImportMethod.Templated
                If SaveTemplatedResource() Then
                    ProcessTabContent.ProgressValueDetail = ProcessTabContent.ProgressMaximumValueDetail
                    ProcessTabContent.ProcessInfoTextDetail = My.Resources.Completed
                    Thread.Sleep(1000)
                Else
                    e.Cancel = True
                End If
        End Select

        Application.DoEvents()
    End Sub

    Private Sub AddGenericResourceWizardForm_WizardTabChanged(ByVal sender As Object, ByVal e As WizardTabChangedEventArgs) Handles Me.WizardTabChanged
        Select Case e.CurrentTab.Tag.ToString
            Case "WelcomeTab"
                WelcomeTabContent.Title = My.Resources.AddingMediaFile
                WelcomeTabContent.Description = String.Format(My.Resources.WizardWelcomeTabDescription, My.Resources.MediaFileS)

            Case "SelectImportMethod"
                SelectMethodTabContent.Task = My.Resources.WizardSelectMethodTask
                SelectMethodTabContent.TaskDescription = String.Empty

            Case "OverviewTab"
                OverviewTabContent.Task = My.Resources.CompleteTheWizard
                OverviewTabContent.TaskDescription = My.Resources.VerifyTheChoicesMadeInTheWizard
                OverviewTabContent.OverviewText =
                    $"{My.Resources.MediaFileToBeAdded} {Environment.NewLine}{GetMediaFilesToAddAsText()}"

            Case "ProcessTab"
                ProcessTabContent.Task = My.Resources.PerformingOperation
                ProcessTabContent.TaskDescription = String.Empty

            Case "ResultTab"
                ResultTabContent.Task = My.Resources.OperationSuccesfull
                ResultTabContent.TaskDescription = String.Format(My.Resources.WizardResultTabTaskDescription, My.Resources.MediaFile)
                ResultTabContent.ResultText = String.Empty

            Case "FileSelectionTab"
                FileSelectionTabContent.Task = My.Resources.SelectFilesToAdd
                FileSelectionTabContent.TaskDescription = String.Format(My.Resources.WizardFileSelectionTabTaskDescription2, My.Resources.MediaFile)

                SelectedFileInfoTextBox.Text = GetSelectFileText()

            Case "SelectedFilesTab"
                SelectedFilesTabContent.Task = My.Resources.AlterOrDeleteSelectedFiles
                SelectedFilesTabContent.TaskDescription = String.Format(My.Resources.WizardSelectedFilesTabTaskDescription, My.Resources.MediaFile)

            Case "SelectTemplateTab"
                SelectTemplateResourceTabContent.Task = My.Resources.WizardSelectTemplateTask
                SelectTemplateResourceTabContent.TaskDescription = My.Resources.WizardSelectTemplateTaskDescription

        End Select
    End Sub



    Private Function GetMediaFilesToAddAsText() As String
        Dim returnText As New StringBuilder()

        For Each item As GridEXRow In GenericGridEX.GetRows()
            returnText.Append(
                $"{item.Cells("Code").Text} - {item.Cells("Path").Text}: {item.Cells("MediaType").Text}{ _
                                 Environment.NewLine}")
        Next
        Return returnText.ToString
    End Function

    Private Function AddFileToList(ByVal file As FileInfo) As Boolean
        Dim code As String = file.Name
        Dim title As String = file.Name.Substring(0, file.Name.Length - file.Extension.Length)
        Dim fileMimeType As String = FileHelper.GetMimeFromFile(file.FullName)
        Dim fullname As String = file.FullName

        Return AddObjectToList(code, title, fileMimeType, fullname, file.Extension)
    End Function


    Private Function AddObjectToList(ByVal code As String, ByVal title As String, ByVal mimeType As String, ByVal fullname As String, ByVal extension As String) As Boolean
        If String.IsNullOrEmpty(extension) Then
            _invalidSelectedFiles.Add(fullname, Nothing)
        ElseIf Not String.IsNullOrEmpty(ValidationHelper.IsValidResourceCode(title)) Then
            _invalidSelectedFiles.Add(fullname, Nothing)
            MessageBox.Show(My.Resources.TheFilenameCannotContainIllegalCharacters, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If FileHelper.MimeTypes.ContainsValue(mimeType) Then
                Dim errorMessages As New List(Of String)()

                If mimeType = "application/x-customInteraction" AndAlso Not New CiPackageValidator(fullname).TryValidate(errorMessages) Then
                    _invalidSelectedFiles.Add(fullname, errorMessages)
                    Return False
                ElseIf mimeType = "application/x-zip-compressed" Then
                    If QuestifyThemeValidator.TryValidate(fullname, errorMessages) Then
                        mimeType = LogicFileHelper.QuestifyThemeMimeType
                    ElseIf errorMessages.Count > 0 Then
                        _invalidSelectedFiles.Add(fullname, errorMessages)
                        Return False
                    End If
                ElseIf mimeType = "application/x-portableCustomInteraction" AndAlso Not New PciPackageValidator(fullname).TryValidate(errorMessages) Then
                    _invalidSelectedFiles.Add(fullname, errorMessages)
                    Return False
                End If

                Dim item As String() = {code, title, mimeType, String.Empty, fullname}

                GenericGridEX.AddItem(item)
                _validSelectedFiles.Add(fullname)
                For Each column As GridEXColumn In GenericGridEX.RootTable.Columns
                    column.AutoSize()
                Next
                Return True
            Else
                _invalidSelectedFiles.Add(fullname, Nothing)
            End If
        End If

        Return False
    End Function


    Private Function SaveResource() As Boolean
        Dim itemIndex As Integer
        For Each item As GridEXRow In GenericGridEX.GetRows()
            itemIndex += 1
            Dim selectedFileUri As New Uri(item.Cells("Path").Text)
            ProcessTabContent.ProgressValueDetail = itemIndex
            ProcessTabContent.ProcessInfoTextDetail = String.Format(My.Resources.Saving0312, item.Cells("Code").Text, itemIndex, GenericGridEX.RowCount, Environment.NewLine)
            Me.Refresh()
            Dim myNewResource As New GenericResourceEntity()

            With myNewResource
                .ResourceId = Guid.NewGuid()
                .Version = "0.1"
                .BankId = Me.BankId
                .Name = item.Cells("Code").Text
                .Title = item.Cells("Title").Text
                .Description = item.Cells("Description").Text
                .MediaType = item.Cells("MediaType").Text

                .ResourceData = New ResourceDataEntity()
                .ResourceData.BinData = FileHelper.MakeByteArrayFromFile(selectedFileUri.LocalPath)

                If .MediaType = "text/plain" Then .ResourceData.FileExtension = ".xml"
                .Size = CInt(.ResourceData.BinData.Length / 1024)

                If .Size = 0 Then
                    Dim realSize As Double = .ResourceData.BinData.Length / 1024
                    If realSize > 0 Then .Size = 1
                End If

                Dim helper As New MediaDimensionsHelper()
                Dim size As Size
                If _filesSizes.ContainsKey(selectedFileUri) Then
                    size = _filesSizes(selectedFileUri)
                ElseIf myNewResource.MediaType.Contains("video") Then
                    size = helper.GetVideoSize(selectedFileUri.LocalPath)
                Else
                    size = helper.GetDimensions(myNewResource.MediaType, myNewResource.ResourceData.BinData)
                End If
                If Not size.IsEmpty Then
                    If Not _filesSizes.ContainsKey(selectedFileUri) Then
                        _filesSizes.Add(selectedFileUri, size)
                    End If
                    .Dimensions = $"{size.Width} x {size.Height}"
                End If
            End With

            Dim result As String = ResourceFactory.Instance.UpdateGenericResource(myNewResource)
            If Not String.IsNullOrEmpty(result) Then
                MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            Else
                _addedGenericResources.Add(myNewResource)
            End If
        Next

        Return True

    End Function


    Private Function SaveTemplatedResource() As Boolean
        If GenericGridEX.GetRows.Length = 1 Then
            Dim item As GridEXRow = GenericGridEX.GetRows(0)
            Dim code As String = item.Cells("Code").Text
            Dim title As String = item.Cells("Title").Text
            Dim description As String = item.Cells("Description").Text
            Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
            Dim template As GenericResourceEntity = CType(ResourceFactory.Instance.GetResourceByIdWithOption(_selectedTemplateEntity.ResourceId, New GenericResourceEntityFactory(), request), GenericResourceEntity)
            If template IsNot Nothing AndAlso template.ResourceData Is Nothing Then
                template.ResourceData = ResourceFactory.Instance.GetResourceData(template)
            End If
            Dim myNewResource = template.CopyToNew(code)
            With myNewResource
                .BankId = Me.BankId
                .Title = title
                .Description = description
                .IsTemplate = False
            End With

            Dim result As String = ResourceFactory.Instance.UpdateGenericResource(myNewResource)
            If Not String.IsNullOrEmpty(result) Then
                MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            _addedTemplateResource = myNewResource
            Return True

        ElseIf GenericGridEX.GetRows.Length <> 0 Then
            Throw New InvalidOperationException("Not expecting multiple items in grid during templated resource import")
        End If
    End Function


    Private Sub BrowseButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BrowseButton.Click

        If SelectFileDialog.ShowDialog(Me) = DialogResult.OK Then
            _validSelectedFiles.Clear()
            _invalidSelectedFiles.Clear()
            GenericGridEX.ClearItems()
            For Each file As String In SelectFileDialog.FileNames
                AddFileToList(New FileInfo(file))
            Next
        End If
        SelectedFileInfoTextBox.Text = GetSelectFileText()
    End Sub


    Private Function GetSelectFileText() As String

        If Not _validSelectedFiles.Any() AndAlso Not _invalidSelectedFiles.Any() Then
            Return String.Empty
        End If

        Dim validFiles As String = String.Empty
        If _validSelectedFiles.Count = 1 Then
            validFiles = String.Format(My.Resources.SelectedSingleValidFile, _validSelectedFiles.FirstOrDefault())
        ElseIf _validSelectedFiles.Count > 1 Then
            validFiles = String.Format(My.Resources.SelectedMultipleValidFiles, _validSelectedFiles.Count, _validSelectedFiles.Select(Function(i) i).Aggregate(Function(i, j) i + Environment.NewLine + j))
        End If

        Dim notValidFiles As String = String.Empty
        If _invalidSelectedFiles.Count = 1 Then
            notValidFiles = String.Format(My.Resources.SelectedSingleInValidFile, CreateErrorMessage())
        ElseIf _invalidSelectedFiles.Count > 1 Then
            notValidFiles = String.Format(My.Resources.SelectedMultipleInValidFiles, _invalidSelectedFiles.Count, CreateErrorMessage())
        End If

        Return String.Concat(validFiles, vbCrLf, notValidFiles)
    End Function

    Private Function CreateErrorMessage() As String
        Dim builder As New StringBuilder()

        For Each kvp As KeyValuePair(Of String, List(Of String)) In _invalidSelectedFiles
            builder.Append(kvp.Key)
            builder.Append(vbNewLine)

            If kvp.Value IsNot Nothing Then
                For Each errormessage As String In kvp.Value
                    builder.Append(vbTab)
                    builder.Append(errormessage)
                    builder.Append(vbNewLine)
                Next
            End If
        Next

        Return builder.ToString()
    End Function

    Private Sub DeleteButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteButton.Click
        For index As Integer = 0 To GenericGridEX.SelectedItems.Count - 1
            _validSelectedFiles.Remove(GenericGridEX.SelectedItems(0).GetRow.Cells("Path").Text)
            GenericGridEX.SelectedItems(0).GetRow.Delete()
        Next
    End Sub

    Private Sub GenericGridEX_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GenericGridEX.SelectionChanged
        DeleteButton.Enabled = GenericGridEX.SelectedItems.Count > 0
    End Sub



    Public ReadOnly Property AddedGenericResources As List(Of GenericResourceEntity)
        Get
            Return _addedGenericResources
        End Get
    End Property

    Public ReadOnly Property AddedTemplateResource As GenericResourceEntity
        Get
            Return _addedTemplateResource
        End Get
    End Property

    Private Sub GenericGridEX_UpdatingCell(ByVal sender As Object, ByVal e As UpdatingCellEventArgs) Handles GenericGridEX.UpdatingCell
        If e.Column.Key = "Code" Then
            Dim value As String = e.Value.ToString()
            Dim illegalChars As String = "<>""#%{}|\/^~[]';?:@=&^$+()!,`* "
            If value.IndexOfAny(illegalChars.ToCharArray()) > -1 Then
                MessageBox.Show(My.Resources.WizardForm_CodeFieldCannotContainIllegalChars, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub SelectedFileInfoTextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles SelectedFileInfoTextBox.TextChanged
        If Me.Controls.Item("NextButton") IsNot Nothing Then
            Me.Controls.Item("NextButton").Enabled = Not _validSelectedFiles.Count = 0
        End If
    End Sub

    Private Sub SelectTemplateButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SelectTemplateButton.Click
        Using resourceManager As DataBaseResourceManager = New DataBaseResourceManager(Me.BankId)
            Using dialog As New SelectMediaResourceDialog(Me.BankId, , resourceManager)

                With dialog
                    .ShowAddNew = False
                    .FilterTemplatesOnly = True
                    .Filter = "text/plain|application/xhtml+xml|text/html"
                    .CanPickFiles = False
                End With

                Dim result As DialogResult = dialog.ShowDialog()

                If result = DialogResult.OK Then
                    If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.ResourceId) Then
                        _selectedTemplateEntity = dialog.SelectedEntity
                        _validSelectedFiles.Clear()
                        _invalidSelectedFiles.Clear()

                        GenericGridEX.ClearItems()

                        Dim uri = New Uri($"{C_TESTBUILDER_SCHEME}://{_selectedTemplateEntity.Name}").ToString()
                        AddObjectToList("newMediaObject.xhtml", "newMediaObject", _selectedTemplateEntity.MediaType, uri, New FileInfo(_selectedTemplateEntity.Name).Extension)

                        TemplateTextBox.Text = _selectedTemplateEntity.Name
                    Else
                        MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            End Using
        End Using
    End Sub

    Private Enum ImportMethod
        Import = 0
        Templated = 1
    End Enum

End Class

