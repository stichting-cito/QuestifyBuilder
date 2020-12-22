Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class RichTextValueCustomBankPropertyRelations
        Inherits CustomBankPropertyRelations
        Public Sub New()
        End Sub

        Public Overrides Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = MyBase.GetAllRelations()
            toReturn.Add(Me.RichTextValueCustomBankPropertyValueEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function


        Public Overrides ReadOnly Property CustomBankPropertyValueEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyValueCollection", True)
                relation.AddEntityFieldPair(RichTextValueCustomBankPropertyFields.CustomBankPropertyId, CustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RichTextValueCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyValueEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property RichTextValueCustomBankPropertyValueEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "RichTextValueCustomBankPropertyValueCollection", True)
                relation.AddEntityFieldPair(RichTextValueCustomBankPropertyFields.CustomBankPropertyId, RichTextValueCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RichTextValueCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RichTextValueCustomBankPropertyValueEntity", False)
                Return relation
            End Get
        End Property


        Public Overrides ReadOnly Property BankEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Bank", False)
                relation.AddEntityFieldPair(BankFields.Id, RichTextValueCustomBankPropertyFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RichTextValueCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property StateEntityUsingStateId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "State", False)
                relation.AddEntityFieldPair(StateFields.StateId, RichTextValueCustomBankPropertyFields.StateId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RichTextValueCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property UserEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CreatedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, RichTextValueCustomBankPropertyFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RichTextValueCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property UserEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ModifiedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, RichTextValueCustomBankPropertyFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RichTextValueCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property

        Friend ReadOnly Property RelationToSuperTypeCustomBankPropertyEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, False)
                relation.AddEntityFieldPair(CustomBankPropertyFields.CustomBankPropertyId, RichTextValueCustomBankPropertyFields.CustomBankPropertyId)
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

    Friend Class StaticRichTextValueCustomBankPropertyRelations
        Friend Shared ReadOnly CustomBankPropertyValueEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New RichTextValueCustomBankPropertyRelations().CustomBankPropertyValueEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly RichTextValueCustomBankPropertyValueEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New RichTextValueCustomBankPropertyRelations().RichTextValueCustomBankPropertyValueEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly BankEntityUsingBankIdStatic As IEntityRelation = New RichTextValueCustomBankPropertyRelations().BankEntityUsingBankId
        Friend Shared ReadOnly StateEntityUsingStateIdStatic As IEntityRelation = New RichTextValueCustomBankPropertyRelations().StateEntityUsingStateId
        Friend Shared ReadOnly UserEntityUsingCreatedByStatic As IEntityRelation = New RichTextValueCustomBankPropertyRelations().UserEntityUsingCreatedBy
        Friend Shared ReadOnly UserEntityUsingModifiedByStatic As IEntityRelation = New RichTextValueCustomBankPropertyRelations().UserEntityUsingModifiedBy

        Shared Sub New()
        End Sub
    End Class
End Namespace
