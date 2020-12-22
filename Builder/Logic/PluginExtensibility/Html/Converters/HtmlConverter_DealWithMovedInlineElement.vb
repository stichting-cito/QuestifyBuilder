Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_DealWithMovedInlineElement : Inherits HtmlConverter_DealWithMovedInline

        Public Sub New(behavior As IHtmlEditorBehaviour)
            MyBase.New(behavior, "isinlineelement")
        End Sub

    End Class

End Namespace