Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ItemLayoutTemplateResourceRelations
        Inherits ResourceRelations
        Public Sub New()
        End Sub

        Public Overrides Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = MyBase.GetAllRelations()
            Return toReturn
        End Function


        Public Overrides ReadOnly Property CustomBankPropertyValueEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyValueCollection", True)
                relation.AddEntityFieldPair(ItemLayoutTemplateResourceFields.ResourceId, CustomBankPropertyValueFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyValueEntity", False)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property DependentResourceEntityUsingDependentResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ReferencedResourceCollection", True)
                relation.AddEntityFieldPair(ItemLayoutTemplateResourceFields.ResourceId, DependentResourceFields.DependentResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DependentResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property DependentResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "DependentResourceCollection", True)
                relation.AddEntityFieldPair(ItemLayoutTemplateResourceFields.ResourceId, DependentResourceFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DependentResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property HiddenResourceEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "HiddenResourceCollection", True)
                relation.AddEntityFieldPair(ItemLayoutTemplateResourceFields.ResourceId, HiddenResourceFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("HiddenResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property ResourceHistoryEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ResourceHistoryCollection", True)
                relation.AddEntityFieldPair(ItemLayoutTemplateResourceFields.ResourceId, ResourceHistoryFields.ResourceId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceHistoryEntity", False)
                Return relation
            End Get
        End Property

        Public Overrides ReadOnly Property ResourceDataEntityUsingResourceId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, "ResourceData", True)

                relation.AddEntityFieldPair(ItemLayoutTemplateResourceFields.ResourceId, ResourceDataFields.ResourceId)



                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceDataEntity", False)
                Return relation
            End Get
        End Property

        Public Overrides ReadOnly Property BankEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Bank", False)
                relation.AddEntityFieldPair(BankFields.Id, ItemLayoutTemplateResourceFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property StateEntityUsingStateId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "State", False)
                relation.AddEntityFieldPair(StateFields.StateId, ItemLayoutTemplateResourceFields.StateId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property UserEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CreatedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, ItemLayoutTemplateResourceFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property UserEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ModifiedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, ItemLayoutTemplateResourceFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemLayoutTemplateResourceEntity", True)
                Return relation
            End Get
        End Property

        Friend ReadOnly Property RelationToSuperTypeResourceEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, False)
                relation.AddEntityFieldPair(ResourceFields.ResourceId, ItemLayoutTemplateResourceFields.ResourceId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property

        Public Overrides Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation
            Return Nothing
        End Function

        Public Overrides Function GetSuperTypeRelation() As IEntityRelation
            Return Me.RelationToSuperTypeResourceEntity
        End Function



    End Class

    Friend Class StaticItemLayoutTemplateResourceRelations
        Friend Shared ReadOnly CustomBankPropertyValueEntityUsingResourceIdStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().CustomBankPropertyValueEntityUsingResourceId
        Friend Shared ReadOnly DependentResourceEntityUsingDependentResourceIdStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().DependentResourceEntityUsingDependentResourceId
        Friend Shared ReadOnly DependentResourceEntityUsingResourceIdStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().DependentResourceEntityUsingResourceId
        Friend Shared ReadOnly HiddenResourceEntityUsingResourceIdStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().HiddenResourceEntityUsingResourceId
        Friend Shared ReadOnly ResourceHistoryEntityUsingResourceIdStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().ResourceHistoryEntityUsingResourceId
        Friend Shared ReadOnly ResourceDataEntityUsingResourceIdStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().ResourceDataEntityUsingResourceId
        Friend Shared ReadOnly BankEntityUsingBankIdStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().BankEntityUsingBankId
        Friend Shared ReadOnly StateEntityUsingStateIdStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().StateEntityUsingStateId
        Friend Shared ReadOnly UserEntityUsingCreatedByStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().UserEntityUsingCreatedBy
        Friend Shared ReadOnly UserEntityUsingModifiedByStatic As IEntityRelation = New ItemLayoutTemplateResourceRelations().UserEntityUsingModifiedBy

        Shared Sub New()
        End Sub
    End Class
End Namespace
