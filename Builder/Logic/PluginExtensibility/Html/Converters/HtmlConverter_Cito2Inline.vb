Option Infer On

Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HtmlHelpers

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_Cito2Inline
        Inherits HtmlConverterBase
        Implements IInlineRetriever

        Private ReadOnly _inlineElementPlaceholders As Dictionary(Of String, XmlNode)
        Private _inlineConverter As HtmlInlineConverter
        Private ReadOnly _behavior As IHtmlEditorBehaviour


        Public Sub New(behavior As IHtmlEditorBehaviour, Optional inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)) = Nothing)
            Me.InlineElements = New Dictionary(Of String, Tuple(Of InlineElement, Boolean))
            If inlineElements IsNot Nothing Then Me.InlineElements = inlineElements
            _behavior = behavior
            _inlineElementPlaceholders = behavior.InlineElementPlaceholders
            If _inlineElementPlaceholders Is Nothing Then _inlineElementPlaceholders = New Dictionary(Of String, XmlNode)
        End Sub


        Protected Overrides Function DoConvert(html As String) As String
            Dim result as String
            If (_inlineConverter Is Nothing) Then
                _inlineConverter = _behavior.CreateInlineConverter()
            End If

            result = _inlineConverter.ConvertCitoControlsToInlineHtmlElements(html, InlineElements, _inlineElementPlaceholders)
            _behavior.InlineElementPlaceholders = _inlineElementPlaceholders
            Return result
        End Function

        Public ReadOnly Property InlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)) Implements IInlineRetriever.InlineElements
    End Class
End Namespace