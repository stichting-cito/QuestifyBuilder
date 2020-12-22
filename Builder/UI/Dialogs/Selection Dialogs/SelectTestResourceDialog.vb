Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class SelectTestResourceDialog

    Private ReadOnly _bankId As Integer
    Private ReadOnly _testPackage As TestPackage
    Private ReadOnly _checkSupportedViewsOfTestFunction As CheckSupportedViewsOfTestDelegate
    Private ReadOnly _assessmentTests As New Dictionary(Of String, AssessmentTest2)
    Private ReadOnly _testPackageResource As TestPackageResourceEntity
    Private ReadOnly _entitiesAlreadyAdded As List(Of AssessmentTestResourceDto)
    Private _selectedEntity As AssessmentTestResourceDto
    Private _selectedEntities As ObjectModel.ReadOnlyCollection(Of AssessmentTestResourceDto)
    Private _entitiesIncompatibleToSelect As List(Of AssessmentTestResourceDto)

    Public Sub New(ByVal bankId As Integer, ByVal testPackageResource As TestPackageResourceEntity, ByVal testPackageModel As TestPackage, ByVal checkSupportedViewsOfTestFunction As CheckSupportedViewsOfTestDelegate, ByVal enableMultipleSelect As Boolean)
        InitializeComponent()

        _bankId = bankId
        _testPackage = testPackageModel
        _checkSupportedViewsOfTestFunction = checkSupportedViewsOfTestFunction
        _testPackageResource = testPackageResource

        TestGrid.MultiSelect = enableMultipleSelect

        _entitiesAlreadyAdded = New List(Of AssessmentTestResourceDto)
        _entitiesIncompatibleToSelect = New List(Of AssessmentTestResourceDto)

        Me.TestGrid.ShowDisabledRowsAsGray = True
        Me.TestGrid.ShouldDoSelectionCheck = True
    End Sub



    Public Delegate Function CheckSupportedViewsOfTestDelegate(ByVal assessmentTest As AssessmentTest2, ByVal mustSupportViewTypes As List(Of String)) As Boolean

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


    Public Sub New(ByVal bankId As Integer)
        InitializeComponent()
        _bankId = bankId
        _entitiesAlreadyAdded = New List(Of AssessmentTestResourceDto)
        _entitiesIncompatibleToSelect = New List(Of AssessmentTestResourceDto)
        Me.TestGrid.GridContentContextMenuDisabled = True
    End Sub



    Public ReadOnly Property SelectedEntities() As ObjectModel.ReadOnlyCollection(Of AssessmentTestResourceDto)
        Get
            Return _selectedEntities
        End Get
    End Property

    Public Property SingleSelect As Boolean

    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return TestGrid.EntitiesProhibitedToSelect
        End Get
    End Property

    Public ReadOnly Property SelectedEntity() As AssessmentTestResourceDto
        Get
            Return _selectedEntity
        End Get
    End Property



    Public Event AddingResource As EventHandler(Of SelectedTestCollectionEventArgs)




    Public Sub RefreshDatasource()
        Me.Cursor = Cursors.WaitCursor
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetTestsForBank, _bankId))
    End Sub

    Private Sub OnAddingResource(ByVal sender As Object, ByVal e As SelectedTestCollectionEventArgs)
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
        If AddButton.Enabled Then
            OnAddingResource(sender, SelectedTestCollectionEventArgs.Empty)
        End If
    End Sub

    Protected Function DoDialogValidations() As Boolean
        Dim selectedCollection As List(Of AssessmentTestResourceDto) = (From entity In TestGrid.SelectedEntities.OfType(Of AssessmentTestResourceDto)() Where IsTestResourceAddable(entity)).ToList()
        _selectedEntities = New ObjectModel.ReadOnlyCollection(Of AssessmentTestResourceDto)(selectedCollection)
        If _selectedEntities.Count > 0 Then
            Return True
        Else
            MessageBox.Show(My.Resources.SelectTestResourceDialog_AddButton_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

    End Function

    Private Function IsTestResourceAddable(testResource As AssessmentTestResourceDto) As Boolean
        Dim testPackageDoesNotContainTest As Boolean = Not Me._entitiesAlreadyAdded.Contains(testResource)
        Dim testStateDoesNotProhibitSelection As Boolean = Not TestGrid.EntitiesProhibitedToSelect.Contains(testResource.resourceId)
        Dim testIsCompatableWithContext As Boolean = Not Me._entitiesIncompatibleToSelect.Contains(testResource)

        Return testPackageDoesNotContainTest AndAlso testStateDoesNotProhibitSelection AndAlso testIsCompatableWithContext
    End Function

    Private Sub GridBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)
        Select Case task.WorkerTask
            Case TaskType.GetTestsForBank
                task.Result = DtoFactory.Test.GetResourcesForBank(DirectCast(task.InputParameter, Integer))
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
                Case TaskType.GetTestsForBank
                    FillAssessmentTestCollection(task.Result)
                    TestGrid.DataSource = task.Result

            End Select
        End If
    End Sub

    Private Sub FillAssessmentTestCollection(taskResult As Object)
        Dim assessmentTestResourceDtos = TryCast(taskResult, IList(Of AssessmentTestResourceDto))
        If (assessmentTestResourceDtos IsNot Nothing) Then
            Dim ids = assessmentTestResourceDtos.Select(Function(t) t.resourceId).ToList
            For Each test In From resourceData In ResourceFactory.Instance.GetResourceDataByResourceIds(ids).OfType(Of ResourceDataEntity)()
                             Select AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(resourceData.BinData, True)

                _assessmentTests(test.AssessmentTestv2.Identifier) = test.AssessmentTestv2
            Next
        End If
    End Sub

    Private Sub SelectItemResourceEntityDialog_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetTestsForBank, _bankId))
    End Sub

    Private Sub TestGridControl_EntityDblClick(ByVal sender As System.Object, ByVal e As Questify.Builder.UI.EntityActionEventArgs) Handles TestGrid.EntityDblClick
        AddButton_Click(Me, New EventArgs)
    End Sub

    Private Sub TestGridControl_EntitySelected(ByVal sender As System.Object, ByVal e As Questify.Builder.UI.EntityActionEventArgs) Handles TestGrid.EntitySelected
        _selectedEntity = DirectCast(TestGrid.SelectedEntity, AssessmentTestResourceDto)
    End Sub


    Private Sub TestGridControl_FormattingRow(ByVal sender As System.Object, ByVal e As RowFormattingEventArgs) Handles TestGrid.FormattingRow
        If e.Resource IsNot Nothing AndAlso TypeOf e.Resource Is AssessmentTestResourceDto Then
            Dim assessmentTestResourceDto = DirectCast(e.Resource, AssessmentTestResourceDto)

            If (_resourceAlreadyInContextFunction IsNot Nothing AndAlso _resourceAlreadyInContextFunction.Invoke(e.Resource.name)) _
    OrElse (_testPackageResource IsNot Nothing AndAlso _testPackageResource.ContainsDependentResource(DirectCast(e.Resource, AssessmentTestResourceDto).resourceId)) Then
                e.Disabled = True
                _entitiesAlreadyAdded.Add(assessmentTestResourceDto)
                Exit Sub
            Else
                e.Disabled = False
            End If

            If _assessmentTests.ContainsKey(assessmentTestResourceDto.name) Then
                If _checkSupportedViewsOfTestFunction IsNot Nothing AndAlso Not _checkSupportedViewsOfTestFunction(_assessmentTests(assessmentTestResourceDto.name), GeneralHelper.GetViewsWithoutGeneral(Me._testPackage.IncludedViews.Select(Function(s) s.ToString()).ToList())) Then
                    e.Disabled = True
                    Exit Sub
                End If
            Else
                e.Disabled = True
            End If
            If e.Disabled Then
                _entitiesIncompatibleToSelect.Add(assessmentTestResourceDto)
            End If
        End If
    End Sub

    Private Sub TestGridControl_SynchronizeItems(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestGrid.SynchronizeItems
        RefreshDatasource()
    End Sub

    Private Sub CloseButton_Click(sender As System.Object, e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub TestGrid_SelectedRowChanged(sender As Object, e As EventArgs) Handles TestGrid.SelectedRowChanged
        AddButton.Enabled = Not TestGrid.GreyedOutItemsSelected
    End Sub



End Class