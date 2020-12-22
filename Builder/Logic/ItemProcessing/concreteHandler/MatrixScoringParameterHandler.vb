Imports Cito.Tester.ContentModel

Namespace ItemProcessing
    Friend Class MatrixScoringParameterHandler
        Inherits ScoringParameterHandler

        Friend Overrides Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)
            Dim newMtxPrm As MatrixScoringParameter = DirectCast(newParam, MatrixScoringParameter)
            Dim currMtxPrm As MatrixScoringParameter = DirectCast(currentParam, MatrixScoringParameter)

            Dim pm = GetConcreteMerger(currMtxPrm.MatrixColumnsDefinition.GetType())
            pm.Merge(newMtxPrm.MatrixColumnsDefinition, currMtxPrm.MatrixColumnsDefinition, warnErr)

            pm = GetConcreteMerger(currMtxPrm.LineLabelColumnWidth.GetType())
            pm.Merge(newMtxPrm.LineLabelColumnWidth, currMtxPrm.LineLabelColumnWidth, warnErr)

            MyBase.Merge(newMtxPrm, currMtxPrm, warnErr)
        End Sub
    End Class
End Namespace