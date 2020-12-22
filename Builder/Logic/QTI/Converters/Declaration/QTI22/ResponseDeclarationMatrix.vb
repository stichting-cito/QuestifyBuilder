Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Declaration.QTI22

    Public Class ResponseDeclarationMatrix
        Inherits ResponseDeclarationPerTypeBase

        Public Overrides Function GetSingleCorrectResponseValueForFact(fact As KeyFact) As ValueType
            If fact IsNot Nothing AndAlso fact.Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values IsNot Nothing Then
                Dim valueY As Integer = GetMatrixRowIndexFromFact(fact)
                Return GetCorrectResponse(DirectCast(fact.Values(0), KeyValue).Values(0), valueY)
            End If
            Return Nothing
        End Function

        Public Overrides Function GetCorrectResponses(fact As KeyFact) As List(Of ValueType)
            Dim correctResponses As New List(Of ValueType)
            For Each keyValue As KeyValue In fact.Values
                For Each value As BaseValue In keyValue.Values
                    Dim correctResponse As ValueType = GetInterpretationCorrectResponse(value)
                    If correctResponse IsNot Nothing Then correctResponses.Add(correctResponse)
                Next
            Next
            Return correctResponses
        End Function

        Public Overrides Function GetInterpretationValueForFact(fact As KeyFact) As String
            Dim result As String = String.Empty
            Dim values As List(Of ValueType) = GetCorrectResponses(fact)
            If values Is Nothing Then Return result
            result = String.Join("#", ResponseDeclarationHelper.GetStringInterpretationOfValueTypes(values))
            Return result
        End Function


        Private Function GetCorrectResponse(baseValue As BaseValue, valueY As Integer) As ValueType
            If baseValue IsNot Nothing Then
                Dim correctResponse As String = $"y_{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(valueY)} x_{(AscW(baseValue.ToString) - 64)}"
                Return New ValueType() With {.Value = correctResponse}
            End If
            Return Nothing
        End Function

        Private Function GetInterpretationCorrectResponse(baseValue As BaseValue) As ValueType
            If baseValue IsNot Nothing Then
                Return New ValueType() With {.Value = DirectCast(baseValue, StringValue).ToString}
            End If
            Return Nothing
        End Function

        Private Function GetMatrixRowIndexFromFact(fact As KeyFact) As Integer
            Return CInt(fact.Id.Substring(0, fact.Id.IndexOf("-")))
        End Function


    End Class

End Namespace