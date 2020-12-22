Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace Scoring
    Public Interface IConceptScoringBrowserDataProvider
        Function PopulateConceptCustomBankPropertyHierarchy(id As Guid) As ConceptStructurePartCustomBankPropertyEntity
        Function ReadConceptStructureCustomBankProperty(customBankPropertyId As Guid) As ConceptStructureCustomBankPropertyEntity
    End Interface
End Namespace
