Imports System.Xml
Imports System.Xml.Serialization
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Public Class QTI22PublicationTestHelper
    Public Shared Function GetResponseIdentifiers(itemBody As XElement) As XmlNodeList
        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(itemBody.ToString())

        Dim xmlNamespaceManager As New XmlNamespaceManager(xmlDoc.NameTable)
        xmlNamespaceManager.AddNamespace("qti", xmlDoc.DocumentElement.NamespaceURI)
        xmlNamespaceManager.AddNamespace("html", "http://www.w3.org/1999/xhtml")
        xmlNamespaceManager.AddNamespace("pci", "http://www.imsglobal.org/xsd/portableCustomInteraction_v1")

        Return ResponseIdentifierHelper.GetResponseIdentifiers(xmlDoc, xmlNamespaceManager)
    End Function

    Public Shared Function GetFirstResponseIdentifier(itemBody As XElement) As XmlNode
        Dim responseAttributes = GetResponseIdentifiers(itemBody)

        Return responseAttributes(0)
    End Function

    Public Shared Function GetResponseDeclarations(responseDeclarations As IEnumerable(Of ResponseDeclarationType)) As XDocument
        Dim responseDeclarationsElement = New XDocument(<responseDeclarations></responseDeclarations>)

        For Each declarationType As ResponseDeclarationType In responseDeclarations
            Dim responseDeclarationString = ChainHandlerHelper.RemoveAllNamespaces(ChainHandlerHelper.ObjectToString(declarationType, New XmlSerializerNamespaces(), True))

            responseDeclarationsElement.Root.Add(XElement.Parse(responseDeclarationString))
        Next

        Return responseDeclarationsElement
    End Function

End Class
