Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class UserBankRoleRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.BankEntityUsingBankId)
            toReturn.Add(Me.RoleEntityUsingBankRoleId)
            toReturn.Add(Me.UserEntityUsingUserId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property BankEntityUsingBankId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Bank", False)
                relation.AddEntityFieldPair(BankFields.Id, UserBankRoleFields.BankId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserBankRoleEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property RoleEntityUsingBankRoleId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Role", False)
                relation.AddEntityFieldPair(RoleFields.Id, UserBankRoleFields.BankRoleId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserBankRoleEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingUserId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", False)
                relation.AddEntityFieldPair(UserFields.Id, UserBankRoleFields.UserId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserBankRoleEntity", True)
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

    Friend Class StaticUserBankRoleRelations
        Friend Shared ReadOnly BankEntityUsingBankIdStatic As IEntityRelation = New UserBankRoleRelations().BankEntityUsingBankId
        Friend Shared ReadOnly RoleEntityUsingBankRoleIdStatic As IEntityRelation = New UserBankRoleRelations().RoleEntityUsingBankRoleId
        Friend Shared ReadOnly UserEntityUsingUserIdStatic As IEntityRelation = New UserBankRoleRelations().UserEntityUsingUserId

        Shared Sub New()
        End Sub
    End Class
End Namespace
