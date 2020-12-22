Imports Cito.Tester.ContentModel
Namespace ItemProcessing
    Friend Class GapMatchScoringParameterHandler
        Inherits ScoringParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            Dim newGmPrm As GapMatchScoringParameter = DirectCast(newParam, GapMatchScoringParameter)
            Dim currGmPrm As GapMatchScoringParameter = DirectCast(currentParam, GapMatchScoringParameter)

            currGmPrm.GapXhtmlParameter.DesignerSettings.AddRange(newGmPrm.GapXhtmlParameter.DesignerSettings)

            newGmPrm.GapXhtmlParameter = currGmPrm.GapXhtmlParameter

            MyBase.Merge(newGmPrm, currGmPrm, warnErr)
        End Sub
    End Class
End Namespace