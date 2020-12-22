Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class TreeStructureCustomBankPropertyRelations
        Inherits CustomBankPropertyRelations
        Public Sub New()
        End Sub

        Public Overrides Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = MyBase.GetAllRelations()
            toReturn.Add(Me.TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyId)
            toReturn.Add(Me.TreeStructurePartCustomBankPropertyEntityUsingCustomBankPropertyId)
            Return toReturn
        End Function


        Public Overrides ReadOnly Property CustomBankPropertyValueEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyValueCollection", True)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyFields.CustomBankPropertyId, CustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyValueEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TreeStructureCustomBankPropertyValueCollection", True)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyFields.CustomBankPropertyId, TreeStructureCustomBankPropertyValueFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyValueEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property TreeStructurePartCustomBankPropertyEntityUsingCustomBankPropertyId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TreeStructurePartCustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(TreeStructureCustomBankPropertyFields.CustomBankPropertyId, TreeStructurePartCustomBankPropertyFields.CustomBankPropertyId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructurePartCustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property


        Public Overrides ReadOnly Property BankEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Bank", False)
                relation.AddEntityFieldPair(BankFields.Id, TreeStructureCustomBankPropertyFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property StateEntityUsingStateId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "State", False)
                relation.AddEntityFieldPair(StateFields.StateId, TreeStructureCustomBankPropertyFields.StateId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StateEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property UserEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CreatedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, TreeStructureCustomBankPropertyFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property
        Public Overrides ReadOnly Property UserEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ModifiedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, TreeStructureCustomBankPropertyFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TreeStructureCustomBankPropertyEntity", True)
                Return relation
            End Get
        End Property

        Friend ReadOnly Property RelationToSuperTypeCustomBankPropertyEntity As IEntityRelation
            Get
                Dim relation As New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToOne, False)
                relation.AddEntityFieldPair(CustomBankPropertyFields.CustomBankPropertyId, TreeStructureCustomBankPropertyFields.CustomBankPropertyId)
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

    Friend Class StaticTreeStructureCustomBankPropertyRelations
        Friend Shared ReadOnly CustomBankPropertyValueEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyRelations().CustomBankPropertyValueEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyRelations().TreeStructureCustomBankPropertyValueEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly TreeStructurePartCustomBankPropertyEntityUsingCustomBankPropertyIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyRelations().TreeStructurePartCustomBankPropertyEntityUsingCustomBankPropertyId
        Friend Shared ReadOnly BankEntityUsingBankIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyRelations().BankEntityUsingBankId
        Friend Shared ReadOnly StateEntityUsingStateIdStatic As IEntityRelation = New TreeStructureCustomBankPropertyRelations().StateEntityUsingStateId
        Friend Shared ReadOnly UserEntityUsingCreatedByStatic As IEntityRelation = New TreeStructureCustomBankPropertyRelations().UserEntityUsingCreatedBy
        Friend Shared ReadOnly UserEntityUsingModifiedByStatic As IEntityRelation = New TreeStructureCustomBankPropertyRelations().UserEntityUsingModifiedBy

        Shared Sub New()
        End Sub
    End Class
End Namespace
