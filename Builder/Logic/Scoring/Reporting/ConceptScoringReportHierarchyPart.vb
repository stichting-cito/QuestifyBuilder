Imports System.Collections.ObjectModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Scoring
Friend Class ConceptScoringReportHierarchyPart
    Implements IConceptScoringBrowserHierarchyPart
    Private ReadOnly _parent As IConceptScoringBrowserHierarchyPart
    Private ReadOnly _depth As Integer = -1

    Public Property IsSelected As Boolean Implements IConceptScoringBrowserHierarchyPart.IsSelected


    Public ReadOnly Property Part() As ConceptStructurePartCustomBankPropertyEntity Implements IConceptScoringBrowserHierarchyPart.Part

    Public ReadOnly Property PartName() As String Implements IConceptScoringBrowserHierarchyPart.PartName
        Get
            Return Part.Name
        End Get
    End Property

    Private Property ConceptScorePart As ObservableCollection(Of IConceptScoringBrowserScoreContainer) Implements IConceptScoringBrowserHierarchyPart.ConceptScorePart


    Public ReadOnly Property Id() As Guid Implements IConceptScoringBrowserHierarchyPart.Id
        Get
            Return Part.ConceptStructurePartCustomBankPropertyId
        End Get
    End Property

    Friend Sub New(conceptPart As ConceptStructurePartCustomBankPropertyEntity, parent As IConceptScoringBrowserHierarchyPart)
        Part = conceptPart
        _parent = parent
        If parent IsNot Nothing Then
            _depth = parent.Depth + 1
        End If
    End Sub

    Public Function IsParentPart(conceptStructurePartCustomBankPropertyId As Guid) As Boolean Implements IConceptScoringBrowserHierarchyPart.IsParentPart
        Dim ret As Boolean = Part.ConceptStructurePartCustomBankPropertyId = conceptStructurePartCustomBankPropertyId
        ret = ret OrElse (If(_parent?.IsParentPart(conceptStructurePartCustomBankPropertyId), False))

        Return ret
    End Function

    Private ReadOnly Property Depth() As Integer Implements IConceptScoringBrowserHierarchyPart.Depth
        Get
            Return _depth
        End Get
    End Property


    Public Sub SetIsSelectedToFalseNoNotification() Implements IConceptScoringBrowserHierarchyPart.SetIsSelectedToFalseNoNotification
    End Sub
End Class
