Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class PermissionRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.RolePermissionEntityUsingPermissionId)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property RolePermissionEntityUsingPermissionId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "RolePermissionCollection", True)
                relation.AddEntityFieldPair(PermissionFields.Id, RolePermissionFields.PermissionId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PermissionEntity", True)
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

    Friend Class StaticPermissionRelations
        Friend Shared ReadOnly RolePermissionEntityUsingPermissionIdStatic As IEntityRelation = New PermissionRelations().RolePermissionEntityUsingPermissionId

        Shared Sub New()
        End Sub
    End Class
End Namespace
