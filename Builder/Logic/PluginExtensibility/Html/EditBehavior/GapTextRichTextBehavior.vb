Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.EditBehavior

    Public Class GapTextRichTextBehavior
        Inherits XhtmlParameterBehaviorDefault

        Public Sub New(resourceEntity As ResourceEntity,
                            resourceManager As ResourceManagerBase,
                            contextIdentifier As Integer?,
                            iltAdapter As ItemLayoutAdapter,
                            stylesheets As Dictionary(Of String, String),
                            headerStyleElementContent As String,
                            param As Cito.Tester.ContentModel.XHtmlParameter)
            MyBase.New(resourceEntity, resourceManager, contextIdentifier, iltAdapter, stylesheets, headerStyleElementContent, param)
        End Sub

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

    End Class

End Namespace