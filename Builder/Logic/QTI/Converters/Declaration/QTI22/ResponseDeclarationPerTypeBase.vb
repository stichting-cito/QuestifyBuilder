Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Interfaces.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Declaration.QTI22

    Public MustInherit Class ResponseDeclarationPerTypeBase
        Implements IResponseDeclaration

        Public Overridable Function GetCorrectResponseValuesForFact(fact As KeyFact) As List(Of ValueType) Implements IResponseDeclaration.GetCorrectResponseValuesForFact
            Return GetCorrectResponses(fact)
        End Function

        Public Overridable Function GetCorrectResponseValuesForFact(fact As KeyFact, valuePartIndex As Integer) As List(Of ValueType) Implements IResponseDeclaration.GetCorrectResponseValuesForFact
            Return GetCorrectResponseValuesForFact(fact)
        End Function

        Public Overridable Function GetSingleCorrectResponseValueForFact(fact As KeyFact) As ValueType Implements IResponseDeclaration.GetSingleCorrectResponseValueForFact
            Return Nothing
        End Function

        Public Overridable Function GetSingleCorrectResponseValueForFact(fact As KeyFact, valuePartIndex As Integer) As ValueType Implements IResponseDeclaration.GetSingleCorrectResponseValueForFact
            Return GetSingleCorrectResponseValueForFact(fact)
        End Function

        Public Overridable Function GetInterpretationValueForFact(fact As KeyFact) As String Implements IResponseDeclaration.GetInterpretationValueForFact
            Dim result As String = String.Empty
            Dim values As List(Of ValueType) = GetCorrectResponseValuesForFact(fact)
            If values Is Nothing Then Return result
            result = String.Join("#", ResponseDeclarationHelper.GetStringInterpretationOfValueTypes(values))
            Return result
        End Function

        Public Overridable Function GetCorrectResponses(fact As KeyFact) As List(Of ValueType) Implements IResponseDeclaration.GetCorrectResponses
            Return Nothing
        End Function

        Public Overridable Function GetResponseDefaultValue(fact As KeyFact, responseIdentifierAttribute As XmlNode) As ValueType Implements IResponseDeclaration.GetResponseDefaultValue
            Return Nothing
        End Function

        Public Overridable Function GetResponseDefaultValue(fact As KeyFact, responseIdentifierAttribute As XmlNode, valuePartIndex As Integer) As ValueType Implements IResponseDeclaration.GetResponseDefaultValue
            Return GetResponseDefaultValue(fact, responseIdentifierAttribute)
        End Function

        Public Overridable Function GetSingleAreaMappingForFact(fact As KeyFact) As AreaMapEntryType Implements IResponseDeclaration.GetSingleAreaMappingForFact
            Return Nothing
        End Function
    End Class
End Namespace