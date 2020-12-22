Imports System.Xml

Namespace QTI.Helpers.QTI30

    Public Class ResponseIdentifierHelper
        Public Shared Function GetResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return GetListOfResponseIdentifiers(itemBody, "//*[@response-identifier][(not(name() = 'qti-custom-interaction') and not(name() = 'qti-media-interaction') and not(name() = 'qti-upload-interaction')) or (name() = 'qti-custom-interaction' and (./html:object/qti:param[contains(@name, 'responseLength')] or ./html:object[@type = 'application/vnd.GeoGebra.file'] or ./qti-portable-custom-interaction))]/@response-identifier", namespaceManager)
        End Function

        Public Shared Function GetMediaResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return GetListOfResponseIdentifiers(itemBody, "//*[@response-identifier][((name() = 'qti-media-interaction')) or (name() = 'qti-custom-interaction' and not(./html:object/qti:param[contains(@name, 'responseLength')]) and not(./html:object[@type = 'application/vnd.GeoGebra.file']) and not(./qti-portable-custom-interaction))]/@response-identifier", namespaceManager)
        End Function

        Public Shared Function GetUploadResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return GetListOfResponseIdentifiers(itemBody, "//*[@response-identifier][(name() = 'qti-upload-interaction')]/@response-identifier", namespaceManager)
        End Function

        Private Shared Function GetListOfResponseIdentifiers(itemBody As XmlNode, xpathQuery As String, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return itemBody.SelectNodes(xpathQuery, namespaceManager)
        End Function
    End Class
End NameSpace