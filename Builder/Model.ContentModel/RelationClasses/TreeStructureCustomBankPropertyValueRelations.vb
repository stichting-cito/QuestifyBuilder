Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class TreeStructureCustomBankPropertyValueRelations
        Inherits CustomBankPropertyValueRelations
        Public Sub New()
        End Sub

        Public Overrides Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = MyBase.GetAllRelations()
            toReturn.Add(Me.TreeStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId)
            toReturn.Add(Me.TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property TreeStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TreeStructureCustomBankPropertySelectedPartCollection", True)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId, TreeStructureCustomBankPropertySelectedPartFields.CustomBankPropertyId)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyValueFields.ResourceId, TreeStructureCustomBankPropertySelectedPartFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyValueEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertySelectedPartEntity", False)
                Return relation
            End Get
        End Property


        Public Overrides ReadOnly Property CustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CustomBankProperty", False)
                relation.AddEntityFieldPair(CustomBankPropertyFields.CustomBankPropertyId, TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property ResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Resource", False)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, TreeStructureCustomBankPropertyValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TreeStructureCustomBankProperty", False)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyFields.CustomBankPropertyId, TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property

        Friend ReadOnly Property RelationToSuperTypeCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, False)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, TreeStructureCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId)
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

    Friend Class StaticTreeStructureCustomBankPropertyValueRelations
        Friend Shared ReadOnly TreeStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyValueRelations().TreeStructureCustomBankPropertySelectedPartEntityUsingCustomBankPropertyIdResourceId
        Friend Shared ReadOnly CustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyValueRelations().CustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ResourceEntityUsingResourceIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyValueRelations().ResourceEntityUsingResourceId
        Friend Shared ReadOnly TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyValueRelations().TreeStructureCustomBankPropertyEntityUsingCustomBankPropertyId

        Shared Sub New()
        End Sub
    End Class
End Namespace
