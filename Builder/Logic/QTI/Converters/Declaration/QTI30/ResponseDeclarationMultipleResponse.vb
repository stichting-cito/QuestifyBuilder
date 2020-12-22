Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Converters.Declaration.QTI30

    Public Class ResponseDeclarationMultipleResponse
        Inherits ResponseDeclarationPerTypeBase

        Public Overrides Function GetSingleCorrectResponseValueForFact(fact As KeyFact) As ValueType
            If fact IsNot Nothing AndAlso fact.Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values IsNot Nothing Then
                Return GetCorrectResponse(fact, DirectCast(fact.Values(0), KeyValue).Values(0))
            End If
            Return GetCorrectResponse(fact, Nothing)
        End Function

        Public Overrides Function GetCorrectResponses(fact As KeyFact) As List(Of ValueType)
            Dim correctResponses As New List(Of ValueType)
            For Each value As KeyValue In fact.Values
                For Each baseValue As BaseValue In value.Values
                    Dim correctResponse As ValueType = GetCorrectResponse(fact, baseValue)
                    If correctResponse IsNot Nothing Then correctResponses.Add(correctResponse)
                Next
            Next
            Return correctResponses
        End Function

        Protected Overridable Function GetCorrectResponse(fact As KeyFact, baseValue As BaseValue) As ValueType
            Dim correctResponse As ValueType = GetCorrectResponseValueType(fact, baseValue)
            If correctResponse IsNot Nothing Then
                Dim guidPart As String = String.Empty
                If fact.Id IsNot Nothing Then
                    guidPart = QTI30CombinedScoringHelper.GetGuidPartOfIdentifier(fact.Id)
                Else
                    Dim domain As String = DirectCast(fact.Values(0), KeyValue).Domain
                    If domain IsNot Nothing Then
                        guidPart = QTI30CombinedScoringHelper.GetGuidPartOfIdentifier(domain)
                    End If
                End If
                If Not String.IsNullOrEmpty(guidPart) Then
                    correctResponse.Value = String.Concat(guidPart, correctResponse.Value)
                End If
            End If
            Return correctResponse
        End Function

        Protected Function GetCorrectResponseValueType(fact As KeyFact, baseValue As BaseValue) As ValueType
            If baseValue IsNot Nothing AndAlso TypeOf baseValue Is BooleanValue Then
                If Not BooleanValueIsCorrect(baseValue) Then Return Nothing
            End If
            If fact.Id IsNot Nothing AndAlso fact.Id.IndexOf("-") > 0 AndAlso Not QTI30CombinedScoringHelper.CheckIdentifierIsGuid(fact.Id) Then
                Dim pos As Integer = fact.Id.LastIndexOf("-")
                If QTI30CombinedScoringHelper.CheckIdentifierIsGuid(fact.Id.Substring(0, pos)) Then
                    Return New ValueType() With {.Value = fact.Id.Substring(0, fact.Id.LastIndexOf("-"))}
                End If
                pos = fact.Id.IndexOf("-")
                If QTI30CombinedScoringHelper.CheckIdentifierIsGuid(fact.Id.Substring(pos + 1, Len(fact.Id) - (pos + 1))) Then
                    Return New ValueType() With {.Value = fact.Id.Substring(0, fact.Id.IndexOf("-"))}
                End If
                Return New ValueType() With {.Value = fact.Id.Substring(0, fact.Id.LastIndexOf("-"))}
            End If
            If baseValue IsNot Nothing AndAlso TypeOf baseValue Is StringValue Then
                Return New ValueType() With {.Value = DirectCast(baseValue, StringValue).Value}
            Else
                Return Nothing
            End If
        End Function

        Private Function BooleanValueIsCorrect(baseValue As BaseValue) As Boolean
            If TypeOf baseValue Is BooleanValue Then
                Dim booleanValue = DirectCast(baseValue, BooleanValue)
                Return booleanValue.Value
            End If
            Return False
        End Function

    End Class

End Namespace