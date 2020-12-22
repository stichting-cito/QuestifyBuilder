Imports System.Linq
Imports System.ComponentModel
Imports System.Threading.Tasks
Imports Questify.Builder.Client.BuilderTasksService
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.UI.Commanding

Friend Class RefreshItemLayouts
    Inherits CommandBase


    Private ReadOnly _displayText As String
    Private ReadOnly _resource As ResourceDto
    Private _resources As IList(Of ResourceDto)


    Public Sub New(ByVal resource As ResourceDto,
                   ByVal displayText As String)
        _resource = resource
        _displayText = displayText
    End Sub

    Public Sub New(ByVal resources As IList(Of ResourceDto), ByVal displayText As String)
        _resources = resources
        _displayText = displayText
    End Sub


    Public Overrides Sub Execute(ByVal parameter As Object)

        If _resource IsNot Nothing OrElse _resources IsNot Nothing AndAlso _resources.Count > 0 Then
            Dim dlg As ProgressDialog
            dlg = New ProgressDialog(Name, False, New DoWorkEventHandler(AddressOf LongRunningProcess),
                                                      Sub(p As ProgressDialog.ProgressHandler)
                                                          If p.Progress Is Nothing Then
                                                              p.ProgressMarquee()
                                                          Else
                                                              If (p.Progress.UserState IsNot Nothing) Then
                                                                  p.SetText(p.Progress.UserState.ToString())
                                                              End If
                                                              p.ProgressBar(p.Progress.ProgressPercentage)
                                                              p.ProgressBlocks()
                                                          End If
                                                      End Sub)

            dlg.ShowDialog()
        End If

    End Sub

    Private Sub LongRunningProcess(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Using worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)

            Dim resourcesToProcess As List(Of Guid)
            If _resources Is Nothing Then
                _resources = New ResourceDto() {_resource}.ToList
            End If
            resourcesToProcess = _resources.Select(Function(r) r.resourceId).ToList
            Dim qBTasksClient As New QuestifyBuilderTasksServiceClient()

            Try
                Dim harmonizationTicket As BuilderTaskSessionTicket
                If _resources.Count > 0 AndAlso TypeOf _resources(0) Is ItemLayoutTemplateResourceDto Then
                    harmonizationTicket = qBTasksClient.HarmonizeWithItemLayoutTemplates(resourcesToProcess, False)
                ElseIf _resources.Count > 0 AndAlso TypeOf _resources(0) Is ItemResourceDto Then
                    harmonizationTicket = qBTasksClient.HarmonizeItems(resourcesToProcess, False)
                Else
                    Exit Sub
                End If
                Dim harmonizationProgress As BuilderTaskProgress = qBTasksClient.PollProgress(harmonizationTicket)
                Dim cancellationRequestPassedOnToService As Boolean = False

                While harmonizationProgress IsNot Nothing AndAlso harmonizationProgress.State = BuilderTaskProgress.ExecutionState.Running
                    Dim percentage = 0
                    Dim lastItem = harmonizationProgress.ProgressItems.LastOrDefault
                    If lastItem IsNot Nothing Then
                        percentage = CInt(Math.Floor(lastItem.ProcessedCount / lastItem.TotalCount * 100))
                        worker.ReportProgress(percentage, lastItem.ProgressItemCode)
                    End If
                    If worker.CancellationPending Then
                        If Not cancellationRequestPassedOnToService Then
                            qBTasksClient.RequestCancellation(harmonizationTicket)
                            cancellationRequestPassedOnToService = True
                        End If
                    End If
                    harmonizationProgress = qBTasksClient.PollProgress(harmonizationTicket)

                    Task.Delay(1000).Wait()
                End While
                Dim harmonizationTaksResult As BuilderTaskResult = qBTasksClient.GetTaskResult(harmonizationTicket)
                If harmonizationTaksResult IsNot Nothing Then
                    e.Cancel = (harmonizationTaksResult.TaskTermination = BuilderTaskResult.TaskTerminationState.Cancelled)
                End If
                worker.ReportProgress(0, string.Empty)
                qBTasksClient.Cleanup(harmonizationTicket)
            Catch timeout As TimeoutException
                MessageBox.Show(String.Format(My.Resources.ServiceNotAvailable, My.Resources.Harmonization, vbNewLine & vbNewLine & "- " & timeout.Message), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.Cancel = True
            End Try
        End Using
    End Sub

    Protected Overrides Function GetCanExecuteState(ByVal parameter As Object) As Boolean
        Return True
    End Function


    Public Overrides ReadOnly Property Image As Image
        Get
            Return My.Resources.RefreshToolStripButton_Image
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return _displayText
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized As String
        Get
            Return _displayText
        End Get
    End Property


End Class
