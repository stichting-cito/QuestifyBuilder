Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Interfaces.QTI22

    Public Interface IResponseDeclaration

        Function GetCorrectResponseValuesForFact(ByVal fact As KeyFact) As List(Of ValueType)

        Function GetCorrectResponseValuesForFact(ByVal fact As KeyFact, valuePartIndex As Integer) As List(Of ValueType)

        Function GetSingleCorrectResponseValueForFact(ByVal fact As KeyFact) As ValueType

        Function GetSingleCorrectResponseValueForFact(ByVal fact As KeyFact, valuePartIndex As Integer) As ValueType

        Function GetResponseDefaultValue(ByVal fact As KeyFact, ByVal responseIdentifierAttribute As XmlNode) As ValueType

        Function GetResponseDefaultValue(ByVal fact As KeyFact, ByVal responseIdentifierAttribute As XmlNode, valuePartIndex As Integer) As ValueType

        Function GetInterpretationValueForFact(ByVal fact As KeyFact) As String

        Function GetCorrectResponses(ByVal fact As KeyFact) As List(Of ValueType)

        Function GetSingleAreaMappingForFact(ByVal fact As KeyFact) As AreaMapEntryType

    End Interface
End NameSpace