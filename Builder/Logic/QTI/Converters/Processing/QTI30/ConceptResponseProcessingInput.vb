Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI30

Namespace QTI.Converters.Processing.QTI30

    Public Class ConceptResponseProcessingInput
        Inherits ResponseProcessingInput

        Private ReadOnly _decimalSeparator As QTI30CombinedScoringHelper.DecimalSeparator = QTI30CombinedScoringHelper.DecimalSeparator.None
        Private ReadOnly _scoringParam As ScoringParameter

        Public Sub New(responseIndex As Integer, owner As ResponseProcessingCustomOperators, scoringParam As ScoringParameter, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, owner, responseSubIndex)
            Me.Owner = owner
            _scoringParam = scoringParam
        End Sub

        Public Sub New(responseIndex As Integer, owner As ResponseProcessingCustomOperators, scoringParam As ScoringParameter, decimalSeparator As QTI30CombinedScoringHelper.DecimalSeparator, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, owner, decimalSeparator, responseSubIndex)
            Me.Owner = owner
            _scoringParam = scoringParam
            _decimalSeparator = decimalSeparator
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim keyValue As ConceptValue = DirectCast(fact.Values.First(), ConceptValue)
            Return GetProcessingForKeyValue(keyValue)
        End Function

        Protected Overrides Function GetProcessingForStringValue(value As BaseValue, responseIndex As Integer, keyValue As KeyValue) As XElement
            Dim processing As XElement = Nothing

            If TypeOf _scoringParam Is TimeScoringParameter Then
                processing = GetProcessingForTimeValue(value, responseIndex)
            ElseIf TypeOf _scoringParam Is DateScoringParameter Then
                processing = GetProcessingForDateValue(value, responseIndex)
            ElseIf keyValue.PreProcessingRules.Count > 0 Then
                processing = GetProcessingForStringValueWithPreProcessingRules(value, responseIndex, keyValue)
            ElseIf TypeOf value Is StringComparisonValue Then
                processing = GetProcessingForStringComparisonValue(value, responseIndex)
            Else
                processing = GetProcessingForStringValue(value, responseIndex)
            End If

            Return processing
        End Function

        Protected Overrides Function GetProcessingForEqualStepsValue(ByVal equalSteps As Boolean, ByVal responseIndex As Integer) As XElement
            Dim processing As XElement = Nothing
            processing = Owner.GetMathMLCustomOperator_EqualSteps(responseIndex, ResponseSubIndex)
            If equalSteps = False Then processing = XElement.Parse($"<qti-not>{processing.ToString}</qti-not>")
            Return processing
        End Function

    End Class
End Namespace