Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class SelectTestTemplateResourceDialog


    Private ReadOnly _bankId As Integer
    Private _selectedEntity As AssessmentTestResourceDto




    Public Sub New(ByVal bankId As Integer)
        InitializeComponent()
        _bankId = bankId
        Me.TestTemplateGridControl.GridContentContextMenuDisabled = True
        Me.TestTemplateGridControl.ShowDisabledRowsAsGray = True
        Me.TestTemplateGridControl.ShouldDoSelectionCheck = True
    End Sub



    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return TestTemplateGridControl.EntitiesProhibitedToSelect
        End Get
    End Property

    Public ReadOnly Property SelectedEntity() As AssessmentTestResourceDto
        Get
            Return _selectedEntity
        End Get
    End Property



    Protected Overrides Function DoDialogValidations() As Boolean
        If _selectedEntity IsNot Nothing Then
            Return True
        Else
            MessageBox.Show(My.Resources.SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
    End Function

    Private Sub GridBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)

        Select Case task.WorkerTask
            Case TaskType.GetTestsForBank
                task.Result = DtoFactory.TestTemplate.GetResourcesForBank(DirectCast(task.InputParameter, Integer))
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
                Case TaskType.GetTestsForBank
                    TestTemplateGridControl.DataSource = task.Result
            End Select
        End If
    End Sub

    Private Sub SelectItemResourceEntityDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetTestsForBank, _bankId))
    End Sub

    Private Sub TestTemplateGridControl_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles TestTemplateGridControl.EntityDblClick
        OkButton_Click(Me, New EventArgs)
    End Sub

    Private Sub TestTemplateGridControl_EntitySelected(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles TestTemplateGridControl.EntitySelected
        _selectedEntity = DirectCast(TestTemplateGridControl.SelectedEntity, AssessmentTestResourceDto)
    End Sub


End Class