Imports Cito.Tester.ContentModel

Namespace ItemProcessing
    Friend Class HotTextScoringParameterHandler
        Inherits ScoringParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            Dim newGmPrm As HotTextScoringParameter = DirectCast(newParam, HotTextScoringParameter)
            Dim currGmPrm As HotTextScoringParameter = DirectCast(currentParam, HotTextScoringParameter)

            currGmPrm.HotTextText.DesignerSettings.AddRange(newGmPrm.HotTextText.DesignerSettings)

            newGmPrm.HotTextText = currGmPrm.HotTextText

            MyBase.Merge(newGmPrm, currGmPrm, warnErr)
        End Sub
    End Class
End Namespace