Imports System.Activities
Imports Cito.Tester.ContentModel

Namespace ContentModel.workers.Flow

    Public NotInheritable Class GetFactsIdsPerScoringParameter
        Inherits CodeActivity(Of Dictionary(Of String, ScoringParameter))

        Property ScoringParameters As InArgument(Of IEnumerable(Of ScoringParameter))


        Protected Overrides Function Execute(ByVal context As CodeActivityContext) As Dictionary(Of String, ScoringParameter)
            Dim scoringParameters = context.GetValue(Me.ScoringParameters)

            Dim ret As New Dictionary(Of String, ScoringParameter)

            For Each scoringParameter As ScoringParameter In scoringParameters
                Dim manipulator = scoringParameter.GetScoreManipulator(New Solution())
                If manipulator IsNot nothing Then
                    For Each parameterCollection As ParameterCollection In scoringParameter.Value
                        Dim factId = manipulator.GetFactIdForKey(parameterCollection.Id)
                        ret.Add(factId, scoringParameter)
                    Next
                End If
            Next

            Return ret
        End Function

    End Class
End Namespace