Imports System.Globalization
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Declaration.QTI22

    Public Class ResponseDeclarationOrder
        Inherits ResponseDeclarationPerTypeBase

        Public Overrides Function GetSingleCorrectResponseValueForFact(fact As KeyFact) As ValueType
            If fact IsNot Nothing AndAlso fact.Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values IsNot Nothing Then
                Return GetCorrectResponse(DirectCast(fact.Values(0), KeyValue).Values(0))
            End If
            Return Nothing
        End Function

        Public Overrides Function GetCorrectResponses(fact As KeyFact) As List(Of ValueType)
            Dim correctResponses As New List(Of ValueType)
            For Each keyValue As KeyValue In fact.Values
                For Each value As BaseValue In keyValue.Values
                    Dim correctResponse As ValueType = GetCorrectResponse(value)
                    If correctResponse IsNot Nothing Then correctResponses.Add(correctResponse)
                Next
            Next
            Return correctResponses
        End Function

        Private Function GetCorrectResponse(baseValue As BaseValue) As ValueType
            If baseValue IsNot Nothing Then
                Dim correctResponse As String = baseValue.ToString()
                If TypeOf baseValue Is IntegerValue Then
                    Dim integerValue = CType(baseValue, IntegerValue)
                    correctResponse = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(CInt(integerValue.Value.ToString(CultureInfo.InvariantCulture.NumberFormat)))
                End If
                Return New ValueType() With {.Value = correctResponse}
            End If
            Return Nothing
        End Function

    End Class

End Namespace