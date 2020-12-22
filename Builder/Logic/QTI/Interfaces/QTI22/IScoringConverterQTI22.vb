Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Interfaces.QTI22

    Public Interface IScoringConverterQTI22

        Function GetResponseDeclarations(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, ByVal type As Nullable(Of ItemTypeEnum)) As List(Of ResponseDeclarationType)

        Function GetOutcomeDeclarations(solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, translationTable As ItemScoreTranslationTable) As List(Of OutcomeDeclarationType)

        Function GetResponseProcessing(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, shouldBeTranslated As Boolean) As XmlDocument

        Sub UpdateDocumentBeforeProcessing(solution As Solution, itemDocument As XmlDocument, packageCreator As QTI22PackageCreator)

        Sub UpdateDocument(solution As Solution, itemDocument As XmlDocument, itemExtensionDocument As XmlDocument, packageCreator As QTI22PackageCreator)

    End Interface
End Namespace