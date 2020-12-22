Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class AddMissingAnswerCatagoryToMC
        Inherits CodeActivity

        Property Solution As InArgument(Of Solution)

        Property CombinedScoringMapKey As InArgument(Of CombinedScoringMapKey)


        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim sol = context.GetValue(Me.Solution)
            Dim combScoringMapKey = context.GetValue(CombinedScoringMapKey)

            Debug.Assert(Not combScoringMapKey.IsGroup)

            Dim sp = TryCast(combScoringMapKey.First().ScoringParameter, ChoiceScoringParameter)
            Dim currentKeyStatus = sp.GetScoreManipulator(sol).GetKeyStatus()

            Debug.Assert(sp IsNot Nothing, "It was expected this to be a choice score parameter")

            If (sp.IsSingleChoice) Then

                Dim conceptScoringManipulator = ScoringParameterFactory.GetConceptScoreManipulator(Of IChoiceScoringManipulator)(sp, sol)
                conceptScoringManipulator.SetFactSetTarget(Nothing)

                Dim defaultKeys = conceptScoringManipulator.GetManipulatableKeys().ToDictionary(Of String, Integer?)(Function(key) key, Function(key) Nothing)

                Dim minValue As Integer = GetMinValue(conceptScoringManipulator, defaultKeys)

                CreateKeysForMissingAnswersCategory(sp, sol, defaultKeys, minValue, currentKeyStatus)
            End If
        End Sub

        Private Sub CreateKeysForMissingAnswersCategory(
            ByVal sp As ChoiceScoringParameter,
            ByVal sol As Solution,
            ByVal defaultKeys As Dictionary(Of String, Integer?),
            ByVal minValue As Integer,
            ByVal currentKeyStatus As IDictionary(Of String, Boolean))

            If (minValue <> Integer.MaxValue) Then
                For Each kvp As KeyValuePair(Of String, Integer?) In defaultKeys
                    If (If(kvp.Value, -1) < minValue AndAlso Not currentKeyStatus(kvp.Key)) Then

                        Dim manipulator = ScoringParameterFactory.GetConceptScoreManipulator(Of IChoiceScoringManipulator)(sp, sol)
                        manipulator.SetFactSetTarget(Nothing)
                        manipulator.SetKey(kvp.Key)

                        Dim conceptFindingManipulator = ScoringParameterFactory.GetConceptFindingManipulator(sp, sol)
                        Dim fact = conceptFindingManipulator.GetFacts(manipulator.GetFactIdForKey(kvp.Key)).Single()
                        fact.Id = manipulator.GetFactIdForKey(kvp.Key + "[" + minValue.ToString() + "]")

                    End If
                Next
            End If
        End Sub

        Private Function GetMinValue(ByVal conceptScoringManipulator As IChoiceScoringManipulator, ByRef defaultKeys As Dictionary(Of String, Integer?)) As Integer

            Dim keysAlreadyManipulated = conceptScoringManipulator.GetKeysAlreadyManipulated()
            Dim minValue = Integer.MaxValue

            For Each keyId As String In keysAlreadyManipulated

                Dim number = DefaultStringOperations.GetNumberFromFactId(keyId)

                If (number.HasValue) Then
                    Dim key = DefaultStringOperations.GetSubParameterId(keyId)

                    If (defaultKeys(key) Is Nothing) Then
                        defaultKeys(key) = number.Value
                    Else
                        defaultKeys(key) = Math.Min(defaultKeys(key).Value, number.Value)
                    End If

                    minValue = Math.Min(minValue, number.Value)

                End If
            Next
            Return minValue
        End Function

    End Class
End Namespace