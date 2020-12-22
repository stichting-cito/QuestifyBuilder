Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_RemoveFontInSpan
        Inherits HtmlConverterBase


        Private _htmlContentHelper As New HtmlContentHelper


        Protected Overrides Function DoConvert(html As String) As String
            Return _htmlContentHelper.RemoveFontTags(html)
        End Function

    End Class
End Namespace