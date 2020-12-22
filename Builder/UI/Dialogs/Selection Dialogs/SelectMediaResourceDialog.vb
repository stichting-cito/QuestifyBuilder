Imports System.ComponentModel
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Public Class SelectMediaResourceDialog


    Private _bankId As Integer
    Private _filter As String = ""
    Private _filePrefix As String = String.Empty
    Private _filterTemplatesOnly As Boolean = False
    Private _testResource As AssessmentTestResourceEntity

    Private _selectedEntity As GenericResourceDto
    Private _selectedEntities As New List(Of GenericResourceDto)
    Private ReadOnly _entitiesAlreadyAdded As List(Of GenericResourceDto)



    Public Sub New(ByVal bankId As Integer, Optional ByVal contextIdentifier As Nullable(Of Integer) = Nothing, Optional ByVal resourcemanager As ResourceManagerBase = Nothing, Optional testResource As AssessmentTestResourceEntity = Nothing)
        InitializeComponent()
        _bankId = bankId
        GenericResourceViewer.BankContextIdentifier = contextIdentifier
        GenericResourceViewer.ResourceManager = resourcemanager
        Me.MediaGridControl.SetLayout(My.Resources.MediaGridLayoutSimple)
        Me.MediaGridControl.ShouldDoSelectionCheck = True
        Me.MediaGridControl.GridContentContextMenuDisabled = True
        Me.MediaGridControl.ShowDisabledRowsAsGray = True
        _entitiesAlreadyAdded = New List(Of GenericResourceDto)
        _testResource = testResource
    End Sub



    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return MediaGridControl.EntitiesProhibitedToSelect
        End Get
    End Property

    Public Property Filter() As String
        Get
            Return _filter
        End Get
        Set(ByVal value As String)
            _filter = value
        End Set
    End Property

    Public Property CanPickFiles As Boolean

    Public Property FilePrefix() As String
        Get
            Return _filePrefix
        End Get
        Set(ByVal value As String)
            _filePrefix = value
        End Set
    End Property

    Public Property FilterTemplatesOnly() As Boolean
        Get
            Return _filterTemplatesOnly
        End Get
        Set(ByVal value As Boolean)
            _filterTemplatesOnly = value
        End Set
    End Property

    Public ReadOnly Property SelectedEntity() As GenericResourceDto
        Get
            Return _selectedEntity
        End Get
    End Property

    Public ReadOnly Property SelectedEntities As List(Of GenericResourceDto)
        Get
            Return _selectedEntities
        End Get
    End Property

    Public Property ShowPreview() As Boolean
        Get
            GenericResourceViewer.Visible = True
        End Get
        Set(ByVal value As Boolean)
            GenericResourceViewer.Visible = False
        End Set
    End Property

    Public Property ShowAddNew() As Boolean
        Get
            Return NewButton.Visible
        End Get
        Set(ByVal value As Boolean)
            NewButton.Visible = value
        End Set
    End Property





    Private Sub GenericResourceViewer_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs) Handles GenericResourceViewer.ResourceNeeded
        RaiseEvent ResourceNeeded(Me, e)
    End Sub


    Private Function IsMediaResourceAddable(mediaResource As GenericResourceDto) As Boolean
        Dim testPackageDoesNotContainMedium As Boolean = Not Me._entitiesAlreadyAdded.Contains(mediaResource)

        Return testPackageDoesNotContainMedium
    End Function

    Private Sub GridBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)

        Select Case task.WorkerTask
            Case TaskType.GetMediaForBank
                task.Result = DtoFactory.Generic.GetListWithFilter(DirectCast(task.InputParameter, Integer), Me.Filter, Me.FilePrefix, Me.FilterTemplatesOnly)

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
                Case TaskType.GetMediaForBank
                    MediaGridControl.DataSource = task.Result
                    Dim resourceEventArgs As New ResourceEventArgs(Nothing)
                    OnResourcesLoaded(resourceEventArgs)

                    If resourceEventArgs.Resource IsNot Nothing Then
                        SetEntitySelected(resourceEventArgs.Resource)
                    ElseIf MediaGridControl.SelectedEntities IsNot Nothing AndAlso MediaGridControl.SelectedEntities.Count = 1 Then
                        SetEntitySelected(MediaGridControl.SelectedEntities(0))
                    End If
            End Select
        End If
    End Sub

    Private Sub SetEntitySelected(resource As ResourceDto)
        MediaGridControl.SelectedEntity = resource
        MediaGridControl_EntitySelected(Me, New EntityActionEventArgs(resource))
    End Sub

    Private Sub MediaGridControl_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles MediaGridControl.EntityDblClick
        OkButton_Click(Me, New EventArgs)
    End Sub

    Private Sub MediaGridControl_EntitySelected(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles MediaGridControl.EntitySelected
        If Not MediaGridControl.SettingDataSource Then
            _selectedEntity = DirectCast(MediaGridControl.SelectedEntity, GenericResourceDto)

            If _selectedEntity IsNot Nothing Then
                Dim datasource As GenericResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(_selectedEntity.resourceId, New GenericResourceEntityFactory(), New ResourceRequestDTO()), GenericResourceEntity)
                GenericResourceViewer.DataSource = datasource
            Else
                GenericResourceViewer.DataSource = Nothing
            End If
        End If
    End Sub

    Private Sub MediaGridControl_EntitiesSelected(ByVal sender As Object, ByVal e As EventArgs) Handles MediaGridControl.EntitiesSelected
        _selectedEntities.Clear()

        For Each s In MediaGridControl.SelectedEntities
            _selectedEntities.Add(DirectCast(s, GenericResourceDto))
        Next

        If _selectedEntities.Count > 1 Then
            GenericResourceViewer.DataSource = Nothing
        End If
    End Sub

    Private Sub NewButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewButton.Click
        Using dlg As New NewGenericResourceDialog(_bankId, Me.Filter, CanPickFiles)
            If dlg.ShowDialog = DialogResult.OK Then
                Dim resource As GenericResourceEntity = dlg.Resource

                Dim result As String = ResourceFactory.Instance.UpdateGenericResource(resource)
                If Not String.IsNullOrEmpty(result) Then
                    MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    resource = ResourceFactory.Instance.GetGenericResource(resource)
                    Dim resourceDto = DtoFactory.Generic.Get(resource.ResourceId)

                    If resourceDto IsNot Nothing Then
                        _selectedEntity = resourceDto
                        _selectedEntities.Clear()
                        _selectedEntities.Add(_selectedEntity)
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                        Me.Close()
                    End If
                End If
            End If
        End Using
    End Sub

    Private Sub SelectItemResourceEntityDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetMediaForBank, _bankId))
    End Sub



    Protected Overrides Function DoDialogValidations() As Boolean

        Dim selectedCollection As List(Of GenericResourceDto) = (From entity In MediaGridControl.SelectedEntities.OfType(Of GenericResourceDto)() Where IsMediaResourceAddable(entity)).ToList()
        _selectedEntities = selectedCollection
        If _selectedEntities.Count > 0 Then
            Return True
        Else
            MessageBox.Show(My.Resources.SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
    End Function

    Protected Overridable Sub OnResourcesLoaded(ByVal e As ResourceEventArgs)
        RaiseEvent ResourcesLoaded(Me, e)
    End Sub



    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Public Event ResourcesLoaded As EventHandler(Of ResourceEventArgs)

    Private Sub MediaGridControl_FormattingRow(sender As Object, e As RowFormattingEventArgs) Handles MediaGridControl.FormattingRow
        Dim ADAPTIVE_DLL_EXTENSION As String = ".otd"
        If e.Resource IsNot Nothing AndAlso TypeOf e.Resource Is GenericResourceDto AndAlso e.Resource.name.EndsWith(ADAPTIVE_DLL_EXTENSION, StringComparison.CurrentCultureIgnoreCase) Then
            Dim mediaResourceDto = DirectCast(e.Resource, GenericResourceDto)
            If _testResource IsNot Nothing AndAlso _testResource.ContainsDependentResource(DirectCast(e.Resource, GenericResourceDto).resourceId) Then
                e.Disabled = True
                _entitiesAlreadyAdded.Add(mediaResourceDto)
                Exit Sub
            Else
                e.Disabled = False
            End If
        End If
    End Sub


End Class
