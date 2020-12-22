Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class BankRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.BankEntityUsingParentBankId)
            toReturn.Add(Me.CustomBankPropertyEntityUsingBankId)
            toReturn.Add(Me.ResourceEntityUsingBankId)
            toReturn.Add(Me.UserBankRoleEntityUsingBankId)
            toReturn.Add(Me.BankEntityUsingIdParentBankId)
            toReturn.Add(Me.UserEntityUsingCreatedBy)
            toReturn.Add(Me.UserEntityUsingModifiedBy)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property BankEntityUsingParentBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "BankCollection", True)
                relation.AddEntityFieldPair(BankFields.Id, BankFields.ParentBankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property CustomBankPropertyEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(BankFields.Id, CustomBankPropertyFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ResourceEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(BankFields.Id, ResourceFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserBankRoleEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(BankFields.Id, UserBankRoleFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserBankRoleEntity", False)
                Return relation
            End Get
        End Property


        Public Overridable ReadOnly Property BankEntityUsingIdParentBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ParentBank", False)
                relation.AddEntityFieldPair(BankFields.Id, BankFields.ParentBankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CreatedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, BankFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ModifiedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, BankFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", True)
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

    Friend Class StaticBankRelations
        Friend Shared ReadOnly BankEntityUsingParentBankIdStatic As IEntityRelation = New BankRelations().BankEntityUsingParentBankId
        Friend Shared ReadOnly CustomBankPropertyEntityUsingBankIdStatic As IEntityRelation = New BankRelations().CustomBankPropertyEntityUsingBankId
        Friend Shared ReadOnly ResourceEntityUsingBankIdStatic As IEntityRelation = New BankRelations().ResourceEntityUsingBankId
        Friend Shared ReadOnly UserBankRoleEntityUsingBankIdStatic As IEntityRelation = New BankRelations().UserBankRoleEntityUsingBankId
        Friend Shared ReadOnly BankEntityUsingIdParentBankIdStatic As IEntityRelation = New BankRelations().BankEntityUsingIdParentBankId
        Friend Shared ReadOnly UserEntityUsingCreatedByStatic As IEntityRelation = New BankRelations().UserEntityUsingCreatedBy
        Friend Shared ReadOnly UserEntityUsingModifiedByStatic As IEntityRelation = New BankRelations().UserEntityUsingModifiedBy

        Shared Sub New()
        End Sub
    End Class
End Namespace
