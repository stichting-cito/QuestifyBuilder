Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_DealWithMovedInlineControl : Inherits HtmlConverter_DealWithMovedInline

        Public Sub New(behavior As IHtmlEditorBehaviour)
            MyBase.New(behavior, "isinlinecontrol")
        End Sub

    End Class

End Namespace