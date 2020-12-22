Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final
Imports Questify.Builder.UnitTests.Framework

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

    Public Shared Function GetResponseDeclarations(responseDeclarations As IEnumerable(Of ResponseDeclarationType)) As XDocument
        Dim responseDeclarationsElement = New XDocument(<responseDeclarations></responseDeclarations>)

        For Each declarationType As ResponseDeclarationType In responseDeclarations
            Dim responseDeclarationString = ChainHandlerHelper.RemoveAllNamespaces(ChainHandlerHelper.ObjectToString(declarationType, New XmlSerializerNamespaces(), True))

            responseDeclarationsElement.Root.Add(XElement.Parse(responseDeclarationString))
        Next

        Return responseDeclarationsElement
    End Function

    Public Shared Sub RunResponseDeclarationTest(itemBody As XElement, solutionElement As XElement, scoringPrms As HashSet(Of ScoringParameter), resultExpectedElement As XElement, resultCount As Integer)
        Dim responseIdentifierAttribute As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(itemBody)
        Dim solution As Solution = solutionElement.Deserialize(Of Solution)()
        Dim converter = New QTI22CombinedScoringConverter(scoringPrms)

        Dim result = converter.GetResponseDeclarations(solution, responseIdentifierAttribute)

        Assert.AreEqual(resultCount, result.Count)
        Dim actual = QTI22PublicationTestHelper.GetResponseDeclarations(result)
        Dim expected = New XDocument(resultExpectedElement)
        Assert.IsTrue(UnitTestHelper.AreSame(expected, actual))
    End Sub

    Public Shared Sub RunResponseProcessingTest(itemBody As XElement, findingElement As XElement, responseProcessingElement As XElement, scoringParams As HashSet(Of ScoringParameter), scoringHelper As QTI22CombinedScoringConverter, Optional findingIndex As Integer = 0, Optional shouldBeTranslated As Boolean = False)

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(itemBody)
        Dim finding As KeyFinding = findingElement.Deserialize(Of KeyFinding)()
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, Nothing, finding, findingIndex, scoringParams, scoringHelper, shouldBeTranslated)

        Dim result = processor.GetProcessing().ToXmlDocument

        Assert.IsTrue(UnitTestHelper.AreSame(responseProcessingElement, result))
    End Sub

End Class
