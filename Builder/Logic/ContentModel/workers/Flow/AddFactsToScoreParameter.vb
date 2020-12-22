Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class AddFactsToScoreParameter : Inherits CodeActivity

        Property Solution As InArgument(Of Solution)

        Property CombinedScoringKey As InArgument(Of CombinedScoringMapKey)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim solution As Solution = context.GetValue(Me.Solution)
            Dim combinedScoringMapKey = context.GetValue(Me.CombinedScoringKey)
            Dim choiceScoringParameter = DirectCast(combinedScoringMapKey.First().ScoringParameter, ChoiceScoringParameter)

            Dim scoreManipulator = choiceScoringParameter.GetScoreManipulator(solution)
            Dim keyStatus = scoreManipulator.GetKeyStatus()

            Dim alreadyManipulated = New HashSet(Of String)(scoreManipulator.GetKeysAlreadyManipulated(), New FactIdEqualityComparer())

            alreadyManipulated.UnionWith(From kvp In keyStatus Where kvp.Value Select kvp.Key)

            Dim newId = combinedScoringMapKey.GetIdForAnswerCategory(solution)

            Debug.Assert(Not combinedScoringMapKey.IsGroup, "Groups are not supported")

            For Each scoringMapKey As ScoringMapKey In combinedScoringMapKey
                AddKeys(choiceScoringParameter, solution, alreadyManipulated, keyStatus, scoringMapKey, newId)
            Next

        End Sub

        Private Sub AddKeys(ByVal choiceScoringParameter As ChoiceScoringParameter, ByVal solution As Solution, ByVal alreadyManipulated As HashSet(Of String), ByVal keyStatus As IDictionary(Of String, Boolean), ByVal scoringMapKey As ScoringMapKey, ByVal newId As Integer)

            If (Not alreadyManipulated.Contains(scoringMapKey.ScoreKey)) Then

                If (choiceScoringParameter.IsSingleChoice AndAlso newId = 1) Then
                    SetSingleChoiceValue(scoringMapKey, newId, choiceScoringParameter, solution)
                End If

            Else

                If (Not choiceScoringParameter.IsSingleChoice) Then
                    If (newId = 1) Then
                        Dim valueToSet = Not keyStatus(scoringMapKey.ScoreKey)
                        SetChoiceValue(scoringMapKey, valueToSet, newId, choiceScoringParameter, solution)
                    End If
                End If

            End If
        End Sub

        Private Sub SetSingleChoiceValue(scoringMapKey As ScoringMapKey, newId As Integer, choiceScoringParameter As ChoiceScoringParameter, solution As Solution)

            Dim key = scoringMapKey.ScoreKey
            Dim dummyScoreParameter As ChoiceScoringParameter = GetDummyScoreParameter(choiceScoringParameter)

            Dim manipulator = ScoringParameterFactory.GetConceptScoreManipulator(Of IChoiceScoringManipulator)(dummyScoreParameter, solution)
            manipulator.SetFactSetTarget(Nothing)
            Dim conceptFindingManipulator = ScoringParameterFactory.GetConceptFindingManipulator(choiceScoringParameter, solution)
            conceptFindingManipulator.SetFactSetTarget(Nothing)

            manipulator.SetKey(key)
            conceptFindingManipulator.SetFactSetTarget(Nothing)

            Dim fact = conceptFindingManipulator.GetFacts(manipulator.GetFactIdForKey(key)).Single()
            fact.Id = manipulator.GetFactIdForKey(key + "[" + newId.ToString() + "]")

        End Sub


        Private Sub SetChoiceValue(scoringMapKey As ScoringMapKey, valueToSet As Boolean, newId As Integer, choiceScoringParameter As ChoiceScoringParameter, solution As Solution)
            Dim key = $"{scoringMapKey.ScoreKey}[{newId}]"
            Dim dummyScoreParameter As ChoiceScoringParameter = GetDummyScoreParameter(choiceScoringParameter, newId)

            For Each subParam In dummyScoreParameter.Value
                subParam.Id = $"{subParam.Id}[{newId}]"
            Next

            Dim manipulator = ScoringParameterFactory.GetConceptScoreManipulator(Of IChoiceScoringManipulator)(dummyScoreParameter, solution)
            manipulator.SetFactSetTarget(Nothing)

            If (valueToSet) Then
                manipulator.SetKey(key)
            Else
                manipulator.RemoveKey(key)
            End If
        End Sub

        Private Function GetDummyScoreParameter(ByVal choiceScoringParameter As ChoiceScoringParameter, Optional ByVal idSuffix As Integer? = Nothing) As ChoiceScoringParameter


            Dim toReturn = New ChoiceScoringParameter() With
                {
                .ControllerId = choiceScoringParameter.ControllerId,
                .FindingOverride = choiceScoringParameter.FindingId,
                .InlineId = choiceScoringParameter.InlineId,
                .MaxChoices = If(choiceScoringParameter.IsSingleChoice, 1, 0),
                .Value = New ParameterSetCollection()
                }

            For Each subSet In choiceScoringParameter.Value
                toReturn.Value.Add(New ParameterCollection() With
                                   {
                                       .Id = If(idSuffix.HasValue,
                                                $"{subSet.Id}[{idSuffix}]",
                                                          subSet.Id)})
            Next

            Return toReturn

        End Function


    End Class

End Namespace