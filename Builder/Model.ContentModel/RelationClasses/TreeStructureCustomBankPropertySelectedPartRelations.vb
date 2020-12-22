Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class TreeStructureCustomBankPropertySelectedPartRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId)
            toReturn.Add(Me.TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TreeStructureCustomBankPropertyValue", False)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId, TreeStructureCustomBankPropertySelectedPartFields.CustomBankPropertyId)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyValueFields.ResourceId, TreeStructureCustomBankPropertySelectedPartFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyValueEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertySelectedPartEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TreeStructurePartCustomBankProperty", False)
                relation.AddEntityFieldPair(TreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId, TreeStructureCustomBankPropertySelectedPartFields.TreeStructurePartId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructurePartCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertySelectedPartEntity", True)
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

    Friend Class StaticTreeStructureCustomBankPropertySelectedPartRelations
        Friend Shared ReadOnly TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceIdStatic As IEntityRelation = New TreeStructureCustomBankPropertySelectedPartRelations().TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdResourceId
        Friend Shared ReadOnly TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartIdStatic As IEntityRelation = New TreeStructureCustomBankPropertySelectedPartRelations().TreeStructurePartCustomBankPropertyEntityUsingTreeStructurePartId

        Shared Sub New()
        End Sub
    End Class
End Namespace
