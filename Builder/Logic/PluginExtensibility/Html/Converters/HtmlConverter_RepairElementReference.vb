Imports System.Xml

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_RepairElementReference
        Inherits HtmlConverterBase


        Private _namespaceManager As XmlNamespaceManager
        Private _htmlContentHelper As New HtmlContentHelper



        Public Sub New(defNamespace As XmlNamespaceManager)
            _namespaceManager = defNamespace
        End Sub


        Protected Overrides Function DoConvert(html As String) As String
            Dim xmlDoc As New XmlDocument()
            Dim tempRoot As XmlElement = xmlDoc.CreateElement("html", _namespaceManager.LookupNamespace("def"))
            tempRoot.InnerXml = html
            xmlDoc.LoadXml(tempRoot.OuterXml)

            _htmlContentHelper.PlaceUnderlineTagOutsideSpanTag(xmlDoc, _namespaceManager)

            Return xmlDoc.DocumentElement.InnerXml
        End Function

    End Class
End Namespace