Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HtmlHelpers
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior

    Public Class PopupTriggerBehavior
        Inherits XhtmlParameterBehaviorDefault

        Public Sub New(resourceEntity As ResourceEntity,
                            resourceManager As ResourceManagerBase,
                            contextIdentifier As Integer?,
                            iltAdapter As ItemLayoutAdapter,
                            stylesheets As Dictionary(Of String, String),
                            headerStyleElementContent As String,
                            param As XHtmlParameter)
            MyBase.New(resourceEntity, New ResourceManagerWithEmbeddedResources(resourceManager), contextIdentifier, iltAdapter, stylesheets, headerStyleElementContent, param)
        End Sub

        Public Overrides ReadOnly Property IsToolstripVisible As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property StoreSizeOfHtml As Boolean
            Get
                Return True
            End Get
        End Property

        Protected Overrides Function ConvertForCalculationOfHtmlSize() As String
            Dim ret As IHtmlConverter = New HtmlConverter_PartialToFull(GetStyle(), HeaderStyleElementCont, ContextIdentifier, DefaultNamespaceManager())
            ret.LastConverter.NextConverter = New HtmlConverter_ConvertForCalculationOfHtmlSize(Me)
            Dim tmp = ret.ConvertHtml(Param.Value)
            Return tmp
        End Function

        Protected Overrides Function GetInlineTemplateNames() As IHtmlInlineTemplateNames
            Return New PopupHtmlInlineTemplateNames()
        End Function

    End Class
End Namespace