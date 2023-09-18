Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.EventArguments
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Interfaces

Public Class ItemPreviewerBase
    Implements IItemPreviewer

    Protected PublicationUrl As String
    Protected ErrorPage As String = String.Empty
    Protected ScreenshotPath As String
    Protected DimensionsForScreenshot As Size

    Public Event ItemRenderingCompleted(sender As Object, e As EventArgs) Implements IItemPreviewer.ItemRenderingCompleted
    Public Event ItemValidatingRequired(sender As Object, e As ItemValidationRequiredEventArgs) Implements IItemPreviewer.ItemValidatingRequired

    Public Sub New()
        InitializeComponent()
    End Sub

    Protected ReadOnly Property IsRefreshNeeded As Boolean
        Get
            Return PublicationUrl IsNot Nothing
        End Get
    End Property

    Public Overridable Property ContextIdentifierForItemViewer As Integer? Implements IItemPreviewer.ContextIdentifierForItemViewer

    Public Overridable Sub DisposePreviewerEngine(handler As IItemPreviewHandler) Implements IItemPreviewer.DisposePreviewerEngine
    End Sub

    Public Overridable Function CreateScreenshot(handler As IItemPreviewHandler, bankId As Integer, assessmentItem As AssessmentItem, screenshotPath As String, size As Size, sequenceNumber As Integer, publicationProperties As List(Of PublicationProperty)) As String Implements IItemPreviewer.CreateScreenshot
        Throw New NotImplementedException
    End Function

    Public Overridable Function PreviewItem(handler As IItemPreviewHandler, bankId As Integer, assessmentItem As AssessmentItem, force As Boolean) As String Implements IItemPreviewer.PreviewItem
        Throw New NotImplementedException
    End Function

    Public Sub ResetRenderer(handler As IItemPreviewHandler) Implements IItemPreviewer.ResetRenderer
        handler.CleanUp()
    End Sub

    Public Overridable Sub StopItemPreview(handler As IItemPreviewHandler) Implements IItemPreviewer.StopItemPreview
    End Sub

    Protected Sub OnItemRenderingCompleted(sender As Object, e As EventArgs)
        RaiseEvent ItemRenderingCompleted(sender, e)
    End Sub

    Protected Sub OnItemValidatingRequired(sender As Object, e As ItemValidationRequiredEventArgs)
        RaiseEvent ItemValidatingRequired(sender, e)
    End Sub

    Public Overridable Sub DisposeItemPreviewer(disposing As Boolean) Implements IItemPreviewer.DisposeItemPreviewer

    End Sub
End Class