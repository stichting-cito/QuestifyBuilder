Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class SelectTestPackageResourceDialog


    Private ReadOnly _bankId As Integer
    Private ReadOnly _initiatingTestPackage As TestPackage
    Private ReadOnly _initiatingTestPackageResource As TestPackageResourceEntity
    Private ReadOnly _checkSupportedViewsOfTestPackageFunction As CheckSupportedViewsOfTestPackageDelegate
    Private ReadOnly _testPackages As New Dictionary(Of String, TestPackage)
    Private ReadOnly _entitiesAlreadyAdded As List(Of TestPackageResourceDto)
    Private ReadOnly _selectSpecificTestPackage As Boolean = False
    Private ReadOnly _guidOfTestPackageToSelect As String = String.Empty
    Private _selectedEntity As TestPackageResourceDto
    Private _selectedEntities As ObjectModel.ReadOnlyCollection(Of TestPackageResourceDto)
    Private _entitiesIncompatibleToSelect As List(Of TestPackageResourceDto)


    Public Sub New(ByVal bankId As Integer, ByVal initiatingTestPackageResource As TestPackageResourceEntity, ByVal initiatingTestPackage As TestPackage, ByVal checkSupportedViewsOfTestPackageFunction As CheckSupportedViewsOfTestPackageDelegate, ByVal enableMultipleSelect As Boolean, ByVal selectSpecificTestPackage As Boolean, ByVal guidOfTestPackageToSelect As String)
        InitializeComponent()

        _bankId = bankId
        _initiatingTestPackage = initiatingTestPackage
        _initiatingTestPackageResource = initiatingTestPackageResource
        _checkSupportedViewsOfTestPackageFunction = checkSupportedViewsOfTestPackageFunction

        _entitiesAlreadyAdded = New List(Of TestPackageResourceDto)
        _entitiesIncompatibleToSelect = New List(Of TestPackageResourceDto)

        _selectSpecificTestPackage = selectSpecificTestPackage
        _guidOfTestPackageToSelect = guidOfTestPackageToSelect

        Me.TestPackageGrid.MultiSelect = enableMultipleSelect
        Me.TestPackageGrid.ShowDisabledRowsAsGray = True
    End Sub

    Public Sub New(ByVal bankId As Integer)
        InitializeComponent()
        _bankId = bankId
        _entitiesAlreadyAdded = New List(Of TestPackageResourceDto)
        _entitiesIncompatibleToSelect = New List(Of TestPackageResourceDto)
        Me.TestPackageGrid.GridContentContextMenuDisabled = True
    End Sub



    Public Delegate Function CheckSupportedViewsOfTestPackageDelegate(ByVal testPackage As TestPackage, ByVal mustSupportViewTypes As List(Of String)) As Boolean

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




    Public ReadOnly Property SelectedEntities() As ObjectModel.ReadOnlyCollection(Of TestPackageResourceDto)
        Get
            Return _selectedEntities
        End Get
    End Property

    Public ReadOnly Property SingleSelect As Boolean
        Get
            Return Not Me.TestPackageGrid.MultiSelect
        End Get
    End Property

    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return Me.TestPackageGrid.EntitiesProhibitedToSelect
        End Get
    End Property

    Public ReadOnly Property SelectedEntity() As TestPackageResourceDto
        Get
            Return _selectedEntity
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldDoSelectionCheck As Boolean
        Get
            Return True
        End Get
    End Property



    Public Event AddingResource As EventHandler(Of SelectedTestPackageCollectionEventArgs)



    Public Sub RefreshDatasource()
        Me.Cursor = Cursors.WaitCursor
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetTestPackageForBank, _bankId))
    End Sub

    Private Sub OnAddingResource(ByVal sender As Object, ByVal e As SelectedTestPackageCollectionEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.AddButton.Enabled = False

            If (DoDialogValidations()) Then

                If Not SingleSelect Then

                    RaiseEvent AddingResource(Me, e)

                    If (e.Cancelled) Then
                        Me.Cursor = Cursors.Default
                        Me.AddButton.Enabled = True
                    End If
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
            Me.AddButton.Enabled = True
        End Try
    End Sub

    Protected Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddButton.Click
        OnAddingResource(sender, SelectedTestPackageCollectionEventArgs.Empty)
    End Sub

    Protected Function DoDialogValidations() As Boolean
        Dim selectedCollection As List(Of TestPackageResourceDto) = (From entity In Me.TestPackageGrid.SelectedEntities.OfType(Of TestPackageResourceDto)() Where IsTestPackageResourceAddable(entity)).ToList()
        _selectedEntities = New ObjectModel.ReadOnlyCollection(Of TestPackageResourceDto)(selectedCollection)
        If _selectedEntities.Count > 0 Then
            Return True
        Else
            MessageBox.Show(My.Resources.SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

    End Function

    Private Function IsTestPackageResourceAddable(testPackageResource As TestPackageResourceDto) As Boolean
        Dim isNotSameAsInitatingTestPackage As Boolean = Not testPackageResource.name.Equals(_initiatingTestPackageResource.Name)
        Dim isNotAlreadyAdded As Boolean = Not Me._entitiesAlreadyAdded.Contains(testPackageResource)
        Dim stateDoesNotProhibitSelection As Boolean = testPackageResource IsNot Nothing AndAlso Not Me.TestPackageGrid.EntitiesProhibitedToSelect.Contains(testPackageResource.resourceId)
        Dim isCompatableWithContext As Boolean = Not Me._entitiesIncompatibleToSelect.Contains(testPackageResource)

        Return isNotSameAsInitatingTestPackage AndAlso isNotAlreadyAdded AndAlso stateDoesNotProhibitSelection AndAlso isCompatableWithContext
    End Function

    Private Sub GridBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)
        Select Case task.WorkerTask
            Case TaskType.GetTestPackageForBank
                task.Result = DtoFactory.TestPackage.GetResourcesForBank(DirectCast(task.InputParameter, Integer))
            Case Else
                Throw New NotSupportedException()
        End Select
        e.Result = task
        If GridBackgroundWorker.CancellationPending Then e.Cancel = True
    End Sub

    Private Sub GridBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles GridBackgroundWorker.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            MessageBox.Show(My.Resources.ErrorThrown & vbCrLf & vbCrLf & e.Error.Message)
        End If

        If e.Error Is Nothing AndAlso Not e.Cancelled Then
            Dim task As BackgroundWorkerTask = DirectCast(e.Result, BackgroundWorkerTask)

            Select Case task.WorkerTask
                Case TaskType.GetTestPackageForBank
                    FillTestPackageCollection(task.Result)
                    Me.TestPackageGrid.DataSource = task.Result
                    If _selectSpecificTestPackage Then Me.TestPackageGrid.RestoreSelectedItemInGrid(_guidOfTestPackageToSelect)
            End Select
        End If
    End Sub

    Private Sub FillTestPackageCollection(taskResult As Object)
        Dim testPackageResourceDtos = TryCast(taskResult, IList(Of TestPackageResourceDto))
        If (testPackageResourceDtos IsNot Nothing) Then
            Dim ids = testPackageResourceDtos.Select(Function(t) t.resourceId).ToList
            For Each testPackage In From resourceData In ResourceFactory.Instance.GetResourceDataByResourceIds(ids).OfType(Of ResourceDataEntity)()
                                    Select TestPackageFactory.ReturnTestPackageModelFromByteArray(resourceData.BinData)

                _testPackages(testPackage.Identifier) = testPackage
            Next
        End If
    End Sub

    Private Sub SelectTestPackageResourceDialog_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetTestPackageForBank, _bankId))
    End Sub

    Private Sub TestGridControl_EntityDblClick(ByVal sender As System.Object, ByVal e As Questify.Builder.UI.EntityActionEventArgs) Handles TestPackageGrid.EntityDblClick
        AddButton_Click(Me, New EventArgs)
    End Sub

    Private Sub TestGridControl_EntitySelected(ByVal sender As System.Object, ByVal e As Questify.Builder.UI.EntityActionEventArgs) Handles TestPackageGrid.EntitySelected
        _selectedEntity = DirectCast(TestPackageGrid.SelectedEntity, TestPackageResourceDto)
    End Sub

    Private Sub TestPackageGridControl_FormattingRow(ByVal sender As System.Object, ByVal e As RowFormattingEventArgs) Handles TestPackageGrid.FormattingRow
        If e.Resource IsNot Nothing AndAlso TypeOf e.Resource Is TestPackageResourceDto Then
            Dim testPackageResourceDto = DirectCast(e.Resource, TestPackageResourceDto)
            If testPackageResourceDto.name.Equals(_initiatingTestPackageResource.Name) Then
                e.Disabled = True
                Exit Sub
            End If

            If _resourceAlreadyInContextFunction IsNot Nothing AndAlso _resourceAlreadyInContextFunction.Invoke(e.Resource.name) Then
                e.Disabled = True
                _entitiesAlreadyAdded.Add(testPackageResourceDto)
                Exit Sub
            Else
                e.Disabled = False
            End If

            If _initiatingTestPackageResource IsNot Nothing AndAlso _initiatingTestPackageResource.ContainsDependentResource(testPackageResourceDto.resourceId) Then
                e.Disabled = True
                _entitiesAlreadyAdded.Add(testPackageResourceDto)
                Exit Sub
            Else
                e.Disabled = False
            End If

            If _testPackages.ContainsKey(testPackageResourceDto.name) Then
                If _checkSupportedViewsOfTestPackageFunction IsNot Nothing AndAlso Not _checkSupportedViewsOfTestPackageFunction(_testPackages(testPackageResourceDto.name), GeneralHelper.GetViewsWithoutGeneral(_initiatingTestPackage.IncludedViews.Select(Function(s) s.ToString()).ToList())) Then
                    e.Disabled = True
                End If
            Else
                e.Disabled = True
            End If

            If e.Disabled Then
                If Not _entitiesIncompatibleToSelect.Contains(testPackageResourceDto) Then _entitiesIncompatibleToSelect.Add(testPackageResourceDto)
            End If
        End If
    End Sub

    Private Sub TestPackageGridControl_SynchronizeItems(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestPackageGrid.SynchronizeItems
        RefreshDatasource()
    End Sub

    Private Sub CloseButton_Click(sender As System.Object, e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub


End Class