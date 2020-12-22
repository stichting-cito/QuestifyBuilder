Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class UserApplicationRoleRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.RoleEntityUsingApplicationRoleId)
            toReturn.Add(Me.UserEntityUsingUserId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property RoleEntityUsingApplicationRoleId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Role", False)
                relation.AddEntityFieldPair(RoleFields.Id, UserApplicationRoleFields.ApplicationRoleId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserApplicationRoleEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingUserId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", False)
                relation.AddEntityFieldPair(UserFields.Id, UserApplicationRoleFields.UserId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserApplicationRoleEntity", True)
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

    Friend Class StaticUserApplicationRoleRelations
        Friend Shared ReadOnly RoleEntityUsingApplicationRoleIdStatic As IEntityRelation = New UserApplicationRoleRelations().RoleEntityUsingApplicationRoleId
        Friend Shared ReadOnly UserEntityUsingUserIdStatic As IEntityRelation = New UserApplicationRoleRelations().UserEntityUsingUserId

        Shared Sub New()
        End Sub
    End Class
End Namespace
