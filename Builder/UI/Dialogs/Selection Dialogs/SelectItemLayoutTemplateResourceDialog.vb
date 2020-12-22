Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories
Imports System.Linq
Imports Cito.Tester.Common
Imports Enums
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Public Class SelectItemLayoutTemplateResourceDialog
    Implements ISelectIltDialog


    Public Event ValidateEntity As EventHandler(Of ValidateResourceEventArgs)



    Private ReadOnly _bankId As Integer
    Private ReadOnly _itemTypes As List(Of ItemTypeEnum)
    Private ReadOnly _exclude As Boolean
    Private _compatibilityCheck As Boolean = False
    Private _selectedEntity As ItemLayoutTemplateResourceDto
    Private _currentIltName As String = String.Empty
    Private _entitiesIncompatibleToSelect As List(Of String)
    Private _controlTemplatesForCurrentIlt As List(Of Guid)
    Private _controlTemplatesForBank As List(Of Guid)


    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return ItemLayoutTemplateGridControl.EntitiesProhibitedToSelect
        End Get
    End Property

    Public ReadOnly Property SelectedEntity() As ItemLayoutTemplateResourceDto Implements ISelectIltDialog.SelectedEntity
        Get
            Return _selectedEntity
        End Get
    End Property




    Public Sub New(ByVal bankId As Integer, itemTypes As List(Of ItemTypeEnum), exclude As Boolean)
        InitializeComponent()

        _bankId = bankId
        Me.ItemLayoutTemplateGridControl.SetLayout(My.Resources.ItemLayoutTemplateSimple)
        Me.ItemLayoutTemplateGridControl.GridContentContextMenuDisabled = True
        Me.ItemLayoutTemplateGridControl.ShowDisabledRowsAsGray = True
        _itemTypes = itemTypes
        _exclude = exclude
    End Sub

    Public Sub New(ByVal bankId As Integer, itemTypes As List(Of ItemTypeEnum), exclude As Boolean, currentIltName As String)
        Me.New(bankId, itemTypes, exclude)
        _currentIltName = currentIltName
        _controlTemplatesForBank = GetControlTemplatesForBank(bankId)
        _controlTemplatesForCurrentIlt = GetControlTemplatesForCurrentIlt(_currentIltName)
        _entitiesIncompatibleToSelect = New List(Of String) From {_currentIltName}
        _compatibilityCheck = True
    End Sub

    Public Overloads Function ShowDialog() As System.Windows.Forms.DialogResult Implements ISelectIltDialog.ShowDialog
        Return MyBase.ShowDialog()
    End Function



    Protected Overrides Function DoDialogValidations() As Boolean
        If _selectedEntity Is Nothing Then
            MessageBox.Show(My.Resources.SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        ElseIf _compatibilityCheck AndAlso Not IsItemLayoutTemplateResourceAddable(_selectedEntity.name) Then
            MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        Else
            Dim e As New ValidateResourceEventArgs(_selectedEntity)
            RaiseEvent ValidateEntity(Me, e)
            Return e.Valid
        End If
    End Function



    Private Sub GridBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)

        Select Case task.WorkerTask
            Case TaskType.GetItemLayoutTemplatesForBank
                task.Result = DtoFactory.ItemLayoutTemplate.GetListWithItemTypeFilter(_bankId, _itemTypes, _exclude)

            Case Else
                Throw New NotSupportedException()
        End Select

        e.Result = task

        If GridBackgroundWorker.CancellationPending Then e.Cancel = True

    End Sub

    Private Sub GridBackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles GridBackgroundWorker.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            MessageBox.Show(My.Resources.ErrorThrown & vbCrLf & vbCrLf & e.Error.Message)
        End If

        If e.Error Is Nothing AndAlso Not e.Cancelled Then
            Dim task As BackgroundWorkerTask = DirectCast(e.Result, BackgroundWorkerTask)
            Select Case task.WorkerTask
                Case TaskType.GetItemLayoutTemplatesForBank
                    ItemLayoutTemplateGridControl.DataSource = task.Result
                    If _compatibilityCheck Then Me.ItemLayoutTemplateGridControl.RestoreSelectedItemInGrid(String.Empty)
            End Select
        End If
    End Sub

    Private Sub SelectItemResourceEntityDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetItemLayoutTemplatesForBank, _bankId))
    End Sub

    Private Sub ItemLayoutTemplateGridControl_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles ItemLayoutTemplateGridControl.EntityDblClick
        OkButton_Click(Me, New EventArgs)
    End Sub

    Private Sub ItemLayoutTemplateGridControl_EntitySelected(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles ItemLayoutTemplateGridControl.EntitySelected
        _selectedEntity = DirectCast(ItemLayoutTemplateGridControl.SelectedEntity, ItemLayoutTemplateResourceDto)
    End Sub

    Private Sub ItemLayoutTemplateGridControl_FormattingRow(sender As Object, e As RowFormattingEventArgs) Handles ItemLayoutTemplateGridControl.FormattingRow
        If _compatibilityCheck AndAlso e.Resource IsNot Nothing AndAlso TypeOf e.Resource Is ItemLayoutTemplateResourceDto Then
            Dim iltResourceDto = DirectCast(e.Resource, ItemLayoutTemplateResourceDto)

            If _entitiesIncompatibleToSelect.Contains(iltResourceDto.name) Then
                e.Disabled = True
                Exit Sub
            End If

            If iltResourceDto.name.Equals(_currentIltName) Then
                If Not _entitiesIncompatibleToSelect.Contains(iltResourceDto.name) Then _entitiesIncompatibleToSelect.Add(iltResourceDto.name)
                e.Disabled = True
                Exit Sub
            End If

            If Not CheckIltIsCompatible(iltResourceDto.resourceId) Then
                If Not _entitiesIncompatibleToSelect.Contains(iltResourceDto.name) Then _entitiesIncompatibleToSelect.Add(iltResourceDto.name)
                e.Disabled = True
                Exit Sub
            End If
        End If
    End Sub

    Private Function GetControlTemplatesForBank(bankId As Integer) As List(Of Guid)
        Dim result As New List(Of Guid)
        For Each ct In ResourceFactory.Instance.GetControlTemplatesForBank(bankId)
            Dim resourceId = DirectCast(ct, Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity).ResourceId
            If Not result.Contains(resourceId) Then result.Add(resourceId)
        Next
        Return result
    End Function

    Private Function GetControlTemplatesForCurrentIlt(iltName As String) As List(Of Guid)
        Dim result As New List(Of Guid)
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = False}
        Dim iltResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByNameWithOption(_bankId, iltName, request), ItemLayoutTemplateResourceEntity)
        Dim controlTemplatesForIlt = iltResourceEntity.DependentResourceCollection.Where(Function(dre) _controlTemplatesForBank.Contains(dre.DependentResourceId))
        For Each dr As DependentResourceEntity In controlTemplatesForIlt
            result.Add(dr.DependentResourceId)
        Next
        Return result
    End Function

    Private Function CheckIltIsCompatible(iltIdentifier As Guid) As Boolean
        Dim list As New List(Of Guid)
        Dim request = New ResourceRequestDTO With {.WithDependencies = True}
        Dim iltResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(iltIdentifier, New ItemLayoutTemplateResourceEntityFactory(), request), ItemLayoutTemplateResourceEntity)
        Dim controlTemplatesForIlt = iltResourceEntity.DependentResourceCollection.Where(Function(dre) _controlTemplatesForBank.Contains(dre.DependentResourceId))

        If Not controlTemplatesForIlt.Count.Equals(_controlTemplatesForCurrentIlt.Count) Then Return False

        For Each dr As DependentResourceEntity In controlTemplatesForIlt
            If Not _controlTemplatesForCurrentIlt.Contains(dr.DependentResourceId) Then Return False

            list.Add(dr.DependentResourceId)
        Next

        Return Enumerable.SequenceEqual(list, _controlTemplatesForCurrentIlt)
    End Function

    Private Function IsItemLayoutTemplateResourceAddable(iltName As String) As Boolean
        Return Not Me._entitiesIncompatibleToSelect.Contains(iltName)
    End Function



End Class
