Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.Service.Enums
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Interfaces

Public MustInherit Class ItemPreviewHandlerBase
    Implements IItemPreviewHandler

    Protected _resourceManager As ResourceManagerBase
    Protected _tempPath As String


    Public Sub New(ByVal handlerConfig As PluginHandlerConfigCollection, ByVal resourceManager As ResourceManagerBase)
        _resourceManager = resourceManager
    End Sub

    Public Overridable ReadOnly Property Dimensions As Dictionary(Of String, System.Drawing.Size) Implements IItemPreviewHandler.Dimensions
        Get
            Return Nothing
        End Get
    End Property

    Public Overridable ReadOnly Property ShowTestMonitor() As Boolean Implements IItemPreviewHandler.ShowTestMonitor
        Get
            Return False
        End Get
    End Property

    Public Property ResourceManager As ResourceManagerBase Implements IItemPreviewHandler.ResourceManager
        Get
            Return _resourceManager
        End Get
        Set(value As ResourceManagerBase)
            _resourceManager = value
        End Set
    End Property


    Protected MustOverride Function SetupItemPreview(ByVal bankId As Integer, ByVal assessmentItem As AssessmentItem, ByVal isDebug As Boolean, ByVal publicationProperties As List(Of PublicationProperty)) As PublicationResult Implements IItemPreviewHandler.SetupItemPreview

    Protected MustOverride ReadOnly Property PreviewControl As PreviewControl Implements IItemPreviewHandler.PreviewControl


    Protected MustOverride ReadOnly Property PublicationLocation() As String Implements IItemPreviewHandler.PublicationLocation

    Protected MustOverride ReadOnly Property ServiceName As String Implements IItemPreviewHandler.ServiceName

    Public MustOverride ReadOnly Property PreviewTarget As String Implements IItemPreviewHandler.PreviewTarget

    Protected Overridable Sub Validate(ByVal assessmentItem As AssessmentItem, ByRef warnings As String, ByRef errors As String) Implements IItemPreviewHandler.Validate
        warnings = String.Empty
        errors = String.Empty
    End Sub

    Protected MustOverride Sub CleanUp() Implements IItemPreviewHandler.CleanUp

    Protected MustOverride ReadOnly Property UserFriendlyName As String Implements IItemPreviewHandler.UserFriendlyName

    Public Overrides Function ToString() As String
        Return UserFriendlyName
    End Function

End Class
