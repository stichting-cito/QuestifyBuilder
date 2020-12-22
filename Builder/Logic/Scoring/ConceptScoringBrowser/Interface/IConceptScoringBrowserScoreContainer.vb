Namespace Scoring
    Public Interface IConceptScoringBrowserScoreContainer
        ReadOnly Property ConceptId() As String
        Sub AddParent(parent As IConceptScoringBrowserHierarchyPart)
        Property IntScore() As System.Nullable(Of Integer)
    End Interface
End Namespace
