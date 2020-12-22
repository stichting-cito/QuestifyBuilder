
Namespace PluginExtensibility.Html.Converters
    Public Interface IHtmlConverter : Inherits IDisposable

        Function ConvertHtml(html As String) As String
        Property NextConverter As IHtmlConverter
        ReadOnly Property LastConverter As IHtmlConverter

    End Interface
End Namespace