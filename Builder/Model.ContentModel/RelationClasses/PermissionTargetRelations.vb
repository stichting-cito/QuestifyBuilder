Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class PermissionTargetRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.RolePermissionEntityUsingPermissionTargetId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property RolePermissionEntityUsingPermissionTargetId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "RolePermissionCollection", True)
                relation.AddEntityFieldPair(PermissionTargetFields.Id, RolePermissionFields.PermissionTargetId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PermissionTargetEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RolePermissionEntity", False)
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

    Friend Class StaticPermissionTargetRelations
        Friend Shared ReadOnly RolePermissionEntityUsingPermissionTargetIdStatic As IEntityRelation = New PermissionTargetRelations().RolePermissionEntityUsingPermissionTargetId

        Shared Sub New()
        End Sub
    End Class
End Namespace
