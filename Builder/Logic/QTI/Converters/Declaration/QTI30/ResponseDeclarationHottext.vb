Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Converters.Declaration.QTI30

    Public Class ResponseDeclarationHottext
        Inherits ResponseDeclarationMultipleResponse

        Public Overrides Function GetCorrectResponses(fact As KeyFact) As List(Of ValueType)
            Dim correctResponses As New List(Of ValueType)
            For Each value As KeyValue In fact.Values
                For Each baseValue As BaseValue In value.Values
                    If baseValue IsNot Nothing AndAlso TypeOf baseValue Is BooleanValue Then
                        Dim correctResponse As ValueType = GetCorrectResponse(fact, baseValue)
                        If correctResponse IsNot Nothing Then correctResponses.Add(correctResponse)
                    End If
                Next
            Next
            Return correctResponses
        End Function

        Protected Overrides Function GetCorrectResponse(fact As KeyFact, baseValue As BaseValue) As ValueType
            Dim correctResponse As ValueType = GetCorrectResponseValueType(fact, baseValue)
            Return correctResponse
        End Function

    End Class

End Namespace
