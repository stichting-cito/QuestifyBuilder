Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Interfaces.QTI30

    Public Interface IScoringConverter

        Function GetResponseDeclarations(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, ByVal type As Nullable(Of ItemTypeEnum)) As List(Of ResponseDeclarationType)

        Function GetOutcomeDeclarations(solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, translationTable As ItemScoreTranslationTable,
                                        packageCreator As IPackageCreator) As List(Of OutcomeDeclarationType)

        Function GetResponseProcessing(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, shouldBeTranslated As Boolean,
                                       packageCreator As IPackageCreator) As XmlDocument

        Sub UpdateDocumentBeforeProcessing(solution As Solution, itemDocument As XmlDocument, packageCreator As PackageCreator)

        Sub UpdateDocument(solution As Solution, itemDocument As XmlDocument, itemExtensionDocument As XmlDocument, packageCreator As PackageCreator)

    End Interface
End Namespace