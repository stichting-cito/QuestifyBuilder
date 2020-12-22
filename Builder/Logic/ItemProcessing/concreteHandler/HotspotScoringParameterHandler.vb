Imports Cito.Tester.ContentModel

Namespace ItemProcessing
    Friend Class HotspotScoringParameterHandler
        Inherits ScoringParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            Dim newHsPrm As HotspotScoringParameter = DirectCast(newParam, HotspotScoringParameter)
            Dim currHsPrm As HotspotScoringParameter = DirectCast(currentParam, HotspotScoringParameter)

            Dim pm As ParameterHandler = GetConcreteMerger(currHsPrm.Area.GetType())
            pm.Merge(newHsPrm.Area, currHsPrm.Area, warnErr)

            MyBase.Merge(newHsPrm, currHsPrm, warnErr)
        End Sub
    End Class
End Namespace