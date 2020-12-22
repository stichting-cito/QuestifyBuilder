Namespace Scoring.Reporting
    Friend Class ConceptScoringReportHierarchyPartScoreContainer
        Implements IConceptScoringBrowserScoreContainer
        Friend Sub New(conceptStructurePartTheScoreRelatesTo As IConceptScoringBrowserHierarchyPart, conceptIdParam As String, score As Nullable(Of Integer))
            ConceptId = conceptIdParam
            Parents = New List(Of IConceptScoringBrowserHierarchyPart)()
            Parents.Add(conceptStructurePartTheScoreRelatesTo)
            IntScore = score
        End Sub

        Public ReadOnly Property ConceptId() As String Implements IConceptScoringBrowserScoreContainer.ConceptId

        Public Property Parents As List(Of IConceptScoringBrowserHierarchyPart)

        Public Property IntScore As Nullable(Of Integer) Implements IConceptScoringBrowserScoreContainer.IntScore


        Public Sub AddParent(parent As IConceptScoringBrowserHierarchyPart) Implements IConceptScoringBrowserScoreContainer.AddParent
            Parents.Add(parent)
        End Sub

    End Class
End Namespace
