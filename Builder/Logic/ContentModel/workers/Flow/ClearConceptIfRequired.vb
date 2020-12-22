Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public Class ClearConceptIfRequired : Inherits CodeActivity

        Property Solution As InArgument(Of Solution)

        Property ScoringParameters As InArgument(Of IEnumerable(Of ScoringParameter))


        Protected Overrides Sub Execute(context As CodeActivityContext)

            Dim solution = context.GetValue(Me.Solution)
            Dim scoringParameters = context.GetValue(Me.ScoringParameters).ToList()


            If (solution.ConceptFindings.Count > 0) Then
                Dim keyFindingMap = New ScoringMap(scoringParameters, solution).GetMap()
                Dim conceptFindingMap = New ConceptScoringMap(scoringParameters, solution).GetMap()

                Dim areEqual = AreMapsEqual(keyFindingMap.ToList(), conceptFindingMap.ToList())

                If (Not areEqual) Then
                    solution.ConceptFindings.Clear()
                End If

            End If



        End Sub

        Private Function AreMapsEqual(keyFindingMap As List(Of CombinedScoringMapKey), conceptFindingMap As List(Of CombinedScoringMapKey)) As Boolean

            If (keyFindingMap.Count <> conceptFindingMap.Count) Then Return False

            For i = 0 To keyFindingMap.Count - 1
                Dim nameEqual = keyFindingMap(i).Name = conceptFindingMap(i).Name

                If (Not nameEqual) Then Return False
            Next

            Return True
        End Function


    End Class

End Namespace