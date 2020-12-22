Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HtmlHelpers
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior

    Public Class InlineChoiceOptionBehavior
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

        Public Sub New(resourceEntity As ResourceEntity,
                       resourceManager As ResourceManagerBase,
                       contextIdentifier As Integer?,
                       param As XHtmlParameter)
            MyBase.New(resourceEntity, New ResourceManagerWithEmbeddedResources(resourceManager), contextIdentifier, param)
        End Sub

        Public Overrides ReadOnly Property IsToolstripVisible As Boolean
            Get
                Return True
            End Get
        End Property

        Protected Overrides Function ConstructChain_FromEditor2Param() As IHtmlConverter
            Dim result = MyBase.ConstructChain_FromEditor2Param()
            result.LastConverter.NextConverter = New HtmlConverter_TextToSpeechToHtml()
            Return result
        End Function

        Protected Overrides Function ConstructChain_FromParam2Editor() As IHtmlConverter
            Dim result = MyBase.ConstructChain_FromParam2Editor()
            result.LastConverter.NextConverter = New HtmlConverter_HtmlToTextToSpeech()
            Return result
        End Function

        Protected Overrides Function GetInlineRetriever() As IInlineRetriever
            Debug.Assert(InlineRetriever IsNot Nothing)
            Return InlineRetriever
        End Function

        Protected Overrides Function GetInlineTemplateNames() As IHtmlInlineTemplateNames
            Return New InlineChoiceOptionHtmlInlineTemplateNames()
        End Function
    End Class

End Namespace