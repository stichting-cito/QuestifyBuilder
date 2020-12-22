Imports System.Xml

Namespace QTI.Helpers.QTI_Base

    Public Class ResponseIdentifierHelper
        Public Shared Function GetResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return GetListOfResponseIdentifiers(itemBody, "//*[@responseIdentifier][(not(name() = 'customInteraction') and not(name() = 'mediaInteraction') and not(name() = 'uploadInteraction')) or (name() = 'customInteraction' and (./html:object/qti:param[contains(@name, 'responseLength')] or ./html:object[@type = 'application/vnd.GeoGebra.file'] or ./pci:portableCustomInteraction))]/@responseIdentifier", namespaceManager)
        End Function

        Public Shared Function GetMediaResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return GetListOfResponseIdentifiers(itemBody, "//*[@responseIdentifier][((name() = 'mediaInteraction')) or (name() = 'customInteraction' and not(./html:object/qti:param[contains(@name, 'responseLength')]) and not(./html:object[@type = 'application/vnd.GeoGebra.file']) and not(./pci:portableCustomInteraction))]/@responseIdentifier", namespaceManager)
        End Function

        Public Shared Function GetUploadResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return GetListOfResponseIdentifiers(itemBody, "//*[@responseIdentifier][(name() = 'uploadInteraction')]/@responseIdentifier", namespaceManager)
        End Function

        Private Shared Function GetListOfResponseIdentifiers(itemBody As XmlNode, xpathQuery As String, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return itemBody.SelectNodes(xpathQuery, namespaceManager)
        End Function
    End Class
End NameSpace