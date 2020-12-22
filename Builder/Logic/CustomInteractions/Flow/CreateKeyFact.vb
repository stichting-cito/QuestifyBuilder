Imports System.Activities
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

Namespace CustomInteractions.Flow

    Public NotInheritable Class CreateKeyFact
        Inherits CodeActivity
        Public Property CustomInterActionScoreParameter As InArgument(Of ScoringParameter)

        Public Property ItemSolution As InArgument(Of Solution)

        Protected Overrides Sub Execute(context As CodeActivityContext)
            Dim scoringParameter = context.GetValue(CustomInterActionScoreParameter)
            Dim solution As Solution = context.GetValue(ItemSolution)

            If TypeOf scoringParameter Is GeogebraScoringParameter Then
                Dim manipulator As IScoreManipulator = DirectCast(scoringParameter, GeogebraScoringParameter).GetScoreManipulator(solution)
                For Each parameterCollection As ParameterCollection In scoringParameter.Value
                    If TypeOf manipulator Is IGapScoringManipulator Then
                        DirectCast(manipulator, IGapScoringManipulator).RemoveKey(parameterCollection.Id)
                        DirectCast(manipulator, IGapScoringManipulator).SetKeyWithDefaultValue(parameterCollection.Id)
                    End If
                Next
            End If
        End Sub
    End Class
End NameSpace