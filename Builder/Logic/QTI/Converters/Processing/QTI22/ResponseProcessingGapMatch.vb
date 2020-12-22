
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI22

Namespace QTI.Converters.Processing.QTI22

    Public Class ResponseProcessingGapMatch
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, scoringParameter As ScoringParameter, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex, scoringParameter)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = <member></member>
            Dim keyValue = DirectCast(fact.Values.First(), KeyValue)
            If keyValue.Values.Count = 1 Then
                Dim value = keyValue.Values.First()
                If Not TypeOf value Is NoValue Then
                    processing.Add(QTI22ScoringHelper.GetDirectPairProcessingForValue(value.ToString, keyValue.Domain))
                    processing.Add(GetProcessingForVariable())
                ElseIf TypeOf value Is NoValue Then
                    processing = QTI22ScoringHelper.GetGapMatchNoValueResponseProcessing(ScoringParameter, keyValue, GetProcessingForVariable())
                End If
            End If
            Return processing
        End Function

    End Class

End Namespace
