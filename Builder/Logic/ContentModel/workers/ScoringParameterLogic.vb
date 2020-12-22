Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring

Namespace ContentModel

    Class ScoringParameterLogic

        Shared Function GetPostfix(prm As ScoringParameter) As String
            Return If(String.IsNullOrEmpty(prm.InlineId),
                   If(String.IsNullOrEmpty(prm.ControllerId),
                      If(String.IsNullOrEmpty(prm.CollectionIdx), String.Empty,
                         prm.CollectionIdx),
                    "-" + prm.ControllerId),
                 "-" + prm.InlineId)
        End Function

        Shared Function GetFactIdFor(scoringMapKey As ScoringMapKey) As String
            Return scoringMapKey.ScoringParameter.GetScoreManipulator(New Solution()).GetFactIdForKey(scoringMapKey.ScoreKey)
        End Function

        Shared Function GetFactIdFor(scoringMapKey As ScoringMapKey, answerCategoryId As String) As String
            Return scoringMapKey.ScoringParameter.GetScoreManipulator(New Solution()).GetFactIdForKey(
                $"{scoringMapKey.ScoreKey}[{answerCategoryId}]")
        End Function

    End Class

End Namespace

