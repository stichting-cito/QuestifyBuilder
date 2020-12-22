Namespace Scoring.Reporting
    Public Class ItemConceptScoringReportData
        Implements IItemConceptScoringReportData
        Public Property ItemCode As String Implements IItemConceptScoringReportData.ItemCode

        Public Property ItemId As String

        Public Property ItemTitle As String Implements IItemConceptScoringReportData.ItemTitle

        Public Property Itemlayouttemplate As String Implements IItemConceptScoringReportData.Itemlayouttemplate

        Public Property ConceptCode As String Implements IItemConceptScoringReportData.ConceptCode

        Public Property ConceptResponseLabel As String Implements IItemConceptScoringReportData.ConceptResponseLabel

        Public Property KeyValuesAndConceptScores As String Implements IItemConceptScoringReportData.KeyValuesAndConceptScores

        Public Property AdditionalKeyValuesAndConceptScores As String Implements IItemConceptScoringReportData.AdditionalKeyValuesAndConceptScores

        Public Property IsGrouped As String Implements IItemConceptScoringReportData.IsGrouped

        Public Property GroupElementCount As Integer Implements IItemConceptScoringReportData.GroupElementCount

        Public Property InteractionCount As Integer Implements IItemConceptScoringReportData.InteractionCount

        Public Property AttributeLevelConceptResponseCount As Integer Implements IItemConceptScoringReportData.AttributeLevelConceptResponseCount

        Public Property SubAttributLevelConceptResponseCount As Integer Implements IItemConceptScoringReportData.SubAttributLevelConceptResponseCount

    End Class
End Namespace
