Option Infer On

Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HtmlHelpers

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_OldHtmlToInline
        Inherits HtmlConverterBase
        Implements IInlineRetriever


        Private ReadOnly _behavior As IHtmlEditorBehaviour
        Private ReadOnly _inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean))
        Private ReadOnly _inlineConverter As HtmlInlineConverter



        Public Sub New(behavior As IHtmlEditorBehaviour, Optional inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)) = Nothing)
            _inlineElements = New Dictionary(Of String, Tuple(Of InlineElement, Boolean))
            If inlineElements IsNot Nothing Then _inlineElements = inlineElements
            _behavior = behavior
            _inlineConverter = _behavior.CreateInlineConverter()
        End Sub


        Protected Overrides Function DoConvert(html As String) As String
            html = _inlineConverter.ConvertHtmlBasedOnOldItemLayoutToInlineElementLayout(html, "<body xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">{0}</body>", _inlineElements, Constants.ResourceProtocolPrefix)
            Return html
        End Function

        Public ReadOnly Property InlineElements As System.Collections.Generic.Dictionary(Of String, Tuple(Of InlineElement, Boolean)) Implements IInlineRetriever.InlineElements
            Get
                Return _inlineElements
            End Get
        End Property
    End Class
End Namespace