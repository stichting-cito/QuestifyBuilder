
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

Public Class QTI22ResponseProcessingTests_Base

    Public Sub RunResponseProcessingTest(itemBody As XElement, findingElement As XElement, responseProcessingElement As XElement, scoringParams As HashSet(Of ScoringParameter), scoringHelper As QTI22CombinedScoringConverter, Optional findingIndex As Integer = 0, Optional shouldBeTranslated As Boolean = False)

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(itemBody)
        Dim finding As KeyFinding = findingElement.Deserialize(Of KeyFinding)()
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, Nothing, finding, findingIndex, scoringParams, scoringHelper, shouldBeTranslated)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(responseProcessingElement, result))
    End Sub

End Class
