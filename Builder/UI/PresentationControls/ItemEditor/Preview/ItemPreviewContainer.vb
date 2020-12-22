Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.Service.Enums
Imports Questify.Builder.Logic.Service.EventArguments
Imports Questify.Builder.Logic.Service.Interfaces

Public Class ItemPreviewContainer

    Private _previewerChromium As Chromium_ItemPreviewer
    Private _previewerWord As Word_ItemPreviewer
    Private _assessmentItem As AssessmentItem
    Private _bankId As Integer
    Private _resourceManager As ResourceManagerBase
    Private _allAvailableItemPreviewHandlers As List(Of IItemPreviewHandler)

    Public Sub New()
        InitializeComponent()
        ItempreviewCombobox.DisableScrollWheel()
        InitializePreviewers()
    End Sub

    Public Event ItemValidatingRequired(ByVal sender As Object, ByVal e As ItemValidationRequiredEventArgs)
    Public Event RefreshPreview(ByVal sender As Object, ByVal e As EventArgs)

    Private ReadOnly Property CurrentPreviewHandler As IItemPreviewHandler
        Get
            If ItempreviewCombobox.Tag IsNot Nothing AndAlso TypeOf ItempreviewCombobox.Tag Is IItemPreviewHandler Then
                Return DirectCast(ItempreviewCombobox.Tag, IItemPreviewHandler)
            End If
            Return Nothing
        End Get
    End Property


    Public Property FormIsClosing As Boolean = False

    Private ReadOnly Property CurrentPreviewer As IItemPreviewer
        Get
            Select Case CurrentPreviewHandler.PreviewControl
                Case PreviewControl.Chromium
                    Return _previewerChromium
                Case PreviewControl.Word
                    Return _previewerWord
            End Select
            Return Nothing
        End Get
    End Property


    Public Sub PreviewItem(assessmentItem As AssessmentItem, bankId As Integer,
                          contextIdentifierForItemViewer As Integer?,
                          resourceManager As ResourceManagerBase)
        _assessmentItem = assessmentItem
        _bankId = bankId
        _resourceManager = resourceManager
        SetupPreviewers()
        StartPreview(False)
    End Sub

    Public Sub StopPreview()
        If CurrentPreviewHandler IsNot Nothing AndAlso CurrentPreviewer IsNot Nothing Then
            CurrentPreviewer.StopItemPreview(CurrentPreviewHandler)
        End If
    End Sub



    Private Sub StartPreview(force As Boolean)
        If ValidateParameters() AndAlso CurrentPreviewHandler IsNot Nothing AndAlso CurrentPreviewer IsNot Nothing Then
            CurrentPreviewer.PreviewItem(CurrentPreviewHandler, _bankId, _assessmentItem, force)
            ShowPreviewer()
        End If
    End Sub

    Private Sub ShowPreviewer()
        _previewerWord.Visible = False
        _previewerChromium.Visible = False
        If CurrentPreviewHandler IsNot Nothing Then
            Select Case CurrentPreviewHandler.PreviewControl
                Case PreviewControl.Word
                    _previewerWord.Visible = True
                    _previewerWord.BringToFront()
                Case PreviewControl.Chromium
                    _previewerChromium.Visible = True
                    _previewerChromium.borderPanel.Enabled = False
                    _previewerChromium.BringToFront()
            End Select
        End If
    End Sub

    Private Sub ItemValidatingProxy(ByVal sender As Object, ByVal e As ItemValidationRequiredEventArgs)
        OnItemValidatingRequired(e)
    End Sub

    Private Sub InitializePreviewers()
        _previewerWord = New Word_ItemPreviewer : AddPreviewer(_previewerWord)
        _previewerChromium = New Chromium_ItemPreviewer : AddPreviewer(_previewerChromium)
    End Sub

    Private Sub AddPreviewer(previewer As IItemPreviewer)
        AddHandler previewer.ItemValidatingRequired, AddressOf ItemValidatingProxy
        Dim uiControl As UserControl = CType(previewer, UserControl)
        PreviewerTableLayoutPanel.Controls.Add(uiControl, 0, 1)
        PreviewerTableLayoutPanel.SetColumnSpan(uiControl, 2)
        uiControl.Dock = DockStyle.Fill
        uiControl.Visible = False
    End Sub

    Private Sub SetupPreviewers()
        _previewerWord.Visible = False
        _previewerChromium.Visible = False
        Dim itemAdapter = New ItemLayoutAdapter(_assessmentItem.LayoutTemplateSourceName, _assessmentItem.Parameters, AddressOf Generic_ResourceNeeded)

        Dim previewHandlers As New Dictionary(Of String, IItemPreviewHandler)
        If _allAvailableItemPreviewHandlers Is Nothing Then
            _allAvailableItemPreviewHandlers = GeneralHelper.CreateItemPreviewHandlers(_resourceManager)
        End If

        For Each target As String In itemAdapter.Template.GetEnabledTargetNames()
            For Each p In _allAvailableItemPreviewHandlers.Where(Function(i) i.PreviewTarget.ToLower() = target.ToLower()).ToList()
                If Not previewHandlers.ContainsKey(p.UserFriendlyName) Then
                    previewHandlers.Add(p.UserFriendlyName, p)
                End If
            Next
        Next

        BindComboBox(previewHandlers.Values.ToList)
    End Sub

    Private Sub ItempreviewCombobox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs) Handles ItempreviewCombobox.SelectionChangeCommitted
        UISettings.ItemPreviewTarget = ItempreviewCombobox.SelectedItem.ToString()
        ItempreviewCombobox.Tag = ItempreviewCombobox.SelectedItem
        RaiseEvent RefreshPreview(Nothing, Nothing)
    End Sub

    Private Sub RefreshButton_Click(sender As Object, e As EventArgs) Handles RefreshButton.Click
        RaiseEvent RefreshPreview(Nothing, Nothing)
    End Sub

    Private Sub BindComboBox(initializedItemPreviewers As IEnumerable(Of IItemPreviewHandler))

        ItempreviewCombobox.Items.Clear()
        ItempreviewCombobox.Items.AddRange(initializedItemPreviewers.ToArray())

        Dim lastOpenedPreviewer = GetPreviewerByUserFriendlyName(UISettings.ItemPreviewTarget)
        If lastOpenedPreviewer IsNot Nothing Then
            ItempreviewCombobox.SelectedItem = lastOpenedPreviewer
        Else
            ItempreviewCombobox.SelectedItem = initializedItemPreviewers.FirstOrDefault()
            UISettings.ItemPreviewTarget = DirectCast(ItempreviewCombobox.SelectedItem, IItemPreviewHandler).ToString()
        End If
        ItempreviewCombobox.Tag = ItempreviewCombobox.SelectedItem
    End Sub

    Private Function GetPreviewerByUserFriendlyName(userFriendlyName As String) As IItemPreviewHandler
        If ItempreviewCombobox.Items.Count > 0 Then

            For Each handler As IItemPreviewHandler In ItempreviewCombobox.Items
                If (handler.UserFriendlyName = userFriendlyName) Then
                    Return handler
                End If
            Next
        End If
        Return Nothing
    End Function

    Private Function ValidateParameters() As Boolean
        Dim suppliedEventArgs As New ItemValidationRequiredEventArgs()
        OnItemValidatingRequired(suppliedEventArgs)

        ValidationFailedLabel.Visible = False
        ValidationFailedLabel.SendToBack()

        If suppliedEventArgs.ValidationValid Then
        Else
            ValidationFailedLabel.Visible = True
            ValidationFailedLabel.BringToFront()
        End If
        Return suppliedEventArgs.ValidationValid
    End Function

    Private Sub OnItemValidatingRequired(ByVal e As ItemValidationRequiredEventArgs)
        RaiseEvent ItemValidatingRequired(Me, e)
    End Sub

    Private Sub Generic_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        Dim resource As BinaryResource = Nothing
        Dim request = New ResourceRequestDTO With {.WithDependencies = False, .WithCustomProperties = False}
        If e.TypedResourceType IsNot Nothing Then
            resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
        Else
            resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
        End If
        e.BinaryResource = resource
    End Sub

End Class
