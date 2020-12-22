Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class SelectAspectResourceDialog


    Private ReadOnly _bankId As Integer
    Private ReadOnly _currentSelectedAspectIds As IList(Of Guid)
    Private ReadOnly _currentSelectedAspects As IList(Of String)
    Private _selectedEntities As IList(Of AspectResourceDto)



    Public Sub New(ByVal bankId As Integer, ByVal currentSelectedAspectIds As IList(Of Guid), Optional ByVal enableMultipleSelect As Boolean = False, Optional ByVal showDisabledRowsAsGray As Boolean = False)
        InitializeComponent()

        _bankId = bankId
        _currentSelectedAspectIds = currentSelectedAspectIds
        _currentSelectedAspects = New List(Of String)

        Me.AspectGrid.MultiSelect = enableMultipleSelect
        Me.AspectGrid.GridContentContextMenuDisabled = True
        Me.AspectGrid.ShowDisabledRowsAsGray = showDisabledRowsAsGray
    End Sub

    Public Sub New(ByVal bankId As Integer, ByVal currentSelectedAspects As IList(Of String), Optional ByVal enableMultipleSelect As Boolean = False, Optional ByVal showDisabledRowsAsGray As Boolean = False)
        InitializeComponent()

        _bankId = bankId
        _currentSelectedAspects = currentSelectedAspects
        _currentSelectedAspectIds = New List(Of Guid)

        Me.AspectGrid.MultiSelect = enableMultipleSelect
        Me.AspectGrid.GridContentContextMenuDisabled = True
        Me.AspectGrid.ShowDisabledRowsAsGray = showDisabledRowsAsGray
    End Sub



    Public ReadOnly Property SelectedEntities() As IList(Of AspectResourceDto)
        Get
            Return _selectedEntities
        End Get
    End Property



    Private Sub GridBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)

        Select Case task.WorkerTask
            Case TaskType.GetAspectsForBank
                If TypeOf task.InputParameter Is Integer Then
                    task.Result = DtoFactory.Aspect.GetResourcesForBank(DirectCast(task.InputParameter, Integer))
                ElseIf task.InputParameter Is Nothing Then
                    task.Result = DtoFactory.Aspect.GetResourcesForBank(_bankId)
                End If
            Case Else
                Throw New NotSupportedException()
        End Select

        e.Result = task

        e.Cancel = GridBackgroundWorker.CancellationPending
    End Sub

    Private Sub GridBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles GridBackgroundWorker.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            MessageBox.Show(My.Resources.ErrorThrown & vbCrLf & vbCrLf & e.Error.Message)
        End If

        If e.Error Is Nothing AndAlso Not e.Cancelled Then
            Dim task As BackgroundWorkerTask = DirectCast(e.Result, BackgroundWorkerTask)

            Select Case task.WorkerTask
                Case TaskType.GetAspectsForBank
                    AspectGrid.DataSource = task.Result

            End Select
        End If
    End Sub

    Private Sub AspectGridControl_EntityDblClick(ByVal sender As System.Object, ByVal e As Questify.Builder.UI.EntityActionEventArgs) Handles AspectGrid.EntityDblClick
        OkButton_Click(Me, New EventArgs)
    End Sub

    Private Sub AspectGridControl_FormattingRow(ByVal sender As System.Object, ByVal e As RowFormattingEventArgs) Handles AspectGrid.FormattingRow
        e.Disabled = TypeOf e.Resource Is AspectResourceDto AndAlso (_currentSelectedAspectIds.Contains(DirectCast(e.Resource, AspectResourceDto).ResourceId) OrElse _currentSelectedAspects.Contains(DirectCast(e.Resource, AspectResourceDto).Name))
    End Sub

    Private Sub SelectItemResourceEntityDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetAspectsForBank, _bankId))
    End Sub

    Private Function IsResourceAddable(aspectResource As AspectResourceDto) As Boolean
        Return Not _currentSelectedAspectIds.Contains(aspectResource.ResourceId) AndAlso Not _currentSelectedAspects.Contains(aspectResource.Name)
    End Function



    Protected Overrides Function DoDialogValidations() As Boolean
        Dim selectedCollection = AspectGrid.SelectedEntities.OfType(Of AspectResourceDto).Where(Function(a) IsResourceAddable(a)).ToList()
        _selectedEntities = selectedCollection
        If _selectedEntities.Count > 0 Then
            Return True
        Else
            MessageBox.Show(My.Resources.SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
    End Function


End Class