Option Infer On
Imports System.Xml

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_FullToPartial
        Inherits HtmlConverterBase

        Private ReadOnly _namespacemanager As XmlNamespaceManager

        Public Sub New()
            _namespacemanager = GetNamespaceMng()
        End Sub


        Protected Overrides Function DoConvert(html As String) As String
            Dim doc As New XmlDocument()
            doc.PreserveWhitespace = True
            doc.LoadXml(html)
            Dim node = doc.SelectSingleNode("//def:body", _namespacemanager)
            If node IsNot Nothing Then
                Return node.InnerXml
            End If
            Throw New ArgumentException("wrong html format")
        End Function

    End Class
End Namespace