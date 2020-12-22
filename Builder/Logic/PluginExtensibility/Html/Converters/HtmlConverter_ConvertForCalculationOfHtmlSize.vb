Imports System.Xml

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_ConvertForCalculationOfHtmlSize
        Inherits HtmlConverterBase

        Private ReadOnly _namespacemanager As XmlNamespaceManager
        Private _behavior As IHtmlEditorBehaviour

        Public Sub New(Optional behavior As IHtmlEditorBehaviour = Nothing)
            _namespacemanager = GetNamespaceMng()
            _behavior = behavior
        End Sub


        Protected Overrides Function DoConvert(html As String) As String
            Dim doc As New XmlDocument()
            doc.PreserveWhitespace = True
            doc.LoadXml(html)
            Dim node = doc.SelectSingleNode("//def:body", _namespacemanager)
            If node IsNot Nothing Then
                If _behavior IsNot Nothing AndAlso _behavior.StoreSizeOfHtml Then
                    Return String.Format("<div id=""calculateSizeDiv"" style=""display: inline-block; border: solid 1px black; float: left;"">{0}</div><div id=""calculateSizeDiv2"" style=""display: inline-block; border: solid 1px red; overflow: hidden;""></div>", node.InnerXml)
                Else
                    Return node.InnerXml
                End If
            End If
            Throw New ArgumentException("wrong html format")
        End Function

    End Class

End Namespace