'ELMO
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

Public Class QTI22ResponseDeclarationTests_Base

    Public Sub RunResponseDeclarationTest(itemBody As XElement, solutionElement As XElement, scoringPrms As HashSet(Of ScoringParameter), resultExpectedElement As XElement, resultCount As Integer)

        'Arrange
        Dim responseIdentifierAttribute As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(itemBody)
        Dim solution As Solution = solutionElement.Deserialize(Of Solution)()
        Dim converter = New QTI22CombinedScoringConverter(scoringPrms)

        'Act
        Dim result = converter.GetResponseDeclarations(solution, responseIdentifierAttribute)

        'Assert
        Assert.AreEqual(resultCount, result.Count)
        Dim actual = QTI22PublicationTestHelper.GetResponseDeclarations(result)
        Dim expected = New XDocument(resultExpectedElement)
        Assert.IsTrue(UnitTestHelper.AreSame(expected, actual))
    End Sub

End Class
