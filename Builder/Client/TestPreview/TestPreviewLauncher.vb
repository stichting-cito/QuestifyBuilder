Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Net
Imports Questify.Builder.UI.PublicationService
Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports System.Threading
Imports System.Web
Imports Questify.Builder.Logic.Service.Interfaces

Public Class TestPreviewLauncher

    Private _worker As BackgroundWorker
    Private ReadOnly _bankId As Integer
    Private ReadOnly _test As String
    Private ReadOnly _isPackage As Boolean
    Private ReadOnly _testPreviewHandler As TestPreviewHandlerIdentifier
    Private _totalNumberOfProgressSteps As Integer

    Public Property Errors As List(Of CustomServiceMessage)

    Public Sub New(bankId As Integer, test As String, testPreviewHandler As TestPreviewHandlerIdentifier, isPackage As Boolean)
        _bankId = bankId
        _testPreviewHandler = testPreviewHandler
        _test = test
        _isPackage = isPackage
    End Sub

    Public Sub PreviewTest(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        _worker = DirectCast(sender, BackgroundWorker)
        _worker.WorkerSupportsCancellation = True
        _worker.WorkerReportsProgress = True
        If Errors Is Nothing Then
            Errors = New List(Of CustomServiceMessage)
        Else
            Errors.Clear()
        End If
        Using publicationClient = New PublicationServiceClient()
            Try
                Dim configOptions As New Dictionary(Of String, String)
                If Not _testPreviewHandler.IsClickOnce AndAlso Not String.IsNullOrEmpty(_testPreviewHandler.Url) Then
                    configOptions.Add(PublicationHandlerConfigurationOptions.PostToServer, _testPreviewHandler.Url)
                End If
                Dim taskId = publicationClient.Publicize(_testPreviewHandler.PublicationHandlerType, configOptions, _bankId, New String() {_test}, New String() {}, True, Nothing)
                Dim publicationTaskProgress = publicationClient.GetProgress(taskId)
                Dim lastTotal As Integer = 0
                While Not publicationTaskProgress.Finished
                    If _worker.CancellationPending Then Return
                    If Not lastTotal = publicationTaskProgress.Total Then
                        lastTotal = publicationTaskProgress.Total
                        Report_PublicationStart(Me, New StartEventArgs(lastTotal))
                    End If
                    Report_PublicationProgress(Me, New ProgressEventArgs(
                        $"{publicationTaskProgress.Progress}/{lastTotal} - {publicationTaskProgress.ProgressString}", publicationTaskProgress.Progress))
                    Thread.Sleep(100)
                    publicationTaskProgress = publicationClient.GetProgress(taskId)
                End While

                If _worker.CancellationPending Then Return

                If publicationTaskProgress.Succeeded Then
                    Dim applicationSetting = New ApplicationSetting(_testPreviewHandler.UserFriendlyName, _testPreviewHandler.DefaultClient)
                    Dim previewClient = applicationSetting.GetValue()

                    If _testPreviewHandler.IsClickOnce Then
                        If String.IsNullOrEmpty(previewClient) Then
                            MessageBox.Show(My.Resources.TheLocationOfTheTestPreviewPlayerIsnTSpecifiedInTheConfigurationFile, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            Dim tempPackageLocation = DownloadPackage(publicationTaskProgress.PublicationLocations.FirstOrDefault(), _test, _testPreviewHandler.FileExtension)
                            Dim launchUrl As String = String.Empty
                            If Not _isPackage Then
                                launchUrl = $"{previewClient}?package={HttpUtility.UrlEncode(
                                    $"file:///{tempPackageLocation}/")}&testmode={True.ToString}"
                            Else
                                launchUrl = $"{previewClient}?package={HttpUtility.UrlEncode(
                                    $"file:///{tempPackageLocation}/")}&testmode={True.ToString}&testid={_test}"
                            End If

                            Dim arguments As String = $"dfshim.dll,ShOpenVerbApplication {launchUrl}"
                            Process.Start("rundll32.exe", arguments)
                        End If
                    ElseIf Not String.IsNullOrEmpty(_testPreviewHandler.Url) Then
                        If String.IsNullOrEmpty(previewClient) Then
                            MessageBox.Show($"Preview Client for preview '{_testPreviewHandler.UserFriendlyName}' not specified.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            previewClient = Environment.ExpandEnvironmentVariables(previewClient)

                            If Not File.Exists(previewClient) Then
                                Throw New FileNotFoundException("Could not find preview client: " + previewClient)
                            End If

                            Process.Start(previewClient, $"--facet-overwrite-url={publicationTaskProgress.PublicationUrls.First()}")
                        End If
                    Else
                        Dim tempPackageLocation = DownloadPackage(publicationTaskProgress.PublicationLocations.FirstOrDefault(), _test, _testPreviewHandler.FileExtension)
                        Process.Start(tempPackageLocation)
                    End If
                ElseIf Not String.IsNullOrEmpty(publicationTaskProgress.Errors) Then
                    Errors.Add(New CustomServiceMessage("ErrorPublicationServiceUnableToPreview", publicationTaskProgress.Errors))
                ElseIf Not String.IsNullOrEmpty(publicationTaskProgress.Warnings) Then
                    MessageBox.Show(publicationTaskProgress.Warnings)
                End If
                publicationClient.FinishPublication(taskId)

            Catch ex As Exception
                Debug.Assert(True, "Failed to preview. Exception: " + ex.Message)
                publicationClient.Abort()
                Errors.Add(New CustomServiceMessage("ErrorUnableToCreatePreview", ex.Message))
            End Try
        End Using
    End Sub

    Private Function DownloadPackage(url As String, testName As String, extension As String) As String
        Dim returnValue =
                $"{Path.Combine(TempStorageHelper.GetTempStoragePath, testName)}_{Guid.NewGuid}.{extension}"
        Try
            Using webClient = New WebClient()
                AddHandler webClient.DownloadProgressChanged, AddressOf DownloadProgressCallback
                webClient.DownloadFileTaskAsync(url, returnValue).Wait()
                RemoveHandler webClient.DownloadProgressChanged, AddressOf DownloadProgressCallback
            End Using
        Catch e As Exception
            Errors.Add(New CustomServiceMessage(e.Message))
        End Try

        Return returnValue
    End Function

    Private Sub Report_PublicationStart(sender As Object, e As StartEventArgs)
        _totalNumberOfProgressSteps = e.NumberOfResources
    End Sub

    Private Sub Report_PublicationProgress(sender As Object, e As ProgressEventArgs)
        Dim percentageCompleted As Integer = 0
        If Not _totalNumberOfProgressSteps = 0 Then
            percentageCompleted = CInt(Math.Round(CType((100 / _totalNumberOfProgressSteps) * e.ProgessValue, Double)))
        End If
        _worker.ReportProgress(percentageCompleted, e.StatusMessage)
    End Sub

    Private Sub DownloadProgressCallback(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Report_PublicationStart(sender, New StartEventArgs(CInt(e.TotalBytesToReceive)))
        Report_PublicationProgress(sender, New ProgressEventArgs(String.Empty, CInt(e.BytesReceived)))
    End Sub

End Class