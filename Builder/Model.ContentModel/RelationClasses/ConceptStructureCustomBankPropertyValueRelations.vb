Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ConceptStructureCustomBankPropertyValueRelations
        Inherits CustomBankPropertyValueRelations
        Public Sub New()
        End Sub

        Public Overrides Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = MyBase.GetAllRelations()
            toReturn.Add(Me.ConceptStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId)
            toReturn.Add(Me.ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property ConceptStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ConceptStructureCustomBankPropertySelectedPartCollection", True)
                relation.AddEntityFieldPair(ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId, ConceptStructureCustomBankPropertySelectedPartFields.CustomBankPropertyId)
                relation.AddEntityFieldPair(ConceptStructureCustomBankPropertyValueFields.ResourceId, ConceptStructureCustomBankPropertySelectedPartFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertyValueEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertySelectedPartEntity", False)
                Return relation
            End Get
        End Property


        Public Overridable ReadOnly Property ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ConceptStructureCustomBankProperty", False)
                relation.AddEntityFieldPair(ConceptStructureCustomBankPropertyFields.CustomBankPropertyId, ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property CustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CustomBankProperty", False)
                relation.AddEntityFieldPair(CustomBankPropertyFields.CustomBankPropertyId, ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property ResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Resource", False)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, ConceptStructureCustomBankPropertyValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ConceptStructureCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property

        Friend ReadOnly Property RelationToSuperTypeCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, False)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, ConceptStructureCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, ConceptStructureCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property

        Public Overrides Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation
            Return Nothing
        End Function

        Public Overrides Function GetSuperTypeRelation() As IEntityRelation
            Return Me.RelationToSuperTypeCustomBankPropertyValueEntity
        End Function



    End Class

    Friend Class StaticConceptStructureCustomBankPropertyValueRelations
        Friend Shared ReadOnly ConceptStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceIdStatic As IEntityRelation = New ConceptStructureCustomBankPropertyValueRelations().ConceptStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId
        Friend Shared ReadOnly ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ConceptStructureCustomBankPropertyValueRelations().ConceptStructureCustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly CustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ConceptStructureCustomBankPropertyValueRelations().CustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ResourceEntityUsingResourceIdStatic As IEntityRelation = New ConceptStructureCustomBankPropertyValueRelations().ResourceEntityUsingResourceId

        Shared Sub New()
        End Sub
    End Class
End Namespace
