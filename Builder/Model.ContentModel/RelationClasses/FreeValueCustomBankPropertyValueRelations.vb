Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class FreeValueCustomBankPropertyValueRelations
        Inherits CustomBankPropertyValueRelations
        Public Sub New()
        End Sub

        Public Overrides Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = MyBase.GetAllRelations()
            toReturn.Add(Me.FreeValueCustomBankPropertyEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function




        Public Overrides ReadOnly Property CustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CustomBankProperty", False)
                relation.AddEntityFieldPair(CustomBankPropertyFields.CustomBankPropertyId, FreeValueCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FreeValueCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property FreeValueCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "FreeValueCustomBankProperty", False)
                relation.AddEntityFieldPair(FreeValueCustomBankPropertyFields.CustomBankPropertyId, FreeValueCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FreeValueCustomBankPropertyEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FreeValueCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property ResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Resource", False)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, FreeValueCustomBankPropertyValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FreeValueCustomBankPropertyValueEntity", True)
                Return relation
            End Get
        End Property

        Friend ReadOnly Property RelationToSuperTypeCustomBankPropertyValueEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, False)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.ResourceId, FreeValueCustomBankPropertyValueFields.ResourceId)
                relation.AddEntityFieldPair(CustomBankPropertyValueFields.CustomBankPropertyId, FreeValueCustomBankPropertyValueFields.CustomBankPropertyId)
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

    Friend Class StaticFreeValueCustomBankPropertyValueRelations
        Friend Shared ReadOnly CustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New FreeValueCustomBankPropertyValueRelations().CustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly FreeValueCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New FreeValueCustomBankPropertyValueRelations().FreeValueCustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ResourceEntityUsingResourceIdStatic As IEntityRelation = New FreeValueCustomBankPropertyValueRelations().ResourceEntityUsingResourceId

        Shared Sub New()
        End Sub
    End Class
End Namespace
