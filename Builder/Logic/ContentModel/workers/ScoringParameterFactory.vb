Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Activities
Imports System.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace ContentModel

    Class ScoringParameterFactory


        Shared ReadOnly Creator As Dictionary(Of System.Type, Func(Of ScoringParameter, FindingManipulatorBase, IScoreManipulator))

        Shared Sub New()

            Creator = New Dictionary(Of System.Type, Func(Of ScoringParameter, FindingManipulatorBase, IScoreManipulator))
            Creator.Add(GetType(ChoiceScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, ChoiceScoringParameter), fm))
            Creator.Add(GetType(MultiChoiceScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, MultiChoiceScoringParameter), fm))
            Creator.Add(GetType(InlineChoiceScoringParameter), Function(param, fm) GetInlineChoiceManipulator(DirectCast(param, InlineChoiceScoringParameter), GetScoreManipulator(DirectCast(param, InlineChoiceScoringParameter), fm)))
            Creator.Add(GetType(GapMatchScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, GapMatchScoringParameter), fm))
            Creator.Add(GetType(GapMatchRichTextScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, GapMatchRichTextScoringParameter), fm))
            Creator.Add(GetType(GraphGapMatchScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, GraphGapMatchScoringParameter), fm))

            Creator.Add(GetType(StringScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, StringScoringParameter), fm))
            Creator.Add(GetType(DateScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, DateScoringParameter), fm))
            Creator.Add(GetType(TimeScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, TimeScoringParameter), fm))
            Creator.Add(GetType(MathScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, MathScoringParameter), fm))
            Creator.Add(GetType(IntegerScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, IntegerScoringParameter), fm))
            Creator.Add(GetType(DecimalScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, DecimalScoringParameter), fm))
            Creator.Add(GetType(CurrencyScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, CurrencyScoringParameter), fm))
            Creator.Add(GetType(OrderScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, OrderScoringParameter), fm))
            Creator.Add(GetType(HotspotScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, HotspotScoringParameter), fm))

            Creator.Add(GetType(MatrixScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, MatrixScoringParameter), fm))
            Creator.Add(GetType(HotTextScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, HotTextScoringParameter), fm))
            Creator.Add(GetType(HotTextCorrectionScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, HotTextCorrectionScoringParameter), fm))
            Creator.Add(GetType(CasEqualStepsScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, CasEqualStepsScoringParameter), fm))
            Creator.Add(GetType(MathCasEqualScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, MathCasEqualScoringParameter), fm))
            Creator.Add(GetType(MathCasDependencyScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, MathCasDependencyScoringParameter), fm))
            Creator.Add(GetType(MathCasEvaluateScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, MathCasEvaluateScoringParameter), fm))
            Creator.Add(GetType(GeogebraScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, GeogebraScoringParameter), fm))
            Creator.Add(GetType(SelectPointScoringParameter), Function(param, fm) GetScoreManipulator(DirectCast(param, SelectPointScoringParameter), fm))

        End Sub



        Private Shared Function GetScoreManipulator(choice As ChoiceScoringParameter, findingManipulator As FindingManipulatorBase) As IChoiceScoringManipulator
            Dim ret As IChoiceScoringManipulator
            If Not choice.IsSingleChoice Then
                ret = New MultiResponseScoringManipulator(findingManipulator, choice)
            Else
                ret = New ChoiceScoringManipulator(findingManipulator, choice)
            End If
            Return ret
        End Function

        Private Shared Function GetInlineChoiceManipulator(choice As InlineChoiceScoringParameter, decoree As IChoiceScoringManipulator) As IChoiceScoringManipulator
            Return New InlineChoiceScoringManipulator(choice, decoree)
        End Function

        Private Shared Function GetScoreManipulator(matrix As MatrixScoringParameter, findingManipulator As FindingManipulatorBase) As IChoiceArrayScoringManipulator
            Dim ret As New MatrixScoringManipulator(findingManipulator, matrix)
            Return ret
        End Function

        Private Shared Function GetScoreManipulator(stringGap As StringScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of String)
            Dim ret As New StringScoringManipulator(findingManipulator, stringGap)
            Return ret
        End Function

        Private Shared Function GetScoreManipulator(dateGap As DateScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of String)
            Dim ret As New DateScoringManipulator(findingManipulator, dateGap)
            Return ret
        End Function

        Private Shared Function GetScoreManipulator(timeGap As TimeScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of String)
            Dim ret As New StringScoringManipulator(findingManipulator, timeGap)
            Return ret
        End Function

        Private Shared Function GetScoreManipulator(mathGap As MathScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of String)
            Dim ret As New MathScoringManipulator(mathGap, New StringScoringManipulator(findingManipulator, mathGap))
            Return ret
        End Function

        Private Shared Function GetScoreManipulator(integerGap As IntegerScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of MultiType)
            Dim ret As New MultiTypeScoringManipulator(findingManipulator, integerGap)
            Return ret
        End Function

        Private Shared Function GetScoreManipulator(decimalGap As DecimalScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of MultiType)
            Dim ret As New MultiTypeScoringManipulator(findingManipulator, decimalGap)
            Return ret
        End Function

        Public Shared Function GetScoreManipulator(currencyGap As CurrencyScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of Decimal?)
            Dim ret As New DecimalScoringManipulator(findingManipulator, currencyGap)
            Return ret
        End Function

        Public Shared Function GetScoreManipulator(gapMatch As GapMatchScoringParameter, findingManipulator As FindingManipulatorBase) As IValidatingChoiceArrayScoringManipulator(Of String)
            Dim ret As New GapMatchScoringManipulator(findingManipulator, gapMatch)
            If (Not gapMatch.IsTransformed) Then
                Return New GapMatchScoringNonTransformedManipulator(gapMatch, ret)
            End If
            Return ret
        End Function

        Public Shared Function GetScoreManipulator(gapMatch As GapMatchRichTextScoringParameter, findingManipulator As FindingManipulatorBase) As IValidatingChoiceArrayScoringManipulator(Of String)
            Dim ret As New GapMatchScoringManipulator(findingManipulator, gapMatch)
            If (Not gapMatch.IsTransformed) Then
                Return New GapMatchRichTextScoringNonTransformedManipulator(gapMatch, ret)
            End If
            Return ret
        End Function

        Public Shared Function GetScoreManipulator(graphGapMatch As GraphGapMatchScoringParameter, findingManipulator As FindingManipulatorBase) As IValidatingChoiceArrayScoringManipulator(Of String)
            Dim manipulator = New GraphGapMatchScoringManipulator(findingManipulator, graphGapMatch)
            If (graphGapMatch.IsTransformed) Then
                Return New GraphGapMatchScoringTransformedManipulator(graphGapMatch, manipulator)
            End If
            Return manipulator
        End Function

        Public Shared Function GetScoreManipulator(order As OrderScoringParameter, findingManipulator As FindingManipulatorBase) As IOrderScoringManipulator
            Dim ret As New OrderScoringManipulator(findingManipulator, order)
            Return ret
        End Function

        Public Shared Function GetScoreManipulator(bool As BooleanScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of Boolean)
            Dim ret As New BooleanScoringManipulator(findingManipulator, bool)
            Return ret
        End Function

        Private Shared Function GetScoreManipulator(geogebra As GeogebraScoringParameter, findingManipulator As FindingManipulatorBase) As IGapScoringManipulator(Of String)
            Dim ret As New GeogebraScoringManipulator(findingManipulator, geogebra)
            Return ret
        End Function

        Private Shared Function GetScoreManipulator(selectPoint As SelectPointScoringParameter, findingManipulator As FindingManipulatorBase) As IChoiceScoringManipulator
            Dim ret As New ChoiceScoringManipulator(findingManipulator, selectPoint)
            Return ret
        End Function



        Public Shared Function GetScoreBaseManipulator(scoringParameter As ScoringParameter, findingManipulator As FindingManipulatorBase) As IScoreManipulator
            Dim typeKey = scoringParameter.GetType()

            If (Creator.ContainsKey(typeKey)) Then
                Return Creator(typeKey).Invoke(scoringParameter, findingManipulator)
            Else
                Debug.Assert(TypeOf scoringParameter Is AspectScoringParameter, "Score Parameter not found in creator dictionary")
            End If

            Return Nothing
        End Function

        Public Shared Function GetScoreBaseManipulator(Of T As {IScoreManipulator})(scoringParameter As ScoringParameter, findingManipulator As FindingManipulatorBase) As T
            Dim ret = GetScoreBaseManipulator(scoringParameter, findingManipulator)
            If (ret IsNot Nothing) Then
                Return DirectCast(ret, T)
            End If
            Return Nothing
        End Function



        Friend Shared Function GetKeyManipulator(ByVal solution As Solution, ByVal scoringParameter As ScoringParameter) As KeyManipulator
            Dim finding = solution.Findings.FindById(scoringParameter.FindingId)
            Return New KeyManipulator(New CreateObjectJIT(Of KeyFinding)(finding, Function() solution.GetFindingOrMakeIt(scoringParameter.FindingId)))
        End Function

        Public Shared Function GetKeyScoreBaseManipulator(scoringParameter As ScoringParameter, solution As Solution) As IScoreManipulator
            Dim manipulator As KeyManipulator = GetKeyManipulator(solution, scoringParameter)
            Return GetScoreBaseManipulator(scoringParameter, manipulator)
        End Function

        Public Shared Function GetKeyScoreBaseManipulator(Of T As {IScoreManipulator})(scoringParameter As ScoringParameter, solution As Solution) As T
            Dim ret = GetKeyScoreBaseManipulator(scoringParameter, solution)
            If (ret IsNot Nothing) Then
                Return DirectCast(ret, T)
            End If
            Return Nothing
        End Function



        Public Shared Function GetConceptManipulator(combinedScoringMapKey As CombinedScoringMapKey, solution As Solution) As IConceptScoreManipulator


            Dim scoreParameters = combinedScoringMapKey.Select(Function(scoreMapKey) scoreMapKey.ScoringParameter).Distinct().ToList()

            Dim inputs = New Dictionary(Of String, Object) From {
                {"CombinedScoringMapKey", combinedScoringMapKey},
                {"ScoreParameters", scoreParameters},
                {"Solution", solution}
            }

            WorkflowInvoker.Invoke(New ConceptInitiFlow(), inputs)

            Return GetConceptManipulatorBare(combinedScoringMapKey, solution)
        End Function

        Friend Shared Function GetConceptManipulatorBare(combinedScoringMapKey As CombinedScoringMapKey, solution As Solution) As IConceptScoreManipulator
            Dim scoreParameters = combinedScoringMapKey.Select(Function(scoreMapKey) scoreMapKey.ScoringParameter).Distinct().ToList()
            Dim conceptFinding = GetConceptFindingOrMakeIt(scoreParameters.First().FindingId, solution)

            If (combinedScoringMapKey.IsGroup) Then
                Dim conceptMapKey = New ConceptScoringMap(combinedScoringMapKey.Select(Function(key) key.ScoringParameter).Distinct(), solution).GetMap().FirstOrDefault(Function(csm) csm.Name = combinedScoringMapKey.Name)

                Debug.Assert(conceptMapKey IsNot Nothing, "conceptMapKey Should have a value")

                Return New ConceptScoreInSetsManipulator(conceptFinding, conceptMapKey, combinedScoringMapKey.SetNumbers)
            Else
                Return New ConceptScoreInFindingManipulator(conceptFinding, combinedScoringMapKey)
            End If
        End Function

        Public Shared Function GetConceptScoreManipulator(scoringParameter As ScoringParameter, solution As Solution) As IScoreManipulator
            Dim conceptFinding = GetConceptFindingOrMakeIt(scoringParameter.FindingId, solution)
            Return GetConceptScoreManipulator(scoringParameter, conceptFinding)
        End Function

        Public Shared Function GetConceptScoreManipulator(scoringParameter As ScoringParameter, finding As ConceptFinding) As IScoreManipulator
            Dim manipulator = New ConceptManipulator(finding)
            Return GetScoreBaseManipulator(scoringParameter, manipulator)
        End Function

        Public Shared Function GetConceptScoreManipulator(Of T As {IScoreManipulator})(scoringParameter As ScoringParameter, solution As Solution) As T
            Dim ret = GetConceptScoreManipulator(scoringParameter, solution)
            If (ret IsNot Nothing) Then
                Return DirectCast(ret, T)
            End If
            Return Nothing
        End Function

        Public Shared Function GetConceptFindingManipulator(scoringParameter As ScoringParameter, solution As Solution) As ConceptManipulator
            Dim conceptFinding = GetConceptFindingOrMakeIt(scoringParameter.FindingId, solution)
            Return GetConceptFindingManipulator(conceptFinding)
        End Function

        Public Shared Function GetConceptFindingManipulator(finding As ConceptFinding) As ConceptManipulator
            Return New ConceptManipulator(finding)
        End Function

        Private Shared Function GetConceptFindingOrMakeIt(id As String, solution As Solution) As ConceptFinding
            For Each kf In solution.ConceptFindings
                If (kf.Id = id) Then
                    Return kf
                End If
            Next
            Dim ret = New ConceptFinding(id)
            solution.ConceptFindings.Add(ret)
            Return ret
        End Function


    End Class

End Namespace