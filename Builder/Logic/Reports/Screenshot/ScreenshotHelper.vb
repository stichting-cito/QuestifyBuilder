Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Windows.Forms
Imports System.IO
Imports Cito.Tester.Common
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.Drawing
Imports System.Threading
Imports System.Threading.Tasks
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.EventArguments
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

Public Class ScreenshotHelper
    Implements IDisposable


    Private WithEvents _previewer As IItemPreviewer
    Private ReadOnly _resourceManager As DataBaseResourceManager
    Private ReadOnly _contextIdentifier As Integer

    Private _size As Size
    Private _errorMessage As String = String.Empty

    Private ReadOnly _tempPath As String = Path.Combine(TempStorageHelper.GetTempStoragePath, "TP_Screenshots")
    Private _itemCollection As EntityCollectionBase2(Of ItemResourceEntity)
    Private ReadOnly _bankId As Integer
    Private ReadOnly _itemPreviewHandler As IItemPreviewHandler

    Private _task As Task = Nothing
    Private ReadOnly _tokenSource As New CancellationTokenSource()
    Private ReadOnly _cancelToken As CancellationToken = _tokenSource.Token
    Private ReadOnly _publicationProperties As List(Of PublicationProperty) = New List(Of PublicationProperty)()


    Public Sub New(ByVal bankId As Integer, ByVal itemPreviewHandler As IItemPreviewHandler, previewer As IItemPreviewer)
        Me.New(bankId, itemPreviewHandler, Nothing, previewer)
    End Sub

    Public Sub New(ByVal bankId As Integer, ByVal itemPreviewHandler As IItemPreviewHandler, publicationProperties As List(Of PublicationProperty), previewer As IItemPreviewer)
        Me._bankId = bankId
        Me._itemPreviewHandler = itemPreviewHandler
        Me._resourceManager = New DataBaseResourceManager(bankId)
        Me._publicationProperties.Add(New PublicationProperty() With {.Key = "ScreenShot", .Value = String.Empty})
        Me._contextIdentifier = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(_resourceManager)

        If publicationProperties IsNot Nothing Then
            Me._publicationProperties.AddRange(publicationProperties)
        End If
        If previewer IsNot Nothing Then
            _previewer = previewer
            AddHandler TestSessionContext.ResourceNeeded, AddressOf GenericHandler_ResourceNeeded
        End If
    End Sub


    Public Event AllScreenshotsCompleted(ByVal sender As Object, ByVal e As ReportCompletedEventArgs)

    Private Sub OnAllScreenshotsCompleted(ByVal e As ReportCompletedEventArgs)
        RaiseEvent AllScreenshotsCompleted(Me, e)
    End Sub

    Public Event ItemScreenshotCompleted(ByVal sender As Object, ByVal e As ScreenshotCompletedEventArgs)

    Private Sub OnItemScreenshotCompleted(ByVal e As ScreenshotCompletedEventArgs)
        RaiseEvent ItemScreenshotCompleted(Me, e)
    End Sub

    Public Event ItemPreviewer_Progress(ByVal sender As Object, ByVal e As Cito.Tester.Common.ProgressEventArgs)

    Private Sub OnItemPreviewerProgress(ByVal e As Cito.Tester.Common.ProgressEventArgs)
        RaiseEvent ItemPreviewer_Progress(Me, e)
    End Sub




    Public ReadOnly Property ErrorMessage As String
        Get
            Return Me._errorMessage
        End Get
    End Property

    Public ReadOnly Property PreviewName() As String
        Get
            If Me._itemPreviewHandler IsNot Nothing Then
                Return Me._itemPreviewHandler.UserFriendlyName
            End If

            Return String.Empty
        End Get
    End Property


    Public Sub ResetAllScreenshotsCompletedEvent()
        Me.AllScreenshotsCompletedEvent = Nothing
    End Sub

    Public Async Function CreateScreenShotsAsync(ByVal itemCollection As EntityCollectionBase2(Of ItemResourceEntity), ByVal size As Size) As Task
        _size = size
        _itemCollection = itemCollection
        _itemPreviewHandler.ResourceManager = Me._resourceManager

        _task = Task.Factory.StartNew(Sub()
                                          CreateScreenshotForItem(0)
                                      End Sub, _cancelToken, TaskCreationOptions.LongRunning)

        For itemIndex = 1 To Me._itemCollection.Count - 1

            Dim index As Integer = itemIndex
            _task = _task.ContinueWith(Sub()
                                           Me.CreateScreenshotForItem(index)
                                       End Sub, _cancelToken)
        Next

        _task = _task.ContinueWith(Sub()
                                       Me.OnAllScreenshotsCompleted(New ReportCompletedEventArgs(String.IsNullOrEmpty(_errorMessage)))
                                   End Sub)

        Await _task
    End Function

    Private Sub CreateScreenshotForItem(itemIndex As Integer)

        Dim itemResource As ItemResourceEntity = Me._itemCollection.Item(itemIndex)

        Dim waitUntilDone As New AutoResetEvent(False)
        Dim itemRenderingCompleted As Boolean = False
        Dim itemRenderingCompletedEventHandler As EventHandler(Of EventArgs) = Sub()
                                                                                   itemRenderingCompleted = True
                                                                                   waitUntilDone.Set()
                                                                               End Sub

        Me.OnItemPreviewerProgress(New Cito.Tester.Common.ProgressEventArgs(String.Format(My.Resources.CreatingScreenshot, itemResource.Title)))

        Dim fileName As String = String.Empty
        Try
            AddHandler _previewer.ItemRenderingCompleted, itemRenderingCompletedEventHandler

            Dim itemLayoutTemplateResource As ItemLayoutTemplateResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByNameWithOption(_bankId, itemResource.ItemLayoutTemplateUsedName, New ResourceRequestDTO()), ItemLayoutTemplateResourceEntity)

            Using itemSetupHelper As New AssessmentItemHelper(Me._resourceManager, itemLayoutTemplateResource.Name, itemResource, Nothing)
                Dim assessmentItem As AssessmentItem = itemSetupHelper.GetExistingAssessmentItem()

                Me._previewer.ContextIdentifierForItemViewer = Me._contextIdentifier
                fileName = Me._previewer.CreateScreenshot(Me._itemPreviewHandler, Me._bankId, assessmentItem, Me._tempPath, Me._size, itemIndex + 1, Me._publicationProperties)
            End Using

        Catch ex As Exception
            waitUntilDone.Set()
            Me._errorMessage = ex.Message
        Finally
            Dim completed As Boolean = False

            While Not completed
                completed = waitUntilDone.WaitOne(1000)
                Application.DoEvents()
            End While

            If itemRenderingCompleted Then
                Me._previewer.StopItemPreview(_itemPreviewHandler)

                If Not String.IsNullOrEmpty(fileName) Then
                    Me.OnItemScreenshotCompleted(New ScreenshotCompletedEventArgs(itemResource, fileName))
                End If
            End If
            If _previewer IsNot Nothing Then
                RemoveHandler Me._previewer.ItemRenderingCompleted, itemRenderingCompletedEventHandler
            End If

        End Try
    End Sub

    Public Function InitScreenshots() As String
        If Not Directory.Exists(Me._tempPath) Then
            Directory.CreateDirectory(Me._tempPath)
        End If

        Return Me._tempPath
    End Function



    Private Sub ItemPreviewInstance_ItemValidatingRequired(ByVal sender As Object, ByVal e As ItemValidationRequiredEventArgs) Handles _previewer.ItemValidatingRequired
        e.ValidationValid = True
    End Sub

    Private Sub GenericHandler_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub


    Private disposedValue As Boolean = False


    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

            End If
            If Directory.Exists(Me._tempPath) Then
                Directory.Delete(Me._tempPath, True)
            End If

            Me.AllScreenshotsCompletedEvent = Nothing
            Me.ItemScreenshotCompletedEvent = Nothing
            Me.ItemPreviewer_ProgressEvent = Nothing

            RemoveHandler TestSessionContext.ResourceNeeded, AddressOf GenericHandler_ResourceNeeded

            If Me._itemPreviewHandler IsNot Nothing and _previewer IsNot Nothing Then
                Me._previewer.DisposePreviewerEngine(_itemPreviewHandler)
                Me._previewer.DisposeItemPreviewer(True)
            End If

            TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(_resourceManager)
            Me._resourceManager.Dispose()
        End If

        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Sub Cancel()
        Me._tokenSource.Cancel()
    End Sub

End Class
