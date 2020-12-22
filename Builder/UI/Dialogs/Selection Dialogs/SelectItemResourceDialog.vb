Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Factories
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.EventArguments
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses


Public Class SelectItemResourceDialog


    Private _bankId As Integer
    Private _testResource As AssessmentTestResourceEntity
    Private _entitiesAlreadyAdded As List(Of ItemResourceDto)
    Private _entitiesIncompatibleToSelect As List(Of ItemResourceDto)
    Private _selectedEntities As List(Of ItemResourceDto)
    Private ReadOnly _checkSupportedViewsOfItemFunction As CheckSupportedViewsOfItemDelegate
    Private ReadOnly _contextIdentifierForItemViewer As Integer?
    Private ReadOnly _resourceManager As ResourceManagerBase






    Public Sub New(ByVal bankId As Integer, ByVal resourceManager As ResourceManagerBase, ByVal contextIdentifierForItemViewer As Nullable(Of Integer), ByVal testResource As AssessmentTestResourceEntity, ByVal checkSupportedViewsOfItemFunction As CheckSupportedViewsOfItemDelegate)
        InitializeComponent()
        _testResource = testResource
        _checkSupportedViewsOfItemFunction = checkSupportedViewsOfItemFunction
        _bankId = bankId
        _contextIdentifierForItemViewer = contextIdentifierForItemViewer
        _resourceManager = resourceManager
        DialogSplitContainer.Panel2Collapsed = True
        Previewer.Enabled = False
        If resourceManager IsNot Nothing Then
            chkPreviewEnabled.Visible = True
        Else
            chkPreviewEnabled.Visible = False
        End If
        ItemGridControl.MultiSelect = True
        ItemGridControl.ShouldDoSelectionCheck = True
        _entitiesAlreadyAdded = New List(Of ItemResourceDto)
        _entitiesIncompatibleToSelect = New List(Of ItemResourceDto)

        Me.ItemGridControl.GridContentContextMenuDisabled = True
        Me.ItemGridControl.ShowDisabledRowsAsGray = True
        Me.ItemGridControl.UseGridAsItemPicker = True
        Me.ItemGridControl.CustomPropertyColumnsVisible = True
    End Sub

    Public Sub New(ByVal bankId As Integer, ByVal resourceManager As ResourceManagerBase, ByVal contextIdentifierForItemViewer As Nullable(Of Integer))
        Me.New(bankId, resourceManager, contextIdentifierForItemViewer, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal bankId As Integer)
        Me.New(bankId, Nothing, Nothing, Nothing, Nothing)
    End Sub



    Public Delegate Function CheckSupportedViewsOfItemDelegate(ByVal itemLayoutTemplateName As String) As Boolean

    Private _resourceAlreadyInContextFunction As ResourceAlreadyInContextDelegate
    Public Delegate Function ResourceAlreadyInContextDelegate(itemIdentifier As String) As Boolean

    Public Property ResourceAlreadyInContextFunction() As ResourceAlreadyInContextDelegate
        Get
            Return _resourceAlreadyInContextFunction
        End Get
        Set(ByVal value As ResourceAlreadyInContextDelegate)
            _resourceAlreadyInContextFunction = value
        End Set
    End Property



    Public Event AddingResource As EventHandler(Of SelectedItemCollectionEventArgs)



    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return ItemGridControl.EntitiesProhibitedToSelect
        End Get
    End Property

    Public Property Multiselect() As Boolean
        Get
            Return ItemGridControl.MultiSelect
        End Get
        Set(ByVal value As Boolean)
            ItemGridControl.MultiSelect = value
        End Set
    End Property

    Public ReadOnly Property SelectedEntities() As IList(Of ItemResourceDto)
        Get
            Return _selectedEntities
        End Get
    End Property



    Public Sub RefreshDatasource()
        Me.Cursor = Cursors.WaitCursor
        _entitiesAlreadyAdded = New List(Of ItemResourceDto)
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetItemsForBank, _bankId))
    End Sub

    Public Sub RefreshGrid(addedItems As IList(Of ItemResourceDto), deletedItems As List(Of String))

        If deletedItems IsNot Nothing Then
            For Each item In deletedItems
                Dim itemInternal As String = item
                Dim itemToRemove = _entitiesAlreadyAdded.Find(Function(d) d.resourceId.Equals(itemInternal))
                If item IsNot Nothing Then
                    _entitiesAlreadyAdded.Remove(itemToRemove)
                End If
            Next
        End If
        If addedItems IsNot Nothing Then
            _entitiesAlreadyAdded.AddRange(addedItems)
        End If

        Dim temp = ItemGridControl.DataSource

        ItemGridControl.DataSource = Nothing
        ItemGridControl.DataSource = temp
    End Sub

    <DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso Me.IsDisposed = False Then
                _testResource = Nothing
                _selectedEntities = Nothing
                ItemGridControl = Nothing
                _entitiesIncompatibleToSelect = Nothing
                Previewer.Dispose()
            End If

            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Catch

        Finally
            If MyBase.IsDisposed = False Then MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Function IsItemResourceAddable(itemResource As ItemResourceDto, ByRef notSelectedReasonList As Dictionary(Of String, NotSelectedReason)) As Boolean
        Dim testDoesNotContainItem As Boolean = Not Me._entitiesAlreadyAdded.Contains(itemResource)
        Dim itemStateDoesNotProhibitSelection As Boolean = Not ItemGridControl.EntitiesProhibitedToSelect.Contains(itemResource.resourceId)
        Dim itemIsCompatableWithContext As Boolean = Not Me._entitiesIncompatibleToSelect.Contains(itemResource)

        If Not testDoesNotContainItem Then
            notSelectedReasonList.Add(itemResource.name, NotSelectedReason.AlreadyInTest)
        ElseIf Not itemStateDoesNotProhibitSelection Then
            notSelectedReasonList.Add(itemResource.name, NotSelectedReason.State)
        ElseIf Not itemIsCompatableWithContext Then
            notSelectedReasonList.Add(itemResource.name, NotSelectedReason.Compatibilty)
        End If
        Return testDoesNotContainItem AndAlso itemStateDoesNotProhibitSelection AndAlso itemIsCompatableWithContext
    End Function

    Enum NotSelectedReason
        Compatibilty
        State
        AlreadyInTest
    End Enum

    Protected Function DoDialogValidations() As Boolean
        Dim notSelected As New Dictionary(Of String, NotSelectedReason)

        Dim selectedCollection As List(Of ItemResourceDto) =
            (From itemResource In ItemGridControl.SelectedEntities.OfType(Of ItemResourceDto)()
             Where IsItemResourceAddable(itemResource, notSelected)).ToList()

        _selectedEntities = New List(Of ItemResourceDto)(selectedCollection)
        If _selectedEntities.Count > 0 Then
            Dim message = GetMessage(notSelected, False)
            If Not String.IsNullOrEmpty(message) Then
                MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            Return True
        Else
            Dim message = GetMessage(notSelected, True)
            If Not String.IsNullOrEmpty(message) Then
                MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                MessageBox.Show(My.Resources.SelectItemResourceDialog_FailedAdding, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return False
        End If
    End Function

    Private Function GetMessage(notSelected As Dictionary(Of String, NotSelectedReason), allExcluded As Boolean) As String
        Dim message As String = String.Empty
        If notSelected.Count = 1 AndAlso allExcluded Then
            Select Case notSelected.FirstOrDefault.Value
                Case NotSelectedReason.AlreadyInTest
                    message = My.Resources.ItemCannotBeAddedAlreadyInTest
                Case NotSelectedReason.Compatibilty
                    message = My.Resources.ItemCannotBeAddedTarget
                Case NotSelectedReason.State
                    message = My.Resources.ItemCannotBeAddedState
            End Select
        ElseIf notSelected.Count > 0 Then
            If allExcluded Then
                message = My.Resources.ItemsCannotBeAdded
            Else
                message = My.Resources.NotAllItemsCannotBeAdded
            End If
            Dim state = notSelected.Where(Function(s) s.Value = NotSelectedReason.State).ToList
            If state.Any() Then
                Dim codes = String.Join(","c, state.Select(Function(k) k.Key).ToArray)
                message = String.Concat(message, vbNewLine, String.Format(My.Resources.StateProhibts, codes))
            End If
            Dim alreadyinTest = notSelected.Where(Function(s) s.Value = NotSelectedReason.AlreadyInTest).ToList
            If alreadyinTest.Any() Then
                Dim codes = String.Join(","c, alreadyinTest.Select(Function(k) k.Key).ToArray)
                message = String.Concat(message, vbNewLine, String.Format(My.Resources.StateProhibts, codes))
            End If
            Dim comp = notSelected.Where(Function(s) s.Value = NotSelectedReason.Compatibilty).ToList
            If comp.Any() Then
                Dim codes = String.Join(","c, comp.Select(Function(k) k.Key).ToArray)
                message = String.Concat(message, vbNewLine, String.Format(My.Resources.TargetSupport, codes))
            End If
        End If
        Return message
    End Function

    Private Sub AddButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddButton.Click
        OnAddingResource(sender, SelectedItemCollectionEventArgs.Empty)
    End Sub

    Private Sub chkPreviewEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPreviewEnabled.CheckedChanged
        Previewer.StopPreview()

        If chkPreviewEnabled.Checked Then
            Previewer.Enabled = True
            DialogSplitContainer.Panel2Collapsed = False
            If ItemGridControl.SelectedEntity IsNot Nothing Then
                ItemGridControl_EntitySelected(Me, New EntityActionEventArgs(ItemGridControl.SelectedEntity))
            End If
        Else
            Previewer.Enabled = False
            DialogSplitContainer.Panel2Collapsed = True
        End If
    End Sub

    Private Sub CloseButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub GridBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)
        Select Case task.WorkerTask
            Case TaskType.GetItemsForBank
                If ItemGridControl IsNot Nothing AndAlso ItemGridControl.IsGridInFilterMode Then
                    Try
                        Dim filterArgs As FastSearchInitiatedEventArgs = ItemGridControl.FilterArgsForFastSearch
                        task.Result = DtoFactory.Item.GetItemsListWithSearchOptions(_bankId, filterArgs.SearchKeywords, filterArgs.IncludeSubbanks, filterArgs.SearchInBankProperties, filterArgs.SearchInItemText, filterArgs.TestContextResourceId, 0)

                    Catch ex As Exception
                        task.Result = ex
                    End Try
                Else
                    task.Result = DtoFactory.Item.GetResourcesForBank(_bankId)
                End If
            Case Else
                Throw New NotSupportedException()
        End Select

        e.Result = task

        If GridBackgroundWorker.CancellationPending Then e.Cancel = True
    End Sub

    Private Sub GridBackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles GridBackgroundWorker.RunWorkerCompleted

        If (Me.IsDisposed) Then
            Me.Cursor = Cursors.Default
            Return
        End If

        If e.Error IsNot Nothing Then
            MessageBox.Show(My.Resources.ErrorThrown & vbCrLf & vbCrLf & e.Error.Message)
        End If

        If e.Error Is Nothing AndAlso Not e.Cancelled Then
            Dim task As BackgroundWorkerTask = DirectCast(e.Result, BackgroundWorkerTask)

            Select Case task.WorkerTask
                Case TaskType.GetItemsForBank
                    ItemGridControl.DataSource = task.Result
                    ItemGridControl.GridControl.SelectedItems.Clear()

                    ItemGridControl.InitializeSearchBar(_bankId)
            End Select
        End If

        Me.Cursor = Cursors.Default
        AddButton.Enabled = True
    End Sub

    Private Sub ItemGridControl_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles ItemGridControl.EntityDblClick
        OnAddingResource(Me, SelectedItemCollectionEventArgs.Empty)
    End Sub

    Private Sub ItemGridControl_EntitySelected(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles ItemGridControl.EntitySelected
        If chkPreviewEnabled.Checked Then
            If Not e.SelectedEntity Is Nothing Then
                Dim itemResource = CType(e.SelectedEntity, ItemResourceDto)
                Dim llblGenItemResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(itemResource.resourceId, New ItemResourceEntityFactory(), new ResourceRequestDTO()), ItemResourceEntity)
                Dim assessmentItem = llblGenItemResourceEntity.GetAssessmentItem()
                Previewer.PreviewItem(assessmentItem, itemResource.bankId, _contextIdentifierForItemViewer, _resourceManager)
            End If
        End If
    End Sub

    Private Sub ItemGridControl_FormattingRow(ByVal sender As Object, ByVal e As RowFormattingEventArgs) Handles ItemGridControl.FormattingRow
        If e.Resource IsNot Nothing AndAlso TypeOf e.Resource Is ItemResourceDto Then
            Dim item = DirectCast(e.Resource, ItemResourceDto)
            If _resourceAlreadyInContextFunction IsNot Nothing AndAlso _resourceAlreadyInContextFunction.Invoke(e.Resource.name) Then
                e.Disabled = True
                _entitiesAlreadyAdded.Add(item)
                Exit Sub
            End If
            If _testResource IsNot Nothing AndAlso _testResource.ContainsDependentResource(e.Resource.resourceId) Then
                e.Disabled = True
                _entitiesAlreadyAdded.Add(item)
                Exit Sub
            End If
            If Me.EntitiesProhibitedToSelect.Contains(e.Resource.resourceId) Then
                e.Disabled = True
                Exit Sub
            End If
            If _checkSupportedViewsOfItemFunction IsNot Nothing AndAlso Not _checkSupportedViewsOfItemFunction(item.ItemLayoutTemplateUsedName) Then
                _entitiesIncompatibleToSelect.Add(DirectCast(e.Resource, ItemResourceDto))
                e.Disabled = True
                Exit Sub
            End If
        End If
    End Sub

    Private Sub ItemGridControl_SelectedRowChanged(sender As Object, e As EventArgs) Handles ItemGridControl.SelectedRowChanged
        AddButton.Enabled = Not ItemGridControl.GreyedOutItemsSelected
    End Sub

    Private Sub ItemGridControl_SynchronizeItems(ByVal sender As Object, ByVal e As EventArgs) Handles ItemGridControl.SynchronizeItems
        RefreshDatasource()
    End Sub

    Private Sub OnAddingResource(ByVal sender As Object, ByVal e As SelectedItemCollectionEventArgs)
        Dim initialButtonState As Boolean = AddButton.Enabled
        Me.Cursor = Cursors.WaitCursor
        AddButton.Enabled = False

        Try
            If (DoDialogValidations()) Then
                RaiseEvent AddingResource(Me, e)

                If (e.Cancelled) Then
                    Me.Cursor = Cursors.Default
                    AddButton.Enabled = initialButtonState
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Previewer_ItemValidatingRequired(ByVal sender As Object, ByVal e As ItemValidationRequiredEventArgs) Handles Previewer.ItemValidatingRequired
        e.ValidationValid = True
    End Sub

    Private Sub SelectItemResourceEntityDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        RefreshDatasource()
    End Sub


End Class