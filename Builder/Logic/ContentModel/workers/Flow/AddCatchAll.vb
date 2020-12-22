Imports System.Activities
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

Namespace ContentModel.workers.Flow

    Public NotInheritable Class AddCatchAll
        Inherits CodeActivity

        Property Solution As InArgument(Of Solution)


        Property CombinedScoringMapKey As InArgument(Of CombinedScoringMapKey)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim theSolution = context.GetValue(Solution)
            Dim combinedScoreMapKey = context.GetValue(CombinedScoringMapKey)

            Dim manipulator = ScoringParameterFactory.GetConceptManipulatorBare(combinedScoreMapKey, theSolution)

            manipulator.AddCatchAllAnswerCategory()
        End Sub

    End Class
End Namespace