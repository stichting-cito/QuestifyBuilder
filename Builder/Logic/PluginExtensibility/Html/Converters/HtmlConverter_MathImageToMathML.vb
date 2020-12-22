Imports Questify.Builder.Logic.HelperClasses

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_MathImageToMathML
        Inherits HtmlConverterBase

        Protected Overrides Function DoConvert(html As String) As String
            Return MathMLHelper.ConvertMathMLImagesToMathML(html)
        End Function

    End Class

End Namespace
