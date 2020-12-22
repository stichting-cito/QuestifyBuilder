Option Infer On


Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_HandleNULLString
        Inherits HtmlConverterBase

        Protected Overrides Function DoConvert(html As String) As String
            Return If(html Is Nothing, String.Empty, html)
        End Function

    End Class
End Namespace