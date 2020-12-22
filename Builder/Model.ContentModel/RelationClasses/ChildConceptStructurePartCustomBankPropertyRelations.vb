Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ChildConceptStructurePartCustomBankPropertyRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.ConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyId)
            toReturn.Add(Me.ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property ConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ChildConceptStructurePartCustomBankProperty", False)
                relation.AddEntityFieldPair(ConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, ChildConceptStructurePartCustomBankPropertyFields.ChildConceptStructurePartCustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ChildConceptStructurePartCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "", False)
                relation.AddEntityFieldPair(ConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, ChildConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ChildConceptStructurePartCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation
            Return Nothing
        End Function
        Public Overridable Function GetSuperTypeRelation() As IEntityRelation
            Return Nothing
        End Function


    End Class

    Friend Class StaticChildConceptStructurePartCustomBankPropertyRelations
        Friend Shared ReadOnly ConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyIdStatic As IEntityRelation = New ChildConceptStructurePartCustomBankPropertyRelations().ConceptStructurePartCustomBankPropertyEntityUsingChildConceptStructurePartCustomBankPropertyId
        Friend Shared ReadOnly ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyIdStatic As IEntityRelation = New ChildConceptStructurePartCustomBankPropertyRelations().ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartCustomBankPropertyId

        Shared Sub New()
        End Sub
    End Class
End Namespace
