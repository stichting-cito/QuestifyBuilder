Option Infer On

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_RemoveContextNumber
        Inherits HtmlConverterBase

        Private ReadOnly _htmlContentHelper As HtmlContentHelper


        Public Sub New()
            _htmlContentHelper = New HtmlContentHelper
        End Sub


        Protected Overrides Function DoConvert(html As String) As String
            Dim tmp As String = _htmlContentHelper.RemoveResourceElementContextNumber(html)
            Return tmp
        End Function
    End Class
End Namespace