Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Enums

Public Class Word_ItemPreviewHandler
    Inherits ItemPreviewHandlerBase

    Public Sub New(ByVal handlerConfig As PluginHandlerConfigCollection, ByVal resourceManager As ResourceManagerBase)
        MyBase.New(handlerConfig, resourceManager)
    End Sub


    Protected Overrides ReadOnly Property UserFriendlyName As String
        Get
            Return "MS Word"
        End Get
    End Property

    Protected Overrides Sub CleanUp()
        If Not String.IsNullOrEmpty(_tempPath) Then
            Dim attempt As Integer = 0

            Do
                Try
                    IO.File.Delete(_tempPath)
                    Exit Do
                Catch ex As IO.IOException When attempt < 3
                    If IO.File.Exists(_tempPath) = False Then Exit Do

                    Threading.Thread.Sleep(250)
                Catch ex As Exception
                    Throw
                End Try
                attempt += 1
            Loop
        End If
    End Sub

    Protected Overrides ReadOnly Property PublicationLocation As String
        Get
            Return IO.Path.GetDirectoryName(_tempPath)
        End Get
    End Property

    Protected Overrides ReadOnly Property PreviewControl As PreviewControl
        Get
            Return PreviewControl.Word
        End Get
    End Property

    Public Overrides ReadOnly Property PreviewTarget As String
        Get
            Return PaperBasedTestPlugin.PLUGIN_NAME
        End Get
    End Property

    Protected Overrides ReadOnly Property ServiceName As String
        Get
            Return "Word"
        End Get
    End Property

    Protected Overrides Function SetupItemPreview(bankId As Integer, assessmentItem As AssessmentItem, isDebug As Boolean, publicationProperties As List(Of PublicationProperty)) As PublicationResult
        Dim msWordGen As New OpenXmlGenerator
        Dim publicationResult = New PublicationResult
        publicationResult.PublicationLocation = msWordGen.SetupPBTItemForPreview(_resourceManager, assessmentItem)
        Return publicationResult
    End Function
End Class
