Namespace Scoring.Reporting
    Public Interface IItemConceptScoringReportData
        Property ItemCode() As String
        Property ItemTitle() As String
        Property Itemlayouttemplate() As String
        Property ConceptCode() As String
        Property ConceptResponseLabel() As String
        Property KeyValuesAndConceptScores() As String
        Property AdditionalKeyValuesAndConceptScores() As String
        Property IsGrouped() As String
        Property GroupElementCount() As Integer
        Property InteractionCount() As Integer
        Property AttributeLevelConceptResponseCount() As Integer
        Property SubAttributLevelConceptResponseCount() As Integer
    End Interface
End Namespace
