Imports System.Linq
Imports System.ComponentModel
Imports System.Threading.Tasks
Imports Enums
Imports Questify.Builder.Client.BuilderTasksService

Public Class HarmonizeDuringImport

    Private ReadOnly _resourceType As ResourceTypeEnum
    Private ReadOnly _resourceIds As IEnumerable(Of Guid)
    Private ReadOnly _itemCodes As IEnumerable(Of String)
    Private ReadOnly _bankId As Integer


    Public Sub New(resourceType As ResourceTypeEnum, resourceIds As IEnumerable(Of Guid))
        _resourceType = resourceType
        _resourceIds = resourceIds
    End Sub

    Public Sub New(bankId As Integer, templatesToHarmonize As IEnumerable(Of Guid), itemCodesToHarmonize As IEnumerable(Of String))
        _bankId = bankId
        _resourceIds = templatesToHarmonize
        _itemCodes = itemCodesToHarmonize
        _resourceType = ResourceTypeEnum.ItemLayoutTemplateResource Or ResourceTypeEnum.ItemResource
    End Sub


    Public Sub HarmonizeOnServer(sender As Object, e As DoWorkEventArgs)
        Dim worker = CType(sender, BackgroundWorker)
        Dim qBTasksClient As New QuestifyBuilderTasksServiceClient()
        Try
            Dim harmonizationTicket As BuilderTaskSessionTicket = Nothing
            If _resourceType = (ResourceTypeEnum.ItemLayoutTemplateResource Or ResourceTypeEnum.ItemResource) Then
                harmonizationTicket = qBTasksClient.HarmonizeAfterImport(_bankId, _resourceIds.ToList, _itemCodes.ToList)
            ElseIf _resourceType = ResourceTypeEnum.ItemLayoutTemplateResource Then
                harmonizationTicket = qBTasksClient.HarmonizeWithItemLayoutTemplates(_resourceIds.ToList, False)
            ElseIf _resourceType = ResourceTypeEnum.ItemResource Then
                harmonizationTicket = qBTasksClient.HarmonizeItems(_resourceIds.ToList, False)
            End If
            Dim harmonizationProgress As BuilderTaskProgress = qBTasksClient.PollProgress(harmonizationTicket)
            Dim cancellationRequestPassedOnToService As Boolean = False

            While harmonizationProgress IsNot Nothing AndAlso harmonizationProgress.State = BuilderTaskProgress.ExecutionState.Running
                Dim lastItem = harmonizationProgress.ProgressItems.LastOrDefault
                If lastItem IsNot Nothing Then
                    If lastItem.TotalCount > 0 Then
                        Dim percentage = CInt(Math.Floor(lastItem.ProcessedCount / lastItem.TotalCount * 100))
                        Dim message = lastItem.ProgressItemCode
                        worker.ReportProgress(percentage, message)
                    End If
                    harmonizationProgress = qBTasksClient.PollProgress(harmonizationTicket)
                End If
                If worker.CancellationPending Then
                    If Not cancellationRequestPassedOnToService Then
                        qBTasksClient.RequestCancellation(harmonizationTicket)
                        cancellationRequestPassedOnToService = True
                    End If
                End If

                Task.Delay(1000).Wait()
            End While

            e.Result = qBTasksClient.GetTaskResult(harmonizationTicket)
            worker.ReportProgress(0, String.Empty)
            qBTasksClient.Cleanup(harmonizationTicket)
        Catch timeout As TimeoutException
            MessageBox.Show(String.Format(My.Resources.ServiceNotAvailable, My.Resources.Harmonization, vbNewLine & vbNewLine & "- " & timeout.Message), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
