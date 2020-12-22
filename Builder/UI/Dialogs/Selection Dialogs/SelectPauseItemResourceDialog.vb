Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities


Public Class SelectPauseItemResourceDialog


    Private ReadOnly _bankId As Integer
    Private _selectedEntity As ItemResourceDto



    Public ReadOnly Property EntitiesProhibitedToSelect() As List(Of Guid)
        Get
            Return PauseItemGridControl.EntitiesProhibitedToSelect
        End Get
    End Property

    Public ReadOnly Property SelectedEntity() As ItemResourceDto
        Get
            Return _selectedEntity
        End Get
    End Property




    Public Sub New(ByVal bankId As Integer)
        InitializeComponent()

        _bankId = bankId
        Me.PauseItemGridControl.ShouldDoSelectionCheck = True
        Me.PauseItemGridControl.GridContentContextMenuDisabled = True
        Me.PauseItemGridControl.ShowDisabledRowsAsGray = True
    End Sub




    Protected Overrides Function DoDialogValidations() As Boolean
        If _selectedEntity Is Nothing Then
            MessageBox.Show(My.Resources.SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        Else
            Dim e As New ValidateResourceEventArgs(_selectedEntity)
            RaiseEvent ValidateEntity(Me, e)
            Return e.Valid
        End If
    End Function



    Public Event ValidateEntity As EventHandler(Of ValidateResourceEventArgs)




    Private Sub GridBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles GridBackgroundWorker.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)

        Select Case task.WorkerTask
            Case TaskType.GetItemsForBank
                task.Result = DtoFactory.Item.GetPauseItemList(DirectCast(task.InputParameter, Integer))

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
                Case TaskType.GetItemsForBank
                    PauseItemGridControl.DataSource = task.Result
            End Select
        End If
    End Sub

    Private Sub SelectItemResourceEntityDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GridBackgroundWorker.RunWorkerAsync(New BackgroundWorkerTask(TaskType.GetItemsForBank, _bankId))
    End Sub

    Private Sub PauseItemGridControl_EntityDblClick(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles PauseItemGridControl.EntityDblClick
        OkButton_Click(Me, New EventArgs)
    End Sub

    Private Sub PauseItemGridControl_EntitySelected(ByVal sender As Object, ByVal e As EntityActionEventArgs) Handles PauseItemGridControl.EntitySelected
        _selectedEntity = DirectCast(PauseItemGridControl.SelectedEntity, ItemResourceDto)
    End Sub


End Class
