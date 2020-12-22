Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ConceptStructureCustomBankPropertySelectedPartRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.ConceptStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId)
            toReturn.Add(Me.ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property ConceptStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ConceptStructureCustomBankPropertyValue", False)
                relation.AddEntityFieldPair(ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId, ConceptStructureCustomBankPropertySelectedPartFields.CustomBankPropertyId)
                relation.AddEntityFieldPair(ConceptStructureCustomBankPropertyValueFields.ResourceId, ConceptStructureCustomBankPropertySelectedPartFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertyValueEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertySelectedPartEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ConceptStructurePartCustomBankProperty", False)
                relation.AddEntityFieldPair(ConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId, ConceptStructureCustomBankPropertySelectedPartFields.ConceptStructurePartId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructurePartCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertySelectedPartEntity", True)
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

    Friend Class StaticConceptStructureCustomBankPropertySelectedPartRelations
        Friend Shared ReadOnly ConceptStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic As IEntityRelation = New ConceptStructureCustomBankPropertySelectedPartRelations().ConceptStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId
        Friend Shared ReadOnly ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartIdStatic As IEntityRelation = New ConceptStructureCustomBankPropertySelectedPartRelations().ConceptStructurePartCustomBankPropertyEntityUsingConceptStructurePartId

        Shared Sub New()
        End Sub
    End Class
End Namespace
