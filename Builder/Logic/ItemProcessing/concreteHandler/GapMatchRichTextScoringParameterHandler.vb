Imports Cito.Tester.ContentModel

Namespace ItemProcessing

    Friend Class GapMatchRichTextScoringParameterHandler
        Inherits ScoringParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            Dim newGmPrm As GapMatchRichTextScoringParameter = DirectCast(newParam, GapMatchRichTextScoringParameter)
            Dim currGmPrm As GapMatchRichTextScoringParameter = DirectCast(currentParam, GapMatchRichTextScoringParameter)

            currGmPrm.GapXhtmlParameter.DesignerSettings.AddRange(newGmPrm.GapXhtmlParameter.DesignerSettings)

            newGmPrm.GapXhtmlParameter = currGmPrm.GapXhtmlParameter

            MyBase.Merge(newGmPrm, currGmPrm, warnErr)
        End Sub
    End Class
End Namespace