Imports Cito.Tester.ContentModel

Namespace ItemProcessing
    Friend Class GraphGapMatchScoringParameterHandler
        Inherits ScoringParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            Dim newGmPrm As GraphGapMatchScoringParameter = DirectCast(newParam, GraphGapMatchScoringParameter)
            Dim currGmPrm As GraphGapMatchScoringParameter = DirectCast(currentParam, GraphGapMatchScoringParameter)

            Dim pm As ParameterHandler = GetConcreteMerger(currGmPrm.Area.GetType())
            pm.Merge(newGmPrm.Area, currGmPrm.Area, warnErr)


            MyBase.Merge(newGmPrm, currGmPrm, warnErr)
        End Sub
    End Class
End Namespace