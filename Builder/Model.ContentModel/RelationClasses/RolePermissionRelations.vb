Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class RolePermissionRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.PermissionEntityUsingPermissionId)
            toReturn.Add(Me.PermissionTargetEntityUsingPermissionTargetId)
            toReturn.Add(Me.RoleEntityUsingRoleId)
            Return toReturn
        End Function




        Public Overridable ReadOnly Property PermissionEntityUsingPermissionId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Permission", False)
                relation.AddEntityFieldPair(PermissionFields.Id, RolePermissionFields.PermissionId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PermissionEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RolePermissionEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property PermissionTargetEntityUsingPermissionTargetId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "PermissionTarget", False)
                relation.AddEntityFieldPair(PermissionTargetFields.Id, RolePermissionFields.PermissionTargetId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PermissionTargetEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RolePermissionEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property RoleEntityUsingRoleId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Role", False)
                relation.AddEntityFieldPair(RoleFields.Id, RolePermissionFields.RoleId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RolePermissionEntity", True)
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

    Friend Class StaticRolePermissionRelations
        Friend Shared ReadOnly PermissionEntityUsingPermissionIdStatic As IEntityRelation = New RolePermissionRelations().PermissionEntityUsingPermissionId
        Friend Shared ReadOnly PermissionTargetEntityUsingPermissionTargetIdStatic As IEntityRelation = New RolePermissionRelations().PermissionTargetEntityUsingPermissionTargetId
        Friend Shared ReadOnly RoleEntityUsingRoleIdStatic As IEntityRelation = New RolePermissionRelations().RoleEntityUsingRoleId

        Shared Sub New()
        End Sub
    End Class
End Namespace
