Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel.Scoring


Namespace Scoring
    Public Interface IConceptScoringBrowserObjectFactory
        Function CreateHierarchyPart(conceptPart As ConceptStructurePartCustomBankPropertyEntity, parent As IConceptScoringBrowserHierarchyPart) As IConceptScoringBrowserHierarchyPart
        Function CreatePartScoreContainer(conceptStructurePartTheScoreRelatesTo As IConceptScoringBrowserHierarchyPart, conceptId As String, score As System.Nullable(Of Integer), conceptScoreManipulator As IConceptScoreManipulator) As IConceptScoringBrowserScoreContainer
    End Interface
End Namespace
