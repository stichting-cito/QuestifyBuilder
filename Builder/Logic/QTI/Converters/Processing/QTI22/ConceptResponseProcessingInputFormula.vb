Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.Processing.QTI22

    Public Class ConceptResponseProcessingInputFormula
        Inherits ResponseProcessingInputFormula

        Public Sub New(responseIndex As Integer, formulaItemType As CombinedScoringHelper.FormulaItemType, owner As IResponseProcessingCustomOperators, multiLineFormulaGap As Boolean, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, formulaItemType, owner, multiLineFormulaGap, responseSubIndex)
        End Sub

        Public Sub New(responseIndex As Integer, formulaItemType As CombinedScoringHelper.FormulaItemType, owner As IResponseProcessingCustomOperators, dependentVariables As List(Of Tuple(Of String, Integer, Boolean)), Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, formulaItemType, owner, dependentVariables, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)
            Dim processing As XElement = Nothing

            If keyValue.Values.Count = 1 Then
                processing = ProcessAnd(fact, keyValue)
            ElseIf keyValue.Values.Count > 1 Then
                processing = ProcessOr(fact, keyValue)
            End If

            Return processing
        End Function

        Protected Overrides Function ProcessOr(fact As KeyFact, keyValue As KeyValue) As XElement
            Dim processing As XElement

            processing = <or></or>
            For Each value In keyValue.Values
                If IsEvaluate(value) Then
                    ProcessEval(fact, processing, value)
                Else
                    processing.Add(GetProcessingForValue(value, False, ResponseIndex, fact))
                End If
            Next
            Return processing
        End Function


    End Class
End Namespace