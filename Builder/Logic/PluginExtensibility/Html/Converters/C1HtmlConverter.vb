Namespace PluginExtensibility.Html.Converters

    Public Class C1HtmlConverter


        Public Function FromCitoFormatOldItem() As IHtmlConverter
            Dim ret = New HtmlConverter_CitoRefToC1Ref()
            ret.LastConverter.NextConverter = New HtmlConverter_AssignDivId()
            Return ret
        End Function

        Public Function ToCitoFormatForReferenceReadOut() As IHtmlConverter
            Dim ret As IHtmlConverter = New HtmlConverter_C1RefToCitoRef()
            ret.LastConverter.NextConverter = New HtmlConverter_RemoveContextNumber()

            Return ret
        End Function



    End Class
End Namespace