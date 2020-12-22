
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Converters.Processing.QTI22

    Public Class ResponseProcessingMultipleResponse
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = Nothing
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)

            If keyValue.Values.Count >= 1 Then
                Dim processingForValue As String = String.Empty
                Dim value = keyValue.Values.First()
                If TypeOf value Is BooleanValue Then
                    If DirectCast(value, BooleanValue).Value OrElse addNotMemberOfElement Then
                        processingForValue = GetCorrectResponse(fact)
                        If keyValue.Domain IsNot Nothing AndAlso Not CombinedScoringHelper.CheckIdentifierIsGuid(processingForValue) Then
                            Dim guidPart As String = CombinedScoringHelper.GetGuidPartOfIdentifier(keyValue.Domain)
                            If Not String.IsNullOrEmpty(guidPart) Then processingForValue = String.Concat(guidPart, processingForValue)
                        End If
                    End If
                ElseIf TypeOf value Is StringValue Then
                    processingForValue = DirectCast(value, StringValue).Value
                    If keyValue.Domain IsNot Nothing AndAlso Not CombinedScoringHelper.CheckIdentifierIsGuid(processingForValue) Then
                        Dim guidPart As String = CombinedScoringHelper.GetGuidPartOfIdentifier(keyValue.Domain)
                        If Not String.IsNullOrEmpty(guidPart) Then processingForValue = String.Concat(guidPart, processingForValue)
                    End If
                End If
                If Not String.IsNullOrEmpty(processingForValue) Then
                    processing = <member></member>
                    processing.Add(GetProcessingForValue(processingForValue))
                    processing.Add(GetProcessingForVariable())
                    If addNotMemberOfElement AndAlso TypeOf value Is BooleanValue AndAlso DirectCast(value, BooleanValue).Value = False Then
                        processing = XElement.Parse($"<not>{processing.ToString}</not>")
                    End If
                End If
            End If

            Return processing
        End Function

        Private Function GetProcessingForValue(value As String) As XElement
            Return <baseValue baseType="identifier"><%= value %></baseValue>
        End Function

        Private Function GetCorrectResponse(fact As KeyFact) As String
            Dim pos As Integer = fact.Id.LastIndexOf("-")
            If CombinedScoringHelper.CheckIdentifierIsGuid(fact.Id.Substring(0, pos)) Then
                Return fact.Id.Substring(0, fact.Id.LastIndexOf("-"))
            End If
            pos = fact.Id.IndexOf("-")
            If CombinedScoringHelper.CheckIdentifierIsGuid(fact.Id.Substring(pos + 1, Len(fact.Id) - (pos + 1))) Then
                Return fact.Id.Substring(0, fact.Id.IndexOf("-"))
            End If
            Return fact.Id.Substring(0, fact.Id.LastIndexOf("-"))
        End Function

    End Class

End Namespace
