Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports Questify.Builder.UnitTests.Framework

Namespace QTI30

    Public Class PublicationTestHelper

        Public Shared Function GetResponseIdentifiers(itemBody As XElement) As XmlNodeList
            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(itemBody.ToString())

            Dim xmlNamespaceManager As New XmlNamespaceManager(xmlDoc.NameTable)
            xmlNamespaceManager.AddNamespace("qti", xmlDoc.DocumentElement.NamespaceURI)
            xmlNamespaceManager.AddNamespace("pci", "http://www.imsglobal.org/xsd/portableCustomInteraction_v1")

            Return ResponseIdentifierHelper.GetResponseIdentifiers(xmlDoc, xmlNamespaceManager)
        End Function

        Public Shared Function GetResponseDeclarations(responseDeclarations As IEnumerable(Of ResponseDeclarationType)) As XDocument
            Dim responseDeclarationsElement = New XDocument(<root></root>)

            For Each declarationType As ResponseDeclarationType In responseDeclarations
                Dim responseDeclarationString = ChainHandlerHelper.RemoveAllNamespaces(ChainHandlerHelper.ObjectToString(declarationType, New XmlSerializerNamespaces(), True))

                responseDeclarationsElement.Root.Add(XElement.Parse(responseDeclarationString))
            Next

            Return responseDeclarationsElement
        End Function

        Public Shared Sub RunResponseDeclarationTest(itemBody As XElement, solutionElement As XElement, scoringPrms As HashSet(Of ScoringParameter), resultExpectedElement As XElement, resultCount As Integer)
            Dim responseIdentifierAttribute As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(itemBody)
            Dim solution As Solution = solutionElement.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(scoringPrms)

            Dim result = converter.GetResponseDeclarations(solution, responseIdentifierAttribute)

            Assert.AreEqual(resultCount, result.Count)
            Dim actual = PublicationTestHelper.GetResponseDeclarations(result)
            Dim expected = New XDocument(resultExpectedElement)
            Assert.IsTrue(UnitTestHelper.AreSame(expected, actual))
        End Sub

        Public Shared Sub RunResponseProcessingTest(itemBody As XElement, findingElement As XElement, responseProcessingElement As XElement, scoringParams As HashSet(Of ScoringParameter), scoringHelper As CombinedScoringConverter, Optional findingIndex As Integer = 0, Optional shouldBeTranslated As Boolean = False, Optional solution As Solution = Nothing)
            Dim finding As KeyFinding = findingElement.Deserialize(Of KeyFinding)()
            If solution Is Nothing Then
                solution = GetSolutionFromFinding(findingElement, shouldBeTranslated)
            End If
            RunResponseProcessingTest(itemBody, solution, finding, responseProcessingElement, scoringParams, scoringHelper, findingIndex, shouldBeTranslated)
        End Sub

        Public Shared Sub RunResponseProcessingTest(itemBody As XElement, solution As Solution, finding As KeyFinding, responseProcessingElement As XElement, scoringParams As HashSet(Of ScoringParameter), scoringHelper As CombinedScoringConverter, Optional findingIndex As Integer = 0, Optional shouldBeTranslated As Boolean = False)
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(itemBody)
            Dim useResponseProcessingTemplate = QTI30CombinedScoringHelper.ShouldUseResponseProcessingTemplate(solution, scoringParams, responseIdentifierAttributeList)
            Dim processor = New ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, scoringParams, scoringHelper, shouldBeTranslated, useResponseProcessingTemplate)

            Dim result = processor.GetProcessing().ToXmlDocument

            Assert.IsTrue(UnitTestHelper.AreSame(responseProcessingElement, result))
        End Sub


        Public Shared ResponseProcessingTemplateMatchCorrect As XElement =
                <qti-response-processing template="https://purl.imsglobal.org/spec/qti/v3p0/rptemplates/match_correct.xml"/>

        Public Shared ResponseProcessingTemplateMapResponse As XElement =
                <qti-response-processing template="https://purl.imsglobal.org/spec/qti/v3p0/rptemplates/map_response.xml"/>

        Private Shared Function GetSolutionFromFinding(findingElement As XElement, Optional shouldBeTranslated As Boolean = False) As Solution
            Dim solutionElement As XElement = XElement.Parse(String.Format("<solution>
                                                  <keyFindings>
                                               {0}
                                           </keyFindings>
                                                  <aspectReferences/>
                                                  <ItemScoreTranslationTable>
                                                      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0""/>
                                                      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""{1}""/>
                                                  </ItemScoreTranslationTable>
                                              </solution>", findingElement, If(shouldBeTranslated, 2, 1)))
            Return solutionElement.Deserialize(Of Solution)
        End Function

    End Class

End Namespace