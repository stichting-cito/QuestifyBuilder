Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Converters.Declaration.QTI30

    Public Class ResponseDeclarationCas
        Inherits ResponseDeclarationEssay

        Public Overrides Function GetCorrectResponses(fact As KeyFact) As List(Of ValueType)
            Dim correctResponses As New List(Of ValueType)
            For Each value As KeyValue In fact.Values
                For Each baseValue As BaseValue In value.Values
                    If baseValue IsNot Nothing AndAlso TypeOf baseValue Is StringValue Then
                        Dim correctResponse As ValueType = GetCorrectResponse(baseValue)
                        If correctResponse IsNot Nothing Then correctResponses.Add(correctResponse)
                    End If
                Next
            Next
            Return correctResponses
        End Function

        Public Overrides Function GetSingleCorrectResponseValueForFact(fact As KeyFact) As ValueType
            If fact IsNot Nothing AndAlso fact.Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values IsNot Nothing Then
                Return GetCorrectResponse(DirectCast(fact.Values(0), KeyValue).Values(0))
            End If
            Return Nothing
        End Function


        Private Function GetCorrectResponse(baseValue As BaseValue) As ValueType
            If baseValue IsNot Nothing Then
                If TypeOf baseValue Is StringValue Then
                    Return New ValueType() With {.Value = DirectCast(baseValue, StringValue).Value}
                ElseIf TypeOf baseValue Is StringComparisonValue Then
                    Return New ValueType() With {.Value = DirectCast(baseValue, StringComparisonValue).Value}
                End If
            End If
            Return Nothing
        End Function
    End Class
End Namespace