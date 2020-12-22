Imports System.Xml
Imports Questify.Builder.Logic.HtmlHelpers

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_Inline2Cito
        Inherits HtmlConverterBase



        Private ReadOnly _inlineConverter As HtmlInlineConverter
        Private ReadOnly _htmlContentHelper As New HtmlContentHelper
        Private ReadOnly _behavior As IHtmlEditorBehaviour
        Private ReadOnly _namespace As XmlNamespaceManager



        Public Sub New(behavior As IHtmlEditorBehaviour, defNamespace As XmlNamespaceManager)
            _behavior = behavior
            _namespace = defNamespace
            _inlineConverter = _behavior.CreateInlineConverter()
            _htmlContentHelper = New HtmlContentHelper()
        End Sub


        Protected Overrides Function DoConvert(html As String) As String
            html = _inlineConverter.ConvertInlineElementToCitoControl(_htmlContentHelper.ConvertStringIntoXmlElement(html, _namespace), _behavior.InlineElements)

            Return html
        End Function

    End Class
End Namespace