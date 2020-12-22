Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class ListCustomBankPropertyRelations
        Inherits CustomBankPropertyRelations
        Public Sub New()
        End Sub

        Public Overrides Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = MyBase.GetAllRelations()
            toReturn.Add(Me.ListCustomBankPropertyValueEntityUsingCustomBankPropertyId)
            toReturn.Add(Me.ListValueCustomBankPropertyEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function


        Public Overrides ReadOnly Property CustomBankPropertyValueEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyValueCollection", True)
                relation.AddEntityFieldPair(ListCustomBankPropertyFields.CustomBankPropertyId, CustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyValueEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ListCustomBankPropertyValueEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ListCustomBankPropertyValueCollection", True)
                relation.AddEntityFieldPair(ListCustomBankPropertyFields.CustomBankPropertyId, ListCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyValueEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ListValueCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ListValueCustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(ListCustomBankPropertyFields.CustomBankPropertyId, ListValueCustomBankPropertyFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListValueCustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property


        Public Overrides ReadOnly Property BankEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Bank", False)
                relation.AddEntityFieldPair(BankFields.Id, ListCustomBankPropertyFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property StateEntityUsingStateId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "State", False)
                relation.AddEntityFieldPair(StateFields.StateId, ListCustomBankPropertyFields.StateId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property UserEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CreatedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, ListCustomBankPropertyFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property UserEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ModifiedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, ListCustomBankPropertyFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property

        Friend ReadOnly Property RelationToSuperTypeCustomBankPropertyEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, False)
                relation.AddEntityFieldPair(CustomBankPropertyFields.CustomBankPropertyId, ListCustomBankPropertyFields.CustomBankPropertyId)
                relation.IsHierarchyRelation = True
                Return relation
            End Get
        End Property

        Public Overrides Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation
            Return Nothing
        End Function

        Public Overrides Function GetSuperTypeRelation() As IEntityRelation
            Return Me.RelationToSuperTypeCustomBankPropertyEntity
        End Function



    End Class

    Friend Class StaticListCustomBankPropertyRelations
        Friend Shared ReadOnly CustomBankPropertyValueEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ListCustomBankPropertyRelations().CustomBankPropertyValueEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ListCustomBankPropertyValueEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ListCustomBankPropertyRelations().ListCustomBankPropertyValueEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly ListValueCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New ListCustomBankPropertyRelations().ListValueCustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly BankEntityUsingBankIdStatic As IEntityRelation = New ListCustomBankPropertyRelations().BankEntityUsingBankId
        Friend Shared ReadOnly StateEntityUsingStateIdStatic As IEntityRelation = New ListCustomBankPropertyRelations().StateEntityUsingStateId
        Friend Shared ReadOnly UserEntityUsingCreatedByStatic As IEntityRelation = New ListCustomBankPropertyRelations().UserEntityUsingCreatedBy
        Friend Shared ReadOnly UserEntityUsingModifiedByStatic As IEntityRelation = New ListCustomBankPropertyRelations().UserEntityUsingModifiedBy

        Shared Sub New()
        End Sub
    End Class
End Namespace
