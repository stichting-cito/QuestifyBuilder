Imports System.Text
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.CustomInteractions

Public Class NewGenericResourceDialog


    Private ReadOnly _filter As String
    Private ReadOnly _shouldShowBrowseField As Boolean
    Private _resource As New GenericResourceEntity(Guid.NewGuid())
    Private _mimeType As String



    Public Sub New(ByVal bankId As Integer, ByVal filter As String, ByVal shouldShowBrowseField As Boolean)
        InitializeComponent()

        With _resource
            .Name = String.Empty
            .Title = String.Empty
            .Description = String.Empty
            .Version = "0.1"
            .BankId = bankId
        End With
        GenericResourceBindingSource.DataSource = _resource

        _filter = filter
        _shouldShowBrowseField = shouldShowBrowseField

        Debug.Assert(shouldShowBrowseField AndAlso _filter.ToLower().Contains("text"), "Is this correct???")

        If shouldShowBrowseField Then
            BrowseLabel.Visible = True
            BrowseTextBox.Visible = True
            BrowseButton.Visible = True
            SetBrowseError()
        End If
    End Sub



    Public ReadOnly Property Resource() As GenericResourceEntity
        Get
            Return _resource
        End Get
    End Property



    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseButton.Click
        Dim dlg As New OpenFileDialog()
        dlg.Title = My.Resources.NewGenericResourceDialogTitle
        dlg.CheckFileExists = True

        Dim result As New List(Of String)
        Dim filters = _filter.Split("|"c).Select(Function(f) f.ToLower())

        If filters.Contains("audio/ogg") Then result.Add(My.Resources.AudioOggFilter)
        If filters.Contains("audio/mpeg") OrElse filters.Contains("audio/mp3") Then result.Add(My.Resources.AudioFilter)

        If filters.Count = 1 AndAlso filters.First.ToLower = "image/svg+xml" Then
            result.Add(My.Resources.SvgFilter)
        ElseIf filters.Any(Function(f) f.Contains("image")) Then
            result.Add(My.Resources.ImageFilter)
        End If
        If filters.Contains("documents") Then result.Add(My.Resources.DocumentFilter)

        If filters.Contains("video/webm") Then result.Add(My.Resources.VideoWebmFilter)
        If filters.Contains("video/mp4") Then result.Add(My.Resources.VideoMp4Filter)
        If filters.Contains("video/mpeg") Then result.Add(My.Resources.VideoMpegFilter)

        If filters.Contains("application/pdf") Then result.Add(My.Resources.PdfFilter)

        If filters.Contains("text/html") Then result.Add(My.Resources.HtmlFilesFilter)

        If filters.Contains("application/x-custominteraction") Then result.Add(My.Resources.CiFilter)

        If filters.Contains("application/x-portablecustominteraction") Then result.Add(My.Resources.PortableCiFilter)

        If filters.Contains("application/vnd.geogebra.file") Then result.Add(My.Resources.GgbFilter)

        If result.Count = 0 Then result.Add(My.Resources.AllFilesFilter)
        dlg.Filter = String.Join("|", result.ToArray())

        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            BrowseTextBox.Text = dlg.FileName
            CodeTextBox.Text = System.IO.Path.GetFileName(dlg.FileName)
            TitleTextBox.Text = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName)
            _mimeType = FileHelper.GetMimeFromFile(dlg.FileName)
            ResourceDataErrorProvider.Clear()
        End If
    End Sub

    Private Sub SetBrowseError()
        Dim err As String = String.Format(My.Resources.FieldIsRequired, BrowseLabel.Text)
        ResourceDataErrorProvider.SetError(BrowseButton, err)
    End Sub

    Private Sub CurrentItemChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GenericResourceBindingSource.CurrentItemChanged
        ResourceDataErrorProvider.Clear()

        If String.IsNullOrWhiteSpace(_resource.Title) Then
            Dim errMsg As String = String.Format(My.Resources.FieldIsRequired, TitleLabel.Text.Replace(":", ""))
            _resource.SetEntityFieldError("Title", errMsg, False)
        End If

        If String.IsNullOrWhiteSpace(_resource.Name) Then
            Dim codeErrMsg As String = String.Format(My.Resources.FieldIsRequired, CodeLabel.Text.Replace(":", ""))
            _resource.SetEntityFieldError("Name", codeErrMsg, False)
        End If

        If String.IsNullOrWhiteSpace(BrowseTextBox.Text) Then
            SetBrowseError()
        End If
    End Sub

    Private Sub SetControlError(ByVal controlName As String, ByVal control As Control, ByVal errorMessage As String)
        Dim err As String = String.Format(errorMessage, controlName)
        ResourceDataErrorProvider.SetError(control, err)
    End Sub

    Private Function IsFormValid() As Boolean
        Dim errors As New StringBuilder
        Dim result = GetValidationErrors(errors)

        If errors.Length > 0 Then
            MessageBox.Show(errors.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        Return result
    End Function

    Private Function GetValidationErrors(errors As StringBuilder) As Boolean
        Dim result As Boolean = True

        If (BrowseTextBox.Visible = True) Then
            If String.IsNullOrWhiteSpace(BrowseTextBox.Text) Then
                SetBrowseError()
                errors.Append(ResourceDataErrorProvider.GetError(BrowseButton) + Environment.NewLine)
            End If
        End If

        Dim codeError As String = ValidationHelper.IsValidResourceCode(CodeTextBox.Text)
        If Not String.IsNullOrEmpty(codeError) Then
            ResourceDataErrorProvider.SetError(CodeTextBox, codeError)
            errors.Append(ResourceDataErrorProvider.GetError(CodeTextBox) + Environment.NewLine)
        End If

        If Not String.IsNullOrWhiteSpace(BrowseTextBox.Text) Then
            Dim selectedFileUri As New Uri(BrowseTextBox.Text)
            Dim rawBytes As Byte() = Nothing
            Dim mimeType As String = FileHelper.GetMimeFromFile(selectedFileUri.LocalPath)

            Dim checkExtensionError As String = LogicFileHelper.ValidateGenericResourceToBeImportedIntoBank_FileExtension(selectedFileUri, rawBytes, CodeTextBox.Text)
            If Not String.IsNullOrEmpty(checkExtensionError) Then
                ResourceDataErrorProvider.SetError(CodeTextBox, checkExtensionError)
                errors.Append(checkExtensionError + Environment.NewLine)
            End If

            Dim checkFileSizeError As String = LogicFileHelper.ValidateGenericResourceToBeImportedIntoBank_FileSize(selectedFileUri, rawBytes, mimeType)
            If Not String.IsNullOrEmpty(checkFileSizeError) AndAlso MessageBox.Show(checkFileSizeError, String.Empty, MessageBoxButtons.YesNo) = DialogResult.No Then
                result = False
            End If

            Array.Resize(rawBytes, 0)

            If FileHelper.MimeTypes.ContainsValue(mimeType) Then
                Dim errorMessages As New List(Of String)()
                Dim fileName As String = selectedFileUri.LocalPath

                If mimeType = "application/x-customInteraction" AndAlso Not New CiPackageValidator(fileName).TryValidate(errorMessages) Then
                    AddToErrors(fileName, errorMessages, errors)
                    result = False
                ElseIf mimeType = "application/x-zip-compressed" Then
                    If QuestifyThemeValidator.TryValidate(fileName, errorMessages) Then
                        mimeType = LogicFileHelper.QuestifyThemeMimeType
                    Else
                        AddToErrors(fileName, errorMessages, errors)
                        result = False
                    End If
                ElseIf mimeType = "application/x-portableCustomInteraction" AndAlso Not New PciPackageValidator(fileName).TryValidate(errorMessages) Then
                    AddToErrors(fileName, errorMessages, errors)
                    result = False
                End If
            End If
        End If

        With DirectCast(_resource, System.ComponentModel.IDataErrorInfo)
            If Not (.Item(GenericResourceFields.Title.Name)) Is Nothing Then errors.Append(.Item(GenericResourceFields.Title.Name) + Environment.NewLine)

            If .Item(GenericResourceFields.Title.Name) Is Nothing AndAlso String.IsNullOrWhiteSpace(TitleTextBox.Text) Then
                SetControlError(TitleLabel.Text, TitleTextBox, My.Resources.FieldIsRequired)
                errors.Append(ResourceDataErrorProvider.GetError(TitleTextBox) + Environment.NewLine)
            End If
        End With

        Return (result AndAlso errors.Length = 0)
    End Function

    Private Sub AddToErrors(file As String, errorMessages As List(Of String), errors As StringBuilder)
        If errorMessages.Any() Then
            errors.Append(file + Environment.NewLine)
        End If
        errorMessages.ForEach(Sub(e)
                                  errors.Append(e + Environment.NewLine)
                              End Sub)
    End Sub



    Protected Overrides Function OnOk() As Boolean
        If Not (IsFormValid()) Then Return False


        With _resource
            .ResourceData = New ResourceDataEntity()

            If _shouldShowBrowseField Then
                .SetResource(BrowseTextBox.Text)
            Else
                .MediaType = "application/xhtml+xml"
                .Size = 1
            End If
        End With

        Return True
    End Function


End Class