Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ScreenshotCompletedEventArgs
    Inherits EventArgs

    Public Sub New(itemResource As ItemResourceEntity, screenshotName As String)
        MyBase.New()

        Me.ItemResource = itemResource
        Me.ScreenshotName = screenshotName
    End Sub

    Public ReadOnly Property ItemResource() As ItemResourceEntity

    Public ReadOnly Property ScreenshotName() As String

End Class