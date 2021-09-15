Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI30

Namespace QTI.Converters.Processing.QTI30

    Public Class ConceptResponseProcessingGapMatch
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, scoringParameter As ScoringParameter, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex, scoringParameter)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = Nothing
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)

            If keyValue.Values.Count = 1 Then
                Dim value = keyValue.Values.First()
                If Not TypeOf value Is CatchAllValue AndAlso Not TypeOf value Is NoValue Then
                    processing = <qti-member></qti-member>
                    processing.Add(ScoringHelper.GetDirectPairProcessingForValue(value.ToString, keyValue.Domain))
                    processing.Add(GetProcessingForVariable())
                ElseIf TypeOf value Is NoValue Then
                    processing = ScoringHelper.GetGapMatchNoValueResponseProcessing(ScoringParameter, keyValue, GetProcessingForVariable())
                End If
            End If

            Return processing
        End Function



    End Class

End Namespace