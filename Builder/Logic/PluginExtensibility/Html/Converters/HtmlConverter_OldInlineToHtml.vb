Imports System.Xml
Imports Questify.Builder.Logic.HtmlHelpers

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_OldInlineToHtml
        Inherits HtmlConverterBase

        Private ReadOnly _inlineConverter As HtmlInlineConverter
        Private ReadOnly _behaviour As IHtmlEditorBehaviour
        Private _htmlContentHelper As New HtmlContentHelper
        Private _namespaceManager As XmlNamespaceManager



        Public Sub New(behaviour As IHtmlEditorBehaviour, defNamespace As XmlNamespaceManager)
            _behaviour = behaviour
            _namespaceManager = defNamespace
            _inlineConverter = _behaviour.CreateInlineConverter()
        End Sub


        Protected Overrides Function DoConvert(html As String) As String

            html = _inlineConverter.ConvertInlineElementLayoutToHtmlBasedOnOldItemLayout(_htmlContentHelper.ConvertStringIntoXmlElement(html, _namespaceManager), _behaviour.InlineElements, Constants.ResourceProtocolPrefix)

            Return html
        End Function

    End Class
End Namespace