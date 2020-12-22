Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class RoleRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.RolePermissionEntityUsingRoleId)
            toReturn.Add(Me.UserApplicationRoleEntityUsingApplicationRoleId)
            toReturn.Add(Me.UserBankRoleEntityUsingBankRoleId)
            toReturn.Add(Me.UserEntityUsingCreatedBy)
            toReturn.Add(Me.UserEntityUsingModifiedBy)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property RolePermissionEntityUsingRoleId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "RolePermissionCollection", True)
                relation.AddEntityFieldPair(RoleFields.Id, RolePermissionFields.RoleId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RolePermissionEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserApplicationRoleEntityUsingApplicationRoleId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "UserApplicationRoleCollection", True)
                relation.AddEntityFieldPair(RoleFields.Id, UserApplicationRoleFields.ApplicationRoleId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserApplicationRoleEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserBankRoleEntityUsingBankRoleId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "UserBankRoleCollection", True)
                relation.AddEntityFieldPair(RoleFields.Id, UserBankRoleFields.BankRoleId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserBankRoleEntity", False)
                Return relation
            End Get
        End Property


        Public Overridable ReadOnly Property UserEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CreatedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, RoleFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ModifiedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, RoleFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", True)
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

    Friend Class StaticRoleRelations
        Friend Shared ReadOnly RolePermissionEntityUsingRoleIdStatic As IEntityRelation = New RoleRelations().RolePermissionEntityUsingRoleId
        Friend Shared ReadOnly UserApplicationRoleEntityUsingApplicationRoleIdStatic As IEntityRelation = New RoleRelations().UserApplicationRoleEntityUsingApplicationRoleId
        Friend Shared ReadOnly UserBankRoleEntityUsingBankRoleIdStatic As IEntityRelation = New RoleRelations().UserBankRoleEntityUsingBankRoleId
        Friend Shared ReadOnly UserEntityUsingCreatedByStatic As IEntityRelation = New RoleRelations().UserEntityUsingCreatedBy
        Friend Shared ReadOnly UserEntityUsingModifiedByStatic As IEntityRelation = New RoleRelations().UserEntityUsingModifiedBy

        Shared Sub New()
        End Sub
    End Class
End Namespace
