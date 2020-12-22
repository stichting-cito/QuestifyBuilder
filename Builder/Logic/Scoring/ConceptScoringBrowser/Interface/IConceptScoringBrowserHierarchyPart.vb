Imports System.Collections.ObjectModel
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace Scoring

    Public Interface IConceptScoringBrowserHierarchyPart
        ReadOnly Property Id() As Guid

        ReadOnly Property PartName() As String
        Function IsParentPart(conceptStructurePartCustomBankPropertyId As Guid) As Boolean
        ReadOnly Property Depth() As Integer
        ReadOnly Property Part() As ConceptStructurePartCustomBankPropertyEntity

        Property IsSelected() As Boolean
        Sub SetIsSelectedToFalseNoNotification()

        Property ConceptScorePart() As ObservableCollection(Of IConceptScoringBrowserScoreContainer)
    End Interface
End Namespace
