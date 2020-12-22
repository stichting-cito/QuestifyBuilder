Imports System.ComponentModel
Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class SelectDataSourceResourceDialog


    Private ReadOnly _bankId As Integer
    Private ReadOnly _isTemplate As Boolean = True
    Private _selectedEntity As DataSourceResourceDto
    Private _filter() As String
    Private _dataSourcesAlreadyAdded As List(Of String)



    Public Sub New(ByVal bankId As Integer, ByVal isTemplate As Boolean, ParamArray filter() As String)
        Me.New(bankId, Nothing, isTemplate, filter)
    End Sub

    Public Sub New(ByVal bankId As Integer, dataSourcesAlreadyAdded As List(Of String), ByVal isTemplate As Boolean, ParamArray filter() As String)
        InitializeComponent()

        _bankId = bankId
        _isTemplate = isTemplate
        _filter = filter
        _dataSourcesAlreadyAdded = dataSourcesAlreadyAdded

        Me.DataSourceGridControl.GridContentContextMenuDisabled = True
        Me.DataSourceGridControl.ShowDisabledRowsAsGray = True
    End Sub



    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return DataSourceGridControl.EntitiesProhibitedToSelect
        End Get
    End Property

    Public ReadOnly Property SelectedEntity() As DataSourceResourceDto
        Get
            Return _selectedEntity
        End Get
    End Property

    Public ReadOnly Property SelectedEntities() As IList(Of DataSourceResourceDto)
        Get
            Return DataSourceGridControl.SelectedEntities.Cast(Of DataSourceResourceDto)().ToList()
        End Get
    End Property

    <Browsable(True), DefaultValue(False)>
    Public Property AllowMultiSelect() As Boolean
        Get
            Return DataSourceGridControl.MultiSelect
        End Get
        Set(ByVal value As Boolean)
            DataSourceGridControl.MultiSelect = value
        End Set
    End Property

    Public Property Filter() As String()
        Get
            Return _filter
        End Get
        Set(value As String())
            _filter = value
        End Set
    End Property



    Public Sub RefreshDatasource()
        Me.Cursor = Cursors.WaitCursor
        Me.OkButton.Enabled = False
        Me.CloseButton.Enabled = False

        If Me._isTemplate Then
            GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetDataSourceTemplatesForBank, _bankId))
        Else
            GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetDataSourcesForBank, _bankId))
        End If
    End Sub

    Protected Overrides Function DoDialogValidations() As Boolean
        Dim returnValue As Boolean = False
        If _selectedEntity IsNot Nothing Then
            Dim prohitedToSelect As Boolean = False
            For Each entity In From resourceId In DataSourceGridControl.EntitiesProhibitedToSelect
                               Where resourceId = _selectedEntity.resourceId
                prohitedToSelect = True
            Next

            If _dataSourcesAlreadyAdded IsNot Nothing AndAlso _dataSourcesAlreadyAdded.Contains(_selectedEntity.Name) Then
                prohitedToSelect = True
            End If

            returnValue = Not prohitedToSelect
        Else
            MessageBox.Show(My.Resources.SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            returnValue = False
        End If

        Return returnValue
    End Function

    Private Sub DataSourceGridControl_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles DataSourceGridControl.EntityDblClick
        OkButton_Click(Me, New EventArgs)
    End Sub

    Private Sub DataSourceGridControl_EntitySelected(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles DataSourceGridControl.EntitySelected
        _selectedEntity = DirectCast(DataSourceGridControl.SelectedEntity, DataSourceResourceDto)
    End Sub

    Private Sub GridBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)
        Dim bankId As Integer = DirectCast(task.InputParameter, Integer)
        Select Case task.WorkerTask
            Case TaskType.GetDataSourcesForBank
                task.Result = DtoFactory.Datasource.GetListWithFilter(bankId, False, _filter)
            Case TaskType.GetDataSourceTemplatesForBank
                task.Result = DtoFactory.Datasource.GetListWithFilter(bankId, True, _filter)
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
                Case TaskType.GetDataSourcesForBank
                    DataSourceGridControl.DataSource = task.Result
                Case TaskType.GetDataSourceTemplatesForBank
                    DataSourceGridControl.DataSource = task.Result
            End Select
        End If

        Me.Cursor = Cursors.Default
        Me.OkButton.Enabled = True
        Me.CloseButton.Enabled = True
    End Sub

    Private Sub SelectDataSourceTemplateResourceDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        RefreshDatasource()
    End Sub

    Private Sub DataSourceGridControl_FormattingRow(ByVal sender As Object, ByVal e As RowFormattingEventArgs) Handles DataSourceGridControl.FormattingRow
        If e.Resource IsNot Nothing AndAlso TypeOf e.Resource Is DataSourceResourceDto Then
            Dim dataSource = DirectCast(e.Resource, DataSourceResourceDto)
            If _dataSourcesAlreadyAdded IsNot Nothing AndAlso _dataSourcesAlreadyAdded.Contains(dataSource.Name) Then
                e.Disabled = True
                Exit Sub
            End If
            If EntitiesProhibitedToSelect.Contains(e.Resource.resourceId) Then
                e.Disabled = True
                Exit Sub
            End If
        End If
    End Sub


End Class