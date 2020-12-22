Imports Cito.Tester.ContentModel

Namespace PluginExtensibility.Html.Converters

    Public Interface IInlineRetriever
        Inherits IHtmlConverter

        ReadOnly Property InlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean))

    End Interface
End Namespace