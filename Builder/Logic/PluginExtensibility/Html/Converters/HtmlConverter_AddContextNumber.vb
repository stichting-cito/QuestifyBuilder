
Option Infer On

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_AddContextNumber
        Inherits HtmlConverterBase

        Private ReadOnly _htmlContentHelper As New HtmlContentHelper
        Private ReadOnly _contextId As Integer?

        Public Sub New(contextId As Integer?)
            _contextId = contextId
        End Sub

        Protected Overrides Function DoConvert(html As String) As String
            Return _htmlContentHelper.GiveResourceElementsContextNumber(html, _contextId)
        End Function
    End Class
End Namespace