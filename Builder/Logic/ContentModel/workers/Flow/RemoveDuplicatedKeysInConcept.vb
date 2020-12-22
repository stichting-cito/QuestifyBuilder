Imports System.Activities
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveDuplicatedKeysInConcept
        Inherits CodeActivity

        Property Solution As InArgument(Of Solution)

        Property CombinedScoringMapKey As InArgument(Of CombinedScoringMapKey)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim sol = context.GetValue(Me.Solution)
            Dim combScoringMapKey = context.GetValue(CombinedScoringMapKey)

            Debug.Assert(Not combScoringMapKey.IsGroup)

            Dim sp = TryCast(combScoringMapKey.First().ScoringParameter, ChoiceScoringParameter)
            Debug.Assert(sp IsNot Nothing, "It was expected this to be a choice score parameter")

            Dim keyScoringManipulator = sp.GetScoreManipulator(sol)

            If (sp.IsSingleChoice) Then

                Dim conceptScoringManipulator = ScoringParameterFactory.GetConceptScoreManipulator(Of IChoiceScoringManipulator)(sp, sol)

                Dim regularKeys = New HashSet(Of String)(keyScoringManipulator.GetKeysAlreadyManipulated())

                Dim toRemove = From key In conceptScoringManipulator.GetKeysAlreadyManipulated()
                               Where (regularKeys.Contains(DefaultStringOperations.GetSubParameterId(key))) _
                               AndAlso (DefaultStringOperations.GetSubParameterId(key) <> key)

                For Each keyToRemove As String In toRemove.ToList()
                    conceptScoringManipulator.RemoveKey(keyToRemove)
                Next

            End If
        End Sub


    End Class
End Namespace