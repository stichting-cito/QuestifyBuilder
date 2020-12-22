Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI30

Namespace QTI.Converters.Processing.QTI30

    Public Class ConceptResponseProcessingGraphicGapMatch
        Inherits ResponseProcessingPerTypeBase

        Private ReadOnly _isCategorizableGraphicGapMatch As Boolean = False

        Public Sub New(responseIndex As Integer, scoringParameter As ScoringParameter, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex, scoringParameter)
            _isCategorizableGraphicGapMatch = (TypeOf scoringParameter Is GraphGapMatchScoringParameter AndAlso DirectCast(scoringParameter, GraphGapMatchScoringParameter).IsCategorizationVariant)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = Nothing
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)

            If keyValue.Values.Count = 1 Then
                Dim value = keyValue.Values.First()
                If Not TypeOf value Is CatchAllValue AndAlso Not TypeOf value Is NoValue Then
                    processing = <qti-member></qti-member>
                    processing.Add(GetProcessingForValue(value, keyValue.Domain))
                    processing.Add(GetProcessingForVariable())
                ElseIf TypeOf value Is NoValue Then
                    processing = ScoringHelper.GetGraphicGapMatchNoValueResponseProcessing(ScoringParameter, keyValue, GetProcessingForVariable())
                End If
            End If

            Return processing
        End Function

        Private Function GetProcessingForValue(value As BaseValue, domain As String) As XElement
            Dim correctResponse As String
            Dim domainValue As String = domain
            If domainValue.IndexOf("-") > 0 Then domainValue = IdentifierHelper.GetStrippedId(domainValue.Substring(0, domainValue.IndexOf("-")))
            If _isCategorizableGraphicGapMatch Then
                correctResponse = $"{domainValue} HS{IdentifierHelper.GetStrippedId(value.ToString)}"
            Else
                correctResponse = $"{IdentifierHelper.GetStrippedId(value.ToString)} HS{domainValue}"
            End If
            Dim correctValue As XElement = <qti-base-value base-type="directedPair"><%= correctResponse %></qti-base-value>
            Return correctValue
        End Function

    End Class
End Namespace