Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Enums
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.UI.PreviewService

Public Class Word_ItemPreviewer
    Public Sub New()
        MyBase.New()

        InitializeComponent()
    End Sub

    Private _handler As IItemPreviewHandler
    Private _bankId As Integer
    Private _item As AssessmentItem
    Private _itemHash As Byte()
    Private _lastPreview As New KeyValuePair(Of String, Byte())
    Private _lastStartedPreviewHash As Byte()

    Public Overrides Function CreateScreenshot(ByVal handler As IItemPreviewHandler, ByVal bankId As Integer, ByVal assessmentItem As AssessmentItem, ByVal screenshotPath As String, ByVal size As Size, sequenceNumber As Integer, ByVal publicationProperties As List(Of PublicationProperty)) As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function PreviewItem(ByVal handler As IItemPreviewHandler, ByVal bankId As Integer, ByVal assessmentItem As AssessmentItem, force As Boolean) As String
        Debug.Assert(handler.PreviewControl = PreviewControl.Word)
        Dim md5Hash As Byte() = assessmentItem.GetMD5Hash
        If force OrElse _lastPreview.Value Is Nothing OrElse Not md5Hash.SequenceEqual(_lastPreview.Value) Then
            _handler = handler
            _bankId = bankId
            _item = assessmentItem
            _itemHash = md5Hash
            _lastStartedPreviewHash = _itemHash

            PreviewItemAsync()
            PublicationUrl = _lastPreview.Key
        Else
            LoadPreview()
        End If
        Return PublicationUrl
    End Function

    Private Sub LoadPreview()
        ItemPreviewPanel.PreviewSource = Me.PublicationUrl
    End Sub

    Private Async Sub PreviewItemAsync()
        Dim task = Await GetPreviewAsync(_handler, _bankId, _item)
        If task IsNot Nothing Then
            If _lastStartedPreviewHash IsNot Nothing AndAlso task.Item1.SequenceEqual(_lastStartedPreviewHash) Then
                PublicationUrl = task.Item2.PublicationLocation
                LoadPreview()
                _lastPreview = New KeyValuePair(Of String, Byte())(Me.PublicationUrl, _itemHash)
            End If
        End If
    End Sub

    Private Async Function GetPreviewAsync(ByVal handler As IItemPreviewHandler, ByVal bankId As Integer, ByVal assessmentItem As AssessmentItem) As Task(Of Tuple(Of Byte(), PublicationResult))
        Dim taskRet As New Task(Of Tuple(Of Byte(), PublicationResult))(Function()
                                                                            Dim hash As Byte() = _itemHash
                                                                            Dim r = handler.SetupItemPreview(bankId, assessmentItem, False, GetPublicationProperties())
                                                                            Return New Tuple(Of Byte(), PublicationResult)(hash, r)
                                                                        End Function)

        taskRet.Start()
        Return Await taskRet
    End Function

    Public Overrides Sub StopItemPreview(ByVal handler As IItemPreviewHandler)
        _lastStartedPreviewHash = Nothing
        handler.CleanUp()
        ItemPreviewPanel.ClearPreviewPane()
    End Sub

    Private Function GetPublicationProperties() As List(Of PublicationProperty)
        Return Nothing
    End Function
End Class