Imports System.Xml

Namespace QTI.Helpers.QTI30

    Public Class ResponseIdentifierHelper

        Public Shared Function GetResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Dim interactions_NonCIs = "not(name() = 'qti-custom-interaction') and not(name() = 'qti-media-interaction') and not(name() = 'qti-upload-interaction')"
            Dim scorableCIs = "name() = 'qti-custom-interaction' and (./qti:object/qti:param[contains(@name, 'responseLength')] or ./object/param[contains(@name, 'responseLength')])"
            Dim geogebraCIs = "name() = 'qti-custom-interaction' and (./qti:object[@type = 'application/vnd.GeoGebra.file'] or ./object[@type = 'application/vnd.GeoGebra.file'])"
            Dim portableCIs = "name() = 'qti-custom-interaction' and ./qti-portable-custom-interaction"
            Return GetListOfResponseIdentifiers(itemBody, $"//*[@response-identifier][({interactions_NonCIs}) or ({scorableCIs}) or ({geogebraCIs}) or ({portableCIs})]/@response-identifier", namespaceManager)
        End Function

        Public Shared Function GetMediaResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Dim mediaInteractions = "name() = 'qti-media-interaction'"
            Dim customInteractions = "name() = 'qti-custom-interaction'"
            Dim notScorableCIs = "not(./qti:object/qti:param[contains(@name, 'responseLength')]) and not(./object/param[contains(@name, 'responseLength')])"
            Dim notGeogebraCIs = "not(./qti:object[@type = 'application/vnd.GeoGebra.file']) and not(./object[@type = 'application/vnd.GeoGebra.file'])"
            Dim notPortableCIs = "not(./qti-portable-custom-interaction)"
            Return GetListOfResponseIdentifiers(itemBody, $"//*[@response-identifier][({mediaInteractions}) or ({customInteractions} and {notScorableCIs} and {notGeogebraCIs} and {notPortableCIs})]/@response-identifier", namespaceManager)
        End Function

        Public Shared Function GetUploadResponseIdentifiers(itemBody As XmlNode, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return GetListOfResponseIdentifiers(itemBody, "//*[@response-identifier][(name() = 'qti-upload-interaction')]/@response-identifier", namespaceManager)
        End Function

        Private Shared Function GetListOfResponseIdentifiers(itemBody As XmlNode, xpathQuery As String, namespaceManager As XmlNamespaceManager) As XmlNodeList
            Return itemBody.SelectNodes(xpathQuery, namespaceManager)
        End Function
    End Class
End Namespace