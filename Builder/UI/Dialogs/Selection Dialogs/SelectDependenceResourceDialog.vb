Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.Interfaces

Public Class SelectDependenceResourceDialog


    Private _propertyEntity As IPropertyEntity



    Public Sub New(ByVal propertyEntity As IPropertyEntity)
        InitializeComponent()

        _propertyEntity = propertyEntity

        Me.ItemGrid.GridContentContextMenuDisabled = True
        Me.ControlTemplateGrid.GridContentContextMenuDisabled = True
        Me.ItemLayoutTemplateGrid.GridContentContextMenuDisabled = True
        Me.MediaGrid.GridContentContextMenuDisabled = True
        Me.AspectGrid.GridContentContextMenuDisabled = True
        Me.ItemGrid.ShowDisabledRowsAsGray = True
        Me.ControlTemplateGrid.ShowDisabledRowsAsGray = True
        Me.ItemLayoutTemplateGrid.ShowDisabledRowsAsGray = True
        Me.MediaGrid.ShowDisabledRowsAsGray = True
        Me.AspectGrid.ShowDisabledRowsAsGray = True
    End Sub

    Private Sub ItemLayoutTemplateGridControl_SelectedRowChanged(sender As Object, e As System.EventArgs) Handles ItemLayoutTemplateGrid.SelectedRowChanged
        OkButton.Enabled = Not ItemLayoutTemplateGrid.GreyedOutItemsSelected
    End Sub

    Private Sub ItemGridControl_SelectedRowChanged(sender As Object, e As System.EventArgs) Handles ItemGrid.SelectedRowChanged
        OkButton.Enabled = Not ItemGrid.GreyedOutItemsSelected
    End Sub

    Private Sub ControlTemplateGrid_SelectedRowChanged(sender As Object, e As System.EventArgs) Handles ControlTemplateGrid.SelectedRowChanged
        OkButton.Enabled = Not ControlTemplateGrid.GreyedOutItemsSelected
    End Sub

    Private Sub MediaGrid_SelectedRowChanged(sender As Object, e As System.EventArgs) Handles MediaGrid.SelectedRowChanged
        OkButton.Enabled = Not MediaGrid.GreyedOutItemsSelected
    End Sub

    Private Sub AspectGrid_SelectedRowChanged(sender As Object, e As System.EventArgs) Handles AspectGrid.SelectedRowChanged
        OkButton.Enabled = Not AspectGrid.GreyedOutItemsSelected
    End Sub

    Private Sub HandleFormattingRowEvents(sender As Object, e As RowFormattingEventArgs) Handles ItemGrid.FormattingRow, ControlTemplateGrid.FormattingRow, ItemLayoutTemplateGrid.FormattingRow, MediaGrid.FormattingRow, AspectGrid.FormattingRow
        e.Disabled = _propertyEntity.ContainsDependentResource(e.Resource.ResourceId)
    End Sub


    Public ReadOnly Property SelectedEntities() As IList(Of ResourceDto)
        Get
            Return GetSelectedEntities()
        End Get
    End Property




    Private Function DoDialogValidations() As Boolean
        Return Not GetSelectedEntities().Count.Equals(0)
    End Function


    Private Function GetSelectedEntities() As List(Of ResourceDto)
        Dim entities As New List(Of ResourceDto)

        Select Case ResourceTabControl.SelectedTab.Name
            Case ItemTabPage.Name
                entities = ItemGrid.SelectedEntities.ToList

            Case ItemLayoutTemplateTabPage.Name
                entities = ItemLayoutTemplateGrid.SelectedEntities.ToList

            Case ControlLayoutTemplateTabPage.Name
                entities = ControlTemplateGrid.SelectedEntities.ToList

            Case GenericTabPage.Name
                entities = MediaGrid.SelectedEntities.ToList

            Case AspectTabPage.Name
                entities = AspectGrid.SelectedEntities.ToList
        End Select

        Return entities
    End Function


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        If DoDialogValidations() Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show(My.Resources.SelectDependentResourceDialog_NoResourceSelectedMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CustomButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomButton.Click
    End Sub



    Private Sub BackGroundDataWorkerPool_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackGroundDataWorkerPool.DoWork
        Dim task As BackgroundWorkerTask = DirectCast(e.Argument, BackgroundWorkerTask)
        Dim worker As System.ComponentModel.BackgroundWorker = DirectCast(sender, System.ComponentModel.BackgroundWorker)
        Dim bankId As Integer = CType(task.InputParameter, Integer)
        Select Case task.WorkerTask
            Case TaskType.GetMediaForBank
                task.Result = DtoFactory.Generic.GetResourcesForBank(bankId)

            Case TaskType.GetItemLayoutTemplatesForBank
                task.Result = DtoFactory.ItemLayoutTemplate.GetResourcesForBank(bankId)

            Case TaskType.GetControlTemplatesForBank
                task.Result = DtoFactory.ControlTemplate.GetResourcesForBank(bankId)

            Case TaskType.GetItemsForBank
                task.Result = DtoFactory.Item.GetResourcesForBank(bankId)

            Case TaskType.GetAspectsForBank
                task.Result = DtoFactory.Aspect.GetResourcesForBank(bankId)

            Case Else
                Throw New NotSupportedException()
        End Select

        e.Result = task
        If worker.CancellationPending Then e.Cancel = True
    End Sub

    Private Sub BackGroundDataWorkerPool_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackGroundDataWorkerPool.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            MessageBox.Show(My.Resources.ErrorThrown & vbCrLf & vbCrLf & e.Error.Message)
        End If

        If e.Error Is Nothing AndAlso Not e.Cancelled Then
            Dim task As BackgroundWorkerTask = DirectCast(e.Result, BackgroundWorkerTask)

            Select Case task.WorkerTask
                Case TaskType.GetMediaForBank
                    MediaGrid.DataSource = task.Result

                Case TaskType.GetItemLayoutTemplatesForBank
                    ItemLayoutTemplateGrid.DataSource = task.Result

                Case TaskType.GetControlTemplatesForBank
                    ControlTemplateGrid.DataSource = task.Result

                Case TaskType.GetItemsForBank
                    ItemGrid.DataSource = task.Result

                Case TaskType.GetAspectsForBank
                    AspectGrid.DataSource = task.Result

                Case Else
                    Throw New NotSupportedException()
            End Select
        End If
    End Sub

    Private Sub SelectDependenceResourceDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CustomButton.Visible = False
        ItemTabPage.Select()
        BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(TaskType.GetItemsForBank, _propertyEntity.BankId), "FillGrid")
    End Sub

    Private Sub MediaGridControl_EntityDblClick(ByVal sender As System.Object, ByVal e As Questify.Builder.UI.EntityActionEventArgs) Handles MediaGrid.EntityDblClick
        OK_Button_Click(Me, New EventArgs)
    End Sub

    Private Sub ResourceTabControl_Selected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles ResourceTabControl.Selected
        Dim inputParameters As New Hashtable(2)
        Dim tType As TaskType

        inputParameters.Add("Bank", _propertyEntity.Bank)

        Select Case e.TabPage.Name
            Case ItemTabPage.Name
                tType = TaskType.GetItemsForBank
                OkButton.Enabled = Not ItemGrid.GreyedOutItemsSelected

            Case ItemLayoutTemplateTabPage.Name
                tType = TaskType.GetItemLayoutTemplatesForBank
                OkButton.Enabled = Not ItemLayoutTemplateGrid.GreyedOutItemsSelected

            Case ControlLayoutTemplateTabPage.Name
                tType = TaskType.GetControlTemplatesForBank
                OkButton.Enabled = Not ControlTemplateGrid.GreyedOutItemsSelected

            Case GenericTabPage.Name
                tType = TaskType.GetMediaForBank
                OkButton.Enabled = Not MediaGrid.GreyedOutItemsSelected

            Case AspectTabPage.Name
                tType = TaskType.GetAspectsForBank
                OkButton.Enabled = Not AspectGrid.GreyedOutItemsSelected
            Case Else
                Throw New NotSupportedException()
        End Select

        BackGroundDataWorkerPool.StartNewTask(New BackgroundWorkerTask(tType, _propertyEntity.BankId), "FillGrid")

    End Sub


End Class