Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Declaration.QTI22

    Public Class ResponseDeclarationGapMatch
        Inherits ResponseDeclarationPerTypeBase

        Public Overrides Function GetSingleCorrectResponseValueForFact(fact As KeyFact) As ValueType
            If fact IsNot Nothing AndAlso fact.Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values IsNot Nothing Then
                Dim keyValue As KeyValue = DirectCast(fact.Values(0), KeyValue)
                Return GetCorrectResponse(keyValue.Values(0), keyValue.Domain)
            End If
            Return Nothing
        End Function

        Public Overrides Function GetCorrectResponses(fact As KeyFact) As List(Of ValueType)
            Dim correctResponses As New List(Of ValueType)
            For Each keyValue As KeyValue In fact.Values
                For Each value As BaseValue In keyValue.Values
                    Dim correctResponse As ValueType = GetCorrectResponse(value, keyValue.Domain)
                    If correctResponse IsNot Nothing Then correctResponses.Add(correctResponse)
                Next
            Next
            Return correctResponses
        End Function

        Protected Overridable Function GetCorrectResponse(baseValue As BaseValue, domain As String) As ValueType
            If baseValue IsNot Nothing Then
                Dim correctResponse As String = $"{baseValue.ToString} {domain}"
                Return New ValueType() With {.Value = correctResponse}
            End If
            Return Nothing
        End Function

    End Class

End Namespace