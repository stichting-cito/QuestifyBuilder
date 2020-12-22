Imports System.Linq
Imports System.Text
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Declaration.QTI22

    Public Class ResponseDeclarationSelectPoint
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

        Public Overrides Function GetSingleAreaMappingForFact(fact As KeyFact) As AreaMapEntryType
            If fact IsNot Nothing AndAlso fact.Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values.Any() Then
                Dim keyValue = DirectCast(fact.Values(0), KeyValue)
                Dim splittedResponse As String() = keyValue.ToString().Split(",".ToCharArray())
                Dim areaMapEntryType = New AreaMapEntryType
                areaMapEntryType.coords = keyValue.Values(0).ToString()
                areaMapEntryType.mappedValue = 1

                If splittedResponse.Count = 3 Then
                    areaMapEntryType.shape = AreaMapEntryTypeShape.circle
                ElseIf splittedResponse.Count = 4 Then
                    areaMapEntryType.shape = AreaMapEntryTypeShape.rect
                Else
                    areaMapEntryType.shape = AreaMapEntryTypeShape.poly
                End If

                Return areaMapEntryType
            End If
            Return Nothing
        End Function

        Private Function GetCorrectResponse(baseValue As BaseValue, domain As String) As ValueType
            If baseValue IsNot Nothing Then
                Dim splittedResponse As String() = baseValue.ToString().Split(",".ToCharArray())
                Return New ValueType() With {.Value = CreateCorrectResponse(splittedResponse)}
            End If
            Return Nothing
        End Function

        Private Function CreateCorrectResponse(ByVal response As String()) As String
            Dim builder As New StringBuilder()

            builder.Append(response(0))
            builder.Append(" ")
            builder.Append(response(1))

            Return builder.ToString()
        End Function

    End Class

End Namespace