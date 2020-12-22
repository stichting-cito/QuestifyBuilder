
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI22

Namespace QTI.Converters.Processing.QTI22

    Public Class ResponseProcessingGraphicGapMatch
        Inherits ResponseProcessingPerTypeBase

        Private ReadOnly _isCategorizableGraphicGapMatch As Boolean = False

        Public Sub New(responseIndex As Integer, scoringParameter As ScoringParameter, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex, scoringParameter)
            _isCategorizableGraphicGapMatch = (TypeOf scoringParameter Is GraphGapMatchScoringParameter AndAlso DirectCast(scoringParameter, GraphGapMatchScoringParameter).IsCategorizationVariant)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = <member></member>
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)

            If keyValue.Values.Count = 1 Then
                Dim value = keyValue.Values.First()
                If Not TypeOf value Is NoValue Then
                    processing.Add(GetProcessingForValue(value, keyValue.Domain))
                    processing.Add(GetProcessingForVariable())
                ElseIf TypeOf value Is NoValue Then
                    processing = QTI22ScoringHelper.GetGraphicGapMatchNoValueResponseProcessing(ScoringParameter, keyValue, GetProcessingForVariable())
                End If
            End If

            Return processing
        End Function

        Private Function GetProcessingForValue(value As BaseValue, domain As String) As XElement

            Dim correctResponse As String
            Dim domainValue As String = domain
            If domainValue.IndexOf("-") > 0 Then domainValue = domainValue.Substring(0, domainValue.IndexOf("-"))
            If _isCategorizableGraphicGapMatch Then
                correctResponse = $"{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(domainValue)} HS{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(value.ToString)}"
            Else
                correctResponse = $"{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(value.ToString)} HS{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(domainValue)}"
            End If
            Dim correctValue As XElement = <baseValue baseType="directedPair"><%= correctResponse %></baseValue>
            Return correctValue

        End Function

    End Class

End Namespace